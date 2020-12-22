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

namespace QSP.OrderExpress.Web.UserControls {
    public partial class HeaderControl : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            //if (this.IconImage.Trim() == String.Empty)
            //    imgIcon.Visible = false;
        }

        public String InstructionText {
            get {
                return lblInstruction.Text;
            }
            set {
                lblInstruction.Text = value;
            }
        }

        public string SectionText {
            get {
                return lblSectionTitle.Text;
            }
            set {
                lblSectionTitle.Text = value;
            }
        }

        public string PageText {
            get {
                return lblPageTitle.Text;
            }
            set {
                lblPageTitle.Text = value;
            }
        }

        public string IconImage {
            get {
                return imgIcon.ImageUrl;
            }
            set {
                imgIcon.ImageUrl = value;
            }
        }

        public bool IconImageVisiblilty {
            get {
                return imgIcon.Visible;
            }
            set {
                imgIcon.Visible = value;
            }
        }
    } 
}