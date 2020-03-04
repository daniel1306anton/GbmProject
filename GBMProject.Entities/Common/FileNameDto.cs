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
        
        public bool IsValid { get; set; }
        public SellOrdersRequestDto SellOrdersRequest { get; set; }
        public SellOrderResponseDto SellOrderResponse { get; set; }
        public string BuildNameResponse()
        {
            var dateTimeStr = DateTime.Now.ToString("yyMMddHHmmss");
            return FileInfoName + dateTimeStr + "_RES";
        }
        
        public string BuildNameErrorResponse()
        {
            var dateTimeError = DateTime.Now.ToString("yyMMddHHmmss");
            return FileInfoName + dateTimeError + "_RES.ERR";           
        }      
    }
}
