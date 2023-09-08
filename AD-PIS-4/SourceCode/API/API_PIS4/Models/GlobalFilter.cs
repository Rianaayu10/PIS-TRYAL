using System.Data;
using System.Data.SqlClient;

namespace API_PIS4.Models
{
    public class GlobalFilter
    {
        public string? Code { get; set; }
        public string? CodeDesc { get; set; }

        public string? ActionType { get; set; }
        public string? Param { get; set; }
        public string? Param1 { get; set; }
        public string? Param2 { get; set; }
        public string? Param3 { get; set; }
    }

    public class GlobalFilterDB
    {
        public Response Filter(string ? connectionString, GlobalFilter prm)
        {
            Response resp = new Response();
            List<GlobalFilter> list = new List<GlobalFilter>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SP_GlobalFilter";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", prm.ActionType);
                    cmd.Parameters.AddWithValue("Param", prm.Param);
                    cmd.Parameters.AddWithValue("Param1", prm.Param1);
                    cmd.Parameters.AddWithValue("Param2", prm.Param2);
                    cmd.Parameters.AddWithValue("Param3", prm.Param3);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        GlobalFilter det = new GlobalFilter();
                        det.Code = dr["Code"].ToString();
                        det.CodeDesc = dr["CodeDesc"].ToString();

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
                resp.Message = "Error API on SP_GlobalFilter (Action type : " + prm.ActionType + ")!, Error Message = " + ex.Message;
                resp.Contents = "";
            }
            return resp;
        }
    }
}
