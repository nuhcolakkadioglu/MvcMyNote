using MyNote.DataAccessLayer.EntityFramework;
using MyNote.Enties;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyNote.DataAccessLayer.MySql
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContextMySQL: DbContext
    {
        public DbSet<NoteUser> NoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DatabaseContextMySQL()
        {
            Database.SetInitializer(new MyInitializerMySQL());
        }
    }
}
