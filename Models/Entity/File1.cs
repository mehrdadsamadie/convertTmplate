namespace convertTmplate.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;

    public partial class File1
    {
        public int Id { get; set; }

        [StringLength(128)]
        [ValidIdentifier]
        public string Identifier { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public int? AccountType { get; set; }

        public DateTime? Opened { get; set; }

        public int? CurrencyType { get; set; }
    }
    public class ValidIdentifier : ValidationAttribute
    {
        protected override ValidationResult
            IsValid(object value, ValidationContext validationContext)
        {
            var array = value.ToString().Split('|');
            if (array.Length == 2 && Regex.IsMatch(array[0], @"^\d+$") && Regex.IsMatch(array[1], @"^[a-zA-Z]+$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Noop");
        }
    }
}
