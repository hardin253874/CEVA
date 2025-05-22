using System.Text.Json;
using Test1.Models;

namespace Test1.Services
{
    public class Users: IUsers
    {
        private readonly string _jsonPath = Path.Combine(AppContext.BaseDirectory, "Users", "IN", "users.json");

        public IEnumerable<User> GetAll() => LoadUsers();

        public User? GetById(int id) => LoadUsers().FirstOrDefault(u => u.ID == id);

        public IEnumerable<User> GetByEmployeeId(string employeeId)
        {
            return LoadUsers().Where(u =>
                !string.IsNullOrWhiteSpace(u.EmployeeID) &&
                u.EmployeeID.Equals(employeeId, StringComparison.OrdinalIgnoreCase));
        }
        public User Create(User newUser)
        {
            var users = LoadUsers();
            newUser.ID = users.Any() ? users.Max(u => u.ID) + 1 : 1;
            users.Add(newUser);
            SaveUsers(users);
            return newUser;
        }

        public bool Update(int id, User updatedUser)
        {
            var users = LoadUsers();
            var existing = users.FirstOrDefault(u => u.ID == id);
            if (existing == null) return false;

            // Update fields
            existing.UserID = updatedUser.UserID;
            existing.EmployeeID = updatedUser.EmployeeID;
            existing.SiteName = updatedUser.SiteName;
            existing.BusinessUnitName = updatedUser.BusinessUnitName;
            existing.AccountName = updatedUser.AccountName;
            existing.GroupName = updatedUser.GroupName;
            existing.CategoryName = updatedUser.CategoryName;
            existing.TypeName = updatedUser.TypeName;
            existing.Date = updatedUser.Date;
            existing.Duration = updatedUser.Duration;
            existing.IsProcessed = updatedUser.IsProcessed;

            SaveUsers(users);
            return true;
        }

        public bool Delete(int id)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.ID == id);
            if (user == null) return false;

            users.Remove(user);
            SaveUsers(users);
            return true;
        }


        private List<User> LoadUsers()
        {
            if (!File.Exists(_jsonPath))
                return new List<User>();

            var json = File.ReadAllText(_jsonPath);
            var users = JsonSerializer.Deserialize<List<User>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return users ?? new List<User>();
        }

        private void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonPath, json);
        }
    }
}
