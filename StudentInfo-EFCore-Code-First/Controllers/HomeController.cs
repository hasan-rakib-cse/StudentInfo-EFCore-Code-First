using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInfo_EFCore_Code_First.Models;
using System.Diagnostics;

namespace StudentInfo_EFCore_Code_First.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDbContext _context;

        public HomeController(ILogger<HomeController> logger, StudentDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var studentData = await _context.Students.ToListAsync();
            return View(studentData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if(ModelState.IsValid)
            {
                _context.Students.Add(std);
                await _context.SaveChangesAsync();
                TempData["insert_success_msg"] = "Data Inserted Successfully.";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.Students == null)
            {
                return NotFound();
            }

            var studentData = await _context.Students.FirstOrDefaultAsync(x=> x.Id == id);

            if(studentData == null)
            {
                return NotFound();
            }

            return View(studentData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var studentData = await _context.Students.FindAsync(id);
            if(studentData == null)
            {
                return NotFound();
            }

            return View(studentData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if(id != std.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Students.Update(std);
                await _context.SaveChangesAsync();
                TempData["update_success_msg"] = "Data Updated Successfully.";
                return RedirectToAction("Index", "Home");
            }

            return View(std);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var studentData = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentData == null)
            {
                return NotFound();
            }

            return View(studentData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var studentData = await _context.Students.FindAsync(id);
            if (studentData != null)
            {
                _context.Students.Remove(studentData);
            }
            await _context.SaveChangesAsync();
            TempData["delete_success_msg"] = "Data Deleted Successfully.";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
