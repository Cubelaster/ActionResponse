namespace ReActionResponse.Core
{
    public class ActionResponse
    {
        public string? Message { get; set; }
        public ActionResponseType ActionResponseType { get; set; }
        public int? ResultCode { get; set; }

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
            return ActionResponse<T>.Success(Data, Message, Code);
        }

        public static ActionResponse<T> Error<T>(T? Data = default, string? Message = null, int? Code = 500)
        {
            return ActionResponse<T>.Error(Data, Message, Code);
        }

        public static ActionResponse<T> Warning<T>(T? Data = default, string? Message = null, int? Code = 299)
        {
            return ActionResponse<T>.Warning(Data, Message, Code);
        }
    }

    public class ActionResponse<T> : ActionResponse
    {
        public ActionResponse() { }

        public ActionResponse(ActionResponse actionResponse)
        {
            ActionResponseType = actionResponse.ActionResponseType;
            Message = actionResponse.Message;
            ResultCode = actionResponse.ResultCode;
        }

        public T? Data { get; set; }

        public static ActionResponse<T> Success(T? Data = default, string? Message = null, int? Code = 200)
        {
            return new ActionResponse<T>
            {
                Data = Data != null ? Data : default,
                Message = Message,
                ActionResponseType = ActionResponseType.Success,
                ResultCode = Code
            };
        }

        public static ActionResponse<T> Error(T? Data = default, string? Message = null, int? Code = 500)
        {
            return new ActionResponse<T>
            {
                Data = Data != null ? Data : default,
                Message = Message,
                ActionResponseType = ActionResponseType.Error,
                ResultCode = Code
            };
        }

        public static ActionResponse<T> Warning(T? Data = default, string? Message = null, int? Code = 299)
        {
            return new ActionResponse<T>
            {
                Data = Data != null ? Data : default,
                Message = Message,
                ActionResponseType = ActionResponseType.Warning,
                ResultCode = Code
            };
        }
    }
}
