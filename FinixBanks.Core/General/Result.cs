using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.Core.General
{
    public class Result<T>
    {        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public static Result<T> Success(T value) => new() { IsSuccess = true, StatusCode = (int)HttpStatusCode.OK, Value = value };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, StatusCode = (int)HttpStatusCode.BadRequest, Error = error };
        public static Result<T> UnAuthorized(string error) => new() { IsSuccess = false, StatusCode = (int)HttpStatusCode.Unauthorized };
    }
}
