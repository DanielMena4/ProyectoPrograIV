using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIV.Data;
using ProyectoPrograIV.Models;
using ProyectoPrograIV.ViewModels;

namespace ProyectoPrograIV.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly AppDBContext _context;
        public FriendsController(UserManager<Users> userManager, AppDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Search(string query)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var results = _context.Users
                .Where(u => u.UserName.Contains(query) && u.Id != currentUser.Id)
                .Select(u => new FriendViewModel
                {
                    UserName = u.UserName,
                }).ToList();

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest(string friendEmail)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByEmailAsync(friendEmail);

            if (friend != null && friend.Id != currentUser.Id)
            {
                var alreadySent = _context.Friendships.Any(f =>
                    f.UserId == currentUser.Id && f.FriendId == friend.Id);

                if (!alreadySent)
                {
                    var request = new Friendship
                    {
                        UserId = currentUser.Id,
                        FriendId = friend.Id,
                        IsConfirmed = false,
                    };

                    _context.Friendships.Add(request);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Search");
        }

        public async Task<IActionResult> Requests()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var pending = _context.Friendships
                .Where(f => f.FriendId == currentUser.Id && !f.IsConfirmed)
                .Select(f => new
                {
                    f.Id,
                    User = f.User.UserName,
                    Email = f.User.Email
                })
                .ToList();

            ViewBag.Requests = pending;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            var request = await _context.Friendships.FindAsync(id);
            if (request != null)
            {
                request.IsConfirmed = true;
                var inverse = new Friendship
                {
                    UserId = request.FriendId,
                    FriendId = request.UserId,
                    IsConfirmed = true
                };

                _context.Friendships.Add(inverse);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Requests");
        }
    }
}
