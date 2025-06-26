using CourseEvaluationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourseEvaluationSystem.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly AppDbContext _context;

        public EvaluationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Evaluation/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "Title");
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "Name");
            return View();
        }

        // POST: Evaluation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evaluation evaluation)
        {
            evaluation.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(evaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Confirmation");
            }

            // Om ModelState inte är giltig – fyll i dropdowns igen
            ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "Title", evaluation.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "Name", evaluation.StudentID);
            return View(evaluation);
        }

        // GET: Evaluation/Confirmation
        public IActionResult Confirmation()
        {
            return View();
        }

        // GET: Evaluation/Index
        public async Task<IActionResult> Index()
        {
            var summary = await _context.Evaluations
                .Include(e => e.Course)
                .GroupBy(e => e.Course.Title)
                .Select(g => new EvaluationSummaryViewModel
                {
                    CourseTitle = g.Key,
                    TotalEvaluations = g.Count(),
                    AverageRating = Math.Round(g.Average(e => e.Rating), 1)
                })
                .ToListAsync();

            return View(summary);
        }

        // GET: Evaluation/Comments
        public async Task<IActionResult> Comments()
        {
            var comments = await _context.Evaluations
                .Include(e => e.Course)
                .Where(e => !string.IsNullOrWhiteSpace(e.Comment))
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return View(comments);
        }

        // GET: Evaluation/Filter
        public async Task<IActionResult> Filter(int? courseId, DateTime? fromDate, DateTime? toDate)
        {
            var model = new EvaluationFilterViewModel
            {
                CourseID = courseId,
                FromDate = fromDate,
                ToDate = toDate,
                CourseList = await _context.Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.Title
                    })
                    .ToListAsync()
            };

            var query = _context.Evaluations
                .Include(e => e.Course)
                .AsQueryable();

            if (courseId.HasValue)
                query = query.Where(e => e.CourseID == courseId.Value);

            if (fromDate.HasValue)
                query = query.Where(e => e.Date >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(e => e.Date <= toDate.Value);

            model.Results = await query.ToListAsync();

            return View(model);
        }
    }

    // ViewModel för sammanställning
    public class EvaluationSummaryViewModel
    {
        public string CourseTitle { get; set; }
        public int TotalEvaluations { get; set; }
        public double AverageRating { get; set; }
    }
}
