using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNextMealApp.Models.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MyNextMealApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        // => GET
        // => /Category/GetCategories
        [HttpGet]
        [Route("GetCategories")]
        [ProducesResponseType(typeof(RootCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<Category>> GetCategories(CancellationToken cancellationToken = default)
        {
            try
            {
                var call = "https://www.themealdb.com/api/json/v1/1/categories.php";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(call);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<RootCategory>(responseBody);
                    return deserializedResponse.categories;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in /Controllers/CategoryController/GetCategories => {ex}");
                return new List<Category>();
            }
        }
    }
}
