using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using xf03notesSql.Models;

namespace xf03notesSql.Data
{
  public class NoteDatabase
  {
    readonly SQLiteAsyncConnection _db;

    public NoteDatabase(string dbPath)
    {
      _db = new SQLiteAsyncConnection(dbPath);
      _db.CreateTableAsync<Note>().Wait();
    }

    public Task<List<Note>> GetNotesAsync()
    {
      return _db.Table<Note>().ToListAsync();
    }

    public Task<Note> GetNoteAsync(int id)
    {
      return _db.Table<Note>()
        .Where(i => i.ID == id)
        .FirstOrDefaultAsync();
    }

    public Task SaveNoteAsync(Note note)
    {
      if(note.ID != 0)
      {
        return _db.UpdateAsync(note);
      }
      else
      {
        return _db.InsertAsync(note);
      }
    }

    public Task<int> DeleteNoteAsync(Note note)
    {
      return _db.DeleteAsync(note);
    }

    public void close()
    {
      //_db.GetConnection().Close();
      //_db.GetConnection().Dispose();

      //GC.Collect();
      //GC.WaitForPendingFinalizers();
    }
  }
}
