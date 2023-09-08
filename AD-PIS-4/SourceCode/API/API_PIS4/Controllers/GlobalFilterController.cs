using API_PIS4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PIS4.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    [Authorize]
    public class GlobalFilterController : ControllerBase
    {
        readonly IConfiguration _config;
        public string? constr;

        GlobalFilterDB db = new GlobalFilterDB();
        public GlobalFilterController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult Filter([FromBody] GlobalFilter prm)
        {
            constr = _config.GetConnectionString("DefaultConnection");
            Response resp = new Response();

            try
            {
                resp = db.Filter(constr, prm);
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
