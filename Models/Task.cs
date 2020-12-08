using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title {get; set;}
        
        [DataType(DataType.MultilineText)]
        public string Description {get; set;}
        
        public string BoardId {get; set;}
        
        public int ListNum { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public bool Open { get; set; }
    }
}