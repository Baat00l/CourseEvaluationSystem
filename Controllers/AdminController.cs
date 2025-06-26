using CourseEvaluationSystem.Controllers;
using CourseEvaluationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context) => _context = context;

    // 🔐 Skyddar så endast Admins får komma åt AdminController
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var role = context.HttpContext.Session.GetString("Role");
        if (role != "Admin")
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }

        base.OnActionExecuting(context);
    }

    public async Task<IActionResult> Summary()
    {
        var summaries = await _context.Evaluations
            .Include(e => e.Course)
            .GroupBy(e => e.Course.Title)
            .Select(g => new EvaluationSummaryViewModel
            {
                CourseTitle = g.Key,
                TotalEvaluations = g.Count(),
                AverageRating = Math.Round(g.Average(e => e.Rating), 1)
            })
            .ToListAsync();

        return View(summaries);
    }
}
