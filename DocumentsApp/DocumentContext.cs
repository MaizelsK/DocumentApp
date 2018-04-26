using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DocumentsApp
{
    public class DocumentContext : DbContext
    {
        public DocumentContext() : base("DocumentDb")
        {
            Database.SetInitializer(new DocumentsInitializer());
        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Sign> Signs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }

    public class DocumentsInitializer : CreateDatabaseIfNotExists<DocumentContext>
    {
        protected override void Seed(DocumentContext context)
        {
            List<Person> people = new List<Person>
            {
                new Person{ FullName = "Pavel Petrov", BirthDate = new DateTime(1995, 10, 11)},
                new Person{ FullName = "Nikita Ivanov", BirthDate = new DateTime(1997, 8, 4)}
            };

            User user = new User
            {
                Login = "admin",
                Password = "qwerty",
            };

            Document document = new Document
            {
                Theme = "Покупка автомобиля",
                Type = "Договор",
                CreateDate = new DateTime(2018, 4, 20),
                ReviewDate = DateTime.Now,
                Creator = user
            };

            user.Documents.Add(document);

            List<Sign> signs = new List<Sign>
            {
                new Sign{ Signer = people[0], SignedDocument = document},
                new Sign{ Signer = people[1], SignedDocument = document}
            };

            context.Documents.Add(document);
            context.Users.Add(user);
            context.People.AddRange(people);
            context.Signs.AddRange(signs);

            base.Seed(context);
        }
    }
}
