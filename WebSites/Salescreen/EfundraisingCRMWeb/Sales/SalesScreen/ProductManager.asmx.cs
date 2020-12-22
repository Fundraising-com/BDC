using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AjaxControlToolkit;
using efundraising.EFundraisingCRM.Linq;

namespace EFundraisingCRMWeb.Sales.SalesScreen
{
    /// <summary>
    /// Summary description for ProductManager
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProductManager : System.Web.Services.WebService
    {

        // Member variables
        private static XmlDocument _document;
        private static Regex _inputValidationRegex;
        private static object _lock = new object();

        // we make these public statics just so we can call them from externally for the
        // page method call
        public static XmlDocument Document
        {
            get
            {
                lock (_lock)
                {
                    if (_document == null)
                    {
                        // Read XML data from disk
                        _document = new XmlDocument();
                        _document.Load(HttpContext.Current.Server.MapPath("~/App_Data/CarsService.xml"));
                    }
                }
                return _document;
            }
        }

        public static string[] Hierarchy
        {
            get { return new string[] { "make", "model" }; }
        }

        public static Regex InputValidationRegex
        {
            get
            {
                lock (_lock)
                {
                    if (null == _inputValidationRegex)
                    {
                        _inputValidationRegex = new Regex("^[0-9a-zA-Z \\(\\)]*$");
                    }
                }
                return _inputValidationRegex;
            }
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownContents(string knownCategoryValues, string category)
           {


               StringDictionary knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
               // Validate input
               string val = "";
               foreach (string v in knownCategoryValuesDictionary.Values)
               {
                   val = v;
               }

               int catalogID = 132;//112
               QSPFulfillmentDataContext db = null;
               if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
               {
                   catalogID = 132;
                   db = new QSPFulfillmentDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString);
               }
               else
               {
                   db = new QSPFulfillmentDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString);
               }
               
               List<CascadingDropDownNameValue> result = new List<CascadingDropDownNameValue>();

               if (category == "Category")
               {
                                      
                  //fill category
                   var categories = from p in db.catalogItemCategories
                             where p.catalog_id == catalogID
                             select p;



                   //query asked for catalogItemCat so the result receives catalogItemCat objects

                 
                   foreach (catalogItemCategory cat in categories){
                       string name = cat.catalog_item_category_name;
                       string value = cat.catalog_item_category_id.ToString();

                       if (name != "EFR WS" && name != "Lollipops" && name != "3393")
                       {

                           result.Add(new CascadingDropDownNameValue(name, value, false));
                       }
                    
                   }

               }
               else if (category == "Items")
               {
                   //result.Add(new CascadingDropDownNameValue("a", "a", false));

                   //fill items
                   
                   var items = from c in db.catalogItemCategoryCatalogItems
                               where c.catalog_item_category_id == Convert.ToInt32(val)
                               select new
                               {
                                   c.catalog_item_category_id,
                                   c.catalogItem.catalog_item_name,
                                   c.catalogItem.catalog_item_id
                               };

                   
                   //query asked for catalogItemCat so the result receives catalogItemCat objects
                   
                   foreach (var cat in items)
                   {
                       
                       string name = cat.catalog_item_name;
                       string value = cat.catalog_item_id.ToString();

                       result.Add(new CascadingDropDownNameValue(name, value, false));

                   }
               }
               else if (category == "Profit")
               {
                   result.Add(new CascadingDropDownNameValue("40%", "0.400", false));
                   result.Add(new CascadingDropDownNameValue("45%", "0.450", false));
                   result.Add(new CascadingDropDownNameValue("50%", "0.500", false));
                   result.Add(new CascadingDropDownNameValue("60%", "0.600", false));
               }
               else if (category == "Country")
               {
                   result.Add(new CascadingDropDownNameValue("US", "US", false));
                   result.Add(new CascadingDropDownNameValue("Canada", "CA", false));
               }   


             return result.ToArray();
 


                    
            
        
               
        
           }


    }
}
