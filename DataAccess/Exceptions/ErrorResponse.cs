using System;
namespace DataAccess.Exceptions
{
    public class ErrorDetailResponse
    {
        public int StatusCode { get; set; }

        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }
    public class ErrorResponse : Exception
    {
        public ErrorDetailResponse Error { get; private set; }
        public ErrorResponse(int statusCode, int errorCode, string message)
        {
            Error = new ErrorDetailResponse
            {
                StatusCode = statusCode,
                ErrorCode = errorCode,
                Message = message
            };
        }
    }
}

