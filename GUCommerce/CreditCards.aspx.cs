using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Globalization;

namespace GUCommerce
{
    public partial class CreditCards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Addc(object sender, EventArgs e)
        {
            try
            {
                //this is how you get data from session variable.
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);


                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("AddCreditCard", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                string cc = TextBox1.Text;
                String cvv = TextBox2.Text;
                try
                {
                    DateTime exp = DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    cmd.Parameters.Add(new SqlParameter("@expirydate", exp.Date));



                }
                catch (Exception x)
                {
                    throw new Exception("Please enter Date format as follows: DD/MM/YYYY");
                }
                cmd.Parameters.Add(new SqlParameter("@cvv", cvv));
                cmd.Parameters.Add(new SqlParameter("@creditcardnumber", cc));
                if (cc.Contains(" ") || cvv.Contains(" "))
                {
                    throw new Exception("no spaces are allowed in any field");
                }
                if (cc.Equals("") || cvv.Equals(""))
                {
                    throw new Exception("please enter all fields");
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Response.Write("You already inserted that mobile number :)");
                }
            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
            }

        }

        protected void homepage(object sender, EventArgs e)
        {
            Response.Redirect("viewproducts.aspx",true);
        }
    }
}