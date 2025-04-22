using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIV.Data;
using ProyectoPrograIV.Models;
using ProyectoPrograIV.ViewModels;

namespace ProyectoPrograIV.Controllers
{
    public class SocialController : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly AppDBContext _context;

        public SocialController(SignInManager<Users> signInManager, UserManager<Users> userManager, AppDBContext context)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._context = context;
        }
        public async Task<IActionResult> ProfileAsync()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            var friendList = await _context.Friendships
            .Where(f => f.UserId == currentUser.Id)
            .Include(f => f.Friend)
            .Select(f => new FriendViewModel
            {
                UserName = f.Friend.UserName,
            })
            .ToListAsync();

            var viewModel = new ProfileViewModel
            {
                UserName = currentUser.UserName,
                FriendList = friendList
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string friendUsername)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var friendUser = await _userManager.FindByNameAsync(friendUsername);

            if (friendUser == null)
            {
                TempData["FriendMessage"] = "El usuario no fue encontrado.";
            }
            else if (currentUser.Id == friendUser.Id)
            {
                TempData["FriendMessage"] = "No puedes agregarte a ti mismo.";
            }
            else
            {
                var exists = _context.Friendships.Any(f =>
                    f.UserId == currentUser.Id && f.FriendId == friendUser.Id);

                if (exists)
                {
                    TempData["FriendMessage"] = "Ya tienes a este usuario como amigo.";
                }
                else
                {
                    _context.Friendships.Add(new Friendship
                    {
                        UserId = currentUser.Id,
                        FriendId = friendUser.Id
                    });
                    await _context.SaveChangesAsync();
                    TempData["FriendMessage"] = "Amigo agregado exitosamente.";
                }
            }

            return RedirectToAction("Profile");
        }
    }
}
