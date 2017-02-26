using System;
using System.Threading.Tasks;
using Contoso.Shop.Infra.Shared.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Contoso.Shop.Api.Shared.Filters
{
    public class DataContextTransactionFilter : IAsyncActionFilter
    {
        private readonly ShopDataContext dataContext;

        public DataContextTransactionFilter(ShopDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var dontUseTransaction = method.Equals("GET") || method.Equals("HEAD") || method.Equals("OPTIONS");

            if (dontUseTransaction)
            {
                await next();

                return;
            }

            try
            {
                dataContext.BeginTransaction();

                await next();

                await dataContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                dataContext.RollbackTransaction();

                throw;
            }
        }
    }
}