using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System;
using PIS4.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PIS4.Controllers
{
    public class GlobalFilterController : Controller
    {
        readonly IConfiguration _configuration;
        public string connectionString;
        public GlobalFilterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Filter(GlobalFilter prm)
        {
            var jwt = Request.Cookies["jwtCookie"];
            Response resp = new Response();

            try
            {
                var apiURL = _configuration["URLAPI"];
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "GlobalFilter/Filter", prm).Result;

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
					/*=================================================================
                      Convert response contentes to class response
                     ==================================================================*/
					string apiResponse = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Response>(apiResponse);

					/*=================================================================
                      Convert response contentes to class global filter
                     ==================================================================*/
					List<GlobalFilter> filter = JsonConvert.DeserializeObject<List<GlobalFilter>>(resp.Contents.ToString());
                    resp.Contents = filter;
				}

                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    resp.ID = "400";
                    resp.Message = "Unauthorized";
                    resp.Contents = "";
                }
                else
                {
                    resp.ID = "1";
                    resp.Message = response.RequestMessage.ToString();
                    resp.Contents = "";
                }
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = ex.Message;
                resp.Contents = "";
            }

			return Json(resp);
		}
    }
}
