using RDPDFLib.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PDFViewerSDK_Win10
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            String inst_path = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            String cmap_path = inst_path + "\\Assets\\cmaps.dat";
            String umap_path = inst_path + "\\Assets\\umaps.dat";
            PDFGlobal.SetCMapsPath(cmap_path, umap_path);
            PDFGlobal.FontFileListStart();
            PDFGlobal.FontFileListAdd(inst_path + "\\Assets\\font\\argbsn00lp.ttf");
            PDFGlobal.FontFileListEnd();

            int face_first = 0;
            int face_count = PDFGlobal.GetFaceCount();
            String fname = null;
            while (face_first < face_count)
            {
                fname = PDFGlobal.GetFaceName(face_first);
                if (fname != null && fname.Length > 0) break;
                face_first++;
            }

            // set default font for fixed width font.
            if (!PDFGlobal.SetDefaultFont("", "AR PL SungtiL GB", true) && fname != null)
                PDFGlobal.SetDefaultFont("", fname, true);
            // set default font for non-fixed width font.
            if (!PDFGlobal.SetDefaultFont("", "AR PL SungtiL GB", false) && fname != null)
                PDFGlobal.SetDefaultFont("", fname, false);

            if (!PDFGlobal.SetAnnotFont("AR PL SungtiL GB") && fname != null)
                PDFGlobal.SetAnnotFont(fname);

            PDFGlobal.LoadStdFont(0, inst_path + "\\Assets\\font\\0");
            PDFGlobal.LoadStdFont(1, inst_path + "\\Assets\\font\\1");
            PDFGlobal.LoadStdFont(2, inst_path + "\\Assets\\font\\2");
            PDFGlobal.LoadStdFont(3, inst_path + "\\Assets\\font\\3");
            PDFGlobal.LoadStdFont(4, inst_path + "\\Assets\\font\\4");
            PDFGlobal.LoadStdFont(5, inst_path + "\\Assets\\font\\5");
            PDFGlobal.LoadStdFont(6, inst_path + "\\Assets\\font\\6");
            PDFGlobal.LoadStdFont(7, inst_path + "\\Assets\\font\\7");
            PDFGlobal.LoadStdFont(8, inst_path + "\\Assets\\font\\8");
            PDFGlobal.LoadStdFont(9, inst_path + "\\Assets\\font\\9");
            PDFGlobal.LoadStdFont(10, inst_path + "\\Assets\\font\\10");
            PDFGlobal.LoadStdFont(11, inst_path + "\\Assets\\font\\11");
            PDFGlobal.LoadStdFont(12, inst_path + "\\Assets\\font\\12");
            PDFGlobal.LoadStdFont(13, inst_path + "\\Assets\\font\\13");

            bool r = PDFGlobal.ActiveLicense(2, "Radaee", "radaeepdf@gmail.com", "YOOW28-VS57CA-H3CRUZ-WAJQ9H-5R5V9L-KM0Y1L");
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
