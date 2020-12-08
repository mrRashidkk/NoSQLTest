using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLTest.Entities
{
    public class EntityType
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public List<EntityAttribute> Attributes { get; set; }
    }
}
