using API_PIS4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_PIS4.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    public class UserLoginController : ControllerBase
    {
        readonly IConfiguration _config;
        public string? constr;

        UserLoginDB db = new UserLoginDB();
        public UserLoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult Login([FromBody] UserLogin prm)
        {
            Response resp = new Response();
            constr = _config.GetConnectionString("DefaultConnection");
            try
            {
                Response respLogin = new Response();
                respLogin = db.Login(constr, prm);

                if (respLogin.ID == "0" && respLogin.Message.ToLower().Contains("success"))
                {
                    //==================================================
                    // Generate Token
                    //==================================================
                    UserSetup? _prm = respLogin.Contents as UserSetup;

                    Response respToken = GenerateToken(_prm);
                    if (respToken.ID == "0" && respToken.Message.ToLower().Contains("success"))
                    {
                        //=========================================
                        // Add Token in parameter
                        //=========================================
                        _prm.Token = respToken.Contents as string;


                        //=========================================
                        // Response
                        //=========================================
                        resp.ID = respToken.ID;
                        resp.Message = respToken.Message;
                        resp.Contents = _prm;
                        return Ok(resp);
                    }
                    else
                    {
                        //=========================================
                        // Bad Response
                        //=========================================
                        resp.ID = respToken.ID;
                        resp.Message = respToken.Message;
                        resp.Contents = "";
                        return BadRequest(resp);
                    }
                    //==================================================
                }
                else
                {
                    //=========================================
                    // Bad Response
                    //=========================================
                    resp.ID = respLogin.ID;
                    resp.Message = respLogin.Message;
                    resp.Contents = "";

                    return BadRequest(resp);
                }

            }
            catch (Exception ex)
            {
                resp.ID = resp.ID;
                resp.Message = ex.Message;
                resp.Contents = "";

                return BadRequest(resp);
            }
        }

        [NonAction]
        private Response GenerateToken(UserSetup? prm)
        {
            Response resp = new Response();

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var authclaims = new[]
                {
				//==================================================
				// Ada 3 cara untuk membuat claim :
				// 1. new Claim(JwtRegisteredClaimNames.NameId, user.UserID)
				// 2. new Claim(ClaimTypes.NameIdentifier, user.UserID),
				// 3. new Claim("userid", user.UserID), 
				//==================================================
				new Claim(JwtRegisteredClaimNames.NameId, prm.UserID),
                new Claim(JwtRegisteredClaimNames.Name, prm.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: authclaims,
                    expires: DateTime.Now.AddMinutes(3),
                    notBefore: DateTime.Now,
                    signingCredentials: credentials
                );

                resp.ID = "0";
                resp.Message = "Success";
                resp.Contents = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                resp.ID = "0";
                resp.Message = "Success";
                resp.Contents = "Generate token error!," + ex.Message + "";
            }

            return resp;
        }
    }
}
