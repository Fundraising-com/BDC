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
    public partial class ProgramList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To Locate a program, you can enter a criteria and/or use the Program Type filter.";
            this.Header.SectionText = "Program:";
            this.Header.PageText = "Program List";
            this.Header.IconImageVisiblilty = false;
            this.Header.IconImage = "~/images/Spacer.gif";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlProgramList.SearchCriteria.Items.Clear();
            this.ctrlProgramList.SearchCriteria.Items.Add(new ListItem("Program Name", "1", true));
            this.ctrlProgramList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlProgramList.SearchName.Text = "Program Name";
            this.ctrlProgramList.SearchName = abcNote;
        }
    }  
}