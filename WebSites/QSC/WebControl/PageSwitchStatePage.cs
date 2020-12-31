using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for PageSwitchStatePage.
	/// </summary>
    public class PageSwitchStatePage : ErrorManagingPage
	{
		private const string QUERYSTRING_PARAMETER_NAME = "PageSwitchStateID";

		private bool loadsPageSwitchState = false;
		private string viewStateString = String.Empty;

		public PageSwitchStateBagCollection PageSwitchState
		{
			get 
			{
				if(Session["PageSwitchState"] == null) 
				{
					Session["PageSwitchState"] = new PageSwitchStateBagCollection();
				}

				return (PageSwitchStateBagCollection) Session["PageSwitchState"];
			}
		}

		[Bindable(true), Category("Misc"), DefaultValue(false)]
		protected virtual bool LoadsPageSwitchState 
		{
			get 
			{
				return loadsPageSwitchState;
			}
			set 
			{
				loadsPageSwitchState = value;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			if(!IsPostBack && this.LoadsPageSwitchState && Request.QueryString[QUERYSTRING_PARAMETER_NAME] != null)
				LoadPageSwitchState(Convert.ToInt32(Request.QueryString[QUERYSTRING_PARAMETER_NAME]));

			base.OnInit (e);
		}

		public virtual void LoadPageSwitchState(int pageSwitchStateID) 
		{
			PageSwitchStateBag pageSwitchStateBag = PageSwitchState[pageSwitchStateID];

			if(pageSwitchStateBag != null) 
			{
				for(int i = 0; i < pageSwitchStateBag.Count; i++) 
				{
					if(pageSwitchStateBag[i] != null) 
					{
						ViewState.Add(pageSwitchStateBag.Keys[i], pageSwitchStateBag[i]).IsDirty = true;
					}
				}
			}
		}

		[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
		public int SavePageSwitchStateFromClient(string viewStateFromClient) 
		{
			LoadPageViewStateFromClient();
			return SavePageSwitchState();
		}

		public virtual int SavePageSwitchState()
		{
			int pageSwitchStateID = PageSwitchState.CreateNewPageSwitchState();

			SavePageSwitchStateRecursive(pageSwitchStateID, this);

			return pageSwitchStateID;
		}

		private void SavePageSwitchStateRecursive(int pageSwitchStateID, Control control) 
		{
			IPageSwitchStateControl pageSwitchStateControl;

			foreach(Control childControl in control.Controls) 
			{
				SavePageSwitchStateRecursive(pageSwitchStateID, childControl);

				pageSwitchStateControl = childControl as IPageSwitchStateControl;

				if(pageSwitchStateControl != null) 
				{
					pageSwitchStateControl.SavePageSwitchState(pageSwitchStateID);
				}
			}
		}

		private void LoadPageViewStateFromClient() 
		{
			long streamPosition = Context.Request.InputStream.Position;
			string inputStreamString;
			int viewStateStringIndex;
			
			Context.Request.InputStream.Position = 0;
			inputStreamString = new System.IO.StreamReader(Context.Request.InputStream).ReadToEnd();
			Context.Request.InputStream.Position = streamPosition;
			
			viewStateStringIndex = inputStreamString.IndexOf("viewStateFromClient");
			if(viewStateStringIndex != -1) 
			{
				viewStateString = Server.UrlDecode(inputStreamString.Substring(viewStateStringIndex + 20)).Replace(" ", "+");
				LoadViewStateObject(LoadPageStateFromPersistenceMedium());
			}

			viewStateString = String.Empty;
		}

		protected override object LoadPageStateFromPersistenceMedium()
		{
			if(viewStateString != String.Empty) 
			{
				object viewStateObj = null;
				//LosFormatter formatter = new LosFormatter();
                ObjectStateFormatter formatter = new ObjectStateFormatter();

				try
				{
					// Deserialize the object
					viewStateObj = formatter.Deserialize(viewStateString);
				}
				catch(Exception ex)
				{
					throw new HttpException("Error restoring page viewstate.\n", ex);
				}
				return viewStateObj;
			} 
			else 
			{
				return base.LoadPageStateFromPersistenceMedium();
			}
		}

        protected virtual void LoadViewStateObject(object viewStateObj)
        {
            Pair pair;
            Pair subPair;
            Pair subSubPair;
            ArrayList list;

            if (viewStateObj != null)
            {
                pair = (Pair)viewStateObj;

                if (pair.First != null)
                    subPair = (Pair)pair.First;
                else if (pair.Second != null)
                    subPair = (Pair)pair.Second;
                else
                    subPair = null;

                if (subPair.Second != null)
                {
                    subSubPair = (Pair)subPair.Second;

                    if (subSubPair.First != null)
                    {
                        list = (ArrayList)subSubPair.First;

                        for (int i = 0; i < list.Count - 1; i = i + 2)
                        {
                            if (list[i + 1] != null)
                            {
                                ViewState.Add(((IndexedString)list[i]).Value, list[i + 1]);
                            }
                            else
                            {
                                ViewState.Add(((IndexedString)list[i]).Value, null);
                            }
                        }

                        //existingKeyList = (ArrayList)list.Clone();
                    }
                }
            }
        }
	}
}
