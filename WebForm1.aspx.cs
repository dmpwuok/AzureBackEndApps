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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Message.Visible = false;
                HLink.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }


        protected void UploadButton_Click1(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExt = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                string cs = ConfigurationManager.ConnectionStrings["imgLoad"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(cs))
                {
                    MySqlCommand cmd = new MySqlCommand("ImageUpload", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter pName = new MySqlParameter()
                    {
                        ParameterName = "@Name",
                        Value = fileName
                    };
                    cmd.Parameters.Add(pName);

                    MySqlParameter pSize = new MySqlParameter()
                    {
                        ParameterName = "@Size",
                        Value = fileSize
                    };
                    cmd.Parameters.Add(pSize);

                    MySqlParameter pImageData = new MySqlParameter()
                    {
                        ParameterName = "@ImageData",
                        Value = bytes
                    };
                    cmd.Parameters.Add(pImageData);

                    MySqlParameter pNewID = new MySqlParameter()
                    {
                        ParameterName = "@NewID",
                        Value = -1,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pNewID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    Message.Visible = true;
                    Message.Text = "Upload Successful!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    HLink.Visible = true;
                }
            }
            else
            {
                Message.Visible = true;
                Message.Text = "Only images (.jpg, .png) can be uploaded!";
                Message.ForeColor = System.Drawing.Color.Red;
                HLink.Visible = false;
            }
        }
    }
}
