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
    public partial class MDRSchoolSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.IconImage = "~/images/title_mdr_school_selector.gif";
            this.Header.PageText = "MDR School Selector";
            this.Header.SectionText = "MDR Selector";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlMDRSchoolSelector.SearchCriteria.Items.Clear();
            this.ctrlMDRSchoolSelector.SearchCriteria.Items.Add(new ListItem("City", "3"));
            this.ctrlMDRSchoolSelector.SearchCriteria.Items.Add(new ListItem("MDR PID", "2"));
            this.ctrlMDRSchoolSelector.SearchCriteria.Items.Add(new ListItem("MDR School Name", "1", true));
            this.ctrlMDRSchoolSelector.SearchCriteria.Items.Add(new ListItem("Zip Code", "4"));
        }
    } 
}