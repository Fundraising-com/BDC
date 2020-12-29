using System;

namespace GA.BDC.Core.Utilities.Javascript
{
	/*
	 * Created by:	Jean-Francois Buist.
	 * Date:		Novembre 2004.
	 * Version:		0.0
	 * 
	 */

	/// <summary>
	/// In an web page, if we want to make a field jump to another automatically after reaching a
	/// certain number of characters.
	/// </summary>
	/// <example>
	/// <code>
	/// txtPhoneNumber1.Attributes.Add("onKeyUp", AutomaticChangeField.GenerateOnKeyUpSetFocus(3));
	/// </code>
	/// </example>
	/// <remarks>
	/// In the HTML -> Javascript tag, you musth put the data from AutoTab()
	/// </remarks>
	public class AutomaticChangeField : JavaScript {
		public AutomaticChangeField() {

		}

		public static string AutoTab() {
			return "<!-- Original:  Cyanide_7 (leo7278@hotmail.com) -->\n" +
				"<!-- Web Site:  http://members.xoom.com/cyanide_7 -->\n" +
				"\n" +
				"<!-- This script and many more are available free online at -->\n" +
				"<!-- The JavaScript Source!! http://javascript.internet.com -->\n" +
				"\n" +
				"<!-- Begin\n" +
				"var isNN = (navigator.appName.indexOf(\"Netscape\")!=-1);\n" +
				"function autoTab(input,len, e) {\n" +
				"	var keyCode = (isNN) ? e.which : e.keyCode; \n" +
				"	var filter = (isNN) ? [0,8,9] : [0,8,9,16,17,18,37,38,39,40,46];\n" +
				"	if(input.value.length >= len && !containsElement(filter,keyCode)) {\n" +
				"		input.value = input.value.slice(0, len);\n" +
				"		input.form[(getIndex(input)+1) % input.form.length].focus();\n" +
				"	}\n" +
				"\n" +
				"	function containsElement(arr, ele) {\n" +
				"		var found = false, index = 0;\n" +
				"		while(!found && index < arr.length)\n" +
				"			if(arr[index] == ele)\n" +
				"				found = true;\n" +
				"			else\n" +
				"				index++;\n" +
				"		return found;\n" +
				"	}\n" +
				"\n" +
				"	function getIndex(input) {\n" +
				"		var index = -1, i = 0, found = false;\n" +
				"		while (i < input.form.length && index == -1)\n" +
				"			if (input.form[i] == input)\n" +
				"				index = i;\n" +
				"			else \n" +
				"				i++;\n" +
				"		return index;\n" +
				"	}\n" +
				"	return true;\n" +
				"}\n" +
				"//  End -->\n";

		}

		/// <summary>
		/// Attributes.Add("OnKeyUp", GenerateonKeyUpSetFocus(x));
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static string GenerateOnKeyUpSetFocus(int len) {
			return "return autoTab(this, " + len.ToString() + ", event);";
		}

	}
}
