using System.Data;
using System.Data.SqlClient;

namespace API_PIS4.Models
{
    public class UserGroup
    {
        public string? ActionType { get; set; }
        public string? UserGroupID { get; set; }
        public string? UserGroupName { get; set; }
        public string? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }
        public string? RegisterUser { get; set; }
    }

    public class UserGroupDB
    {
        public Response GetList(string ? constr, UserGroup prm)
        {
            Response resp = new Response();
			List<UserGroup> list = new List<UserGroup>();

			try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
					con.Open();
					string sql = "SP_UserGroup_Sel";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);

					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						UserGroup det = new UserGroup();
						det.UserGroupID = dr["UserGroupID"].ToString();
						det.UserGroupName = dr["UserGroupName"].ToString();
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
                resp.Message = "Error API on SP_UserGroup_Sel (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
				resp.Contents = "";
            }
            return resp;
        }
        public Response Update(string ?constr, UserGroup prm)
        {
            Response resp = new Response();
			String? Message = "";
            String? ID = "";
			try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
					con.Open();
					string sql = "SP_UserGroup_Upd";

					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
					cmd.Parameters.AddWithValue("UserGroupID", prm.UserGroupID);
					cmd.Parameters.AddWithValue("UserGroupName", prm.UserGroupName);
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
                resp.Message = "Error API on SP_UserGroup_Upd (Action type : " + prm.ActionType + ")!, Error Message" + ex.Message;
				resp.Contents = "";
            }
            return resp;
        }
    }

}
