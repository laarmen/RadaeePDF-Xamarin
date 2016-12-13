using System;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using UIKit;

namespace Com.Radaee.Pdf
{
// @interface RadaeePDFPlugin : NSObject
    [BaseType (typeof(NSObject))]
    interface RadaeePDFPlugin
    {
        // @property (nonatomic) int viewMode;
        [Export("viewMode")]
        int ViewMode { get; set; }

        // @property (nonatomic, strong) UIImage * searchImage;
        [Export("searchImage", ArgumentSemantic.Strong)]
        UIImage SearchImage { get; set; }

        // @property (nonatomic, strong) UIImage * bookmarkImage;
        [Export("bookmarkImage", ArgumentSemantic.Strong)]
        UIImage BookmarkImage { get; set; }

        // @property (nonatomic, strong) UIImage * outlineImage;
        [Export("outlineImage", ArgumentSemantic.Strong)]
        UIImage OutlineImage { get; set; }

        // @property (nonatomic, strong) UIImage * lineImage;
        [Export("lineImage", ArgumentSemantic.Strong)]
        UIImage LineImage { get; set; }

        // @property (nonatomic, strong) UIImage * rectImage;
        [Export("rectImage", ArgumentSemantic.Strong)]
        UIImage RectImage { get; set; }

        // @property (nonatomic, strong) UIImage * ellipseImage;
        [Export("ellipseImage", ArgumentSemantic.Strong)]
        UIImage EllipseImage { get; set; }

        // @property (nonatomic, strong) UIImage * deleteImage;
        [Export("deleteImage", ArgumentSemantic.Strong)]
        UIImage DeleteImage { get; set; }

        // @property (nonatomic, strong) UIImage * doneImage;
        [Export("doneImage", ArgumentSemantic.Strong)]
        UIImage DoneImage { get; set; }

        // @property (nonatomic, strong) UIImage * removeImage;
        [Export("removeImage", ArgumentSemantic.Strong)]
        UIImage RemoveImage { get; set; }

        // @property (nonatomic, strong) UIImage * prevImage;
        [Export("prevImage", ArgumentSemantic.Strong)]
        UIImage PrevImage { get; set; }

        // @property (nonatomic, strong) UIImage * nextImage;
        [Export("nextImage", ArgumentSemantic.Strong)]
        UIImage NextImage { get; set; }

        // @property (nonatomic) BOOL hideViewModeImage;
        [Export("hideViewModeImage")]
        bool hideViewModeImage{ get; set; }

        // @property (nonatomic) BOOL hideSearchImage;
        [Export("hideSearchImage")]
        bool hideSearchImage { get; set; }

        // @property (nonatomic) BOOL hideBookmarkImage;
        [Export("hideBookmarkImage")]
        bool hideBookmarkImage { get; set; }

        // @property (nonatomic) BOOL hideBookmarkListImage;
        [Export("hideBookmarkListImage")]
        bool hideBookmarkListImage { get; set; }

        // @property (nonatomic) BOOL hideOutlineImage;
        [Export("hideOutlineImage")]
        bool hideOutlineImage { get; set; }

        // @property (nonatomic) BOOL hideLineImage;
        [Export("hideLineImage")]
        bool hideLineImage { get; set; }

        // @property (nonatomic) BOOL hideRectImage;
        [Export("hideRectImage")]
        bool hideRectImage { get; set; }

        // @property (nonatomic) BOOL hideEllipseImage;
        [Export("hideEllipseImage")]
        bool hideEllipseImage { get; set; }

        // @property (nonatomic) BOOL hidePrintImage;
        [Export("hidePrintImage")]
        bool hidePrintImage { get; set; }

        // @property (nonatomic, strong) NSArray * cdv_command;
        [Export("cdv_command", ArgumentSemantic.Strong)]
        NSArray Cdv_command { get; set; }

        // @property (nonatomic, weak) UIViewController * viewController;
        [Export("viewController", ArgumentSemantic.Weak)]
        UIViewController ViewController { get; set; }

        // -(void)pluginInitialize;
        [Export("pluginInitialize")]
        void PluginInitialize();

        // -(RDPDFViewController *)show:(NSArray *)command;
        [Export("show:")]
        RDPDFViewController Show(NSArray command);

