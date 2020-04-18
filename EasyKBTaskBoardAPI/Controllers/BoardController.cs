using AutoMapper;
using EasyKBTaskBoard.API.Services;
using Microsoft.AspNetCore.Mvc;
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

        public BoardController(IEasyKBTaskBoardRepository easyKBTaskBoardRepository, IMapper mapper)
        {
            _easyKBTaskBoardRepository = easyKBTaskBoardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetBoardsForAccount(int accountId)
        {
            
        }
    }
}
