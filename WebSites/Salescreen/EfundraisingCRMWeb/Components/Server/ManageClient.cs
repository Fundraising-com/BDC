using System;
using efundraising.EFundraisingCRM;
using System.Data;
using System.Collections;

namespace EFundraisingCRMWeb.Components.Server
{
	/// <summary>
	/// Summary description for ManageClient.
	/// </summary>
	public class ManageClient
	{
		public ManageClient()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static Client GetClient(int clientid, string clientSequenceCode)
		{
			if (clientid == int.MinValue || clientSequenceCode == null || clientSequenceCode == string.Empty)
				return null;
			Client cl = Client.GetClientByID(clientid, clientSequenceCode);
			if (cl != null)
			{
				cl.ClientBillingAddress = ClientAddress.GetClientAddressByIdSequenceAddressType(cl.ClientId, cl.ClientSequenceCode, "bt");
				cl.ClientShippingAddress= ClientAddress.GetClientAddressByIdSequenceAddressType(cl.ClientId, cl.ClientSequenceCode, "st");
			}
			return cl;
		}
		public static bool InsertOrUpdateClientAddress(ClientAddress cl)
		{
			if (cl != null)
			{
				if (cl.AddressId == int.MinValue)
				{
					if (cl.ClientId != int.MinValue)
						cl.Insert();
				}
				else
					cl.Update();
			}
			return true;
		}

		public static string GetSalesProductClass(int saleId)
		{
            efundraising.EFundraisingCRM.SalesItem[] si = efundraising.EFundraisingCRM.SalesItem.GetSalesItemsBySaleID(saleId);
			//take the first item
			if (si.Length > 0)
			{
                efundraising.EFundraisingCRM.ScratchBook sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(si[0].ScratchBookId);
                efundraising.EFundraisingCRM.ProductClass pc = efundraising.EFundraisingCRM.ProductClass.GetProductClassById(sb.ProductClassId);
				return pc.Description;
			}
			else
			{
				return "";
			}
		
			
		}
	}

	public class ManageStudent
	{
		
		public static DataTable CreateDataTableStudentStructure()
		{			
			DataTable dt = new DataTable();
			dt.Columns.Add("participant_id", typeof(int));
			dt.Columns.Add("sales_id", typeof(int));
			dt.Columns.Add("client_id", typeof(int));
			dt.Columns.Add("first_name");
			dt.Columns.Add("last_name");
			dt.Columns.Add("IsNewParticipant", typeof(bool));
			return dt;
		}

		public static DataTable GetStudentsByClient(Client cl)
		{
			// Must be done in a stored procedure
			// Select *
			// From participant inner join sales_item on participant_id
			// inner join sales on sale_id
			// where client_id = cl.ClientId.

			DataTable dt = CreateDataTableStudentStructure();
			Participant[] p = Participant.GetParticipants();

			if (p != null && p.Length > 0)
			{
				ArrayList arStudent = ManageProduct.GetParticipantsByClient(cl);
				string studentIds = string.Empty;
				if (arStudent != null && arStudent.Count > 0)
				{
					for (int i=0; i < arStudent.Count; i++)
						studentIds += ", " + arStudent[i].ToString(); 

					// Retrieve all participants.
					DataRow row = null;
					for (int i=0;i<p.Length; i++)
					{
						row = dt.NewRow();
						row["first_name"] = p[i].FirstName;
						row["last_name"]  = p[i].LastName;
						row["participant_id"] = p[i].ParticipantId;
						row["client_id"]  = cl.ClientId;
						dt.Rows.Add(row);
					}

					// Retrieve only participants associated with the current client.
					DataTable result = dt.Clone();
					DataRow[] rows = dt.Select("participant_id in " + string.Format("({0})", studentIds.Substring(1)) );
					for (int i=0; i < rows.Length; i++)
						result.ImportRow(rows[i]);

					return result;
				}
				
			}
			return dt;
		}


		public static DataTable GetStudentsBySaleId(int saleId, int clientId)
		{
			DataTable dt = CreateDataTableStudentStructure();
			Hashtable hTb = Participant.GetParticipantBySalesId(saleId);
			foreach (object ob in hTb.Values)
			{
				Participant p = ob as Participant;
				if (p != null)
				{
					// Retrieve all participants.
					DataRow row = dt.NewRow();
					row["first_name"] = p.FirstName;
					row["last_name"]  = p.LastName;
					row["participant_id"] = p.ParticipantId;
					row["client_id"]  = clientId;
					dt.Rows.Add(row);					
				}
			}
			return dt;
		}

	
	}
}
