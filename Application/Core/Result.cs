namespace Application.Core
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }
        public bool Succeed { get; set; }

        public static Result<T> Failure(string errorMessage) => new() { ErrorMessage = errorMessage };
        public static Result<T> Success(T value) => new() { Value = value, Succeed = true }; 
    }
}
