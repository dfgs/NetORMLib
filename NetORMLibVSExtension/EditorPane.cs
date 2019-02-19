﻿using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NetORMLibVSExtension.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace NetORMLibVSExtension
{
	/// <summary>
	/// This control host the editor (an extended RichTextBox) and is responsible for
	/// handling the commands targeted to the editor as well as saving and loading
	/// the document.
	/// </summary>
	/// <remarks>
	/// Uses an entry in the new file dialog.
	/// EditorWithToolbox.vsdir contents description:
	/// tbx.tbx|{68a4ede6-8f63-44f2-803e-65f770e709e1}|#106|80|#109|{68a4ede6-8f63-44f2-803e-65f770e709e1}|401|0|#107
	///
	/// The fields in order are as follows:-
	///	- tbx.tbx - our empty tbx file
	///	- {68a4ede6-8f63-44f2-803e-65f770e709e1} - our Editor package guid
	///	- #106 - the ID of "Editor with Toolbox" in the resource
	///	- 80 - the display ordering priority
	///	- #109 - the ID of "Editor with Toolbox File" in the resource
	///	- {68a4ede6-8f63-44f2-803e-65f770e709e1} - resource dll string (we don't use this)
	///	- 401 - the ID of our icon
	///	- 0 - various flags (we don't use this - see vsshell.idl)
	///	- #107 - the default file name "TbxFile.tbx"
	/// </remarks>
	public sealed class EditorPane : WindowPane,  IVsPersistDocData, IPersistFileFormat							 
	{
		private const uint fileFormat = 0;
		private const string fileExtension = ".tbx";
		private const char endLine = (char)10;

		#region Fields

		//private static OleDataObject toolboxData = null;

		// Full path to the file.
		private string fileName;
		/// Determines whether an object has changed since being saved to its current file.
		private bool isDirty;
		// Flag true when we are loading the file. It is used to avoid to change the isDirty flag
		// when the changes are related to the load operation.
		private bool loading;
		// This flag is true when we are asking the QueryEditQuerySave service if we can edit the
		// file. It is used to avoid to have more than one request queued.
		private bool gettingCheckoutStatus;
		// Indicate that object is in NoScribble mode or in Normal mode. 
		// Object enter into the NoScribble mode when IPersistFileFormat.Save() call is occurred.
		// This flag using to indicate SaveCompleted state (entering into the Normal mode).
		private bool noScribbleMode;
		// Object that handles the editor window.
		private DatabaseViewHost databaseViewHost;
		private DatabaseView databaseView;

		#endregion

		
		#region Contructors
		public EditorPane(): base(null)
		{
			PrivateInit();
		}
		/// <summary>
		/// Initialize GUI context objects.
		/// </summary>
		private void PrivateInit()
		{
			noScribbleMode = false;
			loading = false;
			gettingCheckoutStatus = false;

			// This call is required by the Windows.Forms Form Designer.
			databaseView = new DatabaseView();
			this.databaseViewHost = new DatabaseViewHost();
			this.databaseViewHost.Child = databaseView;

			this.databaseViewHost.Name = "EditorPane";
		}

		/// <summary>
		/// This method is called when the pane is sited with a non null service provider.
		/// Here is where you can do all the initialization that requare access to
		/// services provided by the shell.
		/// </summary>
		protected override void Initialize()
		{
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets extended rich text box that are hosted.
		/// This is a required override from the Microsoft.VisualStudio.Shell.WindowPane class.
		/// </summary>
		/// <remarks>The resultant handle can be used with Win32 API calls.</remarks>
		public override System.Windows.Forms.IWin32Window Window
		{
			get
			{
				return databaseViewHost;
			}
		}
		#endregion Properties

		#region Methods

		#region IDisposable Pattern implementation

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (databaseViewHost != null)
					{
						databaseViewHost.Dispose();
						databaseViewHost = null;
						databaseView = null;
					}
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		#endregion


		#region IPersist
		/// <summary>
		/// Retrieves the class identifier (CLSID) of an object.
		/// </summary>
		/// <param name="pClassID">[out] Pointer to the location of the CLSID on return.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int Microsoft.VisualStudio.OLE.Interop.IPersist.GetClassID(out Guid pClassID)
		{
			pClassID = new Guid(NetORMLibCommandPackage.GuidEditorFactory);
			return VSConstants.S_OK;
		}
		#endregion IPersist

		#region IPersistFileFormat Members

		/// <summary>
		/// Returns the class identifier of the editor type.
		/// </summary>
		/// <param name="pClassID">pointer to the class identifier.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IPersistFileFormat.GetClassID(out Guid pClassID)
		{
			((Microsoft.VisualStudio.OLE.Interop.IPersist)this).GetClassID(out pClassID);
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Returns the path to the object's current working file.
		/// </summary>
		/// <param name="ppszFilename">Pointer to the file name.</param>
		/// <param name="pnFormatIndex">Value that indicates the current format of the file as a zero based index
		/// into the list of formats. Since we support only a single format, we need to return zero. 
		/// Subsequently, we will return a single element in the format list through a call to GetFormatList.</param>
		/// <returns>S_OK if the function succeeds.</returns>
		int IPersistFileFormat.GetCurFile(out string ppszFilename, out uint pnFormatIndex)
		{
			// We only support 1 format so return its index
			pnFormatIndex = fileFormat;
			ppszFilename = fileName;
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Provides the caller with the information necessary to open the standard common "Save As" dialog box. 
		/// This returns an enumeration of supported formats, from which the caller selects the appropriate format. 
		/// Each string for the format is terminated with a newline (\n) character. 
		/// The last string in the buffer must be terminated with the newline character as well. 
		/// The first string in each pair is a display string that describes the filter, such as "Text Only 
		/// (*.txt)". The second string specifies the filter pattern, such as "*.txt". To specify multiple filter 
		/// patterns for a single display string, use a semicolon to separate the patterns: "*.htm;*.html;*.asp". 
		/// A pattern string can be a combination of valid file name characters and the asterisk (*) wildcard character. 
		/// Do not include spaces in the pattern string. The following string is an example of a file pattern string: 
		/// "HTML File (*.htm; *.html; *.asp)\n*.htm;*.html;*.asp\nText File (*.txt)\n*.txt\n."
		/// </summary>
		/// <param name="ppszFormatList">Pointer to a string that contains pairs of format filter strings.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IPersistFileFormat.GetFormatList(out string ppszFormatList)
		{
			string formatList = string.Format(CultureInfo.CurrentCulture, "Test Editor (*{0}){1}*{0}{1}{1}", fileExtension, endLine);
			ppszFormatList = formatList;
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Notifies the object that it has concluded the Save transaction.
		/// </summary>
		/// <param name="pszFilename">Pointer to the file name.</param>
		/// <returns>S_OK if the function succeeds.</returns>
		int IPersistFileFormat.SaveCompleted(string pszFilename)
		{
			if (noScribbleMode)
			{
				return VSConstants.S_FALSE;
			}
			// If NoScribble mode is inactive - Save() operation was completed.
			else
			{
				return VSConstants.S_OK;
			}
		}

		/// <summary>
		/// Initialization for the object.
		/// </summary>
		/// <param name="nFormatIndex">Zero based index into the list of formats that indicates the current format
		/// of the file.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IPersistFileFormat.InitNew(uint nFormatIndex)
		{
			if (nFormatIndex != fileFormat)
			{
				throw new ArgumentException("File format exception");
			}
			// until someone change the file, we can consider it not dirty as
			// the user would be annoyed if we prompt him to save an empty file
			isDirty = false;
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Determines whether an object has changed since being saved to its current file.
		/// </summary>
		/// <param name="pfIsDirty">true if the document has changed.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IPersistFileFormat.IsDirty(out int pfIsDirty)
		{
			if (isDirty)
			{
				pfIsDirty = 1;
			}
			else
			{
				pfIsDirty = 0;
			}
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Loads the file content into the TextBox.
		/// </summary>
		/// <param name="pszFilename">Pointer to the full path name of the file to load.</param>
		/// <param name="grfMode">file format mode.</param>
		/// <param name="fReadOnly">determines if the file should be opened as read only.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IPersistFileFormat.Load(string pszFilename, uint grfMode, int fReadOnly)
		{
			if ((pszFilename == null) && ((fileName == null) || (fileName.Length == 0))) throw new ArgumentNullException("pszFilename");

			loading = true;
			int hr = VSConstants.S_OK;
			try
			{
				bool isReload = false;

				// If the new file name is null, then this operation is a reload
				if (pszFilename == null)
				{
					isReload = true;
				}

				// Show the wait cursor while loading the file
				IVsUIShell vsUiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
				if (vsUiShell != null)
				{
					// Note: we don't want to throw or exit if this call fails, so
					// don't check the return code.
					vsUiShell.SetWaitCursor();
				}

				// Set the new file name
				if (!isReload)
				{
					// Unsubscribe from the notification of the changes in the previous file.
					fileName = pszFilename;
				}
				// Load the file
				Database database = Database.LoadFromFile(fileName);
				databaseView.DataContext = database;

				isDirty = false;

				// Notify the load or reload
				NotifyDocChanged();
			}
			finally
			{
				loading = false;
			}
			return hr;
		}

		/// <summary>
		/// Save the contents of the TextBox into the specified file. If doing the save on the same file, we need to
		/// suspend notifications for file changes during the save operation.
		/// </summary>
		/// <param name="pszFilename">Pointer to the file name. If the pszFilename parameter is a null reference 
		/// we need to save using the current file.
		/// </param>
		/// <param name="remember">Boolean value that indicates whether the pszFileName parameter is to be used 
		/// as the current working file.
		/// If remember != 0, pszFileName needs to be made the current file and the dirty flag needs to be cleared after the save.
		///				   Also, file notifications need to be enabled for the new file and disabled for the old file 
		/// If remember == 0, this save operation is a Save a Copy As operation. In this case, 
		///				   the current file is unchanged and dirty flag is not cleared.
		/// </param>
		/// <param name="nFormatIndex">Zero based index into the list of formats that indicates the format in which 
		/// the file will be saved.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IPersistFileFormat.Save(string pszFilename, int fRemember, uint nFormatIndex)
		{
			// switch into the NoScribble mode
			noScribbleMode = true;
			try
			{
				// If file is null or same --> SAVE
				/*if (pszFilename == null || pszFilename == fileName)
				{
					editorControl.SaveFile(fileName, RichTextBoxStreamType.PlainText);
					isDirty = false;
				}
				else
				{// If remember --> SaveAs 
					if (fRemember != 0)
					{
						fileName = pszFilename;
						editorControl.SaveFile(fileName, RichTextBoxStreamType.PlainText);
						isDirty = false;
					}
					else // Else, Save a Copy As
					{
						editorControl.SaveFile(pszFilename, RichTextBoxStreamType.PlainText);
					}
				}*/
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				// switch into the Normal mode
				noScribbleMode = false;
			}
			return VSConstants.S_OK;
		}

		#endregion

		#region IVsPersistDocData Members

		/// <summary>
		/// Close the IVsPersistDocData object.
		/// </summary>
		/// <returns>S_OK if the function succeeds.</returns>
		int IVsPersistDocData.Close()
		{
			if (databaseViewHost != null)
			{
				databaseViewHost.Dispose();
				databaseViewHost = null;
				databaseView = null;
			}
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Returns the Guid of the editor factory that created the IVsPersistDocData object.
		/// </summary>
		/// <param name="pClassID">Pointer to the class identifier of the editor type.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.GetGuidEditorType(out Guid pClassID)
		{
			return ((IPersistFileFormat)this).GetClassID(out pClassID);
		}

		/// <summary>
		/// Used to determine if the document data has changed since the last time it was saved.
		/// </summary>
		/// <param name="pfDirty">Will be set to 1 if the data has changed.</param>
		/// <returns>S_OK if the function succeeds.</returns>
		int IVsPersistDocData.IsDocDataDirty(out int pfDirty)
		{
			return ((IPersistFileFormat)this).IsDirty(out pfDirty);
		}

		/// <summary>
		/// Determines if it is possible to reload the document data.
		/// </summary>
		/// <param name="pfReloadable">set to 1 if the document can be reloaded.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.IsDocDataReloadable(out int pfReloadable)
		{
			// Allow file to be reloaded
			pfReloadable = 1;
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Loads the document data from the file specified.
		/// </summary>
		/// <param name="pszMkDocument">Path to the document file which needs to be loaded.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.LoadDocData(string pszMkDocument)
		{
			return ((IPersistFileFormat)this).Load(pszMkDocument, 0, 0);
		}

		/// <summary>
		/// Called by the Running Document Table when it registers the document data. 
		/// </summary>
		/// <param name="docCookie">Handle for the document to be registered.</param>
		/// <param name="pHierNew">Pointer to the IVsHierarchy interface.</param>
		/// <param name="itemidNew">Item identifier of the document to be registered from VSITEM.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.OnRegisterDocData(uint docCookie, IVsHierarchy pHierNew, uint itemidNew)
		{
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Reloads the document data.
		/// </summary>
		/// <param name="grfFlags">Flag indicating whether to ignore the next file change when reloading the document data.
		/// This flag should not be set for us since we implement the "IVsDocDataFileChangeControl" interface in order to 
		/// indicate ignoring of file changes.
		/// </param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.ReloadDocData(uint grfFlags)
		{
			return ((IPersistFileFormat)this).Load(null, grfFlags, 0);
		}

		/// <summary>
		/// Renames the document data.
		/// </summary>
		/// <param name="grfAttribs">File attribute of the document data to be renamed. See the data type __VSRDTATTRIB.</param>
		/// <param name="pHierNew">Pointer to the IVsHierarchy interface of the document being renamed.</param>
		/// <param name="itemidNew">Item identifier of the document being renamed. See the data type VSITEMID.</param>
		/// <param name="pszMkDocumentNew">Path to the document being renamed.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.RenameDocData(uint grfAttribs, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
		{
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Saves the document data. Before actually saving the file, we first need to indicate to the environment
		/// that a file is about to be saved. This is done through the "SVsQueryEditQuerySave" service. We call the
		/// "QuerySaveFile" function on the service instance and then proceed depending on the result returned as follows:
		/// If result is QSR_SaveOK - We go ahead and save the file and the file is not read only at this point.
		/// If result is QSR_ForceSaveAs - We invoke the "Save As" functionality which will bring up the Save file name 
		///								dialog 
		/// If result is QSR_NoSave_Cancel - We cancel the save operation and indicate that the document could not be saved
		///								by setting the "pfSaveCanceled" flag
		/// If result is QSR_NoSave_Continue - Nothing to do here as the file need not be saved.
		/// </summary>
		/// <param name="dwSave">Flags which specify the file save options:
		/// VSSAVE_Save		- Saves the current file to itself.
		/// VSSAVE_SaveAs	  - Prompts the User for a filename and saves the file to the file specified.
		/// VSSAVE_SaveCopyAs  - Prompts the user for a filename and saves a copy of the file with a name specified.
		/// VSSAVE_SilentSave  - Saves the file without prompting for a name or confirmation.  
		/// </param>
		/// <param name="pbstrMkDocumentNew">Pointer to the path to the new document.</param>
		/// <param name="pfSaveCanceled">value 1 if the document could not be saved.</param>
		/// <returns>S_OK if the method succeeds.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		int IVsPersistDocData.SaveDocData(Microsoft.VisualStudio.Shell.Interop.VSSAVEFLAGS dwSave, out string pbstrMkDocumentNew, out int pfSaveCanceled)
		{
			pbstrMkDocumentNew = null;
			pfSaveCanceled = 0;
			int hr = VSConstants.S_OK;

			switch (dwSave)
			{
				case VSSAVEFLAGS.VSSAVE_Save:
				case VSSAVEFLAGS.VSSAVE_SilentSave:
					{
						IVsQueryEditQuerySave2 queryEditQuerySave = (IVsQueryEditQuerySave2)GetService(typeof(SVsQueryEditQuerySave));

						// Call QueryEditQuerySave
						uint result = 0;
						hr = queryEditQuerySave.QuerySaveFile(
											fileName,	   // filename
											0,			  // flags
											null,		   // file attributes
											out result);	// result

						if (ErrorHandler.Failed(hr))
						{
							return hr;
						}

						// Process according to result from QuerySave
						switch ((tagVSQuerySaveResult)result)
						{
							case tagVSQuerySaveResult.QSR_NoSave_Cancel:
								// Note that this is also case tagVSQuerySaveResult.QSR_NoSave_UserCanceled because these
								// two tags have the same value.
								pfSaveCanceled = ~0;
								break;

							case tagVSQuerySaveResult.QSR_SaveOK:
								{
									// Call the shell to do the save for us
									IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
									hr = uiShell.SaveDocDataToFile(dwSave, (IPersistFileFormat)this, fileName, out pbstrMkDocumentNew, out pfSaveCanceled);
									if (ErrorHandler.Failed(hr))
									{
										return hr;
									}
								}
								break;

							case tagVSQuerySaveResult.QSR_ForceSaveAs:
								{
									// Call the shell to do the SaveAS for us
									IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
									hr = uiShell.SaveDocDataToFile(VSSAVEFLAGS.VSSAVE_SaveAs, (IPersistFileFormat)this, fileName, out pbstrMkDocumentNew, out pfSaveCanceled);
									if (ErrorHandler.Failed(hr))
									{
										return hr;
									}
								}
								break;

							case tagVSQuerySaveResult.QSR_NoSave_Continue:
								// In this case there is nothing to do.
								break;

							default:
								throw new COMException("Default exception");
						}
						break;
					}
				case VSSAVEFLAGS.VSSAVE_SaveAs:
				case VSSAVEFLAGS.VSSAVE_SaveCopyAs:
					{
						// Make sure the file name as the right extension
						if (String.Compare(fileExtension, System.IO.Path.GetExtension(fileName), true, CultureInfo.CurrentCulture) != 0)
						{
							fileName += fileExtension;
						}
						// Call the shell to do the save for us
						IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
						hr = uiShell.SaveDocDataToFile(dwSave, (IPersistFileFormat)this, fileName, out pbstrMkDocumentNew, out pfSaveCanceled);
						if (ErrorHandler.Failed(hr))
							return hr;
						break;
					}
				default:
					throw new ArgumentException("Save as exception");
			};

			return VSConstants.S_OK;
		}

		/// <summary>
		/// Used to set the initial name for unsaved, newly created document data.
		/// </summary>
		/// <param name="pszDocDataPath">String containing the path to the document.
		/// We need to ignore this parameter.
		/// </param>
		/// <returns>S_OK if the method succeeds.</returns>
		int IVsPersistDocData.SetUntitledDocPath(string pszDocDataPath)
		{
			return ((IPersistFileFormat)this).InitNew(fileFormat);
		}

		#endregion

  
	

		#region Other methods
		/// <summary>
		/// This function asks to the QueryEditQuerySave service if it is possible to
		/// edit the file.
		/// </summary>
		/// <returns>True if the editing of the file are enabled, otherwise returns false.</returns>
		private bool CanEditFile()
		{
			// Check the status of the recursion guard
			if (gettingCheckoutStatus)
			{
				return false;
			}

			try
			{
				// Set the recursion guard
				gettingCheckoutStatus = true;

				// Get the QueryEditQuerySave service
				IVsQueryEditQuerySave2 queryEditQuerySave = (IVsQueryEditQuerySave2)GetService(typeof(SVsQueryEditQuerySave));

				// Now call the QueryEdit method to find the edit status of this file
				string[] documents = { this.fileName };
				uint result;
				uint outFlags;

				// Note that this function can pop up a dialog to ask the user to checkout the file.
				// When this dialog is visible, it is possible to receive other request to change
				// the file and this is the reason for the recursion guard.
				int hr = queryEditQuerySave.QueryEditFiles(
					0,			  // Flags
					1,			  // Number of elements in the array
					documents,	  // Files to edit
					null,		   // Input flags
					null,		   // Input array of VSQEQS_FILE_ATTRIBUTE_DATA
					out result,	 // result of the checkout
					out outFlags	// Additional flags
				);
				if (ErrorHandler.Succeeded(hr) && (result == (uint)tagVSQueryEditResult.QER_EditOK))
				{
					// In this case (and only in this case) we can return true from this function.
					return true;
				}
			}
			finally
			{
				gettingCheckoutStatus = false;
			}
			return false;
		}

		/// <summary>
		/// Gets an instance of the RunningDocumentTable (RDT) service which manages the set of currently open 
		/// documents in the environment and then notifies the client that an open document has changed.
		/// </summary>
		private void NotifyDocChanged()
		{
			// Make sure that we have a file name
			if (fileName.Length == 0)
			{
				return;
			}

			// Get a reference to the Running Document Table
			IVsRunningDocumentTable runningDocTable = (IVsRunningDocumentTable)GetService(typeof(SVsRunningDocumentTable));

			// Lock the document
			uint docCookie;
			IVsHierarchy hierarchy;
			uint itemID;
			IntPtr docData;
			int hr = runningDocTable.FindAndLockDocument(
				(uint)_VSRDTFLAGS.RDT_ReadLock,
				fileName,
				out hierarchy,
				out itemID,
				out docData,
				out docCookie
			);
			ErrorHandler.ThrowOnFailure(hr);

			// Send the notification
			hr = runningDocTable.NotifyDocumentChanged(docCookie, (uint)__VSRDTATTRIB.RDTA_DocDataReloaded);

			// Unlock the document.
			// Note that we have to unlock the document even if the previous call failed.
			runningDocTable.UnlockDocument((uint)_VSRDTFLAGS.RDT_ReadLock, docCookie);

			// Check ff the call to NotifyDocChanged failed.
			ErrorHandler.ThrowOnFailure(hr);
		}
		#endregion Other methods

		#endregion Methods
	}
}
