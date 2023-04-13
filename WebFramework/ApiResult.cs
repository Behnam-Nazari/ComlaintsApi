namespace WebFramework
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }


    }

    public class ApiResult<TData>: ApiResult
    {
        public TData Data { get; set;}


        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success,
                Message = "Success",
                Data = data
            };
        }

    }
}