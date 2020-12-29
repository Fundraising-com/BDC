using System;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.eFundraisingStore.DataAccess;


namespace GA.BDC.Core.eFundraisingStore
{
    public class HomePageSpecialProductsHeader
    {

        private int _catagoryid;
        private string _imageurl;
        private string _catagoryTitle;
        private string _catagorydescription;
        private string _producturl;
		


        public HomePageSpecialProductsHeader() : this(int.MinValue) { }
        public HomePageSpecialProductsHeader(int catagoryid) : this(catagoryid, null) { }
        public HomePageSpecialProductsHeader(int catagoryid, string imageurl) : this(catagoryid, imageurl,null) { }
        public HomePageSpecialProductsHeader(int catagoryid, string imageurl, string catagoryTitle) : this(catagoryid, imageurl, catagoryTitle, null){}
        public HomePageSpecialProductsHeader(int catagoryid, string imageurl, string catagoryTitle, string catagorydescription) : this(catagoryid, imageurl, catagoryTitle, catagorydescription, null) { }
        public HomePageSpecialProductsHeader(int catagoryid, string imageurl, string catagoryTitle, string catagorydescription, string producturl) 
        {
            _catagoryid = catagoryid;
            _imageurl = imageurl;
            _catagoryTitle = catagoryTitle;
            _catagorydescription = catagorydescription;
            _producturl = producturl;
        }



        #region Properties
        public int Catagoryid
        {
            set { _catagoryid = value; }
            get { return _catagoryid; }
        }
        
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }

        public string ProductUrl
        {
            set { _producturl = value; }
            get { return _producturl; }
        }

        public string CatagoryTitle
        {
            set { _catagoryTitle = value; }
            get { return _catagoryTitle; }
        }

        public string CatagoryDescription
        {
            set { _catagorydescription = value; }
            get { return _catagorydescription; }
        }
        #endregion

        public static List<HomePageSpecialProductsHeader> GetProductsListHeader()
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetProductsListHeader();
        }

       
        //efundraising.eFundraisingStore.DataAccess.  .eFundStoreDatabase dbo = efundraising.eFundraisingStore.DataAccess.eFundStoreDatabase();
        //List<HomePageSpecialProductsHeader> header_list = HomePageSpecialProductsHeader.GetProductsListHeader();
            


    }
}
