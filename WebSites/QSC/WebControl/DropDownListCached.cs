using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Caching;

namespace QSP.WebControl
{
	public delegate object LoadCachedDataDelegate();

	/// <summary>
	/// Summary description for DropDownListCached.
	/// </summary>
	public class DropDownListCached : InternalDropDownListSearch
	{
		private CacheDependency cacheDependency = null;
		private CacheItemRemovedCallback cacheItemRemovedCallback = null;
		private LoadCachedDataDelegate loadCachedDataDelegate = null;

		protected virtual bool EnableCache 
		{
			get 
			{
				bool enableCache = false;

				if(ViewState["EnableCache"] != null) 
				{
					enableCache = Convert.ToBoolean(ViewState["EnableCache"]);
				}

				return enableCache;
			}
			set 
			{
				ViewState["EnableCache"] = value;
			}
		}

		public override object DataSource
		{
			get
			{
				object dataSource = null;

				if(!EnableCache) 
				{
					dataSource = base.DataSource;
				} 
				else 
				{
					if(CachedDataSource == null) 
					{
						CachedDataSource = LoadCachedData();
					}

					dataSource = CachedDataSource;
				}

				return dataSource;
			}
			set 
			{
				if(!EnableCache) 
				{
					base.DataSource = value;
				}
			}
		}

		protected virtual object CachedDataSource 
		{
			get 
			{
				return Context.Cache[CachedDataSourceName];
			}
			set 
			{
				if(Context.Cache[CachedDataSourceName] == null) 
				{
					Context.Cache.Add(CachedDataSourceName, value, CacheDependency, AbsoluteExpiration, SlidingExpiration, CacheItemPriority, CacheItemRemovedCallback);
				} 
				else 
				{
					Context.Cache[CachedDataSourceName] = value;
				}
			}
		}

		protected virtual string CachedDataSourceName 
		{
			get 
			{
				string cachedDataSourceName = String.Empty;

				if(ViewState["CachedDataSourceName"] != null) 
				{
					cachedDataSourceName = ViewState["CachedDataSourceName"].ToString();
				}

				return cachedDataSourceName;
			}
			set 
			{
				ViewState["CachedDataSourceName"] = value;
			}
		}

		public virtual CacheDependency CacheDependency 
		{
			get 
			{
				return cacheDependency;
			}
			set 
			{
				cacheDependency = value;
			}
		}

		public virtual DateTime AbsoluteExpiration 
		{
			get 
			{
				DateTime absoluteExpiration = Cache.NoAbsoluteExpiration;

				if(ViewState["AbsoluteExpiration"] != null) 
				{
					absoluteExpiration = Convert.ToDateTime(ViewState["AbsoluteExpiration"]);
				}

				return absoluteExpiration;
			}
			set 
			{
				ViewState["AbsoluteExpiration"] = value;
			}
		}

		public virtual TimeSpan SlidingExpiration 
		{
			get 
			{
				TimeSpan slidingExpiration = Cache.NoSlidingExpiration;

				if(ViewState["SlidingExpiration"] != null) 
				{
					slidingExpiration = (TimeSpan) ViewState["SlidingExpiration"];
				}

				return slidingExpiration;
			}
			set 
			{
				ViewState["SlidingExpiration"] = value;
			}
		}

		public virtual CacheItemPriority CacheItemPriority 
		{
			get 
			{
				CacheItemPriority cacheItemPriority = CacheItemPriority.Default;

				if(ViewState["CacheItemPriority"] != null) 
				{
					cacheItemPriority = (CacheItemPriority) ViewState["CacheItemPriority"];
				}

				return cacheItemPriority;
			}
			set 
			{
				ViewState["CacheItemPriority"] = value;
			}
		}

		public virtual CacheItemRemovedCallback CacheItemRemovedCallback 
		{
			get 
			{
				return cacheItemRemovedCallback;
			}
			set 
			{
				cacheItemRemovedCallback = value;
			}
		}

		public virtual LoadCachedDataDelegate LoadCachedDataDelegate
		{
			get 
			{
				return loadCachedDataDelegate;
			}
			set 
			{
				loadCachedDataDelegate = value;
			}
		}

		protected virtual object LoadCachedData() 
		{
			return LoadCachedDataDelegate();
		}
	}
}
