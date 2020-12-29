using System;
using System.Reflection;

namespace GA.BDC.Core.Utilities.Reflection {

	/// <summary>
	/// 
	/// </summary>
	public class Reflect {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pAssemblyName"></param>
		/// <param name="pClassNamespace"></param>
		/// <returns></returns>
		public static object GetInstanceOf(string pAssemblyName, string pClassNamespace) {
			object oInst = null;
			Assembly oAss = Assembly.LoadWithPartialName(pAssemblyName);
			try {
				oInst = Activator.CreateInstance(oAss.GetType(pClassNamespace), BindingFlags.CreateInstance, null, null, System.Globalization.CultureInfo.CurrentCulture);
			} catch(Exception ex) {
				throw ex;
			}
			return oInst;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pPropertyName"></param>
		/// <param name="pObjectToSetProperty"></param>
		/// <param name="pValues"></param>
		public static void SetProperty(string pPropertyName,ref object pObjectToSetProperty, params object[] pValues) {
			pObjectToSetProperty.GetType().InvokeMember(pPropertyName, BindingFlags.SetProperty, null, pObjectToSetProperty, pValues);
		}

		public static string GetProperty(string pPropertyName, object pObjectToGetProperty) {
			return pObjectToGetProperty.GetType().InvokeMember(pPropertyName, BindingFlags.GetProperty, null, pObjectToGetProperty,null).ToString();
		}

		public static object GetPropertyObject(string pPropertyName, object pObjectToGetPropertyObject) {
			return pObjectToGetPropertyObject.GetType().InvokeMember(pPropertyName, BindingFlags.GetProperty, null, pObjectToGetPropertyObject,null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pMethodName"></param>
		/// <param name="pObjectToInvokeMethod"></param>
		/// <param name="pValues"></param>
		public static void CallMethod(string pMethodName, ref object pObjectToInvokeMethod, params object[] pValues) {
			pObjectToInvokeMethod.GetType().InvokeMember(pMethodName, BindingFlags.InvokeMethod, null, pObjectToInvokeMethod, pValues);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pEventName"></param>
		/// <param name="pObjectToSetEvent"></param>
		/// <param name="pValues"></param>
		public static void SetEvent(string pEventName, ref object pObjectToSetEvent, params object[] pValues) {
			throw new NotImplementedException("SetEvent method is not implementated");
		}
	}
}
