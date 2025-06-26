using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseEvaluationSystem.Views.Admin
{
    public class AdminSummaryModel : PageModel // Renamed from SummaryModel to AdminSummaryModel
    {
        public void OnGet() // Removed 'override' keyword
        {
        }
    }
}
