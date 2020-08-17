using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace ImageLoader
{
    public partial class WebForm21 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["imgLoad"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(cs))
            {
                MySqlCommand cmd = new MySqlCommand("sGetImageById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlParameter pId = new MySqlParameter()
                {
                    ParameterName = "@ID",
                    Value = Request.QueryString["ID"]
                };
                cmd.Parameters.Add(pId);

                connection.Open();
                byte[] bytes = (byte[])cmd.ExecuteScalar();

                string strBase = Convert.ToBase64String(bytes);
                Image1.ImageUrl = "data:Image/png;base64" + strBase;
            }
        }
    }
}