using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineFullServiceStrategy.
	/// </summary>
	internal class MagazineFullServiceValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Magazine Full Service";

		internal MagazineFullServiceValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if(AccountShippingProvince(campaignProgram) != "ON")   
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_MAGFS_PROVINCE, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Magazine)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string [] {PROGRAM_NAME, CurrentPrograms.Magazine.ToString()}));
				isValid = false;
			}

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.MagazineExpress.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Gift))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Gift.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.CookieDough.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Chocolate))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Chocolate.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.DrawPrize))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.DrawPrize.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChart))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.LargeChart.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.LargeChartWithNumSubs.ToString() }));
                isValid = false;
            }

			return isValid;
		}

        private string AccountShippingProvince(Business.Objects.CampaignProgram campaignProgram)
        {
            int accountID = campaignProgram.Campaign.dataSet.Campaign[0].ShipToAccountID;

            CAccount account = new CAccount();
            account.GetOneById(accountID);

            int AddressListID = account.dataSet.CAccount[0].AddressListID;

            Address address = new Address();
            address.GetAllByAddressListID(AddressListID);

            string province = address.GetOneByType(AddressType.ShipTo).stateProvince;

            return province;
        }
    }
}
