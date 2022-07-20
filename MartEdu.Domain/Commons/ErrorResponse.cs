using Newtonsoft.Json;


namespace MartEdu.Domain.Commons
{
    public class ErrorResponse
    {
        [JsonIgnore]
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
