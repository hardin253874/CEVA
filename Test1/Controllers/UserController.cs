using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.Services;
using Test1.Models;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _userService;
        public UserController(IUsers userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("employee/{employeeId}")]
        public ActionResult<IEnumerable<User>> GetUsersByEmployeeId(string employeeId)
        {
            var users = _userService.GetByEmployeeId(employeeId);
            if (!users.Any()) return NotFound();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            var created = _userService.Create(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = created.ID }, created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var success = _userService.Update(id, updatedUser);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var success =  _userService.Delete(id);
            return success ? NoContent() : NotFound();
        }
    }
}
