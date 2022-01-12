using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raw_SQL.Entities
{
    public class GrandParent
    {
        public Guid Id { get; set; }
        public List<Parent> Parents { get; set; }
        public string Text { get; set; }
        public GrandParent()
        {
            Id = Guid.NewGuid();
        }
    }
}
