using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MonoDragons.Core.UserInterface
{
    public static class Cursors
    {
        public static Cursor Default = System.Windows.Forms.Cursors.Default;
        public static Cursor Text = System.Windows.Forms.Cursors.IBeam;
        public static Cursor Interactive = System.Windows.Forms.Cursors.Hand;

        public static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCursor = LoadCursorFromFile(path);
            if (hCursor == IntPtr.Zero) throw new Win32Exception();
            var cursor = new Cursor(hCursor);
            // Note: force the cursor to own the handle so it gets released properly
            var cursorHandleField = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            cursorHandleField.SetValue(cursor, true);
            return cursor;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);
    }
}
