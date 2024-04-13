using Flurl.Http;
using Newtonsoft.Json;

namespace BoathouseDotNet
{
	public class BoathouseApi
	{
		private readonly string m_boathouseApi;
		private readonly string m_boathousePortalID;
		private readonly string m_boathouseSecret;


		public BoathouseApi(IConfiguration config)
		{
			m_boathouseApi = config["Boathouse:API"];
			m_boathousePortalID = config["Boathouse:PortalID"];
			m_boathouseSecret = config["Boathouse:Secret"];
		}

		public async Task<BoathouseResponse?> GetBoathouseResponse(string email, string customerID, string returnUrl = null)
		{
			var body = await m_boathouseApi
							.PostJsonAsync(new
							{
								portalId = m_boathousePortalID,
								secret = m_boathouseSecret,
								email = email,
								paddleCustomerId = customerID,
								returnUrl = returnUrl
							})
							.ReceiveString();

			return JsonConvert.DeserializeObject<BoathouseResponse>(body);
		}
	}

	public class BoathouseResponse
	{
		public string paddleCustomerID { get; set; }
		public string billingPortalUrl { get; set; }
		public string pricingTableHtml { get; set; }
		public string[] activeSubscriptions { get; set; }
	}
}
