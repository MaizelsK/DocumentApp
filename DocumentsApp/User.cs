using System;
using System.Collections.Generic;

namespace DocumentsApp
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Documents = new List<Document>();
        }
    }
}
