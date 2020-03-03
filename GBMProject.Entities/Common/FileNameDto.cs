using GBMProject.Entities.Request;
using GBMProject.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Entities.Common
{
    public class FileNameDto
    {
        public FileNameDto()
        {

        }
        public FileNameDto(string fileInfoName)
        {
            FileInfoName = fileInfoName;
        }
        public string FileInfoName { get; set; }
        public string FileInfoNameResponse { get; set; }
        public string FileInfoNameResponseError { get; set; }
        public bool IsValid { get; set; }
        public SellOrdersRequestDto SellOrdersRequest { get; set; }
        public SellOrderResponseDto SellOrderResponse { get; set; }
        public void BuildNameResponse()
        {
            var dateTimeStr = DateTime.Now.ToString("yyMMddHHmmss");
            FileInfoNameResponse = FileInfoName + dateTimeStr + "_RES";
        }
        
        public void BuildNameErrorResponse()
        {
            var dateTimeError = DateTime.Now.ToString("yyMMddHHmmss");
            FileInfoNameResponseError = FileInfoName + dateTimeError + "_RES.ERR";           
        }      
    }
}
