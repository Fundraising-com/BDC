namespace AdminSection.Components.User.Administration
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using efundraising.eFundraisingStore;
	using efundraising.Diagnostics;
	using System.Diagnostics;
	using System.IO;


	/// <summary>
	////		Summary description for PackageDescInfo.
	/// </summary>
	public class PackageDescInfo : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label ErrorLabel;
		protected System.Web.UI.WebControls.Button UploadLargeButton;
		protected System.Web.UI.WebControls.Button UploadButton;
		protected System.Web.UI.HtmlControls.HtmlInputFile FileUpload;
		protected efundraising.Web.UI.InputControls.IntegerTextBox DisplayOrderTextBox;
		protected System.Web.UI.HtmlControls.HtmlInputFile FileLargeUpload;

		private bool enabledValue  = false;
		private int templateValue  = int.MinValue;

		protected System.Web.UI.WebControls.Image Image;
		protected System.Web.UI.WebControls.Button EditRelationButton;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.Button EditProductButton;
		protected efundraising.Web.UI.InputControls.StringTextBox PageNameTextBox;
		protected efundraising.Web.UI.InputControls.StringTextBox PageTitleTextBox;
		protected efundraising.Web.UI.InputControls.StringTextBox ImageNameTextBox;
		protected efundraising.Web.UI.InputControls.StringTextBox ImageAltTextTextBox;
		protected System.Web.UI.WebControls.Button DisplayButton;
		protected System.Web.UI.WebControls.Button DisplayLargeButton;
		protected efundraising.Web.UI.InputControls.StringTextBox NameTextBox;
		protected System.Web.UI.WebControls.DropDownList TemplateDropDownList;
		protected System.Web.UI.WebControls.TextBox ShortDescriptionTextBox;
		protected System.Web.UI.WebControls.TextBox ExtraDescriptionTextBox;
		protected System.Web.UI.WebControls.DropDownList EnabledDropdownlist;
		protected System.Web.UI.WebControls.TextBox LongDescriptionTextBox;
		protected System.Web.UI.WebControls.Button EditAllButton;

		public event EventHandler eventRefresh;


         //This object represent the Description information of a Package, can be used also
		 //for the product description object
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (!(IsPostBack))
				{
					// Put user code to initialize the page here
					// fill package template drop down list
					Template[] Templates = Template.GetTemplates();
					foreach(Template template in Templates) 
					{
						TemplateDropDownList.Items.Add(new ListItem(template.Name, template.TemplateId.ToString()));
					}
					TemplateDropDownList.Items.Add(new ListItem("N/A", int.MinValue.ToString()));

						
					//fill PackageEnabled drop dpwn list
					EnabledDropdownlist.Items.Add(new ListItem("True","1"));
					EnabledDropdownlist.Items.Add(new ListItem("False","0"));
			
					//sets the reste of the data
					SetInfo();
				}
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in Load of Package Desc Info", ex);
			}
		}

		#region Set/Get Methods


		#region Name
		public string GetName() 
		{
			return NameTextBox.Text;
		} 

		public void SetName(string val) 
		{
			
			NameTextBox.Text = val;
		}
		#endregion

		#region Page Title
		public string GetPageTitle() 
		{
			return PageTitleTextBox.Text;
		} 

		public void SetPageTitle(string val) 
		{
			PageTitleTextBox.Text = val;
		}
		#endregion

		#region  Short DescriptionTextBox setter and getters 
		public string GetShortDescriptionTextBox() 
		{
			return ShortDescriptionTextBox.Text;
		}	
		
		public void SetShortDescription(string val) 
		{
			ShortDescriptionTextBox.Text = val;
		}
		#endregion

		#region long DescriptionTextBox setter and getters 
		public string GetLongDescriptionTextBox() 
		{
			return LongDescriptionTextBox.Text;
		}	

		public void SetLongDescription(string val) 
		{
			LongDescriptionTextBox.Text = val;
		}
		#endregion

		#region ExtraDescriptionTextBox setter and getters 
		public string GetExtraDescriptionTextBox() 
		{
			return ExtraDescriptionTextBox.Text;
		}	

		public void SetExtraDescription(string val) 
		{
			ExtraDescriptionTextBox.Text = val;
		}
		#endregion

		#region Enabled
		public bool GetEnabled() 
		{
			return Convert.ToBoolean(Convert.ToInt32(EnabledDropdownlist.SelectedValue));
		}	

		public void SetEnabled(bool val) 
		{
			enabledValue = val;
		}
		#endregion

		#region Page Name
		public string GetPageName() 
		{
			return PageNameTextBox.Text;
		} 

		public void SetPageName(string val) 
		{
			
			PageNameTextBox.Text = val;
		}
		#endregion

		#region Image Name
		public string GetImageName() 
		{
			return ImageNameTextBox.Text;
		} 

		public void SetImageName(string val) 
		{
			
			ImageNameTextBox.Text = val;
		}
		#endregion

		#region small Image Type
		public string GetSmallImageType() 
		{
			return ViewState["SmallImageType"].ToString();
		} 

		public void SetSmallImageType(string val) 
		{
			ViewState["SmallImageType"] = val;
		}
		#endregion

		#region Large Image Type
		public string GetLargeImageType() 
		{
			return ViewState["LargeImageType"].ToString();
		} 

		public void SetLargeImageType(string val) 
		{
			ViewState["LargeImageType"] = val;
		}
		#endregion

		#region Image Alt Text
		public string GetImageAltText() 
		{
			return ImageAltTextTextBox.Text;
		} 

		public void SetImageAltText(string val) 
		{
			
			ImageAltTextTextBox.Text = val;
		}
		#endregion

		#region Template
		public int GetTemplate() 
		{
			return Convert.ToInt32(TemplateDropDownList.SelectedValue);
		} 

		public void SetTemplate(int val) 
		{
			templateValue = val;
		}
		#endregion
		
		#region DisplayOrderTextBox setter and getters 
		public string GetDisplayOrderTextBox() 
		{
			if(DisplayOrderTextBox.Text == "")
			{
				return int.MinValue.ToString();
			}
			else
			{
				return DisplayOrderTextBox.Text;
			}
			
		}	

		public void SetDisplayOrderTextBox(string val) 
		{
			if (val == int.MinValue.ToString())
			{
				DisplayOrderTextBox.Text = "";
			}
			else
			{
				DisplayOrderTextBox.Text = val;
			}
			
			
		}
		#endregion

		#region ImagePath setter and getters 
		public string GetSmallImagePath() 
		{
			if (ViewState["SmallImagePath"] == null)
			{
				return "";
			}
			else
			{
				return ViewState["SmallImagePath"].ToString();
			}
		}	

		public void SetSmallImagePath(string val) 
		{
			ViewState["SmallImagePath"] = val;
			
		}
		#endregion

		#region LargeImagePath setter and getters 
		public string GetLargeImagePath() 
		{
			if (ViewState["LargeImagePath"] == null)
			{
				return "";
			}
			else
			{
				return ViewState["LargeImagePath"].ToString();
			}
		}	

		public void SetLargeImagePath(string val) 
		{
			ViewState["LargeImagePath"] = val;
			
		}
		#endregion
					
		#region UploadImageEnabled
		//Has to be set to disabled for New Packages, no images can be uploaded without the ID
		public void SetUploadButtonEnabled(bool val) 
		{
			UploadButton.Enabled = val;
			UploadLargeButton.Enabled = val;
		}
		#endregion

		#region IsEditMode
		public bool GetIsEditMode() 
		{
			if (ViewState["IsEditMode"] != null)
			{
				return Convert.ToBoolean(ViewState["IsEditMode"]);
			}
			else
			{
               return false;
			}
			
		} 

		public void SetIsEditMode(bool val) 
		{
			
			ViewState["IsEditMode"] = val;
		}
		#endregion

		#region Type (package or product)
		public string GetType() 
		{
			if (ViewState["Type"] != null)
			{
				return Convert.ToString(ViewState["Type"]);
			}
			else
			{
				return null;
			}
			
		} 

		public void SetType(string val) 
		{
	    	ViewState["Type"] = val;
		}
		#endregion

		#region CultureCode
		public string GetCultureCode() 
		{
			if (ViewState["CultureCode"] != null)
			{
				return Convert.ToString(ViewState["CultureCode"]);
			}
			else
			{
				return null;
			}
			
		} 

		public void SetCultureCode(string val) 
		{
			ViewState["CultureCode"] = val;
		}
		#endregion

		#region OriginalImageName
		public string GetOriginalImageName() 
		{
			if (ViewState["OriginalImageName"] != null)
			{
				return Convert.ToString(ViewState["OriginalImageName"]);
			}
			else
			{
				return null;
			}
			
		} 

		public void SetOriginalImageName(string val) 
		{
			ViewState["OriginalImageName"] = val;
		}
		#endregion

		#region OriginalPageName
		public string GetOriginalPageName() 
		{
			if (ViewState["OriginalPageName"] != null)
			{
				return Convert.ToString(ViewState["OriginalPageName"]);
			}
			else
			{
				return null;
			}
			
		} 

		public void SetOriginalPageName(string val) 
		{
			ViewState["OriginalPageName"] = val;
		}
		#endregion


		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
			this.DisplayButton.Click += new System.EventHandler(this.DisplayButton_Click);
			this.UploadLargeButton.Click += new System.EventHandler(this.UploadLargeButton_Click);
			this.DisplayLargeButton.Click += new System.EventHandler(this.DisplayLargeButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Public Methods
		
		//This method lets the user refresh the control from the outside 
		//if we dont want to refresh the whole page
		public void SetInfo()
		{
			try
			{
				TemplateDropDownList.SelectedValue = templateValue.ToString();
				
				//set enabled drop down
				EnabledDropdownlist.SelectedValue = Convert.ToInt32(enabledValue).ToString();
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in Set Info of PackageDescInfo",ex);
			}
		}

        //resets all the fields for a new culture
		public void Clear()
		{
			SetName("");
			SetPageName("");
			SetPageTitle("");
			TemplateDropDownList.SelectedValue = "0";
			SetShortDescription("");
			SetLongDescription("");
			SetExtraDescription("");
			SetImageName("");
			SetImageAltText("");
            SetDisplayOrderTextBox("0");
			EnabledDropdownlist.SelectedValue = "0";
			SetSmallImagePath("");
			SetLargeImagePath("");
			ClearErrorLabel();
		}

		public void ClearErrorLabel()
		{
           ErrorLabel.Text = "";
		}
	
		#endregion
		
		#region private methods
		
		//this method uploads the large image of a specific type to the server and renames it with the package ID
		private void UploadLargeButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(FileLargeUpload.PostedFile.FileName != "") 
				{
					if(GetImageName().Trim() != "")
					{
						string fileName = FileLargeUpload.PostedFile.FileName;
						string[] types = fileName.Split('.');
						string type = types[1];
						//must set the type to the image, if no extension is set
						int pos = GetImageName().IndexOf(".");
						if (pos == -1)
						{
							SetImageName(GetImageName() + "." + type);
						}

						//check if file is type [an image]
						if (FileLargeUpload.PostedFile.ContentType.Substring(0,5) == "image")
						{
           					string imgPath = GetLargeImagePath() + GetImageName();

							//check if image name already exists
							//only if image has changed
						/*	string smallImageName = "";
							if (ViewState["UploadedSmallImageName"] != null)
							{
								smallImageName = ViewState["UploadedSmallImageName"].ToString();
							}*/
							bool exists = ImageNameExists();
							if (exists)// || GetImageName().Equals(smallImageName))
							{
								ErrorLabel.Text = "Error. The Image name already exists";
								ErrorLabel.Visible = true;
							}
							else
							{
								ErrorLabel.Visible = false;
								FileLargeUpload.PostedFile.SaveAs(Server.MapPath(imgPath));
								Session["ImagePathToDisplay"] = imgPath;
				    			ViewState["LargeImage"] = imgPath;
								ViewState["UploadedLargeImageName"] = GetImageName(); //used for checking if the image name has already been uploaded since its not yet in the database
								//refresh image
								if (eventRefresh != null)
									eventRefresh (this, e);
							}
						}
						else
						{
							ErrorLabel.Text = "Error. The Large image file was wrong";
							ErrorLabel.Visible = true;
						}
					}
					else
					{
						ErrorLabel.Text = "Error. No Image Name was entered";
						ErrorLabel.Visible = true;
					}
				}
				else
				{
					ErrorLabel.Text = "Error. No Large image was selected";
					ErrorLabel.Visible = true;
				}
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in package Upload Large Image",ex);
			}
		}

		//like lhe large image, this method uploads a small image to the server
		private void UploadButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(FileUpload.PostedFile.FileName != "") 
				{
					if(GetImageName().Trim() != "")
					{
						string fileName = FileUpload.PostedFile.FileName;
						string[] types = fileName.Split('.');
						string type = types[1];
						//must set the type to the image, if no extension is set
						int pos = GetImageName().IndexOf(".");
						if (pos == -1)
						{
							SetImageName(GetImageName() + "." + type);
						}
					
						//check if file is type [an image]
						if (FileUpload.PostedFile.ContentType.Substring(0,5) == "image")
						{

							string imgPath = GetSmallImagePath() + GetImageName();

							//check if image name already exists
							//only if image has changed
							/*string largeImageName = "";
							if (ViewState["UploadedLargeImageName"] != null)
							{
								largeImageName = ViewState["UploadedLargeImageName"].ToString();
							}*/

							bool exists = ImageNameExists();
                            if (exists){ //|| GetImageName().Equals(largeImageName)){						
								ErrorLabel.Text = "Error. The Image name already exists";
								ErrorLabel.Visible = true;
							}
						 	else
						    {
								ErrorLabel.Visible = false;
					            FileUpload.PostedFile.SaveAs(Server.MapPath(imgPath));
								Session["ImagePathToDisplay"] = imgPath;  //for postback
								ViewState["SmallImage"] = imgPath;  //for display button
								ViewState["UploadedSmallImageName"] = GetImageName(); //used for checking if the image name has already been uploaded since its not yet in the database
								//refresh image
								if (eventRefresh != null)
									eventRefresh (this, e);
						    }
						}
						else
						{
							ErrorLabel.Text = "Error. The small image file was wrong";
							ErrorLabel.Visible = true;
						}
				   }
					else
					{
						ErrorLabel.Text = "Error. No Image Name was entered";
						ErrorLabel.Visible = true;
					}
				}	
				else
				{
					ErrorLabel.Text = "Error. No image was selected";
					ErrorLabel.Visible = true;
				}
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in package Upload Image",ex);
			}
		}

		private bool ImageNameExists()
		{
			bool imageExists = false;
			string type = GetType();
			if (type == "Package")
			{
				PackageDescCollection packageDescs = null;
				if (!(GetOriginalImageName() == null))
				{
					if (!(GetOriginalImageName().Equals(GetImageName())))
					{
						packageDescs = PackageDesc.GetPackageDescsByImageName(GetImageName());
					}
				}
				else
				{
					packageDescs = PackageDesc.GetPackageDescsByImageName(GetImageName());
				}
				
				if (packageDescs != null)
				{
					imageExists = true;
				}
			
			}
			else if (type == "Product")
			{
				ProductDescCollection productDescs = null;
				if (!(GetOriginalImageName().Equals(GetImageName())))
				{
					productDescs = ProductDesc.GetProductDescsByImageName(GetImageName());
				}

				if (productDescs != null)
				{
					imageExists = true;
				}

			}

			return imageExists;
							
		}


		//set the image path in the session so the server displays it on the roundtrip
		private void DisplayLargeButton_Click(object sender, System.EventArgs e)
		{
			if (ViewState["LargeImagePath"] != null) 
			{
				Session["ImagePathToDisplay"] = GetLargeImagePath() + GetImageName();
			}

			if (eventRefresh != null)
				eventRefresh (this, e);
		
		}

		//like DisplayLargeButton
		private void DisplayButton_Click(object sender, System.EventArgs e)
		{
			if (ViewState["SmallImagePath"] != null) 
			{
				Session["ImagePathToDisplay"] = GetSmallImagePath() + GetImageName();
			}
			
			if (eventRefresh != null)
				eventRefresh (this, e);
		
		}
	

#endregion
	

	
	}
}
