using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BoathouseDotNet.Pages
{
	public class ProcessingModel(IConfiguration configuration) : PageModel
	{
		[BindProperty(SupportsGet = true, Name ="pids")]
		public string PriceIDs { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var priceIds = PriceIDs.Split(",");

			var paddleCustomerID = Request.Cookies["PaddleCustomerID"];
			var boathouse = await new BoathouseApi(configuration).GetBoathouseResponse(null, paddleCustomerID);

			var checkoutCompleted = priceIds.All(pid => boathouse.activeSubscriptions.Any(p=>p.Equals(pid, StringComparison.OrdinalIgnoreCase)));

			if (checkoutCompleted)
			{
				return RedirectToPage("Account");
			}
			else
			{
				return Page();
			}
		}
	}
}
