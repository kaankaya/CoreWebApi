using GalacticTourAgency.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GalacticTourAgency.ModelBindings
{
    public class GalacticCoordinateBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            if(string.IsNullOrWhiteSpace(value))
            {
                return Task.CompletedTask;
            }
            //12.36,32.56 coordinate

            var parts = value.Split(',');
            if(parts.Length != 2 || !double.TryParse(parts[0],out double x) || !double.TryParse(parts[1], out double y))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName,"Geçersiz coordinat bilgisi, 'X.XX,Y.YY' Formatı kullanmalısınız");
                return Task.CompletedTask;
            }
            var result = new GalacticCordinate
            {
                X = x,
                Y = y
            };

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