        // -(RDPDFViewController *)activateLicense:(NSArray *)command;
        [Export("activateLicense:")]
        void ActivateLicense(NSArray command);

        // -(RDPDFViewController *)openFromAssets:(NSArray *)command;
        [Export("openFromAssets:")]
        RDPDFViewController OpenFromAssets(NSArray command);

        // +(RadaeePDFPlugin *)pluginInit;
        [Static]
        [Export("pluginInit")]
        RadaeePDFPlugin PluginInit { get; }

        // +(NSMutableArray *)loadBookmark;
        [Static]
        [Export("loadBookmark")]
        NSMutableArray LoadBookmark { get; }

        // +(NSMutableArray *)loadBookmarkForPdf:(NSString *)pdfName;
        [Static]
        [Export("loadBookmarkForPdf:")]
        NSMutableArray LoadBookmarkForPdf(string pdfName);

        // -(void)setPagingEnabled:(BOOL)enabled;
        [Export("setPagingEnabled:")]
        void SetPagingEnabled(bool enabled);

        // -(void)setDoublePageEnabled:(BOOL)enabled;
        [Export("setDoublePageEnabled:")]
        void SetDoublePageEnabled(bool enabled);

        // -(BOOL)setReaderViewMode:(int)mode;
        [Export("setReaderViewMode:")]
        bool SetReaderViewMode(int mode);

        // -(void)toggleThumbSeekBar:(int)mode;
        [Export("toggleThumbSeekBar:")]
        void ToggleThumbSeekBar(int mode);

        // -(void)setColor:(int)color forFeature:(int)feature;
        [Export("setColor:forFeature:")]
        void SetColor(int color, int feature);
    }

    // @interface RDPDFViewController : UIViewController <UISearchBarDelegate, saveTextAnnotDelegate>
    [BaseType(typeof(UIViewController))]
    interface RDPDFViewController
    {

    }

    // @protocol PDFStream
    [BaseType (typeof(NSObject))]
    [Model]
    interface PDFStream
    {
        // @required -(_Bool)writeable;
        [Abstract]
        [Export ("writeable")]
        bool Writeable { get; }

        // @required -(int)read:(void *)buf :(int)len;
        //[Abstract]
        //[Export ("read::")]
        //unsafe int Read (void *buf, int len);

        // @required -(int)write:(const void *)buf :(int)len;
        //[Abstract]
        //[Export ("write::")]
        //unsafe int Write (void *buf, int len);

        // @required -(unsigned long long)position;
        [Abstract]
        [Export ("position")]
        ulong Position { get; }

        // @required -(unsigned long long)length;
        [Abstract]
        [Export ("length")]
        ulong Length { get; }

        // @required -(_Bool)seek:(unsigned long long)pos;
        [Abstract]
        [Export ("seek:")]
        bool Seek (ulong pos);
    }
    // @interface PDFDoc : NSObject
    [BaseType (typeof(NSObject))]
    interface PDFDoc
    {
        // @property (readonly) PDF_DOC handle;
        //[Export ("handle")]
        //unsafe PDF_DOC* Handle { get; }

        // -(int)open:(NSString *)path :(NSString *)password;
        [Export ("open::")]
        int Open (string path, [NullAllowed] string password);

        // -(int)openMem:(void *)data :(int)data_size :(NSString *)password;
        //[Export ("openMem:::")]
        //unsafe int OpenMem (void* data, int data_size, string password);

        // -(int)openStream:(id<PDFStream>)stream :(NSString *)password;
        [Export ("openStream::")]
        int OpenStream (PDFStream stream, [NullAllowed] string password);

        // -(int)create:(NSString *)path;
        [Export ("create:")]
        int Create (string path);

        // -(_Bool)setCache:(NSString *)path;
        [Export ("setCache:")]
        bool SetCache (string path);

        // -(_Bool)setPageRotate:(int)pageno :(int)degree;
        [Export ("setPageRotate::")]
        bool SetPageRotate (int pageno, int degree);

        // -(_Bool)canSave;
        [Export ("canSave")]
        bool CanSave { get; }

        // -(_Bool)isEncrypted;
        [Export ("isEncrypted")]
        bool IsEncrypted { get; }

