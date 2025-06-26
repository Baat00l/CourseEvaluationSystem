using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseEvaluationSystem.Models
{
    public class EvaluationFilterViewModel
    {
        [Display(Name = "Kurs")]
        public int? CourseID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Från datum")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Till datum")]
        public DateTime? ToDate { get; set; }

        public List<SelectListItem> CourseList { get; set; } = new();

        public List<Evaluation> Results { get; set; } = new();
    }
}