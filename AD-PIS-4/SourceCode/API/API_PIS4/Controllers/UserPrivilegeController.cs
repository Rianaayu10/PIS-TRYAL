using API_PIS4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PIS4.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserPrivilegeController : ControllerBase
    {
        public readonly IConfiguration _config;
        public string? constr;

        UserPrivilegeDB db = new UserPrivilegeDB();

        public UserPrivilegeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult GetList([FromBody] UserPrivilege prm)
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
        public ActionResult Update([FromBody] List<UserPrivilege> prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
                foreach (UserPrivilege item in prm)
                {
                    try
                    {
                        resp = db.Update(constr, item);
                        if (resp.ID == "1")
                        {
                            return BadRequest(resp);
                        }

                    }
                    catch (Exception ex)
                    {
                        resp.ID = "0";
                        resp.Message = ex.Message;
                        resp.Contents = "";

                        return BadRequest(resp);
                    }
                }
                return Ok(resp);
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
