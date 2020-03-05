namespace GBMProject.Entities.Common
{
    public class ErrorDto
    {

        private const ushort InternalError = 1;

        private const ushort ExternalError = 2;

        public string Message { get; set; }
        public string Code { get; set; }
        public ushort Type { get; set; }
        public bool IsWarning { get; set; }

        public ErrorDto()
        { }


        public ErrorDto(string code, string message, ushort type, bool isWarning = false)
        {
            Code = code;
            Message = message;
            Type = type;
            IsWarning = isWarning;
        }

        public static ErrorDto BuildTechnical(string message, string code = null)
        {
            return new ErrorDto(code ?? "TE001", message, InternalError);
        }
        public static ErrorDto BuildUser(string message, string code = null)
        {
            return new ErrorDto(code ?? "BE001", message, ExternalError);

        }        
    }
}
