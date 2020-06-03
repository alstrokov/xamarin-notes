using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xf03notesSql.Models;

namespace xf03notesSql
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NotesPage : ContentPage
  {
    public NotesPage()
    {
      InitializeComponent();
    }

    protected override async void OnAppearing()
    {
      base.OnAppearing();

      lvNotes.ItemsSource = 
        (await App.Database.GetNotesAsync()).OrderByDescending(z=>z.Date);
    }

    async private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new NoteEntryPage
      {
        BindingContext = new Note()
      });
    }

    async private void lvNotes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      if(e.SelectedItem != null)
      {
        await Navigation.PushAsync(new NoteEntryPage
        {
          BindingContext = e.SelectedItem as Note
        });
      }
    }

    private async void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
      lvNotes.ItemsSource =
        (await App.Database.GetNotesAsync())
          .Where(n => n.Text.Contains(edFilter.Text))
          .OrderByDescending(z => z.Date);
    }

    private void btnClear_clicked(object sender, EventArgs e)
    {
      edFilter.Text = string.Empty;
    }
  }
}