using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BoathouseDotNet.Pages
{
	public class AccountModel(IConfiguration configuration) : PageModel
	{
		public BoathouseResponse? Boathouse { get; set; }

		public async Task<IActionResult> OnGet()
		{
			var paddleCustomerID = Request.Cookies["PaddleCustomerID"];

			if (string.IsNullOrWhiteSpace(paddleCustomerID))
			{
				return RedirectToPage("CreateAccount");
			}

			var returnUrl = Url.Page("/Account", pageHandler: null, values: new { returnUrl = Request.Path }, protocol: Request.Scheme);

			Boathouse = await new BoathouseApi(configuration).GetBoathouseResponse(null, paddleCustomerID, returnUrl);

			
			return Page();

		}
	}
}
