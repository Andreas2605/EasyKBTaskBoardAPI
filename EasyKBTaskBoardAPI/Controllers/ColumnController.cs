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
    [Route("api/board/{boardId}/columns")]
    public class ColumnController : ControllerBase
    {
        private readonly IEasyKBTaskBoardRepository _easyKBTaskBoardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ColumnController(IEasyKBTaskBoardRepository easyKBTaskBoardRepository, IMapper mapper,
            ILogger<ColumnController> logger)
        {
            _easyKBTaskBoardRepository = easyKBTaskBoardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetColumns(int boardId)
        {
            var columnEntities = _easyKBTaskBoardRepository.GetColumnsForBoard(boardId);

            return Ok(_mapper.Map<IEnumerable<ColumnDto>>(columnEntities));
        }

        [HttpGet("{columnId}", Name ="GetColumn")]
        public ActionResult GetColumn(int boardId, int columnId)
        {
            var columnEntity = _easyKBTaskBoardRepository.GetColumnForBoard(boardId, columnId);

            if (columnEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ColumnDto>(columnEntity));
        }

        [HttpPost]
        public ActionResult CreateColumn(int boardId, [FromBody] ColumnForCreationDto column)
        {
            var boardEntity = _easyKBTaskBoardRepository.GetBoard(boardId);

            if (boardEntity == null)
            {
                return NotFound();
            }

            var columnEntity = _mapper.Map<Column>(column);

            _easyKBTaskBoardRepository.AddColumnToBoard(boardId, columnEntity);
            _easyKBTaskBoardRepository.Save();

            var createdColumnToReturn = _mapper.Map<ColumnDto>(columnEntity);

            return CreatedAtRoute(
                "GetColumn",
                new { boardId, columnId = createdColumnToReturn.Id },
                createdColumnToReturn);
        }

        [HttpPut("{columnId}")]
        public ActionResult EditColumn(int boardId, int columnId,
            [FromBody] JsonPatchDocument<ColumnForUpdateDto> patchDoc)
        {
            var columnEntity = _easyKBTaskBoardRepository.GetColumnForBoard(boardId, columnId);

            if (columnEntity == null)
            {
                return NotFound();
            }

            var columnToPatch = _mapper.Map<ColumnForUpdateDto>(columnEntity);

            patchDoc.ApplyTo(columnToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(columnToPatch, columnEntity);

            _easyKBTaskBoardRepository.UpdateColumnForBoard(boardId, columnEntity);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }

        [HttpDelete("{columnId}")]
        public ActionResult DeleteColumn(int boardId, int columnId)
        {
            var columnToDelete = _easyKBTaskBoardRepository.GetColumnForBoard(boardId, columnId);

            if (columnToDelete == null)
            {
                return NotFound();
            }

            _easyKBTaskBoardRepository.DeleteColumn(columnToDelete);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }
    }
}
