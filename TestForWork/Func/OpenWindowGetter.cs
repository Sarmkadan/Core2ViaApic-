using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestForWork.Classes;

namespace TestForWork.Func
{
    using HWND = IntPtr;
    public class OpenWindowGetter
    {
        private static readonly long _maxDistanseHide = -30000;
    /// <summary>Contains functionality to get all the open windows.</summary>
        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public static List<Window> GetListOfWindows()
        {
            
            List<Window> windowslist = new List<Window>();
            var bla = OpenWindowGetter.GetOpenWindows();
            /*  for (int i = 0; i < bla.Values.Count+1; i++)
              {
                  Console.WriteLine(bla[System.IntPtr.Zeroi]);
              }*/
            OpenWindowGetter.Rect RectofWindow = new OpenWindowGetter.Rect();
            List<OpenWindowGetter.Rect> listofrect = new List<OpenWindowGetter.Rect>();
            foreach (var variable in bla)
            {
                OpenWindowGetter.GetWindowRect(variable.Key, ref RectofWindow);
                listofrect.Add(RectofWindow);
                // Console.WriteLine("NAME: =  " + variable.Value + "   bottom " + NotepadRect.Bottom + "  " + NotepadRect.Left + "  " + NotepadRect.Right + "  " + NotepadRect.Top);
                var w = RectofWindow.Right - RectofWindow.Left;
                var h = RectofWindow.Bottom - RectofWindow.Top;
                if (h > _maxDistanseHide && w > _maxDistanseHide) // проверка на скрытость от глаз)
                {
                    windowslist.Add(new Window()
                    {
                        Height = h,
                        Width = w,
                        Name = variable.Value,
                        X = RectofWindow.Left,
                        Y = RectofWindow.Top
                    });
                }
                // Console.WriteLine("Ширина формы: " + w + "\n\rВысота формы: " + h);
            }
            return windowslist;
        }
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        private static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
    
}
}
