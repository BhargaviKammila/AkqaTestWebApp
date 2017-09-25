using AttributeRouting;
using AttributeRouting.Web.Mvc;
using BusinessContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{    
    [RoutePrefix("api/NumToText")]
    public class NumToTextController : ApiController
    {
        [HttpPost]
        [Route("ConvertToText")]
        public DataOutput ConvertNumToText(DataInput data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.Number))
                {
                    return new DataOutput
                    {
                        Error = "Number should not be empty!"
                    };
                }
                double num;
                string error = string.Empty;
                if (double.TryParse(data.Number, out num))
                {
                    string result = ServiceLocator.GetInstance<INumberToText>().ConvertNumberToText(num);
                    return new DataOutput
                    {
                        Output = result
                    };
                }

                return new DataOutput{
                    Error = "Provided number is not valid"
                };                
            }
            catch (Exception ex)
            {
                return new DataOutput
                {
                    Error = string.Format("Some error occurred while converting number to text {0}", ex.Message)
                };
            }            
        }
    }
}
