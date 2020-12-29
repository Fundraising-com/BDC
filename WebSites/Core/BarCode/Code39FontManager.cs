using System;
using System.Drawing;
using System.Drawing.Text;

namespace GA.BDC.Core.BarCode
{
	/// <summary>
	/// Manages the Barcode Type Code39 Fonts to embed with the tool.
	/// </summary>
	/// <author>Benoit Nadon</author>
	/// <date>02/24/2006</date>
	/// <version>1.0</version>
	internal class Code39FontManager
	{
		private const string CODE39_FONT_NAME = "Free 3 of 9";
		private const string CODE39_FULL_ASCII_FONT_NAME = "Free 3 of 9 Extended";
		private const string CODE39_FILE_NAME = "FREE3OF9.TTF";
		private const string CODE39_FULL_ASCII_FILE_NAME = "FRE3OF9X.TTF";

		private static Code39FontManager singletonInstance;

		private PrivateFontCollection privateFontCollection = null;
		private FontFamily fontFamilyCode39 = null;
		private FontFamily fontFamilyCode39FullAscii = null;

		private Code39FontManager()
		{
			AddCode39Fonts();
		}

		/// <summary>
		/// Singleton instance - The fonts need to be loaded only once for the whole application.
		/// </summary>
		internal static Code39FontManager Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new Code39FontManager();
				}

				return singletonInstance;
			}
		}

		/// <summary>
		/// Private font collection to hold the Barcode fonts if they are not installed.
		/// </summary>
		private PrivateFontCollection PrivateFontCollection 
		{
			get 
			{
				if(privateFontCollection == null) 
				{
					privateFontCollection = new PrivateFontCollection();
				}

				return privateFontCollection;
			}
		}

		/// <summary>
		/// Installed or embedded Code39 font.
		/// </summary>
		internal FontFamily FontFamilyCode39 
		{
			get 
			{
				return fontFamilyCode39;
			}
		}

		/// <summary>
		/// Installed or embedded Code39 Full Ascii (Extended) font.
		/// </summary>
		internal FontFamily FontFamilyCode39FullAscii 
		{
			get 
			{
				return fontFamilyCode39FullAscii;
			}
		}

		/// <summary>
		/// Application setting for the Barcode fonts path.
		/// </summary>
		private string BarcodeFontsPath 
		{
			get 
			{
				string barcodeFontsPath;

				try 
				{
					barcodeFontsPath = System.Configuration.ConfigurationSettings.AppSettings["BarcodeFontsPath"];
				} 
				catch 
				{
					barcodeFontsPath = String.Empty;
				}

				return barcodeFontsPath;
			}
		}

		/// <summary>
		/// Adds the Code39 fonts by using the installed ones or embedding them.
		/// </summary>
		private void AddCode39Fonts() 
		{
			LoadInstalledCode39Fonts();
			LoadPrivateCode39Fonts();
		}

		/// <summary>
		/// Checks if the fonts are installed and uses the installed ones if it is the case.
		/// </summary>
		private void LoadInstalledCode39Fonts() 
		{
			InstalledFontCollection installedFontCollection = new InstalledFontCollection();

			AssignFontFamilies(installedFontCollection);
		}

		/// <summary>
		/// Loads the fonts from files to the private collection.
		/// </summary>
		private void LoadPrivateCode39Fonts() 
		{
			if(FontFamilyCode39 == null) 
			{
				PrivateFontCollection.AddFontFile(BarcodeFontsPath + CODE39_FILE_NAME);
			}
			if(FontFamilyCode39FullAscii == null) 
			{
				PrivateFontCollection.AddFontFile(BarcodeFontsPath + CODE39_FULL_ASCII_FILE_NAME);
			}

			AssignFontFamilies(PrivateFontCollection);
		}
		
		/// <summary>
		/// Searches for the Code39 fonts in a collection and assigns them.
		/// </summary>
		/// <param name="fontCollection">FontCollection to be searched.</param>
		private void AssignFontFamilies(FontCollection fontCollection) 
		{
			foreach(FontFamily fontFamily in fontCollection.Families) 
			{
				if(FontFamilyCode39 == null && fontFamily.Name == CODE39_FONT_NAME) 
				{
					fontFamilyCode39 = fontFamily;
				} 
				else if(FontFamilyCode39FullAscii == null && fontFamily.Name == CODE39_FULL_ASCII_FONT_NAME) 
				{
					fontFamilyCode39FullAscii = fontFamily;
				}
			}
		}
	}
}
