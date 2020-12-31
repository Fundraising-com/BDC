using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.ServiceLocation;
using Business;
using Data;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;
using System.Collections.Specialized;

namespace BusinessServices.Validations
{
    [ConfigurationElementType(typeof(CustomValidatorData))]
    class BatchOrderExistsValidator  : Validator<int>
    {
        public BatchOrderExistsValidator(NameValueCollection attributes)
            : base(string.Empty, string.Empty)
        {
        }
        public BatchOrderExistsValidator(string tag) : base(String.Empty, tag) { }
        protected override void DoValidate(int objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            var nOrderID = objectToValidate;
            IRepository<Batch> _b = ServiceLocator.Current.GetInstance<IRepository<Batch>>();
            var spec = new Specification<Batch>(b => b.ID == nOrderID);


            var query = _b.DoQuery(spec);

            Batch _batch = query.FirstOrDefault<Batch>();

            if(_batch != null)
                this.LogValidationResult(validationResults,
                            "The Order is invalid", currentTarget, key);
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Invalid orderid"; }
        }
    }
}
