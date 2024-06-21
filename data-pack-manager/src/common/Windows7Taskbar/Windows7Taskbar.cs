// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace medea.common
{
    /// <summary>
    /// The primary coordinator of the Windows 7 taskbar-related activities.
    /// </summary>
    public enum ProgressBarState
    {
        Normal = 1,
        Error = 2,
        Pause = 3
    }
    public static class Windows7Taskbar
    {
        private static ITaskbarList3 _taskbarList;
        internal static ITaskbarList3 TaskbarList
        {
            get
            {
                if (_taskbarList == null)
                {
                    lock (typeof(Windows7Taskbar))
                    {
                        if (_taskbarList == null)
                        {
                            _taskbarList = (ITaskbarList3)new CTaskbarList();
                            _taskbarList.HrInit();
                        }
                    }
                }
                return _taskbarList;
            }
        }

        static readonly OperatingSystem osInfo = Environment.OSVersion;

        internal static bool Windows7OrGreater
        {
            get
            {
                return (osInfo.Version.Major == 6 && osInfo.Version.Minor >= 1)
                    || (osInfo.Version.Major > 6);
            }
        }
        public static IntPtr GetTaskBarForm(Form f)
        {
            if (f.ShowInTaskbar)
                return f.Handle;
            Form owner = f.Owner;
            if (owner != null)
                return GetTaskBarForm(owner);
            else
                return IntPtr.Zero;
        }
        public static void SetStatus(Form f, ProgressBarState state)
        {
            IntPtr i = GetTaskBarForm(f);
            if (i != IntPtr.Zero)
                SetStatusHwnd(i, state);
        }
        public static void SetStatusHwnd(IntPtr hwnd, ProgressBarState state)
        {
            if (Windows7OrGreater)
            {
                // set the progress bar state (Normal, Error, Paused)
                Windows7Taskbar.SendMessage(hwnd, 0x410, (int)state, 0);
                ThumbnailProgressState thmState;
                if (state == ProgressBarState.Error)
                    thmState = ThumbnailProgressState.Error;
                else if (state == ProgressBarState.Pause)
                    thmState = ThumbnailProgressState.Paused;
                else
                    thmState = ThumbnailProgressState.NoProgress;
                Windows7Taskbar.SetProgressState(hwnd, thmState);
            }
        }

        /// <summary>
        /// Sets the progress state of the specified window's
        /// taskbar button.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="state">The progress state.</param>
        public static void SetProgressState(IntPtr hwnd, ThumbnailProgressState state)
        {
            if(Windows7OrGreater)
                TaskbarList.SetProgressState(hwnd, state);
        }
        /// <summary>
        /// Sets the progress value of the specified window's
        /// taskbar button.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="current">The current value.</param>
        /// <param name="maximum">The maximum value.</param>
        public static void SetProgressValue(IntPtr hwnd, ulong current, ulong maximum)
        {
            if(Windows7OrGreater)
                TaskbarList.SetProgressValue(hwnd, current, maximum);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    }
}