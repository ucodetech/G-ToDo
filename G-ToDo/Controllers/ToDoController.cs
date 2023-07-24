using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using G_ToDo.Data;
using G_ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace G_ToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly  ApplicationDbContext _db;
       public ToDoController(ApplicationDbContext db)
       {
            _db = db;
       }
        public IActionResult Index(string id)
        {
            //index method recevices the id as parameter and then past it to Filter Model
            //Filter Model checks the various filters and return resonse to index 
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Status = _db.Status.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<ToDo> query = _db.Todos
                            .Include(c => c.Category)
                            .Include(s=>s.Status);

            if (filters.HasCategory){
                query = query.Where(c=>c.CategoryId == filters.StatusId);
            }
            if (filters.HasStatus){
                query = query.Where(c=>c.StatusId == filters.StatusId);
            }
            if (filters.HasDue){
                var today = DateTime.Today;
                if(filters.IsPast){
                    query = query.Where(c=>c.DueDate < today);
                }
                else if(filters.IsFuture){
                    query = query.Where(c=>c.DueDate > today);
                }
                 else if(filters.IsToday){
                    query = query.Where(c=>c.DueDate == today);
                }
            }
            var tasks = query.OrderBy(t=>t.DueDate).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create(){
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Status = _db.Status.ToList();
            var task = new ToDo {StatusId = "open"};
            return View(task);
        }


        [HttpPost]
        public IActionResult Create(ToDo task){
            if(ModelState.IsValid){
                _db.Todos.Add(task);
                _db.SaveChanges();
                return  RedirectToAction("Index");
            }else{
                ViewBag.Categories = _db.Categories.ToList();
                ViewBag.Status = _db.Status.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter){
            string id = string.Join('-', filter); // this could be all-all-all or call-ex-open as an id
            return RedirectToAction("Index", new {ID = id}); // the id is passed to index method
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected){
            selected = _db.Todos.Find(selected.Id)!;
            if(selected != null){
                selected.StatusId = "closed";
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new {ID = id});
        }

        [HttpPost]
        public IActionResult DeleteCompleted(string id){
            var toDelete = _db.Todos.Where(t=>t.StatusId == "closed").ToList();
            foreach (var task in toDelete){
                _db.Todos.Remove(task);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", new {ID = id});
        }
        
    }
}