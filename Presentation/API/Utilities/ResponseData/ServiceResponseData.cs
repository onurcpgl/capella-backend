namespace API.Utilities.ResponseData
{
    public class ServiceResponseData
    {
        public ProcessStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
