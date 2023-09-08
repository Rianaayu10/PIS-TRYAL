using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PIS4_PCD.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace PIS4_PCD.Controllers
{
    public class ShopMasterController : Controller
    {
		readonly IConfiguration _configuration;
		public string connectionString;

		public ShopMasterController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public IActionResult Index()
        {

			if (Request.Cookies["jwtCookie"] is null)
			{
				return RedirectToAction("UserLogin", "UserLogin");
			}

			//==================================================================
			// Viewbag for showing UserLogin & UsrName
			//==================================================================
			ViewBag.UserLogin = Request.Cookies["UserLogin"];
			ViewBag.UserName = Request.Cookies["UserName"];
			ViewBag.UserGroupID = Request.Cookies["UserGroupID"];

			return View();
		}

		public IActionResult GetList(ShopMaster prm)
		{
			var jwt = Request.Cookies["jwtCookie"];
			Response resp = new Response();
			try
			{
				var apiURL = _configuration["URLAPI"];
				var client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
				HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "ShopMaster/GetList", prm).Result;

				if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
				{
					/*================================================================
                     * Convert response to cls response
                     =================================================================*/
					string apiResponse = response.Content.ReadAsStringAsync().Result;
					resp = JsonConvert.DeserializeObject<Response>(apiResponse);

					/*================================================================
					* Convert response.contents to cls usersetup
					=================================================================*/
					List<ShopMaster> shopmaster = JsonConvert.DeserializeObject<List<ShopMaster>>(resp.Contents.ToString());
					resp.Contents = shopmaster;
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

		public IActionResult Update(List<ShopMaster> prm)
		{
			var jwt = Request.Cookies["jwtCookie"];
			Response resp = new Response();
			try
			{
				var apiURL = _configuration["URLAPI"];
				var client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
				HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "ShopMaster/Update", prm).Result;

				if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
				{
					/*================================================================
                     * Convert response to cls response
                     =================================================================*/
					string apiResponse = response.Content.ReadAsStringAsync().Result;
					resp = JsonConvert.DeserializeObject<Response>(apiResponse);
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
