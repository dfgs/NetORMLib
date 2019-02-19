using Microsoft.VisualStudio;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using System;

namespace NetORMLibVSExtension
{
   
    [Guid(NetORMLibCommandPackage.GuidEditorFactory)]
    public class EditorFactory : IVsEditorFactory
    {
        private Package parentPackage;
        private IOleServiceProvider serviceProvider;

        public EditorFactory(Package ParentPackage)
        {
            this.parentPackage = ParentPackage;
        }

     

        int IVsEditorFactory.SetSite(IOleServiceProvider psp)
        {
            this.serviceProvider = psp;
            return VSConstants.S_OK;
        }

        int IVsEditorFactory.Close()
        {
            return VSConstants.S_OK;
        }

        int IVsEditorFactory.MapLogicalView(ref Guid rguidLogicalView, out string pbstrPhysicalView)
        {
            pbstrPhysicalView = null;   // We support only one view.  

            if (rguidLogicalView.Equals(VSConstants.LOGVIEWID_Designer) || rguidLogicalView.Equals(VSConstants.LOGVIEWID_Primary)) return VSConstants.S_OK;
            
            return VSConstants.E_NOTIMPL;
        }

        int IVsEditorFactory.CreateEditorInstance(uint grfCreateDoc, string pszMkDocument, string pszPhysicalView, IVsHierarchy pvHier, uint itemid, IntPtr punkDocDataExisting, out IntPtr ppunkDocView, out IntPtr ppunkDocData, out string pbstrEditorCaption, out Guid pguidCmdUI, out int pgrfCDW)
        {
            // Initialize to null
            ppunkDocView = IntPtr.Zero;
            ppunkDocData = IntPtr.Zero;
            pguidCmdUI = new Guid( NetORMLibCommandPackage.GuidEditorFactory);
            pgrfCDW = 0;
            pbstrEditorCaption = null;

            // Validate inputs
            if ((grfCreateDoc & (VSConstants.CEF_OPENFILE | VSConstants.CEF_SILENT)) == 0) return VSConstants.E_INVALIDARG;
            if (punkDocDataExisting != IntPtr.Zero) return VSConstants.VS_E_INCOMPATIBLEDOCDATA;

            // Create the Document (editor)
            EditorPane newEditor = new EditorPane();
            ppunkDocView = Marshal.GetIUnknownForObject(newEditor);
            ppunkDocData = Marshal.GetIUnknownForObject(newEditor);
            pbstrEditorCaption = "";

            return VSConstants.S_OK;
        }




    }
}
