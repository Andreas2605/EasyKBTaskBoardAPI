using AutoMapper;
using EasyKBTaskBoard.API.Entities;
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
    [Route("api/accounts/{accountId}/boards")]
    public class BoardController : ControllerBase
    {
        private readonly IEasyKBTaskBoardRepository _easyKBTaskBoardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BoardController(IEasyKBTaskBoardRepository easyKBTaskBoardRepository, IMapper mapper,
            ILogger logger)
        {
            _easyKBTaskBoardRepository = easyKBTaskBoardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetBoards(int accountId)
        {
            try
            {
                if (!_easyKBTaskBoardRepository.AccountExists(accountId))
                {
                    return NotFound();
                }

                var boardsForAccount = _easyKBTaskBoardRepository.GetBoardsForAccount(accountId);
                return Ok(_mapper.Map<BoardDto>(boardsForAccount));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting boards for account with id {accountId}.", ex);
                return StatusCode(500, "A problem occured while handling your request");
            }
        }

        [HttpGet("{boardId}", Name = "GetBoard")]
        public ActionResult GetBoard(int accountId, int boardId)
        {
            if (!_easyKBTaskBoardRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            var board = _easyKBTaskBoardRepository.GetBoardForAccount(accountId, boardId);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BoardDto>(board));
        }

        [HttpPost]
        public ActionResult CreateBoard(int accountId, [FromBody] BoardForCreationDto board)
        {
            if (!_easyKBTaskBoardRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            var boardEntity = _mapper.Map<Board>(board);

            _easyKBTaskBoardRepository.AddBoardForAccount(accountId, boardEntity);
            _easyKBTaskBoardRepository.Save();

            var createdBoardToReturn = _mapper.Map<BoardDto>(boardEntity);

            return CreatedAtRoute(
                "GetBoard",
                new { accountId, id = createdBoardToReturn.Id },
                createdBoardToReturn);
        }

        [HttpPatch("{boardId}")]
        public ActionResult EditBoardName (int accountId, int boardId,
            [FromBody] JsonPatchDocument<BoardForUpdateDto> patchDoc)
        {
            if (!_easyKBTaskBoardRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            var boardEntity = _easyKBTaskBoardRepository.GetBoardForAccount(accountId, boardId);
            if (boardEntity == null)
            {
                return NotFound();
            }

            var boardToPatch = _mapper.Map<BoardForUpdateDto>(boardEntity);
            patchDoc.ApplyTo(boardToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(boardToPatch, boardEntity);

            _easyKBTaskBoardRepository.UpdateBoardForAccount(accountId, boardEntity);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }

        [HttpDelete("{boardId}")]
        public ActionResult DeleteBoard(int accountId, int boardId)
        {
            if (!_easyKBTaskBoardRepository.AccountExists(accountId))
            {
                return NotFound();
            }

            var boardToDelete = _easyKBTaskBoardRepository.GetBoardForAccount(accountId, boardId);

            if (boardToDelete == null)
            {
                return NotFound();
            }

            _easyKBTaskBoardRepository.DeleteBoard(boardToDelete);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }
    }
}
