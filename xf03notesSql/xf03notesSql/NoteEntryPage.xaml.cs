using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xf03notesSql.Models;

namespace xf03notesSql
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NoteEntryPage : ContentPage
  {
    public NoteEntryPage()
    {
      InitializeComponent();
    }

    private void showMarkdown()
    {
      var src = new HtmlWebViewSource
      {
        Html = Markdig.Markdown.ToHtml(edNote.Text)
      };

      wvMarkdown.Source = src;

      btnSaveEdit.Text = "Edit";
      edNote.IsVisible = false;
      wvMarkdown.IsVisible = true;
    }

    private void showEditor()
    {
      btnSaveEdit.Text = "Save";
      edNote.IsVisible = true;
      wvMarkdown.IsVisible = false;
    }

    async private void btnSaveClick(object sender, EventArgs e)
    {
      if (btnSaveEdit.Text == "Save")
      {
        var note = (Note)BindingContext;

        note.Date = DateTime.Now;
        await App.Database.SaveNoteAsync(note);
        showMarkdown();
      }
      else
      {
        showEditor();
      }
    }

    async private void btnDeleteClick(object sender, EventArgs e)
    {
      var note = (Note)BindingContext;
      await App.Database.DeleteNoteAsync(note);
      await Navigation.PopAsync();
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
      var note = (Note)BindingContext;
      if (!string.IsNullOrEmpty(note.Text))
      {
        showMarkdown();
        Console.WriteLine("ouch");
      }
    }
  }
}