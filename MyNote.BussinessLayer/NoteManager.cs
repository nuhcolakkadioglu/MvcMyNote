using MyNote.DataAccessLayer.EntityFramework;
using MyNote.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.BussinessLayer
{
   public class NoteManager
    {
        private static Repository<Note> _noteRepo = new Repository<Note>();

        public static List<Note> GetAllNote()
        {
            return _noteRepo.List();
        }

        public static IQueryable<Note> GetAllNoteQueryable()
        {
            return _noteRepo.ListQueryable();
        }
    }
}
