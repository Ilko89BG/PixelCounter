// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PixelCounter
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField pixelCount { get; set; }

		[Action ("Select:")]
		partial void Select (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (pixelCount != null) {
				pixelCount.Dispose ();
				pixelCount = null;
			}
		}
	}
}
