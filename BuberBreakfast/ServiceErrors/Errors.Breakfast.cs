using ErrorOr;
namespace BuberBreakfast.ServiceErrors;

public class Errors
{
    public static class Breakfast
    {
        //expected errors from breakfast service
        //public const string NotFound = "breakfasts/not-found";
        public static Error NotFound => Error.NotFound(
            code:"breakfasts.not-found",
            description:"The breakfast was not found"
            );
    }

}