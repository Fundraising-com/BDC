using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using efundraising.eFundraisingStore;
using System.Data.SqlClient;
using System.Data.Sql;
using efundraising.Core;

namespace AdminSection.DataAccess
{
    public class _efundraisingStoreInterface
    {
        protected string connectionString;
        private PackageCollection childrenPackages;
        private ProductCollection childrenProducts = null;
        private short packageId;
        private short productId;


        public _efundraisingStoreInterface(){
           connectionString = efundraising.Configuration.ApplicationSettings.GetConfig()["EFundraisingStore.SqlConnection.Release", "connectionString"];
        
        }

        public PackageCollection GetPackagesRoot(int packageRootID)
        {
          
            PackageCollection packages = null;
            
            try
            {
                string storedProcName = "efrstore_get_packages_root";

                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                
                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);



                if (dt != null)
                {
                    packages = new PackageCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            
                            Package package = LoadPackage(dt.Rows[i]);
                            if (package.PackageId == packageRootID || packageRootID == int.MinValue)
                            {
                                //packages = GetPackagesByParentPackageID(package.PackageId);
                                package.PackageDescription = GetPackageDescByID(package.PackageId);
                                packageId = package.PackageId;
                                LoadChildrenPackages();
                                LoadProducts();
                                packages.Add(package);


                            }
                          
                        }
                        catch (Exception ex)
                        {
                           //throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
               /* if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }*/
            }
            return packages;
        }


        //private PackageDesc GetPackageDescByPageName(
        public PackageDesc GetPackageDescByID(int id)
        {
           // id = 128;    
            PackageDesc packageDesc = null;
            
            try
            {

                SqlConnection conn = new SqlConnection(connectionString);

                
                
                //
               /* string storedProcName = "select * from package_desc";
                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.Text;

                */

                string storedProcName = "efrstore_get_package_desc_by_id";
                


                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                

          

                //cmd.Parameters.Add("@Package_id", SqlDbType.Int).Value = id;

                SqlParameter p1 = new SqlParameter("@package_id", SqlDbType.Int, 4, "package_id");
                p1.Value = id;
                
                cmd.Parameters.Add(p1); 
                
                
                conn.Open();
                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        packageDesc = LoadPackageDesc(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                       // throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }catch(Exception x){
                int a = 1;
            }

            finally
            {
              /*  if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }*/
            }
            return packageDesc;
        }


        public PackageCollection GetPackagesByParentPackageID(int id)
        {
         
            PackageCollection packages = null;


            try
            {

                string storedProcName = "efrstore_get_packages_by_parent_package_id";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@PackageRootID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Parent_package_id", id));

                conn.Open();


                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

       
                if (dt != null)
                {
                    packages = new PackageCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            Package package = LoadPackage(dt.Rows[i]);

                            package.PackageDescription = GetPackageDescByID(package.PackageId);
                            LoadProducts();
                            packages.Add(package);
                        }
                        catch (Exception ex)
                        {
                           // throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
              /*  if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }*/
            }
            return packages;
        }


        public ProductCollection GetProductsByPackageID(int id)
        {
        
            ProductCollection products = null;

         
            try
            {
                string storedProcName = "efrstore_get_products_by_package_id";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@PackageRootID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Package_id", id));

                conn.Open();


                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                

                if (dt != null && dt.Rows.Count > 0)
                {

                    products = new ProductCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            bool enabled = Convert.ToBoolean(dt.Rows[i]["Enabled"]);
                            if (enabled)
                            {
                                Product product = LoadProduct(dt.Rows[i]);
                                LoadChildrenProducts();
                                product.ProductDescription = GetProductDescByID(product.ProductId);
                                products.Add(product);
                            }
                        }
                        catch (Exception ex)
                        {
                           // throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }


            }
            finally
            {
             /*   if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }*/
            }
            return products;
        }



        public ProductCollection GetProductsByParentId(int id)
        {
          
            ProductCollection products = null;


            try
            {

                string storedProcName = "efrstore_get_products_by_parent_id";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@PackageRootID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Product_id", id));

                conn.Open();


                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                


                if (dt != null)
                {
                    products = new ProductCollection();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            Product p = LoadProduct(dt.Rows[i]);
                            p.ProductDescription = GetProductDescByID(p.ProductId);
                            products.Add(p);
                        }
                        catch (Exception ex)
                        {
                          //  throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                    
                }


            }
            finally
            {
               /* if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }*/
            }
            return products;
        }


        public ProductDesc GetProductDescByID(int id)
        {
           
            ProductDesc productDesc = null;

       

            try
            {
                string storedProcName = "efrstore_get_product_desc_by_id";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@PackageRootID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Product_id", id));

                conn.Open();


                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        productDesc = LoadProductDesc(dt.Rows[0]);
                    }
                    catch (Exception ex)
                    {
                       // throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }


            }
            finally
            {
               /* if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }*/
            }
            return productDesc;
        }






        private Package LoadPackage(DataRow row)
        {
            Package package = new Package();

            // Store database values into our business object
            package.PackageId = DBValue.ToInt16(row["package_id"]);
            package.ParentPackageId = DBValue.ToInt16(row["parent_package_id"]);
            package.Name = DBValue.ToString(row["name"]);
            package.ProfitPercentage = DBValue.ToInt16(row["profit_percentage"]);
            package.Enabled = DBValue.ToInt16(row["enabled"]);
            package.CreateDate = DBValue.ToDateTime(row["create_date"]);

            // return the filled object
            return package;
        }

        private PackageDesc LoadPackageDesc(DataRow row)
        {
            PackageDesc packageDesc = new PackageDesc();

            // Store database values into our business object
            packageDesc.PackageId = DBValue.ToInt32(row["package_id"]);
            packageDesc.CultureCode = DBValue.ToString(row["culture_code"]);
            packageDesc.TemplateId = DBValue.ToInt32(row["template_id"]);
            packageDesc.Name = DBValue.ToString(row["name"]);
            packageDesc.ShortDesc = DBValue.ToString(row["short_desc"]);
            packageDesc.LongDesc = DBValue.ToString(row["long_desc"]);
            packageDesc.ExtraDesc = DBValue.ToString(row["extra_desc"]);
            packageDesc.PageName = DBValue.ToString(row["page_name"]);
            packageDesc.PageTitle = DBValue.ToString(row["page_title"]);
            packageDesc.ImageName = DBValue.ToString(row["image_name"]);
            packageDesc.ImageAltText = DBValue.ToString(row["image_alt_text"]);
            packageDesc.DisplayOrder = DBValue.ToInt32(row["display_order"]);
            packageDesc.Enabled = DBValue.ToInt16(row["enabled"]);
            packageDesc.Configuration = DBValue.ToString(row["configuration"]);
            packageDesc.CreateDate = DBValue.ToDateTime(row["create_date"]);

            // return the filled object
            return packageDesc;
        }

        public void LoadChildrenPackages()
        {
            try
            {
                //packageDescription = PackageDesc.GetPackageDescByID(packageId);
            }
            catch { }
            childrenPackages = GetPackagesByParentPackageID(packageId);
            foreach (Package p in childrenPackages)
            {
                System.Diagnostics.Debug.Write(p.Name);
                LoadChildrenPackage(p);
            }
        }

        private void LoadChildrenPackage(Package package)
        {
            try
            {
                //package.packageDescription = PackageDesc.GetPackageDescByID(package.packageId);
            }
            catch { }

            package.ChildrenPackages = GetPackagesByParentPackageID(package.PackageId);
            foreach (Package p in package.ChildrenPackages)
            {
                LoadChildrenPackage(p);
            }
        }

        public void LoadProducts()
        {
            ProductCollection products;
            products = GetProductsByPackageID(this.packageId);
        }

        private Product LoadProduct(DataRow row)
        {
            Product product = new Product();

            // Store database values into our business object
            product.ProductId = DBValue.ToInt32(row["product_id"]);
            product.ParentProductId = DBValue.ToInt32(row["parent_product_id"]);
            product.ScratchBookId = DBValue.ToInt32(row["scratch_book_id"]);
            product.Name = DBValue.ToString(row["name"]);
            product.RaisingPotential = DBValue.ToDecimal(row["raising_potential"]);
            product.ProductCode = DBValue.ToString(row["product_code"]);
            product.Enabled = DBValue.ToInt16(row["enabled"]);
            product.IsInner = DBValue.ToInt16(row["is_inner"]);
            product.CreateDate = DBValue.ToDateTime(row["create_date"]);

            // return the filled object
            return product;
        }

        public void LoadChildrenProducts()
        {
            try
            {
                //productDescription = ProductDesc.GetProductDescByID(productId);
            }
            catch { }
            childrenProducts = GetProductsByParentId(productId);
            foreach (Product p in childrenProducts)
            {
                LoadChildrenProduct(p);
            }

        }
        private void LoadChildrenProduct(Product product)
        {
            try
            {
                //product.productDescription = ProductDesc.GetProductDescByID(product.productId);
            }
            catch { }

            product.ChildrenProducts = GetProductsByParentId(product.ProductId);
            foreach (Product p in product.ChildrenProducts)
            {
                LoadChildrenProduct(p);
            }
        }


        private ProductDesc LoadProductDesc(DataRow row)
        {
            ProductDesc productDesc = new ProductDesc();

            // Store database values into our business object
            productDesc.ProductId = DBValue.ToInt32(row["product_id"]);
            productDesc.CultureCode = DBValue.ToString(row["culture_code"]);
            productDesc.TemplateId = DBValue.ToInt32(row["template_id"]);
            productDesc.Name = DBValue.ToString(row["name"]);
            productDesc.ShortDesc = DBValue.ToString(row["short_desc"]);
            productDesc.LongDesc = DBValue.ToString(row["long_desc"]);
            productDesc.ExtraDesc = DBValue.ToString(row["extra_desc"]);
            productDesc.PageName = DBValue.ToString(row["page_name"]);
            productDesc.PageTitle = DBValue.ToString(row["page_title"]);
            productDesc.ImageName = DBValue.ToString(row["image_name"]);
            productDesc.ImageAltText = DBValue.ToString(row["image_alt_text"]);
            productDesc.DisplayOrder = DBValue.ToInt32(row["display_order"]);
            productDesc.Enabled = DBValue.ToInt16(row["enabled"]);
            productDesc.Configuration = DBValue.ToString(row["configuration"]);
            productDesc.CreateDate = DBValue.ToDateTime(row["create_date"]);

            // return the filled object
            return productDesc;
        }
       

	


    }
}
