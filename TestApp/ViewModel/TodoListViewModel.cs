using System.Collections.ObjectModel;
using TestApp.Model;

namespace TestApp.ViewModel;

public class TodoListViewModel
{
    private const string ApiUrl = "https://jsonplaceholder.typicode.com/todos";
    private readonly HttpClient _httpClient = new HttpClient();
    private bool _isDataLoaded = false;

    public ObservableCollection<TodoItem> TodoItems { get; } = new ObservableCollection<TodoItem>();

    public async Task LoadDataAsync()
    {
        // Check if data has already been loaded to avoid redundant API calls
        if (_isDataLoaded)
            return;

        try
        {
            // Check for internet connectivity before making the API request
            if (!IsInternetAvailable())
            {
                // Handle the case where the internet is not available
                HandleNoInternetConnection();
                return;
            }

            var json = await _httpClient.GetStringAsync(ApiUrl);
            var items = System.Text.Json.JsonSerializer.Deserialize<List<TodoItem>>(json);

            // Clear the collection and add items
            TodoItems.Clear();
            foreach (var item in items)
            {
                if (item.title != null)
                {
                    TodoItems.Add(item);
                }
            }

            // Set the flag to indicate that data has been loaded
            _isDataLoaded = true;
        }
        catch (HttpRequestException ex)
        {
            // Handle the case where data cannot be fetched (e.g., server issues)
            HandleDataFetchError(ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions, e.g., JSON parsing errors
            HandleGeneralError(ex);
        }
    }

    private bool IsInternetAvailable()
    {
        var currentNetwork = Connectivity.NetworkAccess;

        // Check if the device is connected to the internet
        if (currentNetwork == NetworkAccess.Internet)
        {
            return true;
        }
        else if (currentNetwork == NetworkAccess.ConstrainedInternet)
        {
            // ConstrainedInternet means the device has limited internet connectivity,
            // e.g., a captive portal login page
            return true;
        }
        else
        {
            // No internet connection is available
            return false;
        }

    }

    private void HandleNoInternetConnection()
    {
        // Handle the case where the internet is not available
        // For example, you can display a message to the user
        Application.Current.MainPage.DisplayAlert("No Internet", "Please check your internet connection and try again.", "OK");
        Console.WriteLine("No internet connection available.");
        
    }

    private void HandleDataFetchError(Exception ex)
    {
        // Handle the case where data cannot be fetched (e.g., server issues)
        // You can display an error message or log the error
        Application.Current.MainPage.DisplayAlert("Error", $"Error fetching data: {ex.Message}", "OK");
        Console.WriteLine($"Error fetching data: {ex.Message}");
    }

    private void HandleGeneralError(Exception ex)
    {
        // Handle other exceptions, e.g., JSON parsing errors
        // You can display an error message or log the error
        Console.WriteLine($"Error: {ex.Message}");
    }

}
