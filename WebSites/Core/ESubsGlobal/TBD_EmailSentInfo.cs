using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for TBD_EmailSentInfo.
	/// </summary>
	public class TBD_EmailSentInfo
	{
		private int event_participation_id;
		private int event_id;
		private int member_hierarchy_id;
		private int participation_channel_id;
		private DateTime create_date;
		private int custom_email_template_id;
		private int touch_info_id;
		private string subject;
		private string body_txt;
		private string body_html;
		private DateTime create_date1;
		private int touch_id;
		private int event_participation_id1;
		private int member_hierarchy_id1;
		private int touch_info_id1;
		private DateTime create_date2;
		private int touch_info_id2;
		private int business_rule_id;
		private int visitor_log_id;
		private DateTime launch_date;
		private DateTime create_date3;

		public TBD_EmailSentInfo()
		{
			
		}

//		public static TBD_EmailSentInfo[] GetEmailsSent() {
//			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
//			return dbo.GetCustomObj();
//		}

		public int Event_participation_id {
			set { event_participation_id = value; }
			get { return event_participation_id; }
		}

		public int Event_id {
			set { event_id = value; }
			get { return event_id; }
		}

		public int Member_hierarchy_id {
			set { member_hierarchy_id = value; }
			get { return member_hierarchy_id; }
		}

		public int Participation_channel_id {
			set { participation_channel_id = value; }
			get { return participation_channel_id; }
		}

		public DateTime Create_date {
			set { create_date = value; }
			get { return create_date; }
		}

		public int Custom_email_template_id {
			set { custom_email_template_id = value; }
			get { return custom_email_template_id; }
		}

		public int Touch_info_id {
			set { touch_info_id = value; }
			get { return touch_info_id; }
		}

		public string Subject {
			set { subject = value; }
			get { return subject; }
		}

		public string Body_txt {
			set { body_txt = value; }
			get { return body_txt; }
		}

		public string Body_html {
			set { body_html = value; }
			get { return body_html; }
		}

		public DateTime Create_date1 {
			set { create_date1 = value; }
			get { return create_date1; }
		}

		public int Touch_id {
			set { touch_id = value; }
			get { return touch_id; }
		}

		public int Event_participation_id1 {
			set { event_participation_id1 = value; }
			get { return event_participation_id1; }
		}

		public int Member_hierarchy_id1 {
			set { member_hierarchy_id1 = value; }
			get { return member_hierarchy_id1; }
		}

		public int Touch_info_id1 {
			set { touch_info_id1 = value; }
			get { return touch_info_id1; }
		}

		public DateTime Create_date2 {
			set { create_date2 = value; }
			get { return create_date2; }
		}

		public int Touch_info_id2 {
			set { touch_info_id2 = value; }
			get { return touch_info_id2; }
		}

		public int Business_rule_id {
			set { business_rule_id = value; }
			get { return business_rule_id; }
		}

		public int Visitor_log_id {
			set { visitor_log_id = value; }
			get { return visitor_log_id; }
		}

		public DateTime Launch_date {
			set { launch_date = value; }
			get { return launch_date; }
		}

		public DateTime Create_date3 {
			set { create_date3 = value; }
			get { return create_date3; }
		}
	}
}
