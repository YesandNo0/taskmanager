using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Helpers.Interfaces;
using TaskManager.Models;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers {
    public class TaskWorksController : Controller {
        private readonly ITasksRepository _tasksRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ISelectListHelper _selectListHelper;

        public TaskWorksController(
            ITasksRepository tasksRepository,
            IGroupsRepository groupsRepository,
            IUserService userService,
            IUserRepository userRepository,
            ISelectListHelper selectListHelper
        ) {
            _tasksRepository = tasksRepository;
            _groupsRepository = groupsRepository;
            _userService = userService;
            _userRepository = userRepository;
            _selectListHelper = selectListHelper;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int groupId) {
            return View(new AddTaskViewModel {
                GroupId = groupId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddTaskViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "There is an error in the information you provided us");
                return View(model);
            }

            var group = await _groupsRepository.GetGroup(model.GroupId);

            if (group is null) {
                return NotFound();
            }

            var task = new TaskWork {
                Text = model.Text,
                StartDate = DateTime.Now,
                EndDate = model.FinishDate,
                State = 0,
                GroupId = model.GroupId,
                Group = group
            };

            var band = await _tasksRepository.AddTask(task);

            if (band) {
                return RedirectToAction("Details", "Groups", new { groupId  = model.GroupId });
            } else {
                ModelState.AddModelError(string.Empty, "It is not possible to create this task, please try again.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int taskId, int groupId) {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            if (user is null) {
                return NotFound();
            }

            var task = await _tasksRepository.GetTask(taskId);
            var group = await _groupsRepository.GetGroup(groupId);

            if (task is null || group is null) {
                return NotFound();
            }

            if (!await _groupsRepository.IsUserInGroup(group.Id, user)) {
                return NotFound();
            }

            var model = new EditTaskViewModel {
                Id = task.Id,
                GroupId = group.Id,
                Text = task.Text,
                FinishDate = task.EndDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTaskViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "The information you provided is not correct");
                return View(model);
            }

            var task = await _tasksRepository.GetTask(model.Id);
            var group = await _groupsRepository.GetGroup(model.GroupId);

            if (task is null || group is null) {
                return NotFound();
            }

            task.Text = model.Text;
            task.EndDate = model.FinishDate;

            bool band = await _tasksRepository.EditTask(task);

            if (band) {
                return RedirectToAction("Details", "Groups", new { groupId = group.Id });
            } else {
                ModelState.AddModelError(string.Empty, "An error occurred while modifying the task information");
                return View(model);
            }
        }

        [HttpPut]
        [Route("tasks/complete/{id}")]
        public async Task<IActionResult> Complete(int id) {
            var task = await _tasksRepository.GetTask(id);

            if (task is null) {
                return BadRequest();
            }

            if (task.State == 1) {
                return (await _tasksRepository.UnComplete(task))
                    ? Ok() : BadRequest();
            }

            return (await _tasksRepository.Complete(task))
                ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("tasks/delete/{id}")]
        public async Task<IActionResult> Delete(int id) {
            var task = await _tasksRepository.GetTask(id);

            if (task is null) {
                return BadRequest();
            }

            return (await _tasksRepository.Delete(task))
                ? Ok() : BadRequest();
        }
    }
}