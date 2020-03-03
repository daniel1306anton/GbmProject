using GBMProject.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Entities.Response
{
    public class RobotResponseDto
    {
        public RobotResponseDto(List<ErrorDto> errorList = null)
        {
            Success = errorList == null && errorList.Any();
            ErrorList = errorList;
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
        public bool Success { get; set; }
        public List<ErrorDto> ErrorList { get; set; }
        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
        public void AddMessages(IEnumerable<string> messages)
        {
            Messages.AddRange(messages);
        }
        public void AddError(ErrorDto error)
        {
            Success = false;
            ErrorList = ErrorList ?? new List<ErrorDto>();
            ErrorList.Add(error);
        }
        public void AddErrorList(List<ErrorDto> errorList)
        {
            Success = false;
            ErrorList = ErrorList ?? new List<ErrorDto>();
            ErrorList.AddRange(errorList);
        }

    }
}
