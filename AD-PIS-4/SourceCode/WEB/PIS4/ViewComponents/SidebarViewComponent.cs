using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PIS4.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using SharpYaml.Serialization;

namespace PIS4.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        readonly IConfiguration _configuration;
        public string connectionString;

        public SidebarViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		public Formatting serializerSettings { get; private set; }

		public Task<IViewComponentResult> InvokeAsync()
        {
            List<GroupMenu> GroupMenuList = new List<GroupMenu>();
            connectionString = _configuration.GetConnectionString("ConnectionString");

            //==================================================================
            // Viewbag for showing UserLogin & UsrName
            //==================================================================
            ViewBag.UserLogin = Request.Cookies["UserLogin"];
            ViewBag.UserName = Request.Cookies["UserName"];
            ViewBag.UserGroupID = Request.Cookies["UserGroupID"];

            //==================================================================
            // Get list Group Menu 
            //==================================================================
            var UserGroupID = Request.Cookies["UserGroupID"];
            UserMenuprm prm = new UserMenuprm();
            prm.UserGroupID = UserGroupID;

            var apiURL = _configuration["URLAPI"];
            var client = new HttpClient();

            HttpResponseMessage response = client.PostAsJsonAsync(apiURL + "UserMenu/Menu", prm).Result;

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                Response resp = JsonConvert.DeserializeObject<Response>(apiResponse);
				GroupMenuList = JsonConvert.DeserializeObject<List<GroupMenu>>(resp.Contents.ToString());
			}
            return Task.FromResult<IViewComponentResult>(View(GroupMenuList));
        }
    }
}
