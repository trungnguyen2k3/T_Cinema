using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CinemaBE.Helpers
{
    public static class ModelStateHelper
    {
        public static object GetErrors(ModelStateDictionary modelState)
        {
            return modelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}