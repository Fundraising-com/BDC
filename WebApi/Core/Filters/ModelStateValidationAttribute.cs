using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;


namespace GA.BDC.WebApi.Fundraising.Core.Filters
{
    public class ModelStateValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.ModelState.RemoveDuplicateErrorMessages();
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            base.OnActionExecuting(actionContext);
        }
    }
    public static class ModelStateExtension
    {
        public static void RemoveDuplicateErrorMessages(this ModelStateDictionary modelStateDictionary)
        {
            //Stores the error messages we have seen
            var knownValues = new HashSet<string>();

            //Create a copy of the modelstatedictionary so we can modify the original.
            var modelStateDictionaryCopy = modelStateDictionary.ToDictionary(
                element => element.Key,
                element => element.Value);

            foreach (var modelState in modelStateDictionaryCopy)
            {
                var modelErrorCollection = modelState.Value.Errors;
                for (var i = 0; i < modelErrorCollection.Count; i++)
                {
                    //Check if we have seen the error message before by trying to add it to the HashSet
                    if (!knownValues.Add(modelErrorCollection[i].ErrorMessage))
                    {
                        modelStateDictionary[modelState.Key].Errors.RemoveAt(i);
                    }
                }
            }
        }
    }
}