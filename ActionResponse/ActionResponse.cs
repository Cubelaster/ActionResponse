namespace ActionResponse
{
    public class ActionResponse
    {
        public string? Message { get; set; }
        public ActionResponseType ActionResponseType { get; set; }
        public int? ResultCode { get; set; }        
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
    }
}
