namespace ConnectApp.Shared.Results
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public bool HasErrors { get; set; } = true;

        public bool HasSucess { get; set; } = false;
        public IList<string> Messages { get; } = [];


        //public void AddError(string message)
        //{
        //    if (!string.IsNullOrWhiteSpace(message))
        //        Messages.Add(message);
        //}

        public void AddMessages(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Messages.Add(message);

        }

        public static Result<T> Success(string message = "")
        {
            var result = new Result<T> { HasSucess = true, HasErrors = false };
            result.AddMessages(message);

            return result;
        }
        public static Result<T> Success(T value, string message = "")
        {
            var result = new Result<T> { Value = value, HasSucess = true, HasErrors = false };
            result.AddMessages(message);

            return result;
        }

        public static Result<T> Failure(string message)
        {
            var result = new Result<T>();
            result.AddMessages(message);
            return result;
        }
        public static Result<T> Failure(T value, string message)
        {
            var result = new Result<T>();
            result.AddMessages(message);
            return result;
        }
    }
}
