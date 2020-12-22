using System;

namespace EFundraisingCRMWeb.Components.Server {
	/// <summary>
	/// Summary description for CurrentWorkingObject.
	/// </summary>
	public class CurrentWorkingObject {
		private CurrentWorkingObject() { }

		// save any temporary/working object in the session through this method
		public static void Remove(System.Web.SessionState.HttpSessionState session, string key) {
			session.Remove(key);			
		}

		// save any temporary/working object in the session through this method
		public void Save(object obj, System.Web.SessionState.HttpSessionState session, string key) {
			session[key] = obj;			
		}

		// save any temporary/working object in the application cache through this method
		public void Save(object obj, System.Web.Caching.Cache cache, string key, string fileDependency) {
			if(fileDependency != null || fileDependency != string.Empty) {
				cache.Insert(key, obj, new System.Web.Caching.CacheDependency(fileDependency));
			} else {
				cache.Insert(key, obj);
			}
		}

		// gateway to save data in the session or in the cache using hardcoded keys
		public static void Save(object obj, object datasource, string key, string fileDependency) {
			CurrentWorkingObject cwo = new CurrentWorkingObject();
			if(datasource is System.Web.SessionState.HttpSessionState) {
				System.Web.SessionState.HttpSessionState session =
					(System.Web.SessionState.HttpSessionState)datasource;
				cwo.Save(obj, session, key);
			} else if(datasource is System.Web.Caching.Cache) {
				System.Web.Caching.Cache cache = (System.Web.Caching.Cache)datasource;
				cwo.Save(obj, cache, key, fileDependency);
			} else if (datasource is System.Web.HttpApplicationState) {
				(datasource as System.Web.HttpApplicationState)[key] = obj;
			} 
			else {
				throw new ArgumentException("Current Working Object Save", "datasource");
			}
		}

		// gateway to get data from the session or from the cache using hardcoded keys
		public static object Get(object datasource, string key) {
			if(datasource is System.Web.SessionState.HttpSessionState) {
				System.Web.SessionState.HttpSessionState session = (System.Web.SessionState.HttpSessionState)datasource;
				if(session[key] != null) {
					return session[key];
				}
			} else if(datasource is System.Web.Caching.Cache) {
				System.Web.Caching.Cache cache = (System.Web.Caching.Cache)datasource;
				if(cache[key] != null) {
					return cache[key];
				}
			} else if (datasource is System.Web.HttpApplicationState) {
				return (datasource as System.Web.HttpApplicationState)[key];
			} 
			   else {
				throw new ArgumentException("Current Working Object Get", "datasource");
			}
			return null;
		}
	}
}
