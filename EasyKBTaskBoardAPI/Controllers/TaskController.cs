using AutoMapper;
using EasyKBTaskBoard.API.Models;
using EasyKBTaskBoard.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly IEasyKBTaskBoardRepository _easyKBTaskBoardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TaskController(IEasyKBTaskBoardRepository easyKBTaskBoardRepository, IMapper mapper,
            ILogger<BoardController> logger)
        {
            _easyKBTaskBoardRepository = easyKBTaskBoardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{taskId}", Name ="GetTask")]
        public ActionResult GetTask(int taskId)
        {
            var taskEntity = _easyKBTaskBoardRepository.GetTask(taskId);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TaskDto>(taskEntity));
        }

        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskForCreationDto task)
        {
            var taskEntity = _mapper.Map<Entities.Task>(task);

            if (taskEntity == null)
            {
                return NotFound();
            }

            try
            {
                _easyKBTaskBoardRepository.AddTask(taskEntity);
                _easyKBTaskBoardRepository.Save();
            }
            catch (Exception)
            {
                return NotFound($"Column with id {task.ColumnId} couldn't be found.\nCreating failed.");
            }

            var createdTaskToReturn = _mapper.Map<TaskDto>(taskEntity);

            return CreatedAtRoute(
                "GetTask",
                new { taskId = createdTaskToReturn.Id },
                createdTaskToReturn);
        }

        [HttpPatch("{taskId}")]
        public ActionResult PartiallyUpdateTask(int taskId, [FromBody] JsonPatchDocument<TaskForUpdateDto> patchDoc)
        {
            var taskEntity = _easyKBTaskBoardRepository.GetTask(taskId);

            if (taskEntity == null)
            {
                return NotFound();
            }

            var taskToPatch = _mapper.Map<TaskForUpdateDto>(taskEntity);
            patchDoc.ApplyTo(taskToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(taskToPatch, taskEntity);

            _easyKBTaskBoardRepository.UpdateTask(taskEntity);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        public ActionResult DeleteTask(int taskId)
        {
            var taskToDelete = _easyKBTaskBoardRepository.GetTask(taskId);

            if (taskToDelete == null)
            {
                return NotFound();
            }

            _easyKBTaskBoardRepository.DeleteTask(taskToDelete);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }

        [HttpPost("{taskId}/add-members")]
        public ActionResult AddMembers(int taskId, [FromBody] int accountId)
        {
            _easyKBTaskBoardRepository.AddAccountToTask(accountId, taskId);
            _easyKBTaskBoardRepository.Save();

            return RedirectToRoute("GetTask", new { taskId });
        }
    }
}
