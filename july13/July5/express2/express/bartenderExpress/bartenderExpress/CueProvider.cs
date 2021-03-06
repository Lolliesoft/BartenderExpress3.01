﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bartenderExpress
{
    /// <summary>
    /// Provides textual cues to a text box.
    /// </summary>
    /// <summary>
    
    public static class CueProvider
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage
          (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// Sets a text box's cue text.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="cue">The cue text.</param>
        public static void SetCue (TextBox textBox, string cue)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, cue);
        }

        /// <summary>
        /// Clears a text box's cue text.
        /// </summary>
        /// <param name="textBox">The text box</param>
        public static void ClearCue
          (TextBox textBox1)
        {
            SendMessage (textBox1.Handle, EM_SETCUEBANNER, 0, string.Empty);
        }
    }
}
