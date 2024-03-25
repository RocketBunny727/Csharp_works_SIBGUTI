using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework_8.ViewModels
{
    public class UserApp
    {
        private const string ApiUrl = "https://jsonplaceholder.typicode.com/users";

        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        public async Task LoadUsersAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var users = JsonSerializer.Deserialize<User[]>(json);

                        Users.Clear();
                        foreach (var user in users)
                        {
                            Users.Add(user);
                            Console.WriteLine($"Added user: {user.id}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to load users: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading users: {ex.Message}");
            }
        }
    }
}
