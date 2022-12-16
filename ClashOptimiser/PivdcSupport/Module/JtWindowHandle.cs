using System;

namespace PivdcSupportModule
{
    public class JtWindowHandle : System.Windows.Forms.IWin32Window
    {
        IntPtr IntPtrHandeler { get; set; }

        public JtWindowHandle(IntPtr h)
        {
            IntPtrHandeler = h;
        }

        public IntPtr Handle
        {
            get
            {
                return IntPtrHandeler;
            }
        }
    }
}