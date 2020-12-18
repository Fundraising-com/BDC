using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    public partial class ImageViewer : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            try {
                switch (imgType) {
                    case "1"://logo
                        QSPForm.Business.LogoSystem lSys = new QSPForm.Business.LogoSystem();
                        QSPForm.Common.DataDef.LogoTable ltbl = lSys.SelectOne(Convert.ToInt32(imgID));
                        //img.Src = QSPForm.Common.QSPFormConfiguration.LogoImagePath + this.imgID +"."+ ltbl.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_FILE_EXTENSION].ToString();
                        img.Src = QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath + this.imgID + "." + QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;//ltbl.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_FILE_EXTENSION].ToString();
                        break;
                    case "2"://promo_logo
                        QSPForm.Business.Promo_LogoSystem pSys = new QSPForm.Business.Promo_LogoSystem();
                        QSPForm.Common.DataDef.Promo_LogoTable ptbl = pSys.SelectOne(Convert.ToInt32(imgID));
                        img.Src = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath + this.imgID + "." + QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;//ptbl.Rows[0][QSPForm.Common.DataDef.Promo_LogoTable.FLD_FILE_EXTENSION].ToString();
                        break;
                    default:
                        throw new Exception("This action is not available");
                }

                //calculate size ratio to fit on the screen
                /*
                    double ratio;
                    double width = Convert.ToDouble(img.Width.ToString());
                    double height = Convert.ToDouble(img.Height.ToString());
                    if (width > 800)
                    {
                        ratio = width / 800.0;
                        width = width / ratio;
                        height = height / ratio;
                    }

                    if (height > 600)
                    {
                        ratio = width / 600.0;
                        width = width / ratio;
                        height = height / ratio;
                    }

                    img.Width = Convert.ToInt32(width);
                    img.Height = Convert.ToInt32(height);
                */

            }
            catch (Exception ex) {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                img.Visible = false;
            }
        }

        private string imgID {
            get { return Request.QueryString["imgID"].ToString(); }
        }

        private string imgType {
            get { return Request.QueryString["imgType"].ToString(); }
        }
    } 
}