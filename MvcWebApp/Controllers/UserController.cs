using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebApp.Data;
using MvcWebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MvcWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly MvcWebAppContext _context;

        public UserController(MvcWebAppContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserInfoModel.ToListAsync());
        }

        // GET: User/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            try
            {
                Console.WriteLine($"Login: {username}, {password}");
                var userInfo = await VerifyCredential(username, password);
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim("NickName", userInfo.NickName),
                new Claim(ClaimTypes.Role, userInfo.Roles![0]),
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                    });
                if (returnUrl is null)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    // return Redirect(returnUrl);
                    return LocalRedirect(returnUrl);
                }
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                return Challenge();
            }
        }

        private async Task<UserInfoModel> VerifyCredential(string email, string password)
        {
            var foundUser = await _context.UserInfoModel.SingleAsync(u => u.Email.Equals(email));
            if (foundUser == null)
            {
                throw new Exception("User not found");
            }

            var foundCredential = await _context.UserCredentials.SingleAsync(c => c.UserId.Equals(foundUser.Id));
            if (foundCredential == null)
            {
                throw new Exception("Credential not found");
            }

            var passwordMatched = BCrypt.Net.BCrypt.Verify(password, foundCredential.Password);
            if (!passwordMatched)
            {
                throw new Exception("Password not matched");
            }

            return foundUser;
        }


        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Email,NickName,Roles,AvatarUrl,Gender,Language,City,Province,Country")] UserInfoModel userInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInfoModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfoModel = await _context.UserInfoModel.FindAsync(id);
            if (userInfoModel == null)
            {
                return NotFound();
            }
            return View(userInfoModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Username,Email,NickName,Roles,AvatarUrl,Gender,Language,City,Province,Country")] UserInfoModel userInfoModel)
        {
            if (id != userInfoModel.Id)
            {
                return NotFound();
            }

            Console.WriteLine($"Edit Model: {userInfoModel}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInfoModelExists(userInfoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userInfoModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfoModel = await _context.UserInfoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInfoModel == null)
            {
                return NotFound();
            }

            return View(userInfoModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userInfoModel = await _context.UserInfoModel.FindAsync(id);
            if (userInfoModel != null)
            {
                _context.UserInfoModel.Remove(userInfoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInfoModelExists(string id)
        {
            return _context.UserInfoModel.Any(e => e.Id == id);
        }
    }
}