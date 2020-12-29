using System;

namespace GA.BDC.Core.Web.UI.Uploader.Images
{

	/// <summary>
	/// Internal class containing somes public const variables used for
	/// the Web Server Controls - ImagesUploader
	/// </summary>
	///	<remarks>This class cannot be use externaly of this namespace
	/// and inheritable</remarks>
	internal sealed class ImagesCtrlVars {
		
		#region ViewStates Variables Name

		public const string __Dimension_Width = "Image_Width";
		public const string __Dimension_Height = "Image_Height";
		public const string __ImageSpec = "ImageSpecText";
		public const string __Recommendation = "RecommandationText";
		public const string __MaxSizeText = "MaximumSize";
		public const string __Error_Recommendation_Text = "ErrorRecommandationText";
		public const string __Error_Size_Text = "ErrorSizeText";
		public const string __Error_Dimension_Text = "ErrorDimensionText";
		public const string __TextCss = "TextCss";
		public const string __Error_Color = "Error_Color";
		public const string __SizeType = "SizeType";        
		public const string __ImagesType = "Images_Type";
		public const string __AllowResizing = "AllowResizing";
		
		#endregion

		#region Category for VS.Net IDE Properties Box

		public const string __VSDesign_ImageType = "Images Type";
		public const string __VSDesign_ImageDimension = "Images Dimention";
		public const string __VSDesign_ImageText = "Image Text";

		#endregion
	}
}