        // -(int)getEmbedFileCount;
        [Export ("getEmbedFileCount")]
        int EmbedFileCount { get; }

        // -(NSString *)getEmbedFileName:(int)idx;
        [Export ("getEmbedFileName:")]
        string GetEmbedFileName (int idx);

        // -(_Bool)getEmbedFileData:(int)idx :(NSString *)path;
        [Export ("getEmbedFileData::")]
        bool GetEmbedFileData (int idx, string path);

        // -(NSString *)exportForm;
        [Export ("exportForm")]
        string ExportForm { get; }

        // -(_Bool)save;
        [Export("save")]
        bool Save();

        // -(_Bool)saveAs:(NSString *)dst :(_Bool)rem_sec;
        [Export ("saveAs::")]
        bool SaveAs (string dst, bool rem_sec);

        // -(_Bool)encryptAs:(NSString *)dst :(NSString *)upswd :(NSString *)opswd :(int)perm :(int)method :(unsigned char *)fid;
        //[Export ("encryptAs::::::")]
        //unsafe bool EncryptAs (string dst, string upswd, string opswd, int perm, int method, byte* fid);

        // -(NSString *)meta:(NSString *)tag;
        [Export ("meta:")]
        string Meta (string tag);

        // -(_Bool)setMeta:(NSString *)tag :(NSString *)val;
        [Export ("setMeta::")]
        bool SetMeta (string tag, string val);

        // -(_Bool)PDFID:(unsigned char *)buf;
        //[Export ("PDFID:")]
        //unsafe bool PDFID (byte* buf);

        // -(int)pageCount;
        [Export ("pageCount")]
        int PageCount { get; }

        // -(float)pageWidth:(int)pageno;
        [Export ("pageWidth:")]
        float PageWidth (int pageno);

        // -(float)pageHeight:(int)pageno;
        [Export ("pageHeight:")]
        float PageHeight (int pageno);
    }

    // @interface PDFVLocker : NSObject
    [BaseType (typeof(NSObject))]
    interface PDFVLocker
    {
        // -(void)lock;
        [Export ("lock")]
        void Lock ();

        // -(void)unlock;
        [Export ("unlock")]
        void Unlock ();
    }

    // @interface PDFVEvent : NSObject
    [BaseType (typeof(NSObject))]
    interface PDFVEvent
    {
        // -(void)reset;
        [Export ("reset")]
        void Reset ();

        // -(void)notify;
        [Export ("notify")]
        void Notify ();

        // -(void)wait;
        [Export ("wait")]
        void Wait ();
    }

    [Static]
    partial interface Constants
    {
        //// extern int g_def_view;
        //[Field ("g_def_view", "__Internal")]
        //int g_def_view { get; }

        //// extern float g_zoom_level;
        //[Field ("g_zoom_level", "__Internal")]
        //float g_zoom_level { get; }

        //// extern int g_MatchWholeWord;
        //[Field ("g_MatchWholeWord", "__Internal")]
        //int g_MatchWholeWord { get; }

        //// extern int g_CaseSensitive;
        //[Field ("g_CaseSensitive", "__Internal")]
        //int g_CaseSensitive { get; }

        //// extern NSMutableString * pdfName;
        //[Field ("pdfName", "__Internal")]
        //NSMutableString pdfName { get; }

        //// extern NSMutableString * pdfPath;
        //[Field ("pdfPath", "__Internal")]
        //NSMutableString pdfPath { get; }

        //// extern float g_Ink_Width;
        //[Field ("g_Ink_Width", "__Internal")]
        //float g_Ink_Width { get; }

        //// extern float g_rect_Width;
        //[Field ("g_rect_Width", "__Internal")]
        //float g_rect_Width { get; }

        // extern uint g_rect_color;
        //[Field ("g_rect_color", "__Internal")]
        //uint g_rect_color { get; }

        //// extern uint g_ink_color;
        //[Field ("g_ink_color", "__Internal")]
        //uint g_ink_color { get; }

        //// extern uint g_sel_color;
        //[Field ("g_sel_color", "__Internal")]
        //uint g_sel_color { get; }

