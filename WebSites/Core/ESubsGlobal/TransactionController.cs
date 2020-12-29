using System;
using System.Collections;

using GA.BDC.Core.ESubsGlobal;
using GA.BDC.Core.ESubsGlobal.Payment;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal {

	#region Enums and Structs
	// reponse status of a campaign creation
	public enum GroupEventStatus 
	{
		Ok,
		RedirectAlreadyExists,
		ExternalOrganizationIDAlreadyExists,
		EXIST_MEMBER_HIERACHY_ID_WITH_MEMBER_ID_AND_PARENT_MEMBER_HIERARCHY_ID,
		Unknown_Error
	}
	
	// reponse object of a campaign creation
	public struct GroupEventResponse {
		public int eventParticipationID;
		public GroupEventStatus status;
	}

	
	public enum CampaignStatus : int
	{
		OK=0,
		CAMPAIGN_ALREADY_EXISTS = 2,
		GROUP_ALREADY_EXISTS =3,
		NAME_OR_REDIRECT_ALREADY_EXISTS = 4,
		EMAIL_ADDRESS_PARTNER_ID_ALREADY_EXISTS = 5,
		EXTERNAL_MEMBER_ID_ALREADY_EXISTS = 6,
		EXIST_MEMBER_HIERACHY_ID_WITH_MEMBER_ID_AND_PARENT_MEMBER_HIERARCHY_ID = 7,
		EVENT_PARTICIPATION_ALREADY_EXISTS = 8,
		INSERT_PAYMENT_INFO_ERROR = 9,
		INTERNAL_ERROR = -1,
		UNKNOWN_ERROR = -2
	}
	public enum PaymentGroupPaymentInfoStatus : int
	{
		OK = 0,
		UPDATE_PAYMENT_STATUS_ERROR =1,
		UPDATE_GROUP_STATUS_ERROR=2,
		UPDATE_PAYMENT_INFO_ERROR =3,
		UPDATE_PAYMENT_ERROR =4
	}
	
	public enum PaymentAddressInfoStatus : int
	{
		OK = 0,
		UPDATE_POSTAL_ADDRESS_STATUS_ERROR=1,
		UPDATE_PAYMENT_STATUS_ERROR =2,
		UPDATE_PAYMENT_INFO_OLD_ERROR =3,
		UPDATE_PAYMENT_INFO_NEW_ERROR =4
	}




	#endregion

	/// <summary>
	/// TransactionController handle creation of critical eSubs items
	/// by using SQL transactions when creating items.
	/// </summary>
	public class TransactionController 
	{
		private Partner currentPartner;

		public TransactionController() : this(null) { }
		public TransactionController(eSubsGlobalEnvironment env) {
			if(env != null) {
				currentPartner = env.Partner;
			}
		}


		#region Related to Payment.
		public void InsertPayments(PaymentPeriod paymentPeriod, ArrayList payments) 
		{
			ESubsGlobalDatabase dbo =
				new ESubsGlobalDatabase();
			dbo.InsertPaymentsTransactionController(paymentPeriod, payments);
		}

		public Hashtable InsertPaymentsFromCheckSystem(PaymentPeriod paymentPeriod, ArrayList payments) 
		{
			ESubsGlobalDatabase dbo =
				new ESubsGlobalDatabase();
			return dbo.InsertPaymentsTransactionControllerFromCheckSystem(paymentPeriod, payments);
		}

		public bool InsertPaymentPaymentStatusChecksystem(ESubsGlobal.Payment.PaymentPaymentStatus[] ppaymentStatus)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.InsertPaymentPaymentStatusInCheckSystem(ppaymentStatus);
		}

		
		public bool UpdatePaymentsAfterGenerateSolutranFile(ESubsGlobal.Payment.PaymentPaymentStatus[] ppaymentStatus, Payment.Payment[] pyms )
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.UpdatePaymentsAfterGenerateSolutranFile(ppaymentStatus, pyms);
		}

		public PaymentGroupPaymentInfoStatus UpdatePaymentGroupPaymentInfo(PaymentPaymentStatus pps,GroupGroupStatus ggs, Payment.Payment p)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase( );
			return dbo.UpdatePaymentGroupPaymentInfo(pps,ggs,p);
		}
		
		public PaymentAddressInfoStatus UpdatePaymentAddressInfo(ESubsGlobal.Common.PostalAddress postalAddress, Payment.Payment payment, Payment.PaymentInfo paymentInfoOld, Payment.PaymentInfo paymentInfoNew)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase( );
			return dbo.UpdatePaymentAddressInfo(postalAddress, payment, paymentInfoOld, paymentInfoNew);
		}


		#endregion

		#region Insert a sponsor, group, event, event participation and payment info
		// insert a sponsor, group, event, event participation and payment info
		public GroupEventResponse InsertGroupEvent(Users.eSubsGlobalUser sponsor, Group group, Event _event, Payment.PaymentInfo paymentInformation) 
		{
			// declare the return value which is a struct with insert information
			GroupEventResponse groupEventResponse;
			
			// prefill value as if the whole process was right, so that it can be
			// changed during the insert process
			groupEventResponse.eventParticipationID = int.MinValue;
			groupEventResponse.status = GroupEventStatus.Ok;

			int eventID = Event.IsExistingRedirect(_event.Redirect);

			// check if the redirect already exists
			if(eventID < 0) 
			{
				// database object layer
				DataAccess.ESubsGlobalDatabase dbo =
					new DataAccess.ESubsGlobalDatabase();
				

				EventParticipation eventParticipation = null;
				CampaignStatus campStatus = dbo.InsertCampaignTransactionController(sponsor, _event, group, 
					paymentInformation, ref eventParticipation);

				if (campStatus != CampaignStatus.OK)
				{
					if (campStatus == CampaignStatus.EXIST_MEMBER_HIERACHY_ID_WITH_MEMBER_ID_AND_PARENT_MEMBER_HIERARCHY_ID)
					{
						groupEventResponse.status = GroupEventStatus.EXIST_MEMBER_HIERACHY_ID_WITH_MEMBER_ID_AND_PARENT_MEMBER_HIERARCHY_ID;
						//						if (eventParticipation != null)
						//						{
						//							groupEventResponse.eventParticipationID = eventParticipation.EventParticipationID;
						//							groupEventResponse.status = GroupEventStatus.Ok;
						//						}
						//						else
						//							groupEventResponse.status = GroupEventStatus.Unknown_Error;
					}
					else 
						if (campStatus == CampaignStatus.EXTERNAL_MEMBER_ID_ALREADY_EXISTS)
					{
						groupEventResponse.status = GroupEventStatus.ExternalOrganizationIDAlreadyExists;
					}
					else
						groupEventResponse.status = GroupEventStatus.Unknown_Error;
				}
				else
				{
					if (eventParticipation != null)
					{
						groupEventResponse.eventParticipationID = eventParticipation.EventParticipationID;
						groupEventResponse.status = GroupEventStatus.Ok;
					}
					else
						groupEventResponse.status = GroupEventStatus.Unknown_Error;
				}

		
			} 
			else 
			{
				// the event redirect already exists
				groupEventResponse.status = GroupEventStatus.RedirectAlreadyExists;
				return groupEventResponse;
			}

			// everything is ok, return the response object
			return groupEventResponse;
		}        
        
        public CampaignStatus InsertCampaign(eSubsGlobalUser user, Event evnt, Group grp, 
			PaymentInfo paymentInfo,ref EventParticipation eventParticipation)
		{
            return this.InsertCampaign(user, evnt, grp, paymentInfo, int.MinValue, int.MinValue, false, ref eventParticipation);
        }

		public CampaignStatus InsertCampaign(eSubsGlobalUser user, Event evnt, Group grp, 
			PaymentInfo paymentInfo, int coppaMonth, int coppaYear, 
            bool agreeToTermsServices, ref EventParticipation eventParticipation)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();			
			return dbo.InsertCampaignTransactionController(user, evnt, grp, paymentInfo, coppaMonth, coppaYear, agreeToTermsServices, ref eventParticipation);
		}
		#endregion

		#region CheckSystem

		private bool InsertPartnerPaymentConfig(int paymentInfoId, PartnerPaymentConfig newPaymentConfig)
		{
			ESubsGlobalDatabase dbo =	new ESubsGlobalDatabase();
			return dbo.InsertPartnerPaymentConfigTransactionController(paymentInfoId, newPaymentConfig);
		}

		private bool UpdatePartnerPaymentConfig(int paymentInfoId, PartnerPaymentConfig newPaymentConfig)
		{

			ESubsGlobalDatabase dbo =	new ESubsGlobalDatabase();
			return dbo.UpdatePartnerPaymentConfigTransactionController(paymentInfoId, newPaymentConfig);
		}

		#endregion
	}
}
