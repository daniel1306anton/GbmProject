using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Entities.Common;
using Newtonsoft.Json;
using System;

namespace GBMProject.Framework.Json
{
    public class JsonDeserialize : IDeserialize
    {
        private const string FAILURE_DESERIALIZE_OBJECT = "Ocurrio un error al tratar de deserializar el archivo.";        
        public OperationResult<T> Execute<T>(string value)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(value);
                return new OperationResult<T>(obj);
            }
            catch (Exception ex)
            {                
                return new OperationResult<T>(ErrorDto.BuildTechnical(FAILURE_DESERIALIZE_OBJECT));
            }
        }       
    }
}
