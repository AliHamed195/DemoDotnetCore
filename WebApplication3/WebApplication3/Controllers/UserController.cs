using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.DatabaseHelper.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("UserController")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        // GET: UserController
        [HttpGet("AllUsers")]
        public ActionResult AllUsers()
        {
            IEnumerable<User> users = _userRepository.GetAllUsers();
            return View(users);
        }

        // GET: UserController/Create
        [HttpGet("CreateUser")]
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost("CreateUser")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {
            try
            {
                _userRepository.CreateUser(user.Name, user.Age);
                return RedirectToAction(nameof(AllUsers));
            }
            catch
            {
                return View();
            }
        }

    }
}
