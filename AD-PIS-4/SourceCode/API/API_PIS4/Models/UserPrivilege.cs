using System.Data;
using System.Data.SqlClient;

namespace API_PIS4.Models
{
	public class UserPrivilege
	{
		public string? ActionType { get; set; }
		public string? GroupIcon { get; set; }
		public string? GroupName { get; set; }
		public string? GroupID { get; set; }
		public string? MenuID { get; set; }
		public string? MenuDesc { get; set; }
		public string? AllowAccess { get; set; }
		public string? AllowUpdate { get; set; }
		public string? AllowDelete { get; set; }
		public string? RegisterUser { get; set; }
		public string? UserGroupID { get; set; }
	}

	public class UserPrivilegeDB
	{
		public Response GetList(string ? constr, UserPrivilege prm)
		{
			Response resp = new Response();
			List<UserPrivilege> list = new List<UserPrivilege>();

			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserPrivilege_Sel";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						UserPrivilege det = new UserPrivilege();
						det.GroupName = dr["GroupName"].ToString();
						det.GroupIcon = dr["GroupIcon"].ToString();
						det.MenuID = dr["MenuID"].ToString();
						det.MenuDesc = dr["MenuDesc"].ToString();
						det.AllowAccess = dr["AllowAccess"].ToString();
						det.AllowUpdate = dr["AllowUpdate"].ToString();
						det.AllowDelete = dr["AllowDelete"].ToString();

						list.Add(det);
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = "0";
				resp.Message = "Success";
				resp.Contents = list;
			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_UserPrivilege_Sel (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}
		
		public Response Update(string ? constr, UserPrivilege prm)
		{
			Response resp = new Response();
			String? Message = "";
			String? ID = "";

			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserPrivilege_Upd";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);
					cmd.Parameters.AddWithValue("MenuID", prm.MenuID);
					cmd.Parameters.AddWithValue("AllowAccess", prm.AllowAccess);
					cmd.Parameters.AddWithValue("AllowUpdate", prm.AllowUpdate);
					cmd.Parameters.AddWithValue("AllowDelete", prm.AllowDelete);
					cmd.Parameters.AddWithValue("RegUser", prm.RegisterUser);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						Message = dr["Msg"].ToString();
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = ID;
				resp.Message = Message;
				resp.Contents = "";
			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_UserPrivilege_Upd (Action type : " + prm.ActionType + ")!, Error Message" + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}
	}
}
