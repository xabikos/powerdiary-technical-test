using PowerDiary.Services;

namespace PowerDiary.Configuration
{
    /// <summary>
    /// Custom route constraint to be able to use the enum as route parameter
    /// </summary>
    public class CustomRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var matchingValue = values[routeKey]?.ToString();
            return Enum.TryParse(matchingValue, true, out EventsGranularity _);
        }
    }
}