using Test1.Models;

namespace Test1.Services
{
    public interface IUsers
    {
        IEnumerable<User> GetAll();

        User? GetById(int id);

        IEnumerable<User> GetByEmployeeId(string employeeId);

        User Create(User user);
        bool Update(int id, User updatedUser);
        bool Delete(int id);
    }
}
