using System;
using System.Data;
using Business.Objects;
using Common;

namespace Business.Rules
{
	/// <summary>
	/// Summary description for Rules.
	/// </summary>
	public abstract class RulesBase
	{
		private Message messageManager;
		private Transaction oCurrentTransaction = null;

		public RulesBase(Message messageManager)
		{
			this.messageManager = messageManager;
		}

		protected Message CurrentMessageManager 
		{
			get 
			{
				return this.messageManager;
			}
		}

		public Transaction CurrentTransaction 
		{
			get 
			{
				return oCurrentTransaction;
			}
			set 
			{
				oCurrentTransaction = value;
			}
		}

		public virtual bool Validate(BusinessSystem entity, DataRowState state) 
		{
			return true;
		}

		public virtual bool Validate(DataRow row)
		{
			return true;
		}

		public virtual bool ValidateDelete(DataRow row)
		{
			return true;
		}
	}
}
