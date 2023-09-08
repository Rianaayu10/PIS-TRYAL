using System.Data;
using System.Data.SqlClient;

namespace API_PIS4.Models
{
	public class UserLogin
	{
		public string? ActionType { get; set; }
		public string? UserID { get; set; }
		public string? Password { get; set; }
	}

	public class UserLoginDB
	{
		public Response Login(string? constr, UserLogin prm)
		{
			Response resp = new Response();
			Encryption encrypt = new Encryption();
			UserSetup data = new UserSetup();
			string? Msg = "";
			String? ID = "0";

			try
            {
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_UserLogin";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserID", prm.UserID);
					cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(prm.Password, prm.UserID));

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						data.UserGroupID = dr["UserGroupID"].ToString();
						data.UserName = dr["UserName"].ToString();
						data.UserID = dr["UserID"].ToString();
						Msg = dr["Msg"].ToString();
						ID = dr["ID"].ToString();
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = ID;
				resp.Message = Msg;
				resp.Contents = data;
			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_UserLogin (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;

		}

	}
}
