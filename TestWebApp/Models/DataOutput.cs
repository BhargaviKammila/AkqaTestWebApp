using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Models
{
    public class DataOutput
    {
        public string Output { get; set; }
        // if "Error" is empty that means success
        public string Error { get; set; }
    }
}