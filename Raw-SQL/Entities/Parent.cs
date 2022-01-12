using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raw_SQL.Entities
{
    public class Parent
    {
        public Guid Id { get; set; }
        public Child Child { get; set; }
        public string Text {get;set;}
        public Parent()
        {
            Id = Guid.NewGuid();
        }
    }
}
