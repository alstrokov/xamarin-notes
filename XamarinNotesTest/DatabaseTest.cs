using NUnit.Framework;
using System;
using System.IO;
using xf03notesSql.Models;

namespace XamarinNotesTest
{
  public class DatabaseTests
  {
    private xf03notesSql.Data.NoteDatabase db;
    private string dbFilename = "notes.db3";

    [SetUp]
    public void Setup()
    {
      db = new xf03notesSql.Data.NoteDatabase(dbFilename);
    }

    [Test]
    public void CreationTest()
    {
      Assert.IsNotNull(db);
    }

    [Test]
    public void AddNoteTest()
    {
      var note = new Note { Text = "First note" };
      db.SaveNoteAsync(note).Wait();

      var notesList = db.GetNotesAsync().Result;
      Assert.Greater(notesList.Count, 1);
      var noteReaded = notesList[0];
      Assert.AreEqual(note.Text, noteReaded.Text);
    }
  }
}