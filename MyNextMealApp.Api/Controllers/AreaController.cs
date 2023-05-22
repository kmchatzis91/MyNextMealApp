using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNextMealApp.Models.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNextMealApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly ILogger<AreaController> _logger;

        // => GET
        // => /Area/GetAreas
        [HttpGet]
        [Route("GetAreas")]
        [ProducesResponseType(typeof(RootArea), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<Area>> GetAreas(CancellationToken cancellationToken = default)
        {
            try
            {
                var jsonPath = @"..\MyNextMealApp.Api\Resources\areas.json";

                using (StreamReader streamReader = new StreamReader(jsonPath))
                {
                    string jsonItems = streamReader.ReadToEnd();
                    var deserializedResponse = JsonConvert.DeserializeObject<RootArea>(jsonItems);
                    return deserializedResponse.areas;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in /Controllers/AreaController/GetAreas => {ex}");
                return new List<Area>();
            }
        }
    }
}
