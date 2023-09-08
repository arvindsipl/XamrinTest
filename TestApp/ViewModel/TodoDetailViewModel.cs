using System;
using TestApp.Model;

namespace TestApp.ViewModel
{
    public class TodoDetailViewModel : ViewModelBase
    {
        private TodoItem _selectedItem;
        private string _errorMessage;

        public TodoItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public TodoDetailViewModel()
        {
            // Initialize properties or load data here
        }

        public void LoadSelectedItem()
        {
            try
            {
                // Load the selected item data
                // For example, if fetching additional details from an API:
                // var selectedItemData = await ApiService.GetTodoItemDetailsAsync(SelectedItem.Id);

                // Assign the loaded data to the SelectedItem property
                // SelectedItem = selectedItemData;
            }
            catch (Exception ex)
            {
                // Handle exceptions and set an error message
                ErrorMessage = $"Error: {ex.Message}";
            }
        }
    }
}

