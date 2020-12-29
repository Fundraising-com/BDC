using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Controls
{
	/// <summary>
	/// Summary description for Partners.
	/// </summary>
	public class Partners : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox lstCultures;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboPartner;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel flipPanel;
		private GA.BDC.Core.Web.UI.UIControls.Config.PartnersID partnersID;
		private PageBuilder parent1;
		private ControlBuilder parent2;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdDelete;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Partners(PageBuilder _parent) {
			parent1 = _parent;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
		}

		public Partners(ControlBuilder _parent) {
			parent2 = _parent;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
		}

		public void FillUIControls() {
			if(parent1 != null) {
				parent1.FillUIControler(true);
			} else if(parent2 != null) {
				parent2.LoadPreview();
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public void ListPartner() {
			cboPartner.Items.Clear();
			string first = "";
			foreach(GA.BDC.Core.Web.UI.UIControls.Config.PartnerID p in partnersID.PartnerIdList) {
				if(first == "") {
					first = p.ID;
					foreach(GA.BDC.Core.Web.UI.UIControls.Config.Culture c in p.Cultures.CultureList) {
						lstCultures.Items.Add(c.ID);
					}
				}
				cboPartner.Items.Add(p.ID);
			}
			cboPartner.Text = first;
		}

		public void SetPartnersID(ref GA.BDC.Core.Web.UI.UIControls.Config.PartnersID _partnersID) {
			partnersID = _partnersID;
			ListPartner();
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.flipPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.lstCultures = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cboPartner = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.flipPanel);
			this.groupBox1.Controls.Add(this.splitter1);
			this.groupBox1.Controls.Add(this.lstCultures);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(584, 248);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// flipPanel
			// 
			this.flipPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flipPanel.Location = new System.Drawing.Point(86, 16);
			this.flipPanel.Name = "flipPanel";
			this.flipPanel.Size = new System.Drawing.Size(495, 229);
			this.flipPanel.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(83, 16);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 229);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// lstCultures
			// 
			this.lstCultures.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstCultures.Location = new System.Drawing.Point(3, 16);
			this.lstCultures.Name = "lstCultures";
			this.lstCultures.Size = new System.Drawing.Size(80, 225);
			this.lstCultures.TabIndex = 0;
			this.lstCultures.SelectedIndexChanged += new System.EventHandler(this.lstCultures_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cmdDelete);
			this.groupBox2.Controls.Add(this.cmdAdd);
			this.groupBox2.Controls.Add(this.cboPartner);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(584, 48);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			// 
			// cmdDelete
			// 
			this.cmdDelete.Location = new System.Drawing.Point(264, 16);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(56, 24);
			this.cmdDelete.TabIndex = 3;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(208, 16);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(56, 24);
			this.cmdAdd.TabIndex = 2;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// cboPartner
			// 
			this.cboPartner.Location = new System.Drawing.Point(80, 16);
			this.cboPartner.Name = "cboPartner";
			this.cboPartner.Size = new System.Drawing.Size(121, 21);
			this.cboPartner.TabIndex = 1;
			this.cboPartner.Text = "comboBox1";
			this.cboPartner.SelectedIndexChanged += new System.EventHandler(this.cboPartner_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Partner: ";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(584, 248);
			this.panel1.TabIndex = 2;
			// 
			// Partners
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox2);
			this.Name = "Partners";
			this.Size = new System.Drawing.Size(584, 296);
			this.Load += new System.EventHandler(this.Partners_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cboPartner_SelectedIndexChanged(object sender, System.EventArgs e) {
			foreach(GA.BDC.Core.Web.UI.UIControls.Config.PartnerID p in partnersID.PartnerIdList) {
				if(p.ID == cboPartner.SelectedItem.ToString()) {
					lstCultures.Items.Clear();
					foreach(GA.BDC.Core.Web.UI.UIControls.Config.Culture c in p.Cultures.CultureList) {
						lstCultures.Items.Add(c.ID);
					}
				}
			}
		}

		private void lstCultures_SelectedIndexChanged(object sender, System.EventArgs e) {
			foreach(GA.BDC.Core.Web.UI.UIControls.Config.PartnerID p in partnersID.PartnerIdList) {
				if(p.ID == cboPartner.SelectedItem.ToString()) {
					foreach(GA.BDC.Core.Web.UI.UIControls.Config.Culture c in p.Cultures.CultureList) {
						if(c.ID == lstCultures.SelectedItem.ToString()) {
							DS.DataSource ds = new DS.DataSource(this);
							ds.Data = c.Data;
							flipPanel.Controls.Clear();
							flipPanel.Controls.Add(ds);
							ds.Dock = DockStyle.Fill;
						}
					}
				}
			}
		}

		private void cmdAdd_Click(object sender, System.EventArgs e) {
			InputBox inputBox = new InputBox();
			inputBox.LblMessage.Text = "Insert new partner id";
			inputBox.ShowDialog();
			if(inputBox.Ok) {
				Config.PartnerID p = new Config.PartnerID();
				p.ID = inputBox.TxtInput.Text;
				BaseConfig.GlobalizerConfigs gcs = new BaseConfig.GlobalizerConfigs();
				gcs.LoadXML();
				BaseConfig.GlobalizerConfig gc = gcs.GetCurrentWorkingConfig();
				foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
					Config.Culture c = new Config.Culture();
					c.ID = sc.Name;
					c.Data.Source = "Binary File";
					c.Data.Parameters.Parameter.Add("");
					p.Cultures.CultureList.Add(c);
				}
				partnersID.PartnerIdList.Add(p);
				if(parent1 != null) {
					parent1.FillUIControler(true);
				}
				ListPartner();
			}
		}

		private void cmdDelete_Click(object sender, System.EventArgs e) {
			if(cboPartner.Text == "Default") {
				MessageBox.Show(this, "You can't delete the default partner!");
				return;
			}

			for(int i=0;i<partnersID.PartnerIdList.Count;i++) {
				Config.PartnerID p = (Config.PartnerID)partnersID.PartnerIdList[i];
				
				if(p.ID == cboPartner.Text) {
					partnersID.PartnerIdList.RemoveAt(i);

					if(parent1 != null) {
						parent1.FillUIControler(true);
					}
					cboPartner.Items.Clear();
					lstCultures.Items.Clear();
					ListPartner();
					break;

				}
			}
		}

		private void Partners_Load(object sender, System.EventArgs e) {
		}
	}
}
