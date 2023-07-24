using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace G_ToDo.Models
{
    public class ToDo
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [Column(TypeName = "text")]
        public  string Description {get; set;} = string.Empty;
        public DateTime? DueDate {get; set;}
        public string CategoryId {get; set;} = string.Empty;
        [ValidateNever]
        public Category? Category {get; set;}

        [Required]
        public string StatusId {get; set;} = string.Empty;
        [ValidateNever]
        public Status? Status {get; set;}

        public bool OverDue => StatusId == "open" && DueDate < DateTime.Today;

    }
}