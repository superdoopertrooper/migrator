using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            SyncManager syncManager = new SyncManager();
            syncManager.StartSync();
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }

    public class SyncManager
    {
        public SyncManager()
        {

        }

        public void StartSync()
        {
            OracleDbContext oracleDbContext = new OracleDbContext();
            Console.WriteLine(oracleDbContext.SUBJECTS.Count().ToString());
            oracleDbContext.SUBJECTS.ForEachAsync(r => Console.WriteLine($"{r.NAME} {r.CREATED_DATE}{r.UPDATED_DATE}"));
            Console.WriteLine(oracleDbContext.SUBJECT_PIXS.Count().ToString());
        }

    }


    public class OracleDbContext : DbContext
    {
        public OracleDbContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SYSTEM");
            Database.SetInitializer<OracleDbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SUBJECTS_V>().ToTable("SUBJECTS_V");
            modelBuilder.Entity<TRANSACTION>().ToTable("TRANSACTION");
            modelBuilder.Entity<SUBJECT_PIX>().ToTable("SUBJECT_PIX");
        }

        public DbSet<SUBJECTS_V> SUBJECTS { get; set; }
        public DbSet<TRANSACTION> TRANSACTIONS { get; set; }
        public DbSet<SUBJECT_PIX> SUBJECT_PIXS { get; set; }
        public class SUBJECTS_V
        {
            public int ID { get; set; }
            public string NAME { get; set; }
            public DateTime CREATED_DATE { get; set; }
            public DateTime UPDATED_DATE { get; set; }

        }
        public class SUBJECT_PIX
        {
            public int ID { get; set; }
            public string NAME { get; set; }
 

        }
        public class TRANSACTION
        {
            [Key]
            public int ID { get; set; }
            public string TYPE { get; set; }
            public string MESSAGE { get; set; }
            public DateTime CREATED_DATE { get; set; }
        }
    }
}
