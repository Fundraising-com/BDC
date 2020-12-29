using System;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.eFundraisingStore.DataAccess;

namespace GA.BDC.Core.eFundraisingStore
{
    public class HomePageSpecialProducts
    {

        private int _package_id;
        private int _package_category_id;
        private int _display_order;
        private string _name;
        private string _page_name;

        public HomePageSpecialProducts() : this(int.MinValue) { }
        public HomePageSpecialProducts(int package_id) : this(package_id, int.MinValue) { }
        public HomePageSpecialProducts(int package_id, int package_category_id) : this(package_id, package_category_id, int.MinValue) { }
        public HomePageSpecialProducts(int package_id, int package_category_id, int display_order) : this(package_id, package_category_id, display_order, null) { }
        public HomePageSpecialProducts(int package_id, int package_category_id, int display_order, string name) : this(package_id, package_category_id, display_order, name, null) { }
        public HomePageSpecialProducts(int package_id, int package_category_id, int display_order, string name, string page_name) 
        {
            _package_id = package_id;
            _package_category_id = package_category_id;
            _display_order = display_order;
            _name = name;
            _page_name = page_name;

        }


        #region Properties
        public int PackageId
        {
            set { _package_id = value; }
            get { return _package_id; }
        }

        public int PackageCategoryId
        {
            set { _package_category_id = value; }
            get { return _package_category_id; }
        }

        public int DisplayOrder
        {
            set { _display_order = value; }
            get { return _display_order; }
        }


        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public string PageName
        {
            set { _page_name = value; }
            get { return _page_name; }
        }

       #endregion


        public static List<HomePageSpecialProducts> GetProductsListLinks(int catid)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetProductsListLinks(catid);
        }


    }
}
