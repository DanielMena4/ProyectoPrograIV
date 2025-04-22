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

        public SocialController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        public async Task<IActionResult> ProfileAsync()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            var friendList = _context.Friendships
            .Where(f => f.UserId == currentUser.Id)
            .Select(f => new FriendViewModel
            {
                UserName = f.Friend.UserName,
            })
                .ToList();

            var viewModel = new ProfileViewModel
            {
                UserName = currentUser.UserName,
                FriendList = friendList
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string friendUsername)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var friendUser = await _userManager.FindByEmailAsync(friendUsername);

            if (friendUser != null && currentUser.Id != friendUser.Id)
            {
                var exists = _context.Friendships.Any(f =>
                    f.UserId == currentUser.Id && f.FriendId == friendUser.Id);

                if (!exists)
                {
                    _context.Friendships.Add(new Friendship
                    {
                        UserId = currentUser.Id,
                        FriendId = friendUser.Id
                    });
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Profile");
        }
    }
}
