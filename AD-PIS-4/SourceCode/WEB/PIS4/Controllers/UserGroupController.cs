using Microsoft.AspNetCore.Http;
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
    public class UserGroupController : Controller
    {
        readonly IConfiguration _configuration;
        public string connectionString;
        public UserGroupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ActionResult UserGroup()
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
        public IActionResult Update(UserGroup prm)
        {
            var jwt = Request.Cookies["jwtCookie"];
            Response resp = new Response();
            try
            {
                var apiURL = _configuration["URLAPI"];
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "UserGroup/Update", prm).Result;

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Response>(apiResponse);
                }
				else if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					resp.ID = "400";
					resp.Message = "Unauthorized";
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
        public IActionResult GetList(UserGroup prm)
        {
            var jwt = Request.Cookies["jwtCookie"];
            Response resp = new Response();
            try
            {
                var apiURL = _configuration["URLAPI"];
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "UserGroup/GetList", prm).Result;

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
					/*================================================================
                     * Convert response to cls response
                     =================================================================*/
					string apiResponse = response.Content.ReadAsStringAsync().Result;
					resp = JsonConvert.DeserializeObject<Response>(apiResponse);

                    /*================================================================
					* Convert response.contents to cls usergroup
					=================================================================*/
                    List<UserGroup> UserGroup = JsonConvert.DeserializeObject<List<UserGroup>>(resp.Contents.ToString());
					resp.Contents = UserGroup;
				}
				else if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					resp.ID = "400";
					resp.Message = "Unauthorized";
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

		public IActionResult Update_Privilege([FromBody] List<UserPrivilege> prm)
        {
            var jwt = Request.Cookies["jwtCookie"];
            Response resp = new Response();
            try
            {
                var apiURL = _configuration["URLAPI"];
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "UserPrivilege/Update", prm).Result;

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Response>(apiResponse);
                }
				else if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					return RedirectToAction("UserLogin", "UserLogin");
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
        public IActionResult GetList_Privilege(UserPrivilege prm)
        {
            var jwt = Request.Cookies["jwtCookie"];
            Response resp = new Response();
            try
            {
                var apiURL = _configuration["URLAPI"];
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "UserPrivilege/GetList", prm).Result;

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
					/*================================================================
                     * Convert response to cls response
                     =================================================================*/
					string apiResponse = response.Content.ReadAsStringAsync().Result;
					resp = JsonConvert.DeserializeObject<Response>(apiResponse);

					/*================================================================
					* Convert response.contents to cls usergroup
					=================================================================*/
					List<UserPrivilege> UserPrivilege = JsonConvert.DeserializeObject<List<UserPrivilege>>(resp.Contents.ToString());
					resp.Contents = UserPrivilege;
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
