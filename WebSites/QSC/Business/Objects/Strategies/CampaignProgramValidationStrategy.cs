using System;
using Common;
using Common.TableDef;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for ICampaignProgramValidationStrategy.
	/// </summary>
	internal abstract class CampaignProgramValidationStrategy
	{
		private Message messageManager = null;
      private Transaction currentTransaction = null;

      internal CampaignProgramValidationStrategy(Message messageManager)
      {
         this.messageManager = messageManager;
      }

      internal CampaignProgramValidationStrategy(Message messageManager, Transaction currentTransaction) 
		{
			this.messageManager = messageManager;
         this.currentTransaction = currentTransaction;
		}

		protected Message CurrentMessageManager 
		{
			get 
			{
				return this.messageManager;
			}
		}

      protected Transaction CurrentTransaction
      {
         get
         {
            return this.currentTransaction;
         }
      }

      internal abstract bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet);
   }
}
