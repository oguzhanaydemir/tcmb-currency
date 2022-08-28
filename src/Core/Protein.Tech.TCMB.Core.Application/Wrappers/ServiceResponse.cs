namespace Protein.Tech.TCMB.Core.Application.Wrappers
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
        public ServiceResponse()
        {

        }
        public ServiceResponse(string message)
        {
            Message = message;
        }
        public ServiceResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public ServiceResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public ServiceResponse(T data)
        {
            IsSuccess = true;
            Data = data;
        }
        public ServiceResponse(T data, string message)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }
        public ServiceResponse(T data, bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }
    }
}
