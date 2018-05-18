using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Threading;
using System.Data.Entity;

namespace DocumentsApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ListView.View = CreatePeopleGridView();
            //ListView.ItemsSource = GetDocumentsInfo();

            List<Person> persons;

            using (var context = new DocumentContext())
            {
                persons = context.People.Include(p => p.Signs).ToList();
            }

            ListView.ItemsSource = persons.AsParallel().Where(p => p.Signs.Count > 3).ToList();
        }

        public GridView CreatePeopleGridView()
        {
            GridView gridView = new GridView();

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Полное имя",
                DisplayMemberBinding = new Binding("FullName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Дата рождения",
                DisplayMemberBinding = new Binding("BirthDate")
            });

            return gridView;
        }

        public GridView CreateGridView()
        {
            GridView gridView = new GridView();

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Тема документа",
                DisplayMemberBinding = new Binding("DocumentTheme")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Создатель",
                DisplayMemberBinding = new Binding("Creator")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Дата создания",
                DisplayMemberBinding = new Binding("CreatedDate"),
                HeaderStringFormat = "d"
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Кем подписан",
                DisplayMemberBinding = new Binding("Signs")
            });

            return gridView;
        }

        public List<DocumentInfo> GetDocumentsInfo()
        {
            List<DocumentInfo> documentsInfo = new List<DocumentInfo>();

            using (var context = new DocumentContext())
            {
                foreach (var document in context.Documents)
                {
                    var documentInfo = new DocumentInfo
                    {
                        DocumentTheme = document.Theme,
                        CreatedDate = document.CreateDate,
                        Creator = document.Creator.Login,
                    };
                    documentInfo.InsertSigns(document.Signs);

                    documentsInfo.Add(documentInfo);
                }
            }

            return documentsInfo;
        }
    }
}
