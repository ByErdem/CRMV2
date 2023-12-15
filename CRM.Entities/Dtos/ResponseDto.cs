using CRM.Shared.Utilities.ComplexTypes;

namespace CRM.Entities.Dtos
{
    public class ResponseDto
    {
        public object Data { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ErrorMessages { get; set; }

        public ResponseDto()
        {
        }

        public ResponseDto(object data, ResultStatus resultStatus = 0, string successMessage = "")
        {
            Data = data;
            ResultStatus = resultStatus;
            SuccessMessage = successMessage;
        }

        public ResponseDto(string errorMessage)
        {
            ResultStatus = ResultStatus.Error;
            ErrorMessage = errorMessage;
        }

        public ResponseDto(List<string> errorMessages)
        {
            ResultStatus = ResultStatus.Error;
            ErrorMessages = errorMessages;
        }
    }

    public class ResponseDto<T> : ResponseDto
    {
        public new T Data
        {
            get { return (T)base.Data; }
            set { base.Data = value; }
        }

        public ResponseDto() : base()
        {
        }

        public ResponseDto(T data, ResultStatus resultStatus = 0, string successMessage = "")
            : base(data, resultStatus, successMessage)
        {
        }

        public ResponseDto(string errorMessage)
            : base(errorMessage)
        {
        }

        public ResponseDto(List<string> errorMessages)
            : base(errorMessages)
        {
        }
    }
}
