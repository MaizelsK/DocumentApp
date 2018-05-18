using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DocumentsApp
{
    public class DocumentContext : DbContext
    {
        public DocumentContext() : base("DocumentDb")
        {
            Database.SetInitializer(new DocumentsInitializer());
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class DocumentsInitializer : CreateDatabaseIfNotExists<DocumentContext>
    {
        protected override void Seed(DocumentContext context)
        {
            Random random = new Random();

            string[] names = new string[]
            {
                "Nikita", "Pavel", "Konstantin", "Aleksand",
                "Anton", "Kirill", "Maxim", "Nikolay",
                "Bronislav", "Evlampiy", "Osip", "Kir",
                "Sokrat", "Vladislav", "Kim", "Fadey"
            };

            string[] lastNames = new string[]
            {
                "Petrov", "Pirozhkov", "Plushkin", "Antonov",
                "Aleeksandrov", "Zemlyakov", "Elinskiy", "Hahalin",
                "Zubarev", "Kosinov", "Fokin", "Tereshin",
                "Kruglikov", "Kanadcev"
            };

            List<Person> people = new List<Person>();

            for (int i = 0; i < 40; i++)
            {
                string randomName = names[random.Next(0, names.Length - 1)];
                string randomLastname = lastNames[random.Next(0, lastNames.Length - 1)];

                people.Add(new Person
                {
                    FullName = randomName + " " + randomLastname,
                    BirthDate = new DateTime(random.Next(1970, 2000), random.Next(1, 12), random.Next(1, 28))
                });
            }

            User user = new User
            {
                Login = "admin",
                Password = "qwerty",
            };

            string[] documentTypes = new string[]
            {
                "Акт", "Договор", "Приказ", "Входящий документ",
                "Исходящий документ"
            };

            List<Document> documents = new List<Document>();

            for (int i = 0; i < 15; i++)
            {
                string randomType = documentTypes[random.Next(1, documentTypes.Length - 1)];

                documents.Add(new Document
                {
                    Theme = "Документ №" + i,
                    Type = randomType,
                    CreateDate = new DateTime(2018, 5, random.Next(1, 18)),
                    ReviewDate = DateTime.Now,
                    Creator = user
                });
            };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    Document randomDocument = documents[random.Next(1, documents.Count - 1)];

                    List<Sign> documentSigns = randomDocument.Signs as List<Sign>;
                    Sign existedSign = documentSigns.SingleOrDefault(x => x.Signer == people[j]);

                    if (existedSign == null)
                    {
                        randomDocument.Signs.Add(new Sign
                        {
                            Id = Guid.NewGuid(),
                            SignedDocument = randomDocument,
                            Signer = people[j]
                        });
                    }
                };
            }

            context.Documents.AddRange(documents);
            context.Users.Add(user);
            context.People.AddRange(people);

            base.Seed(context);
        }
    }
}