        //// extern uint g_oval_color;
        //[Field ("g_oval_color", "__Internal")]
        //uint g_oval_color { get; }

        //// extern uint annotHighlightColor;
        //[Field ("annotHighlightColor", "__Internal")]
        //uint annotHighlightColor { get; }

        //// extern uint annotUnderlineColor;
        //[Field ("annotUnderlineColor", "__Internal")]
        //uint annotUnderlineColor { get; }

        //// extern uint annotStrikeoutColor;
        //[Field ("annotStrikeoutColor", "__Internal")]
        //uint annotStrikeoutColor { get; }

        //// extern uint annotSquigglyColor;
        //[Field ("annotSquigglyColor", "__Internal")]
        //uint annotSquigglyColor { get; }

        // extern _Bool g_paging_enabled;
        //[Field ("g_paging_enabled", "__Internal")]
        //bool g_paging_enabled { get; }

        //// extern _Bool g_double_page_enabled;
        //[Field ("g_double_page_enabled", "__Internal")]
        //bool g_double_page_enabled { get; }
    }

    // @interface OUTLINE_ITEM : NSObject
    [BaseType (typeof(NSObject))]
    interface OUTLINE_ITEM
    {
        // @property (retain, strong) NSString * label;
        [Export ("label", ArgumentSemantic.Retain)]
        string Label { get; set; }

        // @property (assign, nonatomic) int dest;
        [Export ("dest")]
        int Dest { get; set; }
    }

    // @protocol saveTextAnnotDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject))]
    interface saveTextAnnotDelegate
    {
        // @required -(void)OnSaveTextAnnot:(NSString *)textAnnot;
        [Abstract]
        [Export ("OnSaveTextAnnot:")]
        void OnSaveTextAnnot (string textAnnot);
    }

    // @interface TextAnnotViewController : UIViewController
    [BaseType (typeof(UIViewController))]
    interface TextAnnotViewController
    {
        // @property (readwrite) int pos_x;
        [Export ("pos_x")]
        int Pos_x { get; set; }

        // @property (readwrite) int pos_y;
        [Export ("pos_y")]
        int Pos_y { get; set; }

        // @property (nonatomic, strong) NSString * text;
        [Export ("text", ArgumentSemantic.Strong)]
        string Text { get; set; }

        // -(void)setDelegate:(id<saveTextAnnotDelegate>)delegate;
        [Export ("setDelegate:")]
        void SetDelegate (saveTextAnnotDelegate @delegate);
    }

    // @protocol PDFThumbViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject))]
    interface PDFThumbViewDelegate
    {
        // @required -(void)OnPageClicked:(int)pageno;
        [Abstract]
        [Export ("OnPageClicked:")]
        void OnPageClicked (int pageno);
    }

    // @protocol BookmarkTableViewDelegate
    [Protocol, Model]
    interface BookmarkTableViewDelegate
    {
        // @required -(void)didSelectItem:(int)page;
        [Abstract]
        [Export ("didSelectItem:")]
        void DidSelectItem (int page);
    }

    // @protocol PDFVInnerDel <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject))]
    interface PDFVInnerDel
    {
    }


    [BaseType(typeof(NSObject))]
    [Model]
    interface PDFAnnot
    {
    }

    [BaseType (typeof(UIScrollView))]
    interface PDFView : PDFVInnerDel, IUIScrollViewDelegate, IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        [Export("initWithFrame:")]
        IntPtr Constructor(CGRect frame);

        [Export("resetZoomLevel")]
        void ResetZoomLevel();

        [Export ("vOpen::")]
        void vOpen(PDFDoc doc, [NullAllowed] PDFViewDelegate deleg);

