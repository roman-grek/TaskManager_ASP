using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class BoardListViewModel
    {
        public IEnumerable<Board> Boards {get; set;}
    }
}