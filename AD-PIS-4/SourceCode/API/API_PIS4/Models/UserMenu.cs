using System.Data;
using System.Data.SqlClient;

namespace API_PIS4.Models
{
	public class GroupMenu
	{
		public string? ActionType { get; set; }
		public string? GroupIcon { get; set; }
		public string? GroupID { get; set; }
		public string? GroupName { get; set; }
		public string? GroupIndeks { get; set; }
		public string? UserGroupID { get; set; }
		public string? SubGroupCount { get; set; }
        public string? GroupURL { get; set; }
        public List<SubGroupMenu>? SubGroupMenu { get; set; }

	}

	public class SubGroupMenu
	{
		public string? ActionType { get; set; }
		public string? GroupID { get; set; }
		public string? SubGroupID { get; set; }
		public string? SubGroupName { get; set; }
		public string? SubGroupIndeks { get; set; }
		public string? UserGroupID { get; set; }
		public List<UserMenu>? Menu { get; set; }
	}

	public class UserMenu
	{
		public string? ActionType { get; set; }
		public string? GroupID { get; set; }
		public string? SubGroupID { get; set; }
		public string? MenuID { get; set; }
		public string? MenuDesc { get; set; }
		public string? Controller { get; set; }
		public string? Action { get; set; }
		public string? MenuIndeks { get; set; }
		public string? UserGroupID { get; set; }
    }

	public class UserMenuprm
	{
		public string? UserGroupID { get; set; }
	}
	public class UserMenuDB
	{
		public Response GroupMenu(string ? constr, GroupMenu prm)
		{
			Response resp = new Response();
			List<GroupMenu> Grouplist = new List<GroupMenu>();

			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserMenu";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						GroupMenu Group = new GroupMenu();
						Group.GroupIcon = dr["GroupIcon"].ToString();
						Group.GroupID = dr["GroupID"].ToString();
						Group.GroupName = dr["GroupName"].ToString();
						Group.GroupIndeks = dr["GroupIndeks"].ToString();
						Group.SubGroupCount = dr["SubGroupCount"].ToString();
                        Group.GroupURL = dr["GroupURL"].ToString();
                        Grouplist.Add(Group);
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = "0";
				resp.Message = "Success";
				resp.Contents = Grouplist;

			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_UserMenu (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;

		}

		public Response SubGroupMenu(string ? constr, SubGroupMenu prm)
		{
			Response resp = new Response();
			List<SubGroupMenu> subGrouplist = new List<SubGroupMenu>();

			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserMenu";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);
					cmd.Parameters.AddWithValue("GroupID", prm.GroupID);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						SubGroupMenu subGroup = new SubGroupMenu();
						subGroup.GroupID = dr["GroupID"].ToString();
						subGroup.SubGroupID = dr["SubGroupID"].ToString();
						subGroup.SubGroupName = dr["SubGroupName"].ToString();
						subGroup.SubGroupIndeks = dr["SubGroupIndeks"].ToString();
						subGrouplist.Add(subGroup);
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = "0";
				resp.Message = "Success";
				resp.Contents = subGrouplist;
			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_UserMenu (Action type : " + prm.ActionType + ") Error, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}


		public Response Menu(string ? constr, UserMenu prm)
		{
			Response resp = new Response();
			List<UserMenu> MenuList = new List<UserMenu>();

			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserMenu";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);
					cmd.Parameters.AddWithValue("GroupID", prm.GroupID);
					cmd.Parameters.AddWithValue("SubGroupID", prm.SubGroupID);

					SqlDataReader dr = cmd.ExecuteReader();


					while (dr.Read())
					{
						UserMenu Menu = new UserMenu();
						Menu.MenuID = dr["MenuID"].ToString();
						Menu.MenuDesc = dr["MenuDesc"].ToString();
						Menu.Controller = dr["Controller"].ToString();
						Menu.Action = dr["Action"].ToString();
						Menu.MenuIndeks = dr["MenuIndeks"].ToString();
                        MenuList.Add(Menu);
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = "0";
				resp.Message = "Success";
				resp.Contents = MenuList;
			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_UserMenu (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}
	}
}
