using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNextMealApp.Models.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MyNextMealApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {
        // => GET
        // => /Meal/GetMealsByCategory
        [HttpGet]
        [Route("GetMealsByCategory")]
        [ProducesResponseType(typeof(RootMealShort), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<MealShort>> GetMealsByCategory(string category, CancellationToken cancellationToken = default)
        {
            try
            {
                var call = $"https://www.themealdb.com/api/json/v1/1/filter.php?c={category}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(call);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<RootMealShort>(responseBody);
                    return deserializedResponse.meals;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in /Controllers/MealController/GetMealsByCategory => {ex}");
                return new List<MealShort>();
            }
        }

        // => GET
        // => /Meal/GetMealsByArea
        [HttpGet]
        [Route("GetMealsByArea")]
        [ProducesResponseType(typeof(RootMealShort), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<MealShort>> GetMealsByArea(string area, CancellationToken cancellationToken = default)
        {
            try
            {
                var call = $"https://www.themealdb.com/api/json/v1/1/filter.php?a={area}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(call);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<RootMealShort>(responseBody);
                    return deserializedResponse.meals;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in /Controllers/MealController/GetMealsByArea => {ex}");
                return new List<MealShort>();
            }
        }

        // => GET
        // => /Meal/GetMealById
        [HttpGet]
        [Route("GetMealById")]
        [ProducesResponseType(typeof(RootMealLong), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MealLong> GetMealById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var call = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(call);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<RootMealLong>(responseBody);
                    return deserializedResponse.meals.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in /Controllers/MealController/GetMealById => {ex}");
                return new MealLong();
            }
        }

        // => GET
        // => /Meal/GetRandomMeal
        [HttpGet]
        [Route("GetRandomMeal")]
        [ProducesResponseType(typeof(RootMealLong), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MealLong> GetRandomMeal(CancellationToken cancellationToken = default)
        {
            try
            {
                var call = $"https://www.themealdb.com/api/json/v1/1/random.php";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(call);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<RootMealLong>(responseBody);
                    return deserializedResponse.meals.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in /Controllers/MealController/GetRandomMeal => {ex}");
                return new MealLong();
            }
        }
    }
}
