using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLTest.Entities
{
    public class EntityAttribute
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string InnerLabel { get; set; }
        public Guid AttributeTypeId { get; set; }
        public AttributeType AttributeType { get; set; }
        public Guid EntityTypeId { get; set; }
        public EntityType EntityType { get; set; }
    }
}
