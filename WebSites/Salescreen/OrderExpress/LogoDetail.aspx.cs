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
    public partial class LogoDetail : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            this.LabelMessage = this.Master.LabelMessage1;

            if (!IsPostBack) {
                SetHeader();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "<br />Complete the logo information below. To upload an image, it must be saved on your hard drive. Click Browse button, choose file, highlight image description and click Open button. The image name will populate the text box. Then click Upload button and Save button to store the image in Order Express. The logo will be available in the Logo List within a few minutes.<br /><br />";
            this.Header.SectionText = "Logo";
            this.Header.PageText = "Logo Detail";
            this.Header.IconImageVisiblilty = false;


        }
    } 
}