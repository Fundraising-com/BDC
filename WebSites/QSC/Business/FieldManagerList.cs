using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;

namespace Business
{
	/// <summary>
	/// Summary description for FieldManagerList.
	/// </summary>
	public class FieldManagerList : QBusinessObject
	{
		#region Item Declarations
		private DAL.FieldManagerDataAccess aFMdata;

		#endregion Item Declarations

		#region Constructors
		public FieldManagerList()
		{
			aFMdata = new DAL.FieldManagerDataAccess();
		}
		#endregion Constructors

		#region ValidateAndSave
		///<summary>here for the compiler, not really needed</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				return this.Save();
			}
			else
			{
				return false;
			}
		}

		private bool Validate()
		{
			return false;
		}

		private bool Save()
		{
			return false;
		}

		#endregion ValidateAndSave

		public System.Collections.ArrayList GetFMDropDownData(int mode, string FMIDselected)
		{			
			if(mode == 1)
			{
				return GetFMDropDownData_Format1(FMIDselected);
			}
			else if(mode == 2)
			{
				return GetFMDropDownData_Format2(FMIDselected);
			}
			else if(mode == 4)
			{
				return GetFMDropDownData_Format4(FMIDselected);
			}
			else
			{
				throw new ArgumentOutOfRangeException("FMList Mode");
			}
		}

		///<summary>FM List - Format 1 - Current FMs</summary>
		///<param name="FMIDselected">4 digit- selected FMID</param>
		///<returns>an ArrayList of Common.QSPListItem(s)</returns>
		public System.Collections.ArrayList GetFMDropDownData_Format1(string FMIDselected)
		{
			System.Collections.ArrayList AL = new System.Collections.ArrayList(75);
			DataTable DT = aFMdata.GetFM_List(1);//mode = 1;
			Common.QSPListItem QLI;
			bool blFound = false;
			if((FMIDselected == null)||(FMIDselected.Trim() == ""))
			{
				FMIDselected = "";
			}

			//blank value
			QLI = new Common.QSPListItem();
			QLI.Value = "";
			QLI.Text  = "";
			if(FMIDselected == "")
			{
				QLI.Selected = true;
				blFound = true;
			}
			AL.Add(QLI);

			string MI;
			for(int i = 0; i < DT.Rows.Count; i++)
			{
				QLI = new Common.QSPListItem();

				QLI.Value = Convert.ToString(DT.Rows[i]["FMID"]);
				QLI.Text  = Convert.ToString(DT.Rows[i]["LastName"]);
				QLI.Text += ", ";
				QLI.Text += Convert.ToString(DT.Rows[i]["FirstName"]);

				MI = "" + Convert.ToString(DT.Rows[i]["MiddleInitial"]);
				if (MI != "")
				{
					QLI.Text += " " + MI + ".";
				}

				if((QLI.Value == FMIDselected)&&(blFound == false))
				{
					QLI.Selected = true;
					blFound = true;
				}
				else
				{
					QLI.Selected = false;
				}

				AL.Add(QLI);
			}

			if(blFound == false)
			{
				//if we still havent found the fm
				//add this fmid to the list
				QLI = new Common.QSPListItem();
				QLI.Value = FMIDselected;
				QLI.Text  = FMIDselected + " : a name can't be found to match this FMID";
				QLI.Selected = true;
				AL.Add(QLI);
				blFound = true;
			}
			return AL;
		}


		///<summary>FM List - Format 2 - Current FMs union Deleted FMs</summary>
		///<param name="FMIDselected">4 digit- selected FMID</param>
		///<returns>an ArrayList of Common.QSPListItem(s)</returns>
		public System.Collections.ArrayList GetFMDropDownData_Format2(string FMIDselected)
		{
			System.Collections.ArrayList AL = new System.Collections.ArrayList(75);
			DataTable DT = aFMdata.GetFM_List(2);//mode = 2;
			Common.QSPListItem QLI;
			bool blFound  = false;
			bool blSwitch = true;
			if((FMIDselected == null)||(FMIDselected.Trim() == ""))
			{
				FMIDselected = "";
			}

			//blank value
			QLI = new Common.QSPListItem();
			QLI.Value = "";
			QLI.Text  = "";
			if(FMIDselected == "")
			{
				QLI.Selected = true;
				blFound = true;
			}
			AL.Add(QLI);
			//note to user
			QLI = new Common.QSPListItem();
			QLI.Value = "";
			QLI.Text  = "Current FMs:";
			AL.Add(QLI);

			string MI;
			for(int i = 0; i < DT.Rows.Count; i++)
			{
				//relies on getting info in order of DeletedTF ASC
				if((blSwitch == true)&&(Convert.ToBoolean(DT.Rows[i]["DeletedTF"]) == false))
				{
					//blank value
					QLI = new Common.QSPListItem();
					QLI.Value = "";
					QLI.Text  = "";
					AL.Add(QLI);
					//note to user
					QLI = new Common.QSPListItem();
					QLI.Value = "";
					QLI.Text  = "Former FMs:";
					AL.Add(QLI);
					blSwitch = false;
				}
				
				QLI = new Common.QSPListItem();

				QLI.Value = Convert.ToString(DT.Rows[i]["FMID"]);
				QLI.Text  = Convert.ToString(DT.Rows[i]["LastName"]);
				QLI.Text += ", ";
				QLI.Text += Convert.ToString(DT.Rows[i]["FirstName"]);

				MI = "" + Convert.ToString(DT.Rows[i]["MiddleInitial"]);
				if (MI != "")
				{
					QLI.Text += " " + MI + ".";
				}

				if((QLI.Value == FMIDselected)&&(blFound == false))
				{
					QLI.Selected = true;
					blFound = true;
				}
				else
				{
					QLI.Selected = false;
				}

				AL.Add(QLI);
			}

			if(blFound == false)
			{
				//if we still havent found the fm
				//add this fmid to the list
				QLI = new Common.QSPListItem();
				QLI.Value = FMIDselected;
				QLI.Text  = FMIDselected + " : a name can't be found to match this FMID";
				QLI.Selected = true;
				AL.Add(QLI);
				blFound = true;
			}

			return AL;
		}
		

		//FM List - Format 3 - is reserved, and is not used in a DDL

		///<summary>FM List - Format 4 - Current FMs union Selected FM</summary>
		///<param name="FMIDselected">4 digit- selected FMID</param>
		///<returns>an ArrayList of Common.QSPListItem(s)</returns>
		public System.Collections.ArrayList GetFMDropDownData_Format4(string FMIDselected)
		{
			System.Collections.ArrayList AL = new System.Collections.ArrayList(75);
			DataTable DT = aFMdata.GetFM_List(4);//mode = 4;
			Common.QSPListItem QLI;
			bool blFound = false;
			if((FMIDselected == null)||(FMIDselected.Trim() == ""))
			{
				FMIDselected = "";
			}

			//blank value
			QLI = new Common.QSPListItem();
			QLI.Value = "";
			QLI.Text  = "";
			if(FMIDselected == "")
			{
				QLI.Selected = true;
				blFound = true;
			}
			AL.Add(QLI);

			string MI;
			for(int i = 0; i < DT.Rows.Count; i++)
			{
				QLI = new Common.QSPListItem();

				QLI.Value = Convert.ToString(DT.Rows[i]["FMID"]);
				QLI.Text  = Convert.ToString(DT.Rows[i]["LastName"]);
				QLI.Text += ", ";
				QLI.Text += Convert.ToString(DT.Rows[i]["FirstName"]);

				MI = "" + Convert.ToString(DT.Rows[i]["MiddleInitial"]);
				if (MI != "")
				{
					QLI.Text += " " + MI + ".";
				}

				if((QLI.Value == FMIDselected)&&(blFound == false))
				{
					QLI.Selected = true;
					blFound = true;
				}
				else
				{
					QLI.Selected = false;
				}

				AL.Add(QLI);
			}

			if(blFound == false)
			{
				//the FMID wasn't in a list of current FMs
				//grab them from DB even if DeltedTF = 1
				//and add to the list (for historical reasons)
				DataTable DT2 = this.aFMdata.Select(FMIDselected);
				for(int j = 0; j < DT2.Rows.Count; j++)
				{
					#region add in new FM(s)
					QLI = new Common.QSPListItem();

					QLI.Value = Convert.ToString(DT2.Rows[j]["FMID"]);
					QLI.Text  = Convert.ToString(DT2.Rows[j]["LastName"]);
					QLI.Text += ", ";
					QLI.Text += Convert.ToString(DT2.Rows[j]["FirstName"]);

					MI = "" + Convert.ToString(DT.Rows[j]["MiddleInitial"]);
					if (MI != "")
					{
						QLI.Text += " " + MI + ".";
					}
					QLI.Text += " - no longer a current FM ";

					if((QLI.Value == FMIDselected)&&(blFound == false))
					{
						QLI.Selected = true;
						blFound = true;
					}
					else
					{
						QLI.Selected = false;
					}

					AL.Add(QLI);
					#endregion add in new FM(s)
				}
			}

			if(blFound == false)
			{
				//if we still havent found the fm
				//add this fmid to the list
				QLI = new Common.QSPListItem();
				QLI.Value = FMIDselected;
				QLI.Text  = FMIDselected + " : a name can't be found to match this FMID";
				QLI.Selected = true;
				AL.Add(QLI);
				blFound = true;
			}

			return AL;
		}

	}
}
