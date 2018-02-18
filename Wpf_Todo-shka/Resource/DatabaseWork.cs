using System.Linq;
using System.Data.Linq;    // DataContext
using System.Data.Linq.Mapping; // []
using System.Windows.Documents;
using System.Diagnostics;
using System.Configuration;
using System;
using System.Windows;


namespace Wpf_Todo_shka.Resource
{

    // class for working with database
    [Table(Name = "Excercises")]
    public class Exercises
    {
        //static string connectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; AttachDbFilename=D:\C#\Todo_shka\Wpf_Todo-shka\Wpf_Todo-shka\DB_TODO.mdf;Integrated Security=True";
        static string connectionString = ConfigurationManager.ConnectionStrings["database"].ToString();

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]      // IsDgGenerated for generated ofrder Id
        public int Id { set; get; }

        [Column(Name = "content")]
        public string content { set; get; }

        [Column]
        public string status { set; get; }

        DataContext db;                             // Connecting with databbase
        Table<Exercises> excercise;               // For GetTable

        public Exercises()
        {
            db = new DataContext(connectionString);
            excercise = db.GetTable<Exercises>();

        }

        public IQueryable<Exercises> Select_Done()
        {
            var query = from n in excercise
                        where n.status == "done"
                        select n;
            return query;
        }

        public IQueryable<Exercises> Select_Deleted()
        {
            return excercise.Where(n => n.status == "deleted");
        }

        public IQueryable<Exercises> Select_Executing()
        {
            return excercise.Where(n => n.status == "executing");
        }

        // for enumerating rows
        public void FOREACH(IQueryable<Exercises> query)
        {
            foreach (var excer in query)
            {
                Debug.WriteLine($"{excer.Id}, {excer.content} , {excer.status}");
            }
        }

        public void AddRow(string _content, string _status = "executing")
        {
            if (!IsSame(_content))
            {
                Exercises ex = new Exercises { content = _content, status = _status };
                excercise.InsertOnSubmit(ex);                                               // Insert in table
                db.SubmitChanges();                                                         // send changes
                ex.DBClose();
            }
            else
            {
                MessageBox.Show("Дане завдання існує, введіть нове завдання!");
            }
            
        }

        public void DoneExcercise(string _content)
        {
            Exercises esc = excercise.FirstOrDefault(n => n.content == _content);
            esc.status = "done";
            db.SubmitChanges();
            esc.DBClose();
            
        }

        public void DeleteExcercise(string _content)
        {
            Exercises esc = excercise.FirstOrDefault(n => n.content == _content);
            esc.status = "deleted";
            db.SubmitChanges();
            esc.DBClose();
        }

        bool IsSame(string _content)
        {
            Exercises isRow =  excercise.FirstOrDefault(c => c.content == _content);
            try
            {
                int n = isRow.Id;
                return true;
            }
            catch(NullReferenceException)
            {
                return false;
            }
        }

        // for cleaning table
        public void DBClear()
        {
            IQueryable<Exercises> list = excercise.Select(n => n);
            excercise.DeleteAllOnSubmit(list);
            db.SubmitChanges();
        }

        public void DBClose()
        {
            db.Dispose();
        }
    }
}
