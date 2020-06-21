using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public static class ActionResultExtensions
    {
        public static IActionResult WithSuccessMessage(this IActionResult actionResult, string message)
        {
            return WithMessage(actionResult, message, "alert-success");
        }

        public static IActionResult WithWarningMessage(this IActionResult actionResult, string message)
        {
            return WithMessage(actionResult, message, "alert-warning");
        }

        public static IActionResult WithDangerMessage(this IActionResult actionResult, string message)
        {
            return WithMessage(actionResult, message, "alert-danger");
        }

        private static IActionResult WithMessage(IActionResult actionResult, string message, string classeAlert)
        {
            return new TempDataActionResult(actionResult, message, classeAlert);
        }

        public static ModelStateDictionary ModelStateReturnError(ModelStateDictionary modelState)
        {
            var errors = from modelstate in modelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Erro = modelstate.Value.Errors[0].ErrorMessage };
            foreach (var erro in errors)
            {
                modelState.AddModelError(erro.Title, erro.Erro);
            }

            return modelState;
        }
    }

    public class TempDataActionResult : IActionResult
    {
        private readonly IActionResult _actionResult;
        private readonly string _message;
        private readonly string _classeAlert;

        public TempDataActionResult(IActionResult actionResult, string message, string classeAlert)
        {
            _actionResult = actionResult;
            _message = message;
            _classeAlert = classeAlert;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var factory = context.HttpContext.RequestServices.GetService<ITempDataDictionaryFactory>();
            var tempData = factory.GetTempData(context.HttpContext);

            tempData["ClasseAlert"] = _classeAlert;
            tempData["Message"] = _message;

            return _actionResult.ExecuteResultAsync(context);
        }
    }
}
