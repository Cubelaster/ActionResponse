using Microsoft.AspNetCore.Mvc;
using ReActionResponse.Core;

namespace ReActionResponse.Api
{
    public static partial class ActionResponseExtensions
    {
        public static ActionResult FinishAction<T>(this ActionResponse<T> actionResponse)
        {
            // 204 has to be empty, otherwise it's error on response
            if (actionResponse.ResultCode == 204)
            {
                return new NoContentResult();
            }

            var response = new ObjectResult(actionResponse)
            {
                StatusCode = actionResponse.ResultCode
            };

            return response;
        }

        public static ActionResult FinishAction(this ActionResponse actionResponse)
        {
            // 204 has to be empty, otherwise it's error on response
            if (actionResponse.ResultCode == 204)
            {
                return new NoContentResult();
            }

            var response = new ObjectResult(actionResponse)
            {
                StatusCode = actionResponse.ResultCode
            };

            return response;
        }

        public static IResult ResultsFinishAction<T>(this ActionResponse<T> actionResponse)
        {
            // 204 has to be empty, otherwise it's error on response
            return actionResponse.ResultCode == 204
                ? Results.NoContent()
                : Results.Json(data: actionResponse, statusCode: actionResponse.ResultCode);
        }

        public static IResult ResultsFinishAction(this ActionResponse actionResponse)
        {
            // 204 has to be empty, otherwise it's error on response
            return actionResponse.ResultCode == 204
                ? Results.NoContent()
                : Results.Json(data: actionResponse, statusCode: actionResponse.ResultCode);
        }

        public static IResult TypedResultsFinishAction<T>(this ActionResponse<T> actionResponse)
        {
            // 204 has to be empty, otherwise it's error on response
            return actionResponse.ResultCode == 204
                ? TypedResults.NoContent()
                : TypedResults.Json<ActionResponse<T>>(data: actionResponse, statusCode: actionResponse.ResultCode);
        }

        public static IResult TypedResultsFinishAction(this ActionResponse actionResponse)
        {
            // 204 has to be empty, otherwise it's error on response
            return actionResponse.ResultCode == 204
                ? TypedResults.NoContent()
                : TypedResults.Json<ActionResponse>(data: actionResponse, statusCode: actionResponse.ResultCode);
        }
    }
}
