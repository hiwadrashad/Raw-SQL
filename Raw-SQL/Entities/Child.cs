using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raw_SQL.Entities
{
    public class Child
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Child()
        {
            Id = Guid.NewGuid();
        }
    }
}
