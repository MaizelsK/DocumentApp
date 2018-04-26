using System;
using System.Collections.Generic;

namespace DocumentsApp
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<Sign> Signs { get; set; }

        public Document()
        {
            Id = Guid.NewGuid();
            Signs = new List<Sign>();
        }
    }
}
