using Microsoft.AspNetCore.Mvc;
using PIS4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;
using System;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace PIS4.Controllers
{
	public class UserLoginController : Controller
	{
		readonly IConfiguration _configuration;
		public string connectionString;
		public UserLoginController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public ActionResult UserLogin(string LogOut)
		{
			if (LogOut is not null)
			{
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                return View();
			}

			if (Request.Cookies["jwtCookie"] is null)
			{
				return View();
			}

			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult Login(UserLogin prm)
		{
			Response resp = new Response();
			try
			{
				var apiURL = _configuration["URLAPI"];
				var client = new HttpClient();

				var response = client.PostAsJsonAsync(apiURL + "UserLogin/Login", prm).Result;
				if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
				{
					string apiResponse = response.Content.ReadAsStringAsync().Result;
					resp = JsonConvert.DeserializeObject<Response>(apiResponse);

					if (resp.Message.ToLower().Contains("success"))
					{
						UserSetup user = JsonConvert.DeserializeObject<UserSetup>(resp.Contents.ToString());
						SetJWTCookie(user);
					}
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
		private void SetJWTCookie(UserSetup prm)
		{
			var cookieOptions = new CookieOptions
			{
				Expires = DateTime.UtcNow.AddMinutes(60),
			};

			Response.Cookies.Append("jwtCookie", prm.Token, cookieOptions);
			Response.Cookies.Append("UserLogin", prm.UserID, cookieOptions);
			Response.Cookies.Append("UserName", prm.UserName, cookieOptions);
			Response.Cookies.Append("UserGroupID", prm.UserGroupID, cookieOptions);
		}
	}
}
