using ExpirationDateControl_API.Logs;
using ExpirationDateControl_API.Models;
using ExpirationDateControl_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpirationDateControl_API.Filters
{
    public class CustomLogsFilter : IActionFilter
    {
        private readonly List<int> _sucessStatusCodes;
        private readonly IProductRepository _repository;
        private readonly Dictionary<int, Product> _contextDict;

        public CustomLogsFilter(IProductRepository repository)
        {
            _repository = repository;
            _sucessStatusCodes = new List<int>() { StatusCodes.Status200OK, StatusCodes.Status201Created, StatusCodes.Status204NoContent };
            _contextDict = new Dictionary<int, Product>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.Equals(context.ActionDescriptor.RouteValues["controller"], "Product", StringComparison.InvariantCultureIgnoreCase))
            {
                int id = 0;
                if (context.ActionArguments.ContainsKey("id") && int.TryParse(context.ActionArguments["id"].ToString(), out id))
                {
                    if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var product = _repository.GetById(id);
                        if (product != null)
                        {
                            //var gameClone = CloneService.Clone<Games>(game); // Deep Clone
                            product = product.Clone(); // Shallow Clone
                            _contextDict.Add(id, product);
                        }
                    }
                }

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (string.Equals(context.ActionDescriptor.RouteValues["controller"], "Product", StringComparison.InvariantCultureIgnoreCase))
            {
                if (_sucessStatusCodes.Contains(context.HttpContext.Response.StatusCode))
                {
                    if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var id = int.Parse(context.HttpContext.Request.Path.ToString().Split("/").Last());
                        var afterUpdate = _repository.GetById(id);
                        if (afterUpdate != null)
                        {
                            Product beforeUpdate;
                            if (_contextDict.TryGetValue(id, out beforeUpdate))
                            {
                                CustomLogs.SaveLog(afterUpdate.Id, "Product", afterUpdate.Description, context.HttpContext.Request.Method, beforeUpdate, afterUpdate);
                                _contextDict.Remove(id);
                            }
                        }
                    }
                    else if (context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var id = int.Parse(context.HttpContext.Request.Path.ToString().Split("/").Last());
                        Product beforeUpdate;
                        if (_contextDict.TryGetValue(id, out beforeUpdate))
                        {
                            CustomLogs.SaveLog(beforeUpdate.Id, "Product", beforeUpdate.Description, context.HttpContext.Request.Method);
                            _contextDict.Remove(id);
                        }
                    }
                }
            }
        }
    }
}
