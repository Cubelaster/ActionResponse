﻿using Microsoft.AspNetCore.Mvc;

namespace ActionResponse
{
    public static partial class ActionResponseExtensions
    {
        public static ActionResponse Success(string? Message = null, int? Code = 200)
        {
            return new ActionResponse
            {
                Message = Message,
                ActionResponseType = ActionResponseType.Success,
                ResultCode = Code
            };
        }

        public static ActionResponse Error(string? Message = null, int? Code = 500)
        {
            return new ActionResponse
            {
                Message = Message,
                ActionResponseType = ActionResponseType.Error,
                ResultCode = Code
            };
        }

        public static ActionResponse Warning(string? Message = null, int? Code = 299)
        {
            return new ActionResponse
            {
                Message = Message,
                ActionResponseType = ActionResponseType.Warning,
                ResultCode = Code
            };
        }

        public static ActionResponse<T> Success<T>(T? Data = default, string? Message = null, int? Code = 200)
        {
            return new ActionResponse<T>
            {
                Data = Data != null ? Data : default,
                Message = Message,
                ActionResponseType = ActionResponseType.Success,
                ResultCode = Code
            };
        }

        public static ActionResponse<T> Error<T>(T? Data = default, string? Message = null, int? Code = 500)
        {
            return new ActionResponse<T>
            {
                Data = Data != null ? Data : default,
                Message = Message,
                ActionResponseType = ActionResponseType.Error,
                ResultCode = Code
            };
        }

        public static ActionResponse<T> Warning<T>(T? Data = default, string? Message = null, int? Code = 299)
        {
            return new ActionResponse<T>
            {
                Data = Data != null ? Data : default,
                Message = Message,
                ActionResponseType = ActionResponseType.Warning,
                ResultCode = Code
            };
        }

        public static T? GetData<T>(this ActionResponse<T> actionResponse)
        {
            return actionResponse.Data;
        }

        public static bool IsSuccess(this ActionResponse actionResponse)
        {
            return actionResponse.ActionResponseType == ActionResponseType.Success;
        }

        public static bool IsSuccess<T>(this ActionResponse<T> actionResponse, out T? Data)
        {
            Data = actionResponse.Data;
            return actionResponse.ActionResponseType == ActionResponseType.Success;
        }

        public static bool IsSuccessAndHasData<T>(this ActionResponse<T> actionResponse, out T? Data)
        {
            Data = actionResponse.Data;
            return actionResponse.ActionResponseType == ActionResponseType.Success && Data != null;
        }

        public static bool IsNotSuccess(this ActionResponse response)
        {
            return response.ActionResponseType != ActionResponseType.Success;
        }

        public static bool IsNotSuccess(this ActionResponse response, out ActionResponse actionResponse)
        {
            actionResponse = response;
            return response.ActionResponseType != ActionResponseType.Success;
        }

        public static bool IsNotSuccess<T>(this ActionResponse<T> response, out ActionResponse<T> actionResponse, out T? Data)
        {
            actionResponse = response;
            Data = actionResponse.Data != null ? actionResponse.Data : default;
            return response.ActionResponseType != ActionResponseType.Success;
        }

        public static bool IsNotSuccess<T>(this ActionResponse<T> response)
        {
            return response.ActionResponseType != ActionResponseType.Success;
        }

        public static bool IsNotSuccess<T>(this ActionResponse<T> response, out ActionResponse<T> actionResponse)
        {
            actionResponse = response;
            return response.ActionResponseType != ActionResponseType.Success;
        }

        public static ActionResponse SetErrorMessage(this ActionResponse response, string errorMessage)
        {
            if (response.IsNotSuccess())
            {
                response.AppendErrorMessage(errorMessage);
                return response;
            }

            response.ActionResponseType = ActionResponseType.Error;
            response.ResultCode = 500;
            response.Message = errorMessage;
            return response;
        }

        public static ActionResponse<T> SetErrorMessage<T>(this ActionResponse<T> response, string errorMessage)
        {
            if (response.IsNotSuccess())
            {
                response.AppendErrorMessage(errorMessage);
                return response;
            }

            response.ActionResponseType = ActionResponseType.Error;
            response.ResultCode = 500;
            response.Message = errorMessage;
            return response;
        }

        public static ActionResponse AppendErrorMessage(this ActionResponse response, string errorMessage)
        {
            response.ActionResponseType = ActionResponseType.Error;
            response.ResultCode = 500;
            response.Message += string.IsNullOrEmpty(response.Message)
                || response.Message.EndsWith(Environment.NewLine)
                ? errorMessage : Environment.NewLine + errorMessage;
            return response;
        }

        public static ActionResponse<T> AppendErrorMessage<T>(this ActionResponse<T> response, string errorMessage)
        {
            response.ActionResponseType = ActionResponseType.Error;
            response.ResultCode = 500;
            response.Message += string.IsNullOrEmpty(response.Message)
                || response.Message.EndsWith(Environment.NewLine)
                ? errorMessage : Environment.NewLine + errorMessage;
            return response;
        }

        public static ActionResponse AppendMessage(this ActionResponse response, string message)
        {
            response.Message += string.IsNullOrEmpty(response.Message)
                || response.Message.EndsWith(Environment.NewLine)
                ? message : Environment.NewLine + message;
            return response;
        }

        public static ActionResponse<T> AppendMessage<T>(this ActionResponse<T> response, string message)
        {
            response.Message += string.IsNullOrEmpty(response.Message)
                || response.Message.EndsWith(Environment.NewLine)
                ? message : Environment.NewLine + message;
            return response;
        }

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