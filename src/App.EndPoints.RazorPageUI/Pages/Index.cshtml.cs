using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.EndPoints.RazorPageUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            FirstName = "Bamdad";
        }

        [BindProperty]
        public string FirstName { get; set; }


        public void OnGet()
        {
            //return Forbid();
        }

        public void OnPost()
        {

        }
    }
}
