using ConnectApp.Shared.Types.Generics;

namespace ConnectApp.Shared.Results
{
    public class ResponseMessage
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public IList<KeyValue>? Values { get; set; }
        public Guid? Id { get; set; }
        public bool Status { get; set; } = false;
        public object? Data { get; set; }
    }
}
