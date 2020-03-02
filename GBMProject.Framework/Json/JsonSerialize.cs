using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Entities.Common;
using Newtonsoft.Json;
using System;

namespace GBMProject.Framework.Json
{
    public class JsonSerialize : ISerialize
    {
        private const string FAILURE_SERIALIZE_OBJECT = "Ocurrio un error al tratar de serializar el archivo.";
        public OperationResult<string> Execute<T>(T objectDto)
        {
            try
            {
                return new OperationResult<string>(JsonConvert.SerializeObject(objectDto));
            }
            catch (Exception)
            {
                return new OperationResult<string>(ErrorDto.BuildTechnical(FAILURE_SERIALIZE_OBJECT));
            }
        }
    }
}
