namespace ConnectApp.Shared.Results
{

    public class Result<T>
    {


        public T? Value { get; set; }
        public bool HasErrors { get; set; } = true;
        public bool HasSucess { get; set; } = false;
        public IList<string> Messages { get; } = [];

        public void AddMessages(string message) { }

        //public static Result<T> Success(string message = "") { ... }
        //public static Result<T> Success(T value, string message = "") { ... }
        //public static Result<T> Failure(string message) { ... }
        //public static Result<T> Failure(T value, string message) { ... }


        //-------------------------------// PROPERTIES //-------------------------------//
        public bool Success { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; } = [];

        private Result() { }

        public static Result<T> Ok(T data, string message = "")
            => new() { Success = true, Data = data, Message = message };

        public static Result<T> Failure(string message, params string[] errors)
            => new() { Success = false, Message = message, Errors = errors?.ToList() ?? new List<string>() };
    }
}


