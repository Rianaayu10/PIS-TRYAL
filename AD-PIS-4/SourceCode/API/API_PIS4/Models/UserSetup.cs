using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace API_PIS4.Models
{
	public class UserSetup
	{
		public string? ActionType { get; set; }
		public string? UserID { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
        public string? ShopType { get; set; }
        public string? ShopDesc { get; set; }
        public string? Shift { get; set; }
        public string? ShiftDesc { get; set; }
        public string? UserGroupID { get; set; }
        public string? UserGroupDesc { get; set; }
        public string? Locked { get; set; }
		public string? Description { get; set; }
		public string? RegisterDate { get; set; }
		public string? RegisterUser { get; set; }
		public string? UpdateDate { get; set; }
		public string? UpdateUser { get; set; }
        public string? Token { get; set; }
    }

	public class UserSetupDB
	{
		public Response GetList(string ? constr, UserSetup prm)
		{
			Encryption encrypt = new Encryption();
			Response resp = new Response();
			List<UserSetup> list = new List<UserSetup>();

			try
            {
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserSetUp_Sel";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserID", prm.UserID);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						UserSetup det = new UserSetup();
						det.UserID = dr["UserID"].ToString();
						det.UserName = dr["UserName"].ToString();
						det.Password = encrypt.Decrypt(dr["Password"].ToString(), dr["UserID"].ToString());
						det.UserGroupID = dr["UserGroupID"].ToString();
						det.UserGroupDesc = dr["UserGroupDesc"].ToString();
						det.Shift = dr["Shift"].ToString();
						det.ShiftDesc = dr["ShiftDesc"].ToString();
						det.ShopType = dr["ShopType"].ToString();
						det.ShopDesc = dr["ShopDesc"].ToString();
						det.Locked = dr["Locked"].ToString();
						det.Description = dr["Description"].ToString();
						det.UpdateUser = dr["UpdateUser"].ToString();
						det.UpdateDate = dr["UpdateDate"].ToString();

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
				resp.Message = "Error API on SP_UserSetUp_Sel (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}

		public Response Update(string ? constr, UserSetup prm)
		{
			Encryption encrypt = new Encryption();
			Response resp = new Response();
			String? Message = "";
			string? ID = "";

			try
            {

				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserSetUp_Upd";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserID", prm.UserID);
					cmd.Parameters.AddWithValue("UserName", prm.UserName);
					string? pwd = "";
					if(prm.Password != null)
					{
						pwd = encrypt.Encrypt(prm.Password, prm.UserID);
					}
					cmd.Parameters.AddWithValue("Password", pwd);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);
					cmd.Parameters.AddWithValue("ShopType", prm.ShopType);
					cmd.Parameters.AddWithValue("Shift", prm.Shift);
					cmd.Parameters.AddWithValue("Description", prm.Description);
					cmd.Parameters.AddWithValue("Locked", prm.Locked);
					cmd.Parameters.AddWithValue("RegUser", prm.RegisterUser);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						Message = dr["Msg"].ToString();
						ID = dr["ID"].ToString();
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
				resp.Message = "Error API on SP_UserSetUp_Upd (Action type :" + prm.ActionType + ")!,  Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}
	}
}
