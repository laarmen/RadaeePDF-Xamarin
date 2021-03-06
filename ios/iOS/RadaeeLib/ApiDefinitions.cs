using Foundation;
using ObjCRuntime;
using UIKit;

namespace RadaeeLib
{
	// @interface RadaeePDFPlugin : NSObject
	[BaseType (typeof(NSObject))]
	interface RadaeePDFPlugin
	{
		// @property (nonatomic) int viewMode;
		[Export ("viewMode")]
		int ViewMode { get; set; }

		// @property (nonatomic, strong) UIImage * searchImage;
		[Export ("searchImage", ArgumentSemantic.Strong)]
		UIImage SearchImage { get; set; }

		// @property (nonatomic, strong) UIImage * bookmarkImage;
		[Export ("bookmarkImage", ArgumentSemantic.Strong)]
		UIImage BookmarkImage { get; set; }

		// @property (nonatomic, strong) UIImage * outlineImage;
		[Export ("outlineImage", ArgumentSemantic.Strong)]
		UIImage OutlineImage { get; set; }

		// @property (nonatomic, strong) UIImage * lineImage;
		[Export ("lineImage", ArgumentSemantic.Strong)]
		UIImage LineImage { get; set; }

		// @property (nonatomic, strong) UIImage * rectImage;
		[Export ("rectImage", ArgumentSemantic.Strong)]
		UIImage RectImage { get; set; }

		// @property (nonatomic, strong) UIImage * ellipseImage;
		[Export ("ellipseImage", ArgumentSemantic.Strong)]
		UIImage EllipseImage { get; set; }

		// @property (nonatomic, strong) UIImage * deleteImage;
		[Export ("deleteImage", ArgumentSemantic.Strong)]
		UIImage DeleteImage { get; set; }

		// @property (nonatomic, strong) UIImage * doneImage;
		[Export ("doneImage", ArgumentSemantic.Strong)]
		UIImage DoneImage { get; set; }

		// @property (nonatomic, strong) UIImage * removeImage;
		[Export ("removeImage", ArgumentSemantic.Strong)]
		UIImage RemoveImage { get; set; }

		// @property (nonatomic, strong) UIImage * prevImage;
		[Export ("prevImage", ArgumentSemantic.Strong)]
		UIImage PrevImage { get; set; }

		// @property (nonatomic, strong) UIImage * nextImage;
		[Export ("nextImage", ArgumentSemantic.Strong)]
		UIImage NextImage { get; set; }

		// @property (nonatomic, strong) NSArray * cdv_command;
		[Export ("cdv_command", ArgumentSemantic.Strong)]
		[Verify (StronglyTypedNSArray)]
		NSObject[] Cdv_command { get; set; }

		// @property (nonatomic, weak) UIViewController * viewController;
		[Export ("viewController", ArgumentSemantic.Weak)]
		UIViewController ViewController { get; set; }

		// -(void)pluginInitialize;
		[Export ("pluginInitialize")]
		void PluginInitialize ();

		// -(RDPDFViewController *)show:(NSArray *)command;
		[Export ("show:")]
		[Verify (StronglyTypedNSArray)]
		RDPDFViewController Show (NSObject[] command);

		// -(RDPDFViewController *)activateLicense:(NSArray *)command;
		[Export ("activateLicense:")]
		[Verify (StronglyTypedNSArray)]
		RDPDFViewController ActivateLicense (NSObject[] command);

		// -(RDPDFViewController *)openFromAssets:(NSArray *)command;
		[Export ("openFromAssets:")]
		[Verify (StronglyTypedNSArray)]
		RDPDFViewController OpenFromAssets (NSObject[] command);

		// +(RadaeePDFPlugin *)pluginInit;
		[Static]
		[Export ("pluginInit")]
		[Verify (MethodToProperty)]
		RadaeePDFPlugin PluginInit { get; }

		// +(NSMutableArray *)loadBookmark;
		[Static]
		[Export ("loadBookmark")]
		[Verify (MethodToProperty)]
		NSMutableArray LoadBookmark { get; }

		// +(NSMutableArray *)loadBookmarkForPdf:(NSString *)pdfName;
		[Static]
		[Export ("loadBookmarkForPdf:")]
		NSMutableArray LoadBookmarkForPdf (string pdfName);

		// -(void)setPagingEnabled:(BOOL)enabled;
		[Export ("setPagingEnabled:")]
		void SetPagingEnabled (bool enabled);

		// -(void)setDoublePageEnabled:(BOOL)enabled;
		[Export ("setDoublePageEnabled:")]
		void SetDoublePageEnabled (bool enabled);

		// -(BOOL)setReaderViewMode:(int)mode;
		[Export ("setReaderViewMode:")]
		bool SetReaderViewMode (int mode);

		// -(void)toggleThumbSeekBar:(int)mode;
		[Export ("toggleThumbSeekBar:")]
		void ToggleThumbSeekBar (int mode);

		// -(void)setColor:(int)color forFeature:(int)feature;
		[Export ("setColor:forFeature:")]
		void SetColor (int color, int feature);
	}
}
