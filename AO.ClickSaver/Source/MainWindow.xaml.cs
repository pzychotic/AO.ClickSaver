using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using System.Windows;
using EasyHook;

namespace pzy.AO.ClickSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string _channelName = null;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                RemoteHooking.IpcCreateServer<HookInterface>( ref _channelName, WellKnownObjectMode.SingleCall );

                string hookLib = Path.Combine( Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location ), "AO.Hook.dll" );

                Process[] processes = Process.GetProcessesByName( "AnarchyOnline" );

                foreach( Process process in processes )
                {
                    RemoteHooking.Inject( process.Id, InjectionOptions.DoNotRequireStrongName, hookLib, hookLib, _channelName );
                }
            }
            catch( Exception e )
            {
                MessageBox.Show( "Hooking into AO failed!\n" + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        private void _btnStart_Click( object sender, RoutedEventArgs e )
        {
            // try to find the AO window, can't use FindWindow() here because
            // the window title changes with the logged-in character name
            IntPtr hWnd = new IntPtr();
            WinApiWrapper.EnumWindows( new WinApiWrapper.EnumWindowsFunc( EnumWindowsCallback ), ref hWnd );

            if( hWnd != IntPtr.Zero )
            {
                // get the top-left corner of the window in screen coordinates
                System.Drawing.Point point = new System.Drawing.Point( 0, 0 );
                WinApiWrapper.ClientToScreen( hWnd, ref point );
            }
        }

        /// <summary>
        /// Callback that gets called for every top-level window.
        /// </summary>
        /// <param name="hWnd">The window handle of the window.</param>
        /// <param name="hWndOut">The window handle we will return.</param>
        /// <returns>True to keep on enumerating, otherwise false.</returns>
        private static bool EnumWindowsCallback( IntPtr hWnd, ref IntPtr hWndOut )
        {
            int titleLength = WinApiWrapper.GetWindowTextLength( hWnd );

            StringBuilder sb = new StringBuilder( titleLength );
            WinApiWrapper.GetWindowText( hWnd, sb, sb.Capacity );

            if( sb.ToString().StartsWith( "Anarchy Online" ) )
            {
                hWndOut = hWnd;
                return false;  // stop enumerating
            }

            return true;  // keep enumerating
        }
    }
}
