using API_PIS4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PIS4.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    [Authorize]
    public class UserGroupController : ControllerBase
    {
        public readonly IConfiguration _config;
        public string? constr;

        UserGroupDB db = new UserGroupDB();

        public UserGroupController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult GetList([FromBody] UserGroup prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
                resp = db.GetList(constr, prm);
                if (resp.ID == "0")
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = ex.Message;
                resp.Contents = "";

                return BadRequest(resp);
            }
        }

        [HttpPost]
        public ActionResult Update([FromBody] UserGroup prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
                resp = db.Update(constr, prm);
                if (resp.ID == "0")
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            catch (Exception ex)
            {
                resp.ID = "1";
                resp.Message = ex.Message;
                resp.Contents = "";

                return BadRequest(resp);
            }
        }
    }
}
