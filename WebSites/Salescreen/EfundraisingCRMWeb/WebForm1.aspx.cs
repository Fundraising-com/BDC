using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using EfundraisingCRM.AddressHygiene;

namespace EfundraisingCRM
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        //EFundraisingCRMWeb.Components.User.AddressHygiene.AddressHygiene AddressHygiene1;

        protected void Page_Load(object sender, EventArgs e)
        {
/*
            Trace.Write("aaaaaaaaaaaaaaaaaaaaaaaaaa");
            AddressHygiene1.OutputAddress += new EFundraisingCRMWeb.Components.User.AddressHygiene.OutputAddressEventHandler(AddressSelected);
            
          Address address = new Address();*/
        /*    address.Address1 = "23 Chapman Rd.";
            address.Address2 = "";
            address.City = "Florence";
            address.County = "";
            address.Region = "IN";
            address.PostCode = "4702";
            address.PostCode2 = "";
            address.Country = "US";
            *//*
            address.Address1 = "68 vagnes-martin";
            address.Address2 = "";
            address.City = "Terrebonne";
            address.County = "";
            address.Region = "QC";
            address.PostCode = "j6y0cf";
            address.PostCode2 = "";
            address.Country = "CA";

           bool enableSuggestionList = true;
           bool addressChanged = false;
           if (AddressHygiene1.DoAddressHygieneNoDelagate(address, enableSuggestionList, ref addressChanged, true))
           {
               Business.com.ses.ws.AddressHygiene.OutputAddress outputAddress = AddressHygiene1.OutAddress;



                   if (outputAddress.SuggestionListInformation.Error != SuggestionListError.None)
                   {
                       AddressHygiene1.Visible = true;
                       return;
                   }
                   else if (outputAddress.Fault != Fault.NoError)
                   {
                       AddressHygiene1.Visible = true;
                       ZIPlbl.Text = outputAddress.PostCode;
                       AddressLabel.Text = outputAddress.Address1;
                   }
                   else if (addressChanged)
                   {
                       AddressHygiene1.Visible = true;
                   }
                   else
                   {
                       //All was good
                   }
                   
               
               
           }
        }*/


      /*  private void AddressSelected(object Sender, AddressHygiene.OutputAddress OutputAddress, bool ChangeStatus)
        {
        //Check if there was an error with the address that came back
	       if (OutputAddress.Fault != Fault.NoError)
	       {
		      AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
              AddressHygieneStatusLabel.Text = "Error: " + AddressHygiene1.GetErrorFromFault(OutputAddress.Fault.ToString());
	       }
	       //Check if there was a Suggestion List error
          else if (OutputAddress.SuggestionListInformation.Error != SuggestionListError.None && OutputAddress.SuggestionListInformation.Error != SuggestionListError.MoreInformationRequired)
	      {
		     AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
	         AddressHygieneStatusLabel.Text = "Error: " + OutputAddress.SuggestionListInformation.Error.ToString();
	      }
	      else
	      {
		     string newStreet1 = OutputAddress.Address1;
             string newStreet2 = OutputAddress.Address2;
             string newCity = OutputAddress.City;
             string newCounty = OutputAddress.County;
             string newPostalCode = OutputAddress.PostCode;
             string newPostalCode2 = OutputAddress.PostCode2;
             string newProvince = OutputAddress.Region;
             string newCountry = OutputAddress.Country;



             ZIPlbl.Text = newPostalCode;
             AddressLabel.Text = newStreet1;



	
	        //Display Status 
		        if (ChangeStatus)
                {
			        AddressHygieneStatusLabel.Text = "Address Updated";
		        }else
                {
			        AddressHygieneStatusLabel.Text = "Address Validated";
		        }
	        }*/
        }


    }
}
