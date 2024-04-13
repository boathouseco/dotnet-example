using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoathouseDotNet.Pages
{
	public class CreateAccountModel(IConfiguration configuration) : PageModel
	{
		public bool Success { get; set; }

		public async Task OnGet()
		{
			var paddleCustomerID = Request.Cookies["PaddleCustomerID"];

			if (!string.IsNullOrWhiteSpace(paddleCustomerID))
			{
				Success = true;
			}
		}

		public async Task OnPost()
		{
			var randomEmailAddress = "playground-" + Guid.NewGuid().ToString("N") + "@mailexample.com";

			var resp = await new BoathouseApi(configuration).GetBoathouseResponse(randomEmailAddress, null);

			Response.Cookies.Append("PaddleCustomerID", resp.paddleCustomerID);

			Success = true;
		}
	}
}
