using System;
using System.IO;
using System.Drawing;
using CoreImage;

using AppKit;
using Foundation;


namespace PixelCounter
{

	public partial class ViewController : NSViewController
	{
		private string initialLabeltext;

		public ViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			initialLabeltext = pixelCount.StringValue;
			// Do any additional setup after loading the view.
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		partial void Select(NSObject sender)
		{
			var dlg = NSOpenPanel.OpenPanel;
			dlg.CanChooseDirectories = true;
			dlg.CanChooseFiles = false;

			if (dlg.RunModal() == 1)
			{
				// Nab the first file
				var url = dlg.Urls[0];
				if (url != null)
				{
					var path = url.Path;
					GetFilesAndCountPixels(path);
				}
			}
		}

		private void GetFilesAndCountPixels(string path) {
			pixelCount.TextColor = NSColor.Black;
			int pixelCounter = 0;

			string[] containingFiles;

			containingFiles = Directory.GetFiles(path,"*.*",SearchOption.AllDirectories);
			if (containingFiles.Length > 0)
			{
				foreach (string str in containingFiles)
				{
					if (str.EndsWith("jpg") || str.EndsWith("png"))
					{

						NSImage img = new NSImage(str);
						NSImageRep[] imgReps = img.Representations();
						int imgWidth = 0;
						int imgHeight = 0;
						if (imgReps!=null && imgReps.Length > 0)
						{
							long lastSquare = 0, curSquare;
							//NSImageRep imageRep = new NSImageRep();
							foreach (NSImageRep rep in imgReps)
							{
								curSquare = rep.PixelsWide * rep.PixelsHigh;
								if (curSquare > lastSquare)
								{
									imgWidth = (int)rep.PixelsWide;
									imgHeight = (int)rep.PixelsHigh;
									lastSquare = curSquare;
								}
							}
						}


						var imgPixels = imgWidth * imgHeight;

						Console.WriteLine(imgWidth+"x"+imgHeight);

						pixelCounter += imgPixels;
					}
				}

				if (pixelCounter == 0) {
					pixelCount.StringValue = "There are no images in the folder!";
					pixelCount.TextColor = NSColor.Red;
					return;
				}

				float newPixelCounter = (float)pixelCounter / (1024 * 1024);
				pixelCount.StringValue = initialLabeltext + " " + newPixelCounter.ToString("n2") + "MP";
			} else {
				pixelCount.StringValue = "There are no files in this folder!";
				pixelCount.TextColor = NSColor.Red;
					
			}
		}

	}
}
