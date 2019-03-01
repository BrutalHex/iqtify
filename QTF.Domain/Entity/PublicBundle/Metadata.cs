using QTF.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Domain.Entity.PublicBundle
{
    public class Metadata
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public MetaCategory Category { get; set; }
    }
}

namespace QTF.Data.Models
{
    public enum MetaCategory
    {
        Unknown,
        DatabaseStatus
    }
}