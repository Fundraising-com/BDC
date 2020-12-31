// Created by:	Benoit Nadon
// Date:		2005-10-03

using System;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// ParameterValue is a clone of the RS web service's ParameterValue class.
	/// It allows to remove unnecessary coupling between all the pages calling reports and
	/// the web service.
	/// </summary>
	[Serializable]
	public class ParameterValue
	{
		private string _label;
		private string _name;
		private string _value;

		public ParameterValue() { }

		public ParameterValue(string name, string _value) : this(name, _value, String.Empty) { }

		public ParameterValue(string name, string _value, string label)
		{
			this._name = name;
			this._value = _value;
			this._label = label;
		}

		public string Label 
		{
			get 
			{
				return _label;
			}
			set 
			{
				_label = value;
			}
		}

		public string Name 
		{
			get 
			{
				return _name;
			}
			set 
			{
				_name = value;
			}
		}

		public string Value 
		{
			get 
			{
				return _value;
			}
			set 
			{
				_value = value;
			}
		}
	}
}
