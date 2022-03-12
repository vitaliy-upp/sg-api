namespace Domain.BusinessLogic.Models
{
    public class ApiResponse<T> where T : class
    {
        public T Data { get; set; }
        public ApiErrorResponse Error { get; private set; }


        #region Contructors
        public ApiResponse()
        { }

        #endregion


        public void SetErrorMsg(string errorMessage)
        {
            Error = new ApiErrorResponse(errorMessage);
        }
    }
}
