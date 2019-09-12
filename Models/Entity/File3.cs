namespace convertTmplate.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File3
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string AccountCode { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public int? AccountType { get; set; }

        public DateTime? Date { get; set; }

        public int? CurrencyType { get; set; }
    }
}
