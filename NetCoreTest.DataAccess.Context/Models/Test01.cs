using System;
using System.Collections.Generic;

namespace NetCoreTest.DataAccess.Context.Models
{
    public partial class Test01
    {
        public int Id { get; set; }
        public string? Nama { get; set; }
        public short? Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
