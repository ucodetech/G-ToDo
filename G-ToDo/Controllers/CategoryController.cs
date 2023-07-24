using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using G_ToDo.Data;
using G_ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace G_ToDo.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> catList = _db.Categories.ToList();
            return View(catList);
        }

       public IActionResult Upsert(string? id){
        Category category = new ();
            if(id==null){
                //add 
               return View(category);             
            }else{
                // update
                Category obj = _db.Categories.Where(t=>t.CategoryId == id).FirstOrDefault();
                return View(obj);
            }
       }

       [HttpPost]
       public IActionResult Upsert(Category obj){
       
            if(ModelState.IsValid){
              
                 if(obj.CategoryId==null){
                      _db.Add(obj);
                       TempData["success"] = "Category Created Successfully!";
                }else{
                     _db.Update(obj);
                      TempData["success"] = "Category Created Updated!";         
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
               
            }else{
                TempData["error"] = "Please check your form!";
                return View();
            }

       }


    }
}