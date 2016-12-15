using PDFViewerSDK_Win10.view;
using RDPDFLib.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PDFReader
{
    public sealed partial class PDFReaderView : Canvas
    {
        private PDFDoc m_doc;
        private PDFView m_view;
        public PDFReaderView()
        {
            this.InitializeComponent();
            m_view = new PDFViewVert(this);
        }

        public void OpenStream(IRandomAccessStream stream, PDFView.PDFViewListener listener)
        {
            m_doc = new PDFDoc();
            PDF_ERROR err = m_doc.Open(stream, "");
            switch (err)
            {
                case PDF_ERROR.err_ok:
                    //PDFView.init();
                    m_view.vOpen(m_doc, 4, 0xFFCCCCCC, listener);
                    break;
                case PDF_ERROR.err_password:
                default:
                    break;
            }
            //m_view.vSelStart();
            //m_view.vNoteStart();
            //m_view.vInkStart();
            //m_view.vRectStart();
            //m_view.vLineStart();

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //if (!MainPage.FileToken.Equals(string.Empty) &&
            //            Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(MainPage.FileToken)
            //            )
            //{
            //    int page = Convert.ToInt32(Windows.Storage.ApplicationData.Current.LocalSettings.Values[MainPage.FileToken]);
            //    m_view.vGotoPage(page);
            //}
            //m_view.vZoom(2);
            //m_view.vGotoPage(10);
            //m_view.vFindStart( "线程", false, false );
            //m_view.vFind(1);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*
            PDFCanvas.Width = e.NewSize.Width;
            PDFCanvas.Height = e.NewSize.Height;
            */
            if (e.PreviousSize.Height > 0 && e.PreviousSize.Width > 0)
            {
                PDFView.PDFPos pos = m_view.vGetPos(0, 0);
                m_view.vSetPos(pos, 0, 0);
            }
        }
    }
}
