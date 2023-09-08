using TestApp.Model;
using TestApp.ViewModel;
using TestApp.Views;

namespace TestApp;

public partial class MainPage : ContentPage
{
    private readonly TodoListViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = (TodoListViewModel)BindingContext;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var selectedItem = e.SelectedItem as TodoItem;

        var detailViewModel = new TodoDetailViewModel();
        detailViewModel.SelectedItem = selectedItem;
        detailViewModel.LoadSelectedItem();

        if (string.IsNullOrEmpty(detailViewModel.ErrorMessage))
        {
            Navigation.PushAsync(new TodoDetailPage { BindingContext = detailViewModel });
        }
        else
        {
            // Handle and display the error message
            DisplayAlert("Error", detailViewModel.ErrorMessage, "OK");
        }

    // Deselect item
    ((ListView)sender).SelectedItem = null;
    }
}

