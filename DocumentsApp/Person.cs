using System;
using System.Collections.Generic;

namespace DocumentsApp
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Sign> Signs { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
            Signs = new List<Sign>();
        }
    }
}
