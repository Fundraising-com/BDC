using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
    [Serializable]
    public class BusinessRule
    {
        private int _business_rule_id;
        private int _email_template_id;
        private string _business_rule_name;
        private string _stored_procedure_call;
        private int _priority_level;
        private int _member_type_id;
        private int _email_priority;
        private bool _active;
        private DateTime _create_date;


        public BusinessRule() : this(int.MinValue) { }
        public BusinessRule(int business_rule_id) : this(business_rule_id, int.MinValue) { }
        public BusinessRule(int business_rule_id, int email_template_id) : this(business_rule_id, email_template_id, null) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name) : this(business_rule_id, email_template_id, business_rule_name, null) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name, string stored_procedure_call) : this(business_rule_id, email_template_id, business_rule_name, stored_procedure_call, int.MinValue) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name, string stored_procedure_call, int priority_level) : this(business_rule_id, email_template_id, business_rule_name, stored_procedure_call, priority_level, int.MinValue) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name, string stored_procedure_call, int priority_level, int member_type_id) : this(business_rule_id, email_template_id, business_rule_name, stored_procedure_call, priority_level, member_type_id, int.MinValue) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name, string stored_procedure_call, int priority_level, int member_type_id, int email_priority) : this(business_rule_id, email_template_id, business_rule_name, stored_procedure_call, priority_level, member_type_id, email_priority, false) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name, string stored_procedure_call, int priority_level, int member_type_id, int email_priority, bool active) : this(business_rule_id, email_template_id, business_rule_name, stored_procedure_call, priority_level, member_type_id, email_priority, active, DateTime.MinValue) { }
        public BusinessRule(int business_rule_id, int email_template_id, string business_rule_name, string stored_procedure_call, int priority_level, int member_type_id, int email_priority, bool active, DateTime create_date)
        {
            _business_rule_id = business_rule_id;
            _email_template_id = email_template_id;
            _business_rule_name = business_rule_name;
            _stored_procedure_call = stored_procedure_call;
            _priority_level = priority_level;
            _member_type_id = member_type_id;
            _email_priority = email_priority;
            _active = active;
            _create_date = create_date;
        }




        #region Properties
        public int business_rule_id
        {
            set { _business_rule_id = value; }
            get { return _business_rule_id; }
        }

        public int email_template_id
        {
            set { _email_template_id = value; }
            get { return _email_template_id; }
        }

        public string business_rule_name
        {
            set { _business_rule_name = value; }
            get { return _business_rule_name; }
        }

        public string stored_procedure_call
        {
            set { _stored_procedure_call = value; }
            get { return _stored_procedure_call; }
        }

        public int priority_level
        {
            set { _priority_level = value; }
            get { return _priority_level; }
        }

        public int member_type_id
        {
            set { _member_type_id = value; }
            get { return _member_type_id; }
        }

        public int email_priority
        {
            set { _email_priority = value; }
            get { return _email_priority; }
        }

        public bool active
        {
            set { _active = value; }
            get { return _active; }
        }

        public DateTime create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }

        public EmailTemplateList emailTemplate {get;set;}
        #endregion
    }
}
