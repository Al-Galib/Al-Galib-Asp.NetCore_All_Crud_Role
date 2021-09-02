using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.Utilities
{
    public class ValidEmailDomainAttribute:ValidationAttribute
    {
        private readonly string allawedDomain;

        public ValidEmailDomainAttribute( string allawedDomain)
        {
            this.allawedDomain = allawedDomain;
        }
        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == allawedDomain.ToUpper();
        }
    }
}
