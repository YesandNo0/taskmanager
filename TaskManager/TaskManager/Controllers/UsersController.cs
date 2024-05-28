using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Models.DTOs;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers {
    public class UsersController : Controller {
        private readonly UserManager<UserDTO> _userManager;
        private readonly SignInManager<UserDTO> _signInManager;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(
            UserManager<UserDTO> userManager,
            SignInManager<UserDTO> signInManager,
            IUserService userService,
            IUserRepository userRepository,
            IMapper mapper
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Wrong data enterred!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded) {
                return RedirectToAction("Index", "Home");
            } else {
                ModelState.AddModelError(string.Empty, "Incorrect email or password.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Error in the information provided!");
                return View(model);
            }

            Guid imageId = Guid.Empty;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                imageId = Guid.NewGuid();

                string imageFilename = imageId.ToString() + ".png";
                string path = Path.Combine("wwwroot/images/users", imageFilename);

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var image = Image.FromStream(model.ImageFile.OpenReadStream()))
                {
                    image.Save(path, ImageFormat.Png);
                }
            }

            model.ImageId = imageId;

            var userStorage = _mapper.Map<User>(model);
            var user = _mapper.Map<UserDTO>(userStorage);

            var result = await _userManager.CreateAsync(user, password: model.Password);

            if (result.Succeeded) {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            } else {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details() {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);
            var model = _mapper.Map<UserDetailsViewModel>(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditInfo() {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);
            var model = _mapper.Map<EditInfoViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(EditInfoViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "You have an error in the information provided");
                return View(model);
            }
            
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            if (user.ImageId != Guid.Empty)
            {
                string existingImagePath = Path.Combine("wwwroot/images/users", user.ImageId.ToString() + ".png");
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath);
                }
            }

            Guid imageId = Guid.Empty;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                imageId = Guid.NewGuid();

                string imageFilename = imageId.ToString() + ".png";
                string path = Path.Combine("wwwroot/images/users", imageFilename);

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var image = Image.FromStream(model.ImageFile.OpenReadStream()))
                {
                    image.Save(path, ImageFormat.Png);
                }
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.ImageId = imageId;

            await _userRepository.UpdateUserAsync(user);

            return RedirectToAction(nameof(Details));
        }
    }
}