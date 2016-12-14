using PDFViewerSDK_Win10.SettingsControls;
using PDFViewerSDK_Win10.view;
using PDFViewerSDK_Win10.OptionPanelControls;
using RDPDFLib.pdf;
using System;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PDFViewerSDK_Win10
{
    public delegate void OnPageCloseHandler(PDFView.PDFPos pos);

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PDFReaderPage : Page, PDFView.PDFViewListener
    {
        IRandomAccessStream m_stream;
        private PDFDoc m_doc;
        private PDFView m_view;
        private AnnotControl mAnnotControl;
        private SearchControl mSearchPanel;
        private EditAnnotControl mEditAnnotControl;
        private AnnotControl.AnnotType mAnnotType = AnnotControl.AnnotType.TypeNone;
        private PasswordControl mPasswordControl = null;
        private SaveDocControl mSaveDocControl = null;
        private string mSearchKey = "";
        private bool mIsModified;

        private MenuDataSet mMenuData;

        static public event OnPageCloseHandler OnPageClose;

        public PDFReaderPage()
        {
            this.InitializeComponent();
            Windows.UI.Core.CoreWindow rcWindow = Windows.UI.Xaml.Window.Current.CoreWindow;
            Rect rcScreen = rcWindow.Bounds;
            mPDFView.Width = rcScreen.Width;
            mPDFView.Height = rcScreen.Height;
        }

        private void OnViewModeDialogClose(int mode)
        {
            PDFView.PDFPos pos = m_view.vGetPos(0, 0);
            switch (mode)
            {
                case 0:
                    //Vert
                    m_view.vClose();
                    m_view = new PDFViewVert(mPDFView);
                    m_view.vOpen(m_doc, 4, 0xFFCCCCCC, this);
                    m_view.vSetPos(pos, 0, 0);
                    break;
                case 1:
                    //Horz
                    m_view.vClose();
                    m_view = new PDFViewHorz(mPDFView);
                    m_view.vOpen(m_doc, 4, 0xFFCCCCCC, this);
                    m_view.vSetPos(pos, 0, 0);
                    break;
                case 2:
                    //Dual Page
                    m_view.vClose();
                    m_view = new PDFViewDual(mPDFView);
                    m_view.vOpen(m_doc, 4, 0xFFCCCCCC, this);
                    m_view.vSetPos(pos, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            m_stream = (IRandomAccessStream)e.Parameter;
            m_doc = new PDFDoc();
            PDF_ERROR err = m_doc.Open(m_stream, "");
            switch (err)
            {
                case PDF_ERROR.err_ok:
                    //PDFView.init();
                    if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("View_mode"))
                        PDFView.viewMode = Convert.ToInt32(Windows.Storage.ApplicationData.Current.LocalSettings.Values["View_mode"]);
                    switch (PDFView.viewMode)
                    {
                        case 0:
                            m_view = new PDFViewVert(mPDFView);
                            break;
                        case 1:
                            m_view = new PDFViewHorz(mPDFView);
                            break;
                        case 2:
                            m_view = new PDFViewDual(mPDFView);
                            break;
                        default:
                            m_view = new PDFViewVert(mPDFView);
                            break;
                    }
                    m_view.vOpen(m_doc, 4, 0xFFCCCCCC, this);

                    initOptionPanels();
                    mAnnotType = AnnotControl.AnnotType.TypeNone;
                    mIsModified = false;
                    mMenuData = new MenuDataSet();
                    mMenuData.Init(m_doc);
                    viewMenuBtn.IsEnabled = (mMenuData.Length > 0);
                    mPageDisplay.Text = "/" + m_doc.PageCount;
                    GotoHistoryPage();
                    break;
                case PDF_ERROR.err_password:
                    mPasswordControl = new PasswordControl();
                    mPasswordControl.OnDialogClose += OnPasswordDialogClose;
                    mPasswordControl.Show();
                    break;
                default:
                    break;
            }
            //m_view.vSelStart();
            //m_view.vNoteStart();
            //m_view.vInkStart();
            //m_view.vRectStart();
            //m_view.vLineStart();
        }

        private void OnPasswordDialogClose(string password)
        {
            if (password.Length == 0)
            {
                //Cancel
                mPasswordControl.Dismiss();
            }
            PDF_ERROR err = m_doc.Open(m_stream, password);
            switch (err)
            {
                case PDF_ERROR.err_ok:
                    //PDFView.init();
                    if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("View_mode"))
                        PDFView.viewMode = Convert.ToInt32(Windows.Storage.ApplicationData.Current.LocalSettings.Values["View_mode"]);
                    switch (PDFView.viewMode)
                    {
                        case 0:
                            m_view = new PDFViewVert(mPDFView);
                            break;
                        case 1:
                            m_view = new PDFViewHorz(mPDFView);
                            break;
                        case 2:
                            m_view = new PDFViewDual(mPDFView);
                            break;
                        default:
                            m_view = new PDFViewVert(mPDFView);
                            break;
                    }
                    m_view.vOpen(m_doc, 4, 0xFFCCCCCC, this);
                    initOptionPanels();
                    mAnnotType = AnnotControl.AnnotType.TypeNone;
                    mIsModified = false;
                    mPasswordControl.Dismiss();
                    mMenuData = new MenuDataSet();
                    mMenuData.Init(m_doc);
                    viewMenuBtn.IsEnabled = (mMenuData.Length > 0);
                    mPageDisplay.Text = "/" + m_doc.PageCount;
                    GotoHistoryPage();
                    break;
                case PDF_ERROR.err_password:
                    mPasswordControl.showHint();
                    break;
                default:
                    mPasswordControl.Dismiss();
                    break;
            }
        }

        private void GotoHistoryPage()
        {
            int page;
            int x;
            int y;
            if (MainPage.FileToken.Equals(string.Empty))
            {
                mPageInput.Text = "1";
                return;
            }

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(MainPage.FileToken + "_page"))
            {
                page = Convert.ToInt32(Windows.Storage.ApplicationData.Current.LocalSettings.Values[MainPage.FileToken + "_page"]);
            }
            else
            {
                mPageInput.Text = "1";
                return;
            }

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(MainPage.FileToken + "_x"))
            {
                x = Convert.ToInt32(Windows.Storage.ApplicationData.Current.LocalSettings.Values[MainPage.FileToken + "_x"]);
            }
            else
            {
                mPageInput.Text = "1";
                return;
            }

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(MainPage.FileToken + "_y"))
            {
                y = Convert.ToInt32(Windows.Storage.ApplicationData.Current.LocalSettings.Values[MainPage.FileToken + "_y"]);
            }
            else
            {
                mPageInput.Text = "1";
                return;
            }

            PDFView.PDFPos pos = new PDFView.PDFPos();
            pos.pageno = page;
            pos.x = x;
            pos.y = y;
            m_view.vSetPos(pos, 0, 0);
            mPageInput.Text = (pos.pageno + 1).ToString();
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
            if (mAnnotType != AnnotControl.AnnotType.TypeNone)
            {
                switch (mAnnotType)
                {
                    case AnnotControl.AnnotType.TypeEllipse:
                        m_view.vEllipseEnd();
                        break;
                    case AnnotControl.AnnotType.TypeLine:
                        m_view.vInkEnd();
                        break;
                    case AnnotControl.AnnotType.TypeRect:
                        m_view.vRectEnd();
                        break;
                    case AnnotControl.AnnotType.TypeText:
                        m_view.vNoteEnd();
                        break;
                }
                doneAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                removeAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                mAnnotControl.ResetLayout();
                mAnnotType = AnnotControl.AnnotType.TypeNone;
            }
            mPDFView.Width = e.NewSize.Width;
            mPDFView.Height = e.NewSize.Height;
            if (e.PreviousSize.Height > 0 && e.PreviousSize.Width > 0)
            {
                PDFView.PDFPos pos = m_view.vGetPos(0, 0);
                m_view.vSetPos(pos, 0, 0);
            }
        }

        private void initOptionPanels()
        {
            mAnnotControl = new AnnotControl(m_doc.CanSave);
            mAnnotControl.OnButtonClick += OnAnnotItemTapped;
            mSearchPanel = new SearchControl();
            mSearchPanel.OnButtonTapped += OnSearchItemTapped;
            mEditAnnotControl = new EditAnnotControl();
            mEditAnnotControl.OnButtonClick += OnEditAnnotButtonTapped;
        }

        private void OnEditAnnotButtonTapped(int btnCode)
        {

            switch (btnCode)
            {
                case 0:
                    //open annot
                    m_view.vAnnotPerform();
                    break;
                case 1:
                    //remove annot
                    m_view.vAnnotRemove();
                    mIsModified = true;
                    break;
                case 2:
                    //cancel
                    m_view.vAnnotEnd();
                    break;
            }

            PDFOptionPanel.Children.Clear();
            PDFOptionPanel.Children.Add(mAnnotControl);
            mAppBar.IsOpen = false;
        }

        private void OnSearchItemTapped(int btnCode, string searchKey, bool matchCase, bool matchWholeWord)
        {
            if (searchKey.Length == 0)
            {
                m_view.vFindEnd();
                return;
            }
            if (!searchKey.Equals(mSearchKey))
            {
                mSearchKey = searchKey;
                m_view.vFindStart(mSearchKey, matchCase, matchWholeWord);
            }
            switch (btnCode)
            {
                case 0:
                    //search prev
                    m_view.vFind(-1);
                    break;
                case 1:
                    //search next
                    m_view.vFind(1);
                    break;
                case -1:
                    //cancel
                    m_view.vFindEnd();
                    break;
            }
        }

        private void OnAnnotItemTapped(AnnotControl.AnnotType type)
        {
            switch (type)
            {
                case AnnotControl.AnnotType.TypeLine:
                    m_view.vInkStart();
                    break;
                case AnnotControl.AnnotType.TypeRect:
                    m_view.vRectStart();
                    break;
                case AnnotControl.AnnotType.TypeEllipse:
                    m_view.vEllipseStart();
                    break;
                case AnnotControl.AnnotType.TypeText:
                    m_view.vNoteStart();
                    break;
                default:
                    break;
            }
            mAppBar.IsOpen = false;
            doneAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            removeAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            mAnnotType = type;
        }

        private void OnOptionItemTapped(object sender, TappedRoutedEventArgs e)
        {
            mAppBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Button button = sender as Button;
            String name = button.Name;
            if (name.Equals("viewAnnotBtn"))
            {
                PDFOptionPanel.Children.Clear();
                PDFOptionPanel.Children.Add(mAnnotControl);
                mAppBar.IsOpen = true;

            }
            else if (name.Equals("searchBtn"))
            {
                PDFOptionPanel.Children.Clear();
                PDFOptionPanel.Children.Add(mSearchPanel);
                mAppBar.IsOpen = true;

            }
            else if (name.Equals("doneAnnotBtn"))
            {
                mIsModified = true;
                mAnnotControl.ResetLayout();
                switch (mAnnotType)
                {
                    case AnnotControl.AnnotType.TypeLine:
                        m_view.vInkEnd();
                        break;
                    case AnnotControl.AnnotType.TypeRect:
                        m_view.vRectEnd();
                        break;
                    case AnnotControl.AnnotType.TypeEllipse:
                        m_view.vEllipseEnd();
                        break;
                    case AnnotControl.AnnotType.TypeText:
                        m_view.vNoteEnd();
                        break;
                    default:
                        break;
                }
                doneAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                removeAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else if (name.Equals("removeAnnotBtn"))
            {
                mAnnotControl.ResetLayout();
                switch (mAnnotType)
                {
                    case AnnotControl.AnnotType.TypeLine:
                        m_view.vInkCancel();
                        break;
                    case AnnotControl.AnnotType.TypeRect:
                        m_view.vRectCancel();
                        break;
                    case AnnotControl.AnnotType.TypeEllipse:
                        m_view.vEllipseCancel();
                        break;
                    case AnnotControl.AnnotType.TypeText:
                        m_view.vNoteCancel();
                        break;
                    default:
                        break;
                }
                doneAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                removeAnnotBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            else if (name.Equals(viewMenuBtn.Name))
            {
                if (mMenuView.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                {
                    mMenuView.ItemsSource = mMenuData.Items;
                    mMenuView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                    mMenuView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            else if (name.Equals(viewInfoBtn.Name))
            {
                InformationControl infoDialog = new InformationControl();
                infoDialog.show();
            }

            else if (name.Equals(settingsBtn.Name))
            {
                SettingsControl settingsControl = new SettingsControl();
                settingsControl.OnViewModeSelected += OnViewModeDialogClose;
                settingsControl.show();
            }
        }


        private void OnBtnGoBack(Object sender, RoutedEventArgs e)
        {
            //mIsMenuTapped = true;
            OnClose(null, null);
        }

        private void OnClose(Object sender, CoreWindowEventArgs e)
        {
            if (mIsModified && m_doc.CanSave)
            {
                if (mSaveDocControl == null)
                {
                    mSaveDocControl = new SaveDocControl();
                    mSaveDocControl.onSaveDialogClose += OnSaveDialogClose;
                    mSaveDocControl.show();
                }
            }
            else
                vClose();
        }

        private void OnSaveDialogClose(int button)
        {
            switch (button)
            {
                case 0:
                    //OK
                    m_doc.Save();
                    vClose();
                    break;
                case 1:
                    //No
                    vClose();
                    break;
                default:
                    //cancel
                    break;
            }
            mSaveDocControl.dismiss();
        }

        private void vClose()
        {
            if (m_stream != null)
            {
                if (m_view != null)
                {
                    PDFView.PDFPos pos = m_view.vGetPos(0, 0);
                    if (OnPageClose != null)
                        OnPageClose(pos);
                    m_view.vClose();
                    m_view = null;
                }
                m_doc.Close();
                m_doc = null;
                m_stream.Dispose();
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnMenuListItemClicked(object sender, ItemClickEventArgs e)
        {
            MenuItem clickedItem = e.ClickedItem as MenuItem;
            if (clickedItem != null)
            {
                int page = clickedItem.Page;
                m_view.vGotoPage(page);
            }
        }



        public void OnPDFPageChanged(int pageno)
        {
            mPageInput.Text = (pageno + 1).ToString();
        }

        public bool OnPDFSingleTapped(float x, float y)
        {
            return false;
        }

        public void OnPDFLongPressed(float x, float y)
        {
        }

        public void OnPDFFound(bool found)
        {
        }

        public void OnPDFPageDisplayed(Canvas canvas, PDFVPage vpage)
        {
        }

        public void OnPDFSelecting(Canvas canvas, PDFRect rect1, PDFRect rect2)
        {
        }

        public void OnPDFSelected()
        {
        }

        public void OnPDFAnnotClicked(PDFPage page, PDFAnnot annot)
        {
            if (annot != null)
            {
                PDFOptionPanel.Children.Clear();
                PDFOptionPanel.Children.Add(mEditAnnotControl);
                mAppBar.IsOpen = true;
            }
        }

        public void OnPDFAnnotEnd()
        {
        }

        public void OnPDFAnnotGoto(int pageno)
        {
            m_view.vGotoPage(pageno);
        }

        public async void OnPDFAnnotURI(string uri)
        {
            if (!uri.StartsWith("www."))
                uri = "http://www." + uri;
            if (!uri.StartsWith("http://"))
                uri = "http://" + uri;
            Uri url = new Uri(uri);
            await Windows.System.Launcher.LaunchUriAsync(url);
        }

        public void OnPDFAnnotMovie(PDFAnnot annot, string name)
        {
        }

        public void OnPDFAnnotSound(PDFAnnot annot, string name)
        {
        }

        private PDFAnnot mAnnot;
        private TextAnnotControl mTextAnnotDialog = null;
        public void OnPDFAnnotPopup(PDFAnnot annot, string subj, string text)
        {
            mAnnot = annot;
            mTextAnnotDialog = new TextAnnotControl();
            if (annot != null)
                mTextAnnotDialog.SetContent(annot.PopupSubject, annot.PopupText);
            mTextAnnotDialog.OnCloseDialog += OnCloseDialog;
            mTextAnnotDialog.show();
        }

        private void OnCloseDialog(String subject, String content, bool cancel, bool edit)
        {
            if (cancel)
            {
                m_view.vNoteRemoveLast();
                return;
            }
            if (mAnnot == null)
            {
                int index = -1;
                PDFView.PDFPos pos = m_view.vGetPos(0, 0);
                PDFVPage vpage = m_view.vGetPage(pos.pageno);
                if (vpage != null)
                {
                    PDFPage page = vpage.GetPage();
                    if (page != null)
                    {
                        index = page.AnnotCount;
                        if (index > 0)
                            mAnnot = page.GetAnnot(index - 1);
                        if (mAnnot == null)
                            return;
                    }
                }
            }
            mAnnot.PopupSubject = subject;
            mAnnot.PopupText = content;
            mTextAnnotDialog.dismiss();
            mTextAnnotDialog = null;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                try
                {
                    int page = Convert.ToInt32(mPageInput.Text);
                    m_view.vGotoPage(--page);
                }
                catch
                {
                }
            }
        }

        public async void OnPDFAnnotRemoteDest(string dest)
        {
            string filename = dest;
            string pagenumber = "";
            char[] separator = { '/' };
            string[] elements = dest.Split(separator);

            long elementscount = elements.Length;


            if (elements[elementscount - 1] != null) // page number is not null
            {
                pagenumber = "/" + elements[elementscount - 1];
            }
            else
            {
                pagenumber = "/0";
            }

            if (filename.EndsWith(pagenumber))
            {
                filename = filename.Substring(0, filename.LastIndexOf(pagenumber));
            }

            if (pagenumber == "/0") // page number isn't set
            {
                // handle file without goto page
                // simple example below, it works only with absolute file paths

                // convert / (Linux) to \ (Windows)
                filename = filename.Replace("/", "\\");

                Uri url = new System.Uri(filename, UriKind.RelativeOrAbsolute);
                await Windows.System.Launcher.LaunchUriAsync(url);
            }
            else
            {
                // todo handle spawn new reader and goto page number
                // filename: new pdf file
                // pagenumber: page number
            }

        }
        public async void OnPDFAnnotFileLink(string filelink)
        {
            // handle file without goto page
            // simple example below
            Uri url = new Uri(filelink);
            await Windows.System.Launcher.LaunchUriAsync(url);
        }

        private void OnThumbItemTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PDFThumbPage), m_doc);
        }

        public void OnPDFPageTapped(PDFVPage vpage)
        {
        }
    }
}