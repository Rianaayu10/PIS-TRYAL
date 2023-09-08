using System.Data;
using System.Data.SqlClient;

namespace API_PIS4.Models
{
	public class ChangePassword
	{
		public string? UserID { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }

	}

	public class ChangePasswordDB
	{
		public Response GetList(string ? constr, ChangePassword prm)
		{
			Response resp = new Response();
			Encryption encrypt = new Encryption();
			ChangePassword data = new ChangePassword();
			
			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_ChangePassword_Sel";
					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("UserID", prm.UserID);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						data.UserID = dr["UserID"].ToString();
						data.UserName = dr["UserName"].ToString();					
						data.Password = encrypt.Decrypt(dr["Password"].ToString(), prm.UserID);
					}

					dr.Dispose();
					cmd.Dispose();
					con.Close();
				}

				resp.ID = "0";
				resp.Message = "Success";
				resp.Contents = data;
			}
			catch (Exception ex)
			{
				resp.ID = "1";
				resp.Message = "Error API on SP_ChangePassword_Sel Error !, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}

		public Response Update(string ? constr, ChangePassword prm)
		{
			Response resp = new Response();
			Encryption encrypt = new Encryption();			
			String? Message = "";
			String? ID = "";

			try
			{
				using (SqlConnection con = new SqlConnection(constr))
				{
					con.Open();
					string sql = "SP_ChangePassword_Upd";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("UserID", prm.UserID);
					cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(prm.Password, prm.UserID));

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
				resp.Message = "Error API on SP_UserPrivilege_Upd Error !, Error Message = " + ex.Message;
				resp.Contents = "";
			}
			return resp;
		}

	}
}
