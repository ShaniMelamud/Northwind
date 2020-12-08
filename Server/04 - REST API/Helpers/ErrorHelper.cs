using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Cool
{
    public static class ErrorHelper
    {
        public static List<string> ExtractErrors(ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();
            foreach (KeyValuePair<string, ModelStateEntry> entry in modelState)
            {
                foreach (ModelError err in entry.Value.Errors)
                {
                    errors.Add(err.ErrorMessage);
                }
            }
            return errors;
        }
    }
}
