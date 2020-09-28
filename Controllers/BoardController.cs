using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.Models;
using TaskManager.Interfaces;

namespace TaskManager.Controllers
{
    public class BoardController : Controller
    {
        private readonly IBoardRepository boardRepository;
        public BoardController(IBoardRepository boardRepository) {
            this.boardRepository = boardRepository;
        }

        public ViewResult List() {
            var boards = boardRepository.Boards;
            return View(boards);
        }
    }
}