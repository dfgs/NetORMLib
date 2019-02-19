using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media;

namespace NetORMLibVSExtension
{
	public class WPFControlWrapper : System.Windows.Forms.IWin32Window
	{
		private IntPtr _hwnd;
		public IntPtr Handle
		{
			get { return _hwnd; }
		}

		public WPFControlWrapper(Visual Visual)
		{
			HwndSource source = (HwndSource)HwndSource.FromVisual(Visual);
			_hwnd = source.Handle;
		}

		

		

	}
}
