using System;

namespace efundraising.efundraisingCore.FormObject {

	public class Title {
	
		private int _titleId = -1;
		private string _titleDescription = string.Empty;

		public Title() {
		
		}

		public Title(int titleId, string titleDescription) {
			_titleId = titleId;
			_titleDescription = titleDescription;
		}

		public int TitleId {
			get{ return _titleId; }
		}

		public string TitleDescription {
			get{ return _titleDescription; }
		}
	}
}
