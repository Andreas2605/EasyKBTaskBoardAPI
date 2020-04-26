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
    [Route("api/board")]
    public class BoardController : ControllerBase
    {
        private readonly IEasyKBTaskBoardRepository _easyKBTaskBoardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BoardController(IEasyKBTaskBoardRepository easyKBTaskBoardRepository, IMapper mapper,
            ILogger<BoardController> logger)
        {
            _easyKBTaskBoardRepository = easyKBTaskBoardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{boardId}", Name = "GetBoard")]
        public ActionResult GetBoard(int boardId)
        {
            var boardEntity = _easyKBTaskBoardRepository.GetBoard(boardId);

            if (boardEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BoardDto>(boardEntity));
        }

        [HttpPost]
        public ActionResult CreateBoard([FromBody] BoardForCreationDto board)
        {
            var boardEntity = _mapper.Map<Board>(board);

            try
            {
                _easyKBTaskBoardRepository.AddBoard(boardEntity);
                _easyKBTaskBoardRepository.Save();
            }
            catch (Exception)
            {
                return NotFound($"Account with id {board.AccountId} couldn't be found.\nCreating failed.");
            }
          
            var createdBoardToReturn = _mapper.Map<BoardDto>(boardEntity);

            return CreatedAtRoute(
                "GetBoard",
                new { boardId = createdBoardToReturn.Id },
                createdBoardToReturn);
        }

        [HttpPatch("{boardId}")]
        public ActionResult EditBoardName(int boardId, [FromBody] JsonPatchDocument<BoardForUpdateDto> patchDoc)
        {
            var boardEntity = _easyKBTaskBoardRepository.GetBoard(boardId);
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

            _easyKBTaskBoardRepository.UpdateBoard(boardEntity);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }

        [HttpDelete("{boardId}")]
        public ActionResult DeleteBoard(int boardId)
        {
            var boardToDelete = _easyKBTaskBoardRepository.GetBoard(boardId);

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
