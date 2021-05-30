using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace GUCommerce
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void userLogin(object sender, EventArgs e)
        {
            //configuration el ana I saved f el webconfig
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);
            String username = TextBox1.Text;
            String passw = TextBox2.Text;
        
            SqlCommand cmd = new SqlCommand("userlogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cmd.Parameters.Add(new SqlParameter("@password", passw));

            SqlParameter success = cmd.Parameters.Add("@success", SqlDbType.Int);
            SqlParameter type = cmd.Parameters.Add("@type", SqlDbType.Int);
            success.Direction = ParameterDirection.Output;
            type.Direction = ParameterDirection.Output;
            //execute procedure
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            if (success.Value.ToString().Equals("1") & type.Value.ToString().Equals("0"))
            {
                
                Session["cname"] = username; //page el wa7eeda el shayfa el customer name heya el login fa bab3ato f session
                Response.Redirect("viewproducts.aspx", true);
                //Response.Write("Success");

            }
            else if (success.Value.ToString().Equals("1") & type.Value.ToString().Equals("1"))
            {
                Session["cname"] = username; //page el wa7eeda el shayfa el customer name heya el login fa bab3ato f session
                Response.Redirect("products2.aspx", true);
                //Response.Write("Success");
            }
            else if(success.Value.ToString().Equals("1") & type.Value.ToString().Equals("2"))
            {
                Session["cname"] = username; //page el wa7eeda el shayfa el customer name heya el login fa bab3ato f session
                Response.Redirect("Home.aspx", true);
                //Response.Write("Success");
            }
            else
            {
                Response.Write("Fail");
            }

        }
        protected void redirectC(object sender, EventArgs e)
        {
            Response.Redirect("CustomerRegistration.aspx", true);
        }
        protected void redirectV(object sender, EventArgs e)
        {
            Response.Redirect("VendorRegistration.aspx", true);
        }
    }
}