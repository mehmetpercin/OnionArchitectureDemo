namespace Application.Dtos
{
    public class SuccessResponse<T> : Response<T>
    {
        public T Data { get; set; }

        public static Response<T> Success(T data, int statusCode)
        {
            return new SuccessResponse<T> { Data = data, StatusCode = statusCode };
        }

        public static Response<T> Success(int statusCode)
        {
            return new SuccessResponse<T> { Data = default, StatusCode = statusCode };
        }
    }
}
