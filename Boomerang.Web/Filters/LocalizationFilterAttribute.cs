using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Boomerang.Web.Filters
{
    public class LocalizationFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB");

            base.OnResultExecuting(context);
        }
    }
}
