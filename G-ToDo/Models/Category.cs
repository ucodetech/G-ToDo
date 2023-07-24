using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace G_ToDo.Models
{
    public class Category
    {
        
        [Display(Name = "Category ID")]
        public string CategoryId {get; set;} = string.Empty;
        [Column(TypeName = "nvarchar(100)")]
        public string Name {get; set;} = string.Empty;
        [Column(TypeName = "nvarchar(100)")]
        public string? Icon {get; set;} = string.Empty;   
    }
}