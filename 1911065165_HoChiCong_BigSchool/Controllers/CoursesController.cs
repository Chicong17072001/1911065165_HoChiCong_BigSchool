using _1911065165_HoChiCong_BigSchool.Models;
using _1911065165_HoChiCong_BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1911065165_HoChiCong_BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        private readonly ApplicationDbContext _dbcontext;
        public CoursesController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbcontext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };  
            _dbcontext.Courses.Add(course);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbcontext.Categories.ToList()
            };
            return View(viewModel);
        }

    }
}