using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
	public class RegionsController(IHttpClientFactory httpClientFactory) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<RegionDto> response = [];

			try
			{
				// Get all regions from the web API
				var client = httpClientFactory.CreateClient();

				// This URL should be in a configuration file like appsettings.json
				var httpResponse = await client.GetAsync("https://localhost:7244/api/regions");

				httpResponse.EnsureSuccessStatusCode(); // Esnure success or throw error

				response.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
			}
			catch (Exception ex)
			{
				// Log error
				return View("Error");
			}

			return View(response);
		}

		[HttpGet]
		// Only referenced by the "Add Region" button in the Index view.
		// This method basically opens up a new view when that button is clicked
		public IActionResult Add()
		{
			return View();
		}

		// Works with the Add.cshtml view, though I don't see how it's called.
		// NOTE: IMPORTANT: This method is called because the view is associated
		// with it when we right click `return View()` and select "Add View".
		// Seems to be that it's called because it's the only POST method in the
		// controller, which matches the form action in the Add.cshtml view.
		// It receives the model that was passed from the view-model
		[HttpPost]
		public async Task<IActionResult> Add(AddRegionViewModel model)
		{
			var client = httpClientFactory.CreateClient();

			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://localhost:7244/api/regions"),
				Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
			};

			var httpResponseMessage = await client.SendAsync(httpRequestMessage);
			httpResponseMessage.EnsureSuccessStatusCode(); // Esnure success or throw error

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

			if (response is not null)
			{
				// Redirect to the index view (action) in the regions controller (directory) 
				return RedirectToAction("Index", "Regions");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var client = httpClientFactory.CreateClient();
			var httpResponse = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7244/api/regions/{id}");

			if (httpResponse is not null)
			{
				return View(httpResponse);
			}

			return View(null);
		}

		// The view forms only support GET and POST methods
		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, RegionDto model)
		{
			var client = httpClientFactory.CreateClient();
			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"https://localhost:7244/api/regions/{id}"),
				Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
			};
			var httpResponseMessage = await client.SendAsync(httpRequestMessage);
			httpResponseMessage.EnsureSuccessStatusCode(); // Esnure success or throw error
			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
			if (response is not null)
			{
				// Redirect to the edit action / view in the regions controller / directory
				return RedirectToAction("Edit", "Regions");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var client = httpClientFactory.CreateClient();
				var httpResponseMessage = await client.DeleteAsync($"https://localhost:7244/api/regions/{id}");
				httpResponseMessage.EnsureSuccessStatusCode(); // Esnure success or throw error
				return RedirectToAction("Index", "Regions");
			}
			catch (Exception e)
			{
				// Log to console
			}

			// Return to the edit view?
			return View("Edit");
		}
	}
}
