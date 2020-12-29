/* Title:	Creation Channel
 * Author:	Jean-Francois Buist
 * Summary:	Every members has a creation channel to know how they have been created.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal.Users
{
	public enum CreationChannelType : int {
		SPONSOR_AUTO_REGISTER_1 = 1,
		SPONSOR_IMPORTE_LIST_2 = 2,
		SPONSOR_FREE_KIT_AUTO_REPLY_3 = 3,
		SPONSOR_PARTNER_WEB_SITE_4 = 4,
		SPONSOR_NEWS_LETTER_MASS_EMAIL_5 = 5,
		SPONSOR_CONSULTANT_MODULE_6 = 6,
		PARTICIPANT_INVITE_PARTICIPANT_KICK_OFF_7 = 7,
		PARTICIPANT_SPLIT_PAGE_8 = 8,
		PARTICIPANT_TELL_A_FRIEND_9 = 9,
		DEPRECIATED_PARTICIPANT_DEFAULT_PARTICIPANT_10 = 10,
		DEPRECIATED_PARTICIPANT_PARTICIPANT_PAGE_11 = 11,
		SUPPORTER_INVITED_BY_PARTICIPANT_IN_COLLECTION_PAGE_12 = 12,
		SUPPORTER_BUY_NOW_FROM_PARTICIPANT_EMAIL_13 = 13,
		SUPPORTER_IMPORTED_FROM_PARTICIPANT_ADDRESS_BOOK_14 = 14,
		SUPPORTER_BUY_NOW_FROM_FIND_GROUP_15 = 15,
		DEPRECIATED_SUPPORTER_DEFAULT_SUPPORTER_16 = 16,
		DEPRECIATED_PARTICIPANT_CREATED_BY_SUPPORTER_17 = 17,
		DEPRECIATED_PARTICIPANT_CREATED_BY_ORGANIZER_18 = 18,
		SUPPORTER_CREATED_BY_PARTICIPANT_19 = 19,
		PARTICIPANT_CREATED_BY_ROSTER_20 = 20,
		SPONSOR_CREATED_FROM_ROSTER_21 = 21,
		PARTICIPANT_VIRTUAL_22 = 22,
		PARTICIPANT_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK_23 = 23,
		SUPPORTER_GROUP_SITE_LINK_TO_STORE_24 = 24,
		SPONSOR_FORM_COMPLETED_25 = 25,
		SUPPORTER_FROM_ORGANIZER_29 = 29,
		SPONSOR_CREATED_BY_PARTNER_BANNER_30 = 30,
		SECOND_LAUNCH_SPONSOR_CREATED_THROUGH_QSP_FORM_34 = 34,
		SECOND_LAUNCH_PARENT_CREATED_THROUGH_QSP_FORM_35 = 35,
		SECOND_LAUNCH_STUDENT_CREATED_THROUGH_QSP_FORM_36 = 36,
		SECOND_LAUNCH_SUPPORTER_CREATED_THROUGH_QSP_FORM_37 = 37,
		FACEBOOK_SPONSOR = 39,
		FACEBOOK_USER_NO_EMAIL = 40,
		FACEBBOK_USER_WITH_EMAIL = 41,
		ELAUNCH_FSM_CLICKED_YES = 42,
        EFUNDS_CARD_GROUP_AUTO_CREATED = 43,
        SAMPLE_KIT_AUTO_CREATION = 44,
        SUPPORTER_DIRECT_MAIL = 45,
        SUPPORTER_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK = 46
	}

	/// <summary>
	/// Summary description for CreationChannel.
	/// </summary>
    [Serializable]
	public class CreationChannel {
        private int id = int.MinValue;
		private string name;
		private string description;

		public CreationChannel() {

		}

		public static CreationChannel CreateFromID(int id) {
			switch(id) {
				case 1:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_AUTO_REGISTER_1, "Sponsor: Auto-Register");
				case 2:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_IMPORTE_LIST_2, "Sponsor: Imported List");
				case 3:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_FREE_KIT_AUTO_REPLY_3, "Sponsor: Free Kit Form");
				case 4:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_PARTNER_WEB_SITE_4, "Partner Web Site - To Be removed");
				case 5:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_NEWS_LETTER_MASS_EMAIL_5, "Sponsor: Newsletter");
				case 6:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_CONSULTANT_MODULE_6, "Sponsor: Consultant Module");
				case 7:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_INVITE_PARTICIPANT_KICK_OFF_7, "Participant: Kick-Fff");
				case 8:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_SPLIT_PAGE_8, "Participant: Find Group/Link");
				case 9:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_TELL_A_FRIEND_9, "Thank You Page (Tell a Friend) - To be removed");
				case 10:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_DEFAULT_PARTICIPANT_10, "Default Participant - To be removed");
				case 11:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_PARTICIPANT_PAGE_11, "???Participant Collection Page By Organizer Section???? - To be removed or renamed- in use to go directly to qspstore");
				case 12:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_INVITED_BY_PARTICIPANT_IN_COLLECTION_PAGE_12, "Supporter: Invited by Participant");
				case 13:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_BUY_NOW_FROM_PARTICIPANT_EMAIL_13, "Supporter: Participant Email");
				case 14:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_IMPORTED_FROM_PARTICIPANT_ADDRESS_BOOK_14, "Supporter: Imported By Participant's Address Book");
				case 15:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_BUY_NOW_FROM_FIND_GROUP_15, "Supporter: Find My Group/Link");
				case 16:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_SUPPORTER_DEFAULT_SUPPORTER_16, "Supporter: Default (DEPRECIATED)");
				case 17:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_SUPPORTER_17, "Participant created by Supporter");
				case 18:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_ORGANIZER_18, "Participant created by Organizer");
				case 19:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_CREATED_BY_PARTICIPANT_19, "Supporter: Created by Participant");
				case 20:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_CREATED_BY_ROSTER_20, "Participant: Roster Web Services");
				case 21:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_CREATED_FROM_ROSTER_21, "Sponsor: Roster Web Services");
				case 22:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_VIRTUAL_22, "Participant: Virtual Participant");
				case 23:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK_23, "Participant: Imported from Organizer Address Book");
				case 24:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_GROUP_SITE_LINK_TO_STORE_24, "Supporter: Group Site Link to Store");
				case 25:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_FORM_COMPLETED_25, "Sponsor: Free Kit Form");
				case 29:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_FROM_ORGANIZER_29, "Supporter: Organizer Invitation");
				case 30:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_CREATED_BY_PARTNER_BANNER_30, "Sponsor: X-Factor");
				case 34:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_SPONSOR_CREATED_THROUGH_QSP_FORM_34, "2nd Launch : Sponsor Created through QSP form");
				case 35:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_PARENT_CREATED_THROUGH_QSP_FORM_35, "2nd Launch : Parent Created through QSP form");
				case 36:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_STUDENT_CREATED_THROUGH_QSP_FORM_36, "2nd Launch : Student Created through QSP form");
				case 37:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_SUPPORTER_CREATED_THROUGH_QSP_FORM_37, "2nd Launch : Supporter Created through QSP form");
				case 39:
					return CreateCreationChannel((int)CreationChannelType.FACEBOOK_SPONSOR, "Facebook : Sponsor");
				case 40:
					return CreateCreationChannel((int)CreationChannelType.FACEBOOK_USER_NO_EMAIL, "Facebook : Internal user");
				case 41:
					return CreateCreationChannel((int)CreationChannelType.FACEBBOK_USER_WITH_EMAIL, "Facebook : External user (with email)");
				case 42:
					return CreateCreationChannel((int) CreationChannelType.ELAUNCH_FSM_CLICKED_YES, "ELaunch : FSM clicked Yes");
                case 43:
                    return CreateCreationChannel((int)CreationChannelType.EFUNDS_CARD_GROUP_AUTO_CREATED, "EFunds Card : Group Auto Created");
                case 44:
                    return CreateCreationChannel((int)CreationChannelType.SAMPLE_KIT_AUTO_CREATION, "Sample Kit Auto Creation");
                case 45:
                    return CreateCreationChannel((int)CreationChannelType.SUPPORTER_DIRECT_MAIL, "Supporter created by direct mail");
                case 46:
                    return CreateCreationChannel((int)CreationChannelType.SUPPORTER_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK, "Supporter imported from organizer address book");
            }
			return null;
		}

		public static CreationChannel SPONSOR_AUTO_REGISTER_1 {
			get { return Create(CreationChannelType.SPONSOR_AUTO_REGISTER_1); }
		}


		public static CreationChannel SPONSOR_IMPORTE_LIST_2 {
			get { return Create(CreationChannelType.SPONSOR_IMPORTE_LIST_2); }
		}


		public static CreationChannel SPONSOR_FREE_KIT_AUTO_REPLY_3 {
			get { return Create(CreationChannelType.SPONSOR_FREE_KIT_AUTO_REPLY_3); }
		}


		public static CreationChannel SPONSOR_PARTNER_WEB_SITE_4 {
			get { return Create(CreationChannelType.SPONSOR_PARTNER_WEB_SITE_4); }
		}


		public static CreationChannel SPONSOR_NEWS_LETTER_MASS_EMAIL_5 {
			get { return Create(CreationChannelType.SPONSOR_NEWS_LETTER_MASS_EMAIL_5); }
		}


		public static CreationChannel SPONSOR_CONSULTANT_MODULE_6 {
			get { return Create(CreationChannelType.SPONSOR_CONSULTANT_MODULE_6); }
		}


		public static CreationChannel PARTICIPANT_INVITE_PARTICIPANT_KICK_OFF_7 {
			get { return Create(CreationChannelType.PARTICIPANT_INVITE_PARTICIPANT_KICK_OFF_7); }
		}


		public static CreationChannel PARTICIPANT_SPLIT_PAGE_8 {
			get { return Create(CreationChannelType.PARTICIPANT_SPLIT_PAGE_8); }
		}


		public static CreationChannel PARTICIPANT_TELL_A_FRIEND_9 {
			get { return Create(CreationChannelType.PARTICIPANT_TELL_A_FRIEND_9); }
		}

		public static CreationChannel SUPPORTER_FROM_ORGANIZER_29 {
			get { return Create(CreationChannelType.SUPPORTER_FROM_ORGANIZER_29); }
		}

		public static CreationChannel DEPRECIATED_PARTICIPANT_DEFAULT_PARTICIPANT_10 {
			get { return Create(CreationChannelType.DEPRECIATED_PARTICIPANT_DEFAULT_PARTICIPANT_10); }
		}


		public static CreationChannel DEPRECIATED_PARTICIPANT_PARTICIPANT_PAGE_11 {
			get { return Create(CreationChannelType.DEPRECIATED_PARTICIPANT_PARTICIPANT_PAGE_11); }
		}


		public static CreationChannel SUPPORTER_INVITED_BY_PARTICIPANT_IN_COLLECTION_PAGE_12 {
			get { return Create(CreationChannelType.SUPPORTER_INVITED_BY_PARTICIPANT_IN_COLLECTION_PAGE_12); }
		}


		public static CreationChannel SUPPORTER_BUY_NOW_FROM_PARTICIPANT_EMAIL_13 {
			get { return Create(CreationChannelType.SUPPORTER_BUY_NOW_FROM_PARTICIPANT_EMAIL_13); }
		}


		public static CreationChannel SUPPORTER_IMPORTED_FROM_PARTICIPANT_ADDRESS_BOOK_14 {
			get { return Create(CreationChannelType.SUPPORTER_IMPORTED_FROM_PARTICIPANT_ADDRESS_BOOK_14); }
		}


		public static CreationChannel SUPPORTER_BUY_NOW_FROM_FIND_GROUP_15 {
			get { return Create(CreationChannelType.SUPPORTER_BUY_NOW_FROM_FIND_GROUP_15); }
		}


		public static CreationChannel DEPRECIATED_SUPPORTER_DEFAULT_SUPPORTER_16 {
			get { return Create(CreationChannelType.DEPRECIATED_SUPPORTER_DEFAULT_SUPPORTER_16); }
		}


		public static CreationChannel DEPRECIATED_PARTICIPANT_CREATED_BY_SUPPORTER_17 {
			get { return Create(CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_SUPPORTER_17); }
		}


		public static CreationChannel DEPRECIATED_PARTICIPANT_CREATED_BY_ORGANIZER_18 {
			get { return Create(CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_ORGANIZER_18); }
		}


		public static CreationChannel SUPPORTER_CREATED_BY_PARTICIPANT_19 {
			get { return Create(CreationChannelType.SUPPORTER_CREATED_BY_PARTICIPANT_19); }
		}


		public static CreationChannel PARTICIPANT_CREATED_BY_ROSTER_20 {
			get { return Create(CreationChannelType.PARTICIPANT_CREATED_BY_ROSTER_20); }
		}


		public static CreationChannel SPONSOR_CREATED_FROM_ROSTER_21 {
			get { return Create(CreationChannelType.SPONSOR_CREATED_FROM_ROSTER_21); }
		}


		public static CreationChannel PARTICIPANT_VIRTUAL_22 {
			get { return Create(CreationChannelType.PARTICIPANT_VIRTUAL_22); }
		}


		public static CreationChannel PARTICIPANT_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK_23 {
			get { return Create(CreationChannelType.PARTICIPANT_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK_23); }
		}


		public static CreationChannel SUPPORTER_GROUP_SITE_LINK_TO_STORE_24 {
			get { return Create(CreationChannelType.SUPPORTER_GROUP_SITE_LINK_TO_STORE_24); }
		}


		public static CreationChannel SPONSOR_FORM_COMPLETED_25 {
			get { return Create(CreationChannelType.SPONSOR_FORM_COMPLETED_25); }
		}

		public static CreationChannel SPONSOR_CREATED_BY_PARTNER_BANNER_30 
		{
			get { return Create(CreationChannelType.SPONSOR_CREATED_BY_PARTNER_BANNER_30); }
		}

		public static CreationChannel SECOND_LAUNCH_SPONSOR_CREATED_THROUGH_QSP_FORM_34 
		{
			get { return Create(CreationChannelType.SECOND_LAUNCH_SPONSOR_CREATED_THROUGH_QSP_FORM_34); }
		}

		public static CreationChannel SECOND_LAUNCH_PARENT_CREATED_THROUGH_QSP_FORM_35 
		{
			get { return Create(CreationChannelType.SECOND_LAUNCH_PARENT_CREATED_THROUGH_QSP_FORM_35); }
		}

		public static CreationChannel SECOND_LAUNCH_STUDENT_CREATED_THROUGH_QSP_FORM_36 
		{
			get { return Create(CreationChannelType.SECOND_LAUNCH_STUDENT_CREATED_THROUGH_QSP_FORM_36); }
		}

		public static CreationChannel SECOND_LAUNCH_SUPPORTER_CREATED_THROUGH_QSP_FORM_37 
		{
			get { return Create(CreationChannelType.SECOND_LAUNCH_SUPPORTER_CREATED_THROUGH_QSP_FORM_37); }
		}

		public static CreationChannel FACEBOOK_SPONSOR {
			get { return Create(CreationChannelType.FACEBOOK_SPONSOR); }
		}

		public static CreationChannel FACEBBOK_USER_WITH_EMAIL {
			get { return Create(CreationChannelType.FACEBBOK_USER_WITH_EMAIL); }
		}

		public static CreationChannel FACEBOOK_USER_NO_EMAIL {
			get { return Create(CreationChannelType.FACEBOOK_USER_NO_EMAIL); }
		}

		public static CreationChannel ELAUNCH_FSM_CLICKED_YES
		{
			get { return Create(CreationChannelType.ELAUNCH_FSM_CLICKED_YES);}
		}

        public static CreationChannel EFUNDS_CARD_GROUP_AUTO_CREATED
        {
            get { return Create(CreationChannelType.EFUNDS_CARD_GROUP_AUTO_CREATED); }
        }

        public static CreationChannel SAMPLE_KIT_AUTO_CREATION
        {
            get { return Create(CreationChannelType.SAMPLE_KIT_AUTO_CREATION); }
        }

        public static CreationChannel SUPPORTER_DIRECT_MAIL
        {
            get { return Create(CreationChannelType.SUPPORTER_DIRECT_MAIL); }
        }

        public static CreationChannel SUPPORTER_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK
        {
            get { return Create(CreationChannelType.SUPPORTER_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK); }
        }

        private static CreationChannel CreateCreationChannel(int id, string msg) {
			CreationChannel cc = new CreationChannel();
			cc.id = id;
			cc.name = msg;
			return cc;
		}

		private static CreationChannel Create(CreationChannelType type) {
			switch(type) {
				case CreationChannelType.SPONSOR_AUTO_REGISTER_1:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_AUTO_REGISTER_1, "Sponsor: Auto-Register");
				case CreationChannelType.SPONSOR_IMPORTE_LIST_2:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_IMPORTE_LIST_2, "Sponsor: Imported List");
				case CreationChannelType.SPONSOR_FREE_KIT_AUTO_REPLY_3:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_FREE_KIT_AUTO_REPLY_3, "Sponsor: Free Kit Form");
				case CreationChannelType.SPONSOR_PARTNER_WEB_SITE_4:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_PARTNER_WEB_SITE_4, "Partner Web Site - To Be removed");
				case CreationChannelType.SPONSOR_NEWS_LETTER_MASS_EMAIL_5:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_NEWS_LETTER_MASS_EMAIL_5, "Sponsor: Newsletter");
				case CreationChannelType.SPONSOR_CONSULTANT_MODULE_6:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_CONSULTANT_MODULE_6, "Sponsor: Consultant Module");
				case CreationChannelType.PARTICIPANT_INVITE_PARTICIPANT_KICK_OFF_7:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_INVITE_PARTICIPANT_KICK_OFF_7, "Participant: Kick-Fff");
				case CreationChannelType.PARTICIPANT_SPLIT_PAGE_8:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_SPLIT_PAGE_8, "Participant: Find Group/Link");
				case CreationChannelType.PARTICIPANT_TELL_A_FRIEND_9:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_TELL_A_FRIEND_9, "Thank You Page (Tell a Friend) - To be removed");
				case CreationChannelType.DEPRECIATED_PARTICIPANT_DEFAULT_PARTICIPANT_10:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_DEFAULT_PARTICIPANT_10, "Default Participant - To be removed");
				case CreationChannelType.DEPRECIATED_PARTICIPANT_PARTICIPANT_PAGE_11:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_PARTICIPANT_PAGE_11, "???Participant Collection Page By Organizer Section???? - To be removed or renamed- in use to go directly to qspstore");
				case CreationChannelType.SUPPORTER_INVITED_BY_PARTICIPANT_IN_COLLECTION_PAGE_12:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_INVITED_BY_PARTICIPANT_IN_COLLECTION_PAGE_12, "Supporter: Invited by Participant");
				case CreationChannelType.SUPPORTER_BUY_NOW_FROM_PARTICIPANT_EMAIL_13:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_BUY_NOW_FROM_PARTICIPANT_EMAIL_13, "Supporter: Participant Email");
				case CreationChannelType.SUPPORTER_IMPORTED_FROM_PARTICIPANT_ADDRESS_BOOK_14:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_IMPORTED_FROM_PARTICIPANT_ADDRESS_BOOK_14, "Supporter: Imported By Participant's Address Book");
				case CreationChannelType.SUPPORTER_BUY_NOW_FROM_FIND_GROUP_15:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_BUY_NOW_FROM_FIND_GROUP_15, "Supporter: Find My Group/Link");
				case CreationChannelType.DEPRECIATED_SUPPORTER_DEFAULT_SUPPORTER_16:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_SUPPORTER_DEFAULT_SUPPORTER_16, "Supporter: Default (DEPRECIATED)");
				case CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_SUPPORTER_17:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_SUPPORTER_17, "Participant created by Supporter");
				case CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_ORGANIZER_18:
					return CreateCreationChannel((int)CreationChannelType.DEPRECIATED_PARTICIPANT_CREATED_BY_ORGANIZER_18, "Participant created by Organizer");
				case CreationChannelType.SUPPORTER_CREATED_BY_PARTICIPANT_19:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_CREATED_BY_PARTICIPANT_19, "Supporter: Created by Participant");
				case CreationChannelType.PARTICIPANT_CREATED_BY_ROSTER_20:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_CREATED_BY_ROSTER_20, "Participant: Roster Web Services");
				case CreationChannelType.SPONSOR_CREATED_FROM_ROSTER_21:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_CREATED_FROM_ROSTER_21, "Sponsor: Roster Web Services");
				case CreationChannelType.PARTICIPANT_VIRTUAL_22:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_VIRTUAL_22, "Participant: Virtual Participant");
				case CreationChannelType.PARTICIPANT_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK_23:
					return CreateCreationChannel((int)CreationChannelType.PARTICIPANT_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK_23, "Participant: Imported from Organizer Address Book");
				case CreationChannelType.SUPPORTER_GROUP_SITE_LINK_TO_STORE_24:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_GROUP_SITE_LINK_TO_STORE_24, "Supporter: Group Site Link to Store");
				case CreationChannelType.SPONSOR_FORM_COMPLETED_25:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_FORM_COMPLETED_25, "Sponsor: Free Kit Form");
				case CreationChannelType.SUPPORTER_FROM_ORGANIZER_29:
					return CreateCreationChannel((int)CreationChannelType.SUPPORTER_FROM_ORGANIZER_29, "Supporter: Organizer Invitation");
				case CreationChannelType.SPONSOR_CREATED_BY_PARTNER_BANNER_30:
					return CreateCreationChannel((int)CreationChannelType.SPONSOR_CREATED_BY_PARTNER_BANNER_30, "Sponsor: X-Factor");
				case CreationChannelType.SECOND_LAUNCH_SPONSOR_CREATED_THROUGH_QSP_FORM_34:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_SPONSOR_CREATED_THROUGH_QSP_FORM_34, "2nd Launch : Sponsor Created through QSP form");
				case CreationChannelType.SECOND_LAUNCH_PARENT_CREATED_THROUGH_QSP_FORM_35:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_PARENT_CREATED_THROUGH_QSP_FORM_35, "2nd Launch : Parent Created through QSP form");
				case CreationChannelType.SECOND_LAUNCH_STUDENT_CREATED_THROUGH_QSP_FORM_36:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_STUDENT_CREATED_THROUGH_QSP_FORM_36, "2nd Launch : Student Created through QSP form");
				case CreationChannelType.SECOND_LAUNCH_SUPPORTER_CREATED_THROUGH_QSP_FORM_37:
					return CreateCreationChannel((int)CreationChannelType.SECOND_LAUNCH_SUPPORTER_CREATED_THROUGH_QSP_FORM_37, "2nd Launch : Supporter Created through QSP form");
				case CreationChannelType.FACEBOOK_SPONSOR:
					return CreateCreationChannel((int)CreationChannelType.FACEBOOK_SPONSOR, "Facebook Application : Sponsor Created through Facebook");
				case CreationChannelType.FACEBOOK_USER_NO_EMAIL:
					return CreateCreationChannel((int)CreationChannelType.FACEBOOK_USER_NO_EMAIL, "Facebook Application : Facebook user (no email address) act as Supporter/Participant Created through Facebook");
				case CreationChannelType.FACEBBOK_USER_WITH_EMAIL:
					return CreateCreationChannel((int)CreationChannelType.FACEBBOK_USER_WITH_EMAIL, "Facebook Application : Supporter/Participant Created through Facebook");
				case CreationChannelType.ELAUNCH_FSM_CLICKED_YES:
					return CreateCreationChannel((int) CreationChannelType.ELAUNCH_FSM_CLICKED_YES, "ELaunch : FSM clicked Yes");
                case CreationChannelType.EFUNDS_CARD_GROUP_AUTO_CREATED:
                    return CreateCreationChannel((int)CreationChannelType.EFUNDS_CARD_GROUP_AUTO_CREATED, "EFunds Card : Group Auto Created");
                case CreationChannelType.SAMPLE_KIT_AUTO_CREATION:
                    return CreateCreationChannel((int)CreationChannelType.SAMPLE_KIT_AUTO_CREATION, "Sample Kit Auto Creation");
                case CreationChannelType.SUPPORTER_DIRECT_MAIL:
                    return CreateCreationChannel((int)CreationChannelType.SUPPORTER_DIRECT_MAIL, "Supporter created by direct mail");
                case CreationChannelType.SUPPORTER_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK:
                    return CreateCreationChannel((int)CreationChannelType.SUPPORTER_IMPORTED_FROM_ORGANIZER_ADDRESS_BOOK, "Supporter imported from organizer address book");
			}
			return null;
		}

		#region Properties
		public int ID {
			set { id = value; }
			get { return id; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}
		#endregion
	}
}
