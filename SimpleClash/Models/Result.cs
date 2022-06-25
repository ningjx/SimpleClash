using System.Net;

namespace SimpleClash.Models
{
    public class Result<T>
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static Result<T> Success()
        {
            return new Result<T>()
            {
                Code = HttpStatusCode.OK
            };
        }

        public static Result<T> Success(string message)
        {
            return new Result<T>()
            {
                Code = HttpStatusCode.OK,
                Message = message
            };
        }

        public static Result<T> Success(string message, T data)
        {
            return new Result<T>()
            {
                Code = HttpStatusCode.OK,
                Message = message,
                Data = data
            };
        }

        public static Result<T> Error()
        {
            return new Result<T>()
            {
                Code = HttpStatusCode.BadRequest
            };
        }

        public static Result<T> Error(string message)
        {
            return new Result<T>()
            {
                Code = HttpStatusCode.BadRequest,
                Message = message
            };
        }

        public static Result<T> Error(string message, T data)
        {
            return new Result<T>()
            {
                Code = HttpStatusCode.BadRequest,
                Message = message,
                Data = data
            };
        }
    }
}
