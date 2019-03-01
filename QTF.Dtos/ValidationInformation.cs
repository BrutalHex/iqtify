using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Dtos
{
    public class ValidationInformation
    {
        public bool Success { get; set; }

        public string[] Validation { get; set; }

        public string Message
        {
            get { return string.Join(",", Validation); }
        }

        public dynamic Entity { get; set; }
    }
}
