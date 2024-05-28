using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IUserGroupsRepository _userGroupsRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public GroupsController(
            IUserService userService,
            IUserRepository userRepository,
            IGroupsRepository groupsRepository,
            IUserGroupsRepository userGroupsRepository,
            ITasksRepository tasksRepository,
            IMapper mapper
        )
        {
            _userService = userService;
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
            _userGroupsRepository = userGroupsRepository;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int groupId)
        {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var group = await _groupsRepository.GetGroup(groupId);

            if (!group.UserGroups.Any(g => g.UserId == userId))
            {
                return NotFound();
            }

            var imagePath = group.ImageId == Guid.Empty
                ? $"http://localhost:5157/images/noimage.png"
                : $"/images/groups/{group.ImageId}.png";

            var model = new GroupTasksViewModel
            {
                GroupId = groupId,
                Name = group.Name,
                ImageFullPath = imagePath,
                Tasks = await _tasksRepository
                    .GetAllTasks(groupId)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateGroupViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Wrong information enterred!");
                return View(model);
            }

            Guid imageId = Guid.Empty;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                imageId = Guid.NewGuid();

                string imageFilename = imageId.ToString() + ".png";
                string path = Path.Combine("wwwroot/images/groups", imageFilename);

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var image = Image.FromStream(model.ImageFile.OpenReadStream()))
                {
                    image.Save(path, ImageFormat.Png);
                }
            }

            model.ImageId = imageId;

            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);
            var group = _mapper.Map<Group>(model);

            if ((user is null) || (group is null))
            {
                return NotFound();
            }

            var band = await _groupsRepository.Add(group, user);

            if (band)
            {
                return RedirectToAction(nameof(Details), new { groupId = group.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the group");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View(new EnterGroupViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enter(EnterGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Write a valid code");
                return View(model);
            }

            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            string code = (model.Code).Trim(new char[] { '#' });
            int groupId = int.Parse(code);
            var group = await _groupsRepository.GetGroup(groupId);

            if ((user is null) || (group is null))
            {
                return NotFound();
            }

            var band = await _groupsRepository.Enter(groupId, user);

            if (band)
            {
                return RedirectToAction(nameof(Details), new { groupId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect code");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int groupId)
        {
            var group = await _groupsRepository.GetGroup(groupId);

            if (group is null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditGroupViewModel>(group);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "The group information is invalid");
                return View(model);
            }

            var group = await _groupsRepository.GetGroup(model.Id);
            Guid imageId = Guid.Empty;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                if (group.ImageId != Guid.Empty)
                {
                    string existingImagePath = Path.Combine("wwwroot/images/groups", group.ImageId.ToString() + ".png");
                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath);
                    }
                }


                imageId = Guid.NewGuid();

                string imageFilename = imageId.ToString() + ".png";
                string path = Path.Combine("wwwroot/images/groups", imageFilename);

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var image = Image.FromStream(model.ImageFile.OpenReadStream()))
                {
                    image.Save(path, ImageFormat.Png);
                }
            }

            group.Name = model.Name;
            group.ImageId = imageId;

            bool band = await _groupsRepository.UpdateGroup(group);

            if (band)
            {
                return RedirectToAction(nameof(Details), new { groupId = group.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the group");
                return View(model);
            }
        }

        [HttpDelete]
        [Route("groups/exit/{id}")]
        public async Task<IActionResult> Exit(int id)
        {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);
            var group = await _groupsRepository.GetGroup(id);

            if (user is null || group is null)
            {
                return BadRequest();
            }

            var userGroup = await _userGroupsRepository.GetUserGroupAsync(userId, id);

            if (userGroup is null)
            {
                return BadRequest();
            }

            return (await _userGroupsRepository.DeleteUserGroupAsync(userGroup.Id)
                ? Ok() : BadRequest());
        }
    }
}