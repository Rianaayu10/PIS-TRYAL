using API_PIS4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_PIS4.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserMenuController : ControllerBase
	{
		readonly IConfiguration _config;
		public string? constr;

		UserMenuDB db = new UserMenuDB();
		public UserMenuController(IConfiguration config)
		{
			_config = config;
		}

		[HttpPost]
		public ActionResult Menu([FromBody] UserMenuprm prm)
		{
			constr = _config.GetConnectionString("DefaultConnection");
			Response resp = new Response();

			try
			{
				List<GroupMenu> GroupMenuList = new List<GroupMenu>();

				//=============================================================
				// Get list Group Menu
				//=============================================================
				GroupMenu prmGroup = new GroupMenu();
				prmGroup.GroupID = prm.UserGroupID;
				prmGroup.ActionType = "0";

				Response RespGroup = db.GroupMenu(constr, prmGroup);
				if (RespGroup.ID == "0")
				{
					List<GroupMenu>? GroupList = RespGroup.Contents as List<GroupMenu>;
					foreach (GroupMenu GroupItem in GroupList)
					{
						try
						{
							//=============================================================
							// Get list Sub Group Menu
							//=============================================================
							List<SubGroupMenu> SubGroupMenuList = new List<SubGroupMenu>();
							SubGroupMenu prmSubGroup = new SubGroupMenu();
							prmSubGroup.GroupID = prm.UserGroupID;
							prmSubGroup.ActionType = "1";
							prmSubGroup.GroupID = GroupItem.GroupID;

							Response RespSubGroup = db.SubGroupMenu(constr, prmSubGroup);

							if (RespSubGroup.ID == "0")
							{
								List<SubGroupMenu>? SubGroupList = RespSubGroup.Contents as List<SubGroupMenu>;
								foreach (SubGroupMenu SubGroupItem in SubGroupList)
								{
									try
									{
										//=============================================
										// Get list Menu 
										//=============================================
										List<UserMenu> MenuList = new List<UserMenu>();
										UserMenu prmMenu = new UserMenu();
										prmMenu.GroupID = prm.UserGroupID;
										prmMenu.ActionType = "2";
										prmMenu.GroupID = SubGroupItem.GroupID;
										prmMenu.SubGroupID = SubGroupItem.SubGroupID;

										Response RespMenu = db.Menu(constr, prmMenu);
										if (RespMenu.ID == "0")
										{
											List<UserMenu>? SubMenuList = RespMenu.Contents as List<UserMenu>;
											foreach (UserMenu MenuItem in SubMenuList)
											{
												try
												{
													UserMenu Menu = new UserMenu();
													Menu.MenuID = MenuItem.MenuID;
													Menu.MenuDesc = MenuItem.MenuDesc;
													Menu.Controller = MenuItem.Controller;
													Menu.Action = MenuItem.Action;
													Menu.MenuIndeks = MenuItem.MenuIndeks;
                                                    MenuList.Add(Menu);
												}
												catch (Exception ex)
												{
													resp.ID = "1";
													resp.Message = ex.Message.ToString();
													resp.Contents = "";

													return BadRequest(resp);
												}
											}
										}
										else
										{
											return BadRequest(RespMenu);
										}

										//======== End - Get list Menu ===============

										SubGroupMenu SubGroupMenu = new SubGroupMenu();
										SubGroupMenu.SubGroupID = SubGroupItem.SubGroupID;
										SubGroupMenu.SubGroupName = SubGroupItem.SubGroupName;
										SubGroupMenu.SubGroupIndeks = SubGroupItem.SubGroupIndeks;
										SubGroupMenu.Menu = MenuList;
										SubGroupMenuList.Add(SubGroupMenu);
									}
									catch (Exception ex)
									{
										resp.ID = "1";
										resp.Message = ex.Message.ToString();
										resp.Contents = "";

										return BadRequest(resp);
									}

								}
								//========== End - Get list Sub Group Menu ===============
							}
							else
							{
								return BadRequest(RespSubGroup);
							}


							GroupMenu GroupMenu = new GroupMenu();
							GroupMenu.GroupIcon = GroupItem.GroupIcon;
							GroupMenu.GroupID = GroupItem.GroupID;
							GroupMenu.GroupName = GroupItem.GroupName;
							GroupMenu.GroupIndeks = GroupItem.GroupIndeks;
							GroupMenu.SubGroupCount = GroupItem.SubGroupCount;
							GroupMenu.SubGroupMenu = SubGroupMenuList;
							GroupMenu.GroupURL = GroupItem.GroupURL;
							GroupMenuList.Add(GroupMenu);
						}
						catch (Exception ex)
						{
							resp.ID = "1";
							resp.Message = ex.Message.ToString();
							resp.Contents = "";

							return BadRequest(resp);
						}
					}
					//============ End - Get list Group Menu ==================


					//============================
					// Response OK
					//============================
					resp.ID = RespGroup.ID;
					resp.Message = RespGroup.Message;
					resp.Contents = GroupMenuList;

					return Ok(resp);
				}
				else
				{
					return BadRequest(RespGroup);
				}

			}
			catch (Exception ex)
			{

				resp.ID = "1";
				resp.Message = ex.Message.ToString();
				resp.Contents = "";

				return BadRequest(resp);
			}
		}

	}
}