        [Export ("vGetX")]
        int DocX { get; }
        [Export ("vGetY")]
        int DocY { get; }
        [Export ("vGetDocWidth")]
        int DocWidth { get; }
        [Export ("vGetDocHeight")]
        int DocHeight { get; }
		[Export("vGetDocScale")]
		float DocScale { get; }
		[Export("vGetViewScale")]
		float ViewScale { get; }

    }
    [BaseType (typeof(NSObject))]
    [Model]
    interface PDFViewDelegate
    {
        //- (void)OnPageChanged :(int)pageno;
        [Abstract]
        [Export("OnPageChanged:")]
        void OnPageChanged(int pageno);

        //- (void)OnLongPressed:(float)x :(float)y;
        [Abstract]
        [Export("OnLongPressed::")]
        void OnPageChanged(float x, float y);

        //- (void)OnSingleTapped:(float)x :(float)y;
        [Abstract]
        [Export("OnSingleTapped::")]
        void OnSingleTapped(float x, float y);

        //- (void)OnDoubleTapped:(float)x :(float)y;
        [Abstract]
        [Export("OnDoubleTapped::")]
        void OnDoubleTapped(float x, float y);

        //- (void)OnFound:(bool)found;
        [Abstract]
        [Export("OnFound:")]
        void OnFound(bool found);

        //- (void)OnSelStart:(float)x :(float)y;
        [Abstract]
        [Export("OnSelStart::")]
        void OnSelStart(float x, float y);

        //- (void)OnSelEnd:(float)x1 :(float)y1 :(float)x2 :(float)y2;
        [Abstract]
        [Export("OnSelEnd::::")]
        void OnSelEnd(float x1, float y1, float x2, float y2);

        ////enter annotation status.
        //- (void)OnAnnotClicked:(PDFPage*)page :(PDFAnnot*)annot :(float)x :(float)y;
        [Abstract]
        [Export("OnAnnotClicked:")]
        void OnAnnotClicked(int pageno);

        ////notified when annotation status end.
        //- (void)OnAnnotEnd;
        [Abstract]
        [Export("OnAnnotEnd")]
        void OnAnnotEnd();

        ////this mehod fired only when vAnnotPerform method invoked.
        //- (void)OnAnnotGoto:(int)pageno;
        [Abstract]
        [Export("OnAnnotGoto:")]
        void OnAnnotGoto(int pageno);

        ////this mehod fired only when vAnnotPerform method invoked.
        //- (void)OnAnnotPopup:(PDFAnnot*)annot :(NSString*)subj :(NSString*)text;
        [Abstract]
        [Export("OnAnnotPopup:::")]
        void OnAnnotPopup(PDFAnnot annot, string subj, string text);

        ////this mehod fired only when vAnnotPerform method invoked.
        //- (void)OnAnnotOpenURL:(NSString*)url;
        [Abstract]
        [Export("OnAnnotOpenUrl:")]
        void OnAnnotOpenUrl(string url);

        ////this mehod fired only when vAnnotPerform method invoked.
        //- (void)OnAnnotMovie:(NSString*)fileName;
        [Abstract]
        [Export("OnAnnotMovie:")]
        void OnAnnotMovie(string filename);

        ////this mehod fired only when vAnnotPerform method invoked.
        //- (void)OnAnnotSound:(NSString*)fileName;
        [Abstract]
        [Export("OnAnnotSound:")]
        void OnAnnotSound(string filename);

        //- (void)OnAnnotEditBox :(CGRect)annotRect :(NSString*)editText;
        [Abstract]
        [Export("OnAnnotEditBox::")]
        void OnAnnotEditBox(CGRect annotRect, string editText);

        ////- (void)OnAnnotCommboBox:(NSArray*)dataArray;
        [Abstract]
        [Export("OnAnnotCommboBox:")]
        void OnAnnotComboBox(NSArray dataArray);

        //- (void)OnDidScroll;
        [Abstract]
        [Export("OnDidScroll")]
        void OnDidScroll();
        //- (void)OnZoomStart;
        [Abstract]
        [Export("OnZoomStart")]
        void OnZoomStart();
        [Abstract]
        //- (void)OnZoomEnd;
        [Export("OnZoomEnd")]
        void OnZoomEnd();
    }

    // @protocol ViewModeDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject))]
    interface ViewModeDelegate
    {
        // @required -(void)setReaderViewMode:(int)mode;
        [Abstract]
        [Export ("setReaderViewMode:")]
        void SetReaderViewMode (int mode);
    }

    // @interface ViewModeTableViewController : UITableViewController
    [BaseType (typeof(UITableViewController))]
    interface ViewModeTableViewController
    {
        [Wrap ("WeakDelegate")]
        ViewModeDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<ViewModeDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }
    }
}
