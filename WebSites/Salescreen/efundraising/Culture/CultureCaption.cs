using System;

namespace efundraising.efundraisingCore.Culture
{
	[Serializable()]
	public class CultureCaption {

		#region private fields

		private Culture _culture;
		private string _captionValue;

		#endregion	

		public CultureCaption() {
				
		}

		public CultureCaption(Culture culture, string captionValue) {
			_culture = culture;
			_captionValue = captionValue;
		}


		#region public properties

		public Culture culture {
			get{ return _culture; }
			set{ _culture = value; }
		}

		public string CaptionValue {
			get{ return _captionValue; }
			set{ _captionValue = value; }
		}

		#endregion
	}
}
