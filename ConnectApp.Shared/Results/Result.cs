namespace ConnectApp.Shared.Results
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public bool HasErrors => Errors.Count != 0;
        public List<string> Errors { get; } = [];
        public List<string> Successes { get; } = [];

        public void AddError(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Errors.Add(message);
        }

        public void AddSuccess(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Successes.Add(message);
        }

        public static Result<T> Success(T value, string message = "")
        {
            var result = new Result<T> { Value = value };
            if (!string.IsNullOrWhiteSpace(message))
                result.AddSuccess(message);
            return result;
        }

        public static Result<T> Failure(string message)
        {
            var result = new Result<T>();
            result.AddError(message);
            return result;
        }
    }
}
