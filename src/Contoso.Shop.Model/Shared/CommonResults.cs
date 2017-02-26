using Contoso.Shop.Model.Shared.Resources;

namespace Contoso.Shop.Model.Shared
{
    public static class CommonResults
    {
        public static Result<T> NotFound<T>(int id)
        {
            var message = string.Format(Messages.EntityNotFound, typeof(T).Name, id);

            return Result.Fail<T>(message, ResultCode.NotFound);
        }


        public static Result<T> EntityNotFoundToRemove<T>(int id)
        {
            var message = string.Format(Messages.EntityNotFoundToRemove, typeof(T).Name, id);

            return Result.Fail<T>(message);
        }
    }
}