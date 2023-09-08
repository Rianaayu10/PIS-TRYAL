using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using Newtonsoft.Json;
using PIS4.Models;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace PIS4.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {     
            //==================================================================
            // Viewbag for showing UserLogin & UsrName
            //==================================================================
            ViewBag.UserLogin = Request.Cookies["UserLogin"];
            ViewBag.UserName = Request.Cookies["UserName"];
            ViewBag.UserGroupID = Request.Cookies["UserGroupID"];

            List<UserMenu> yourModelClass = new List<UserMenu>();
            return Task.FromResult<IViewComponentResult>(View(yourModelClass));
        }
    }
}
