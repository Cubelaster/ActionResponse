using Microsoft.AspNetCore.Mvc;
using ReActionResponse.Core;

namespace ReActionResponse.Api
{
    public static partial class ActionResponseExtensions
    {
        public static ActionResult FinishAction<T>(this ActionResponse<T> actionResponse)
        {
            var response = new ObjectResult(actionResponse)
            {
                StatusCode = actionResponse.ResultCode
            };

            return response;
        }

        public static ActionResult FinishAction(this ActionResponse actionResponse)
        {
            var response = new ObjectResult(actionResponse)
            {
                StatusCode = actionResponse.ResultCode
            };

            return response;
        }
    }
}
