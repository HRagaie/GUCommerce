using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUCommerce
{
    public partial class TodaysDeal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CreateDeal(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["Milestone"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("createTodaysDeal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //To read the input from the user
            int dealAmount = System.Int32.Parse(damountT.Text);
            string admin = adminT.Text;
            DateTime exp = System.DateTime.Parse(expdateT.Text);



            //pass parameters to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@deal_amount", dealAmount));
            cmd.Parameters.Add(new SqlParameter("@admin_username", admin));
            cmd.Parameters.Add(new SqlParameter("@expiry_date", exp));

            //Save the output from the procedure
            SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
            count.Direction = ParameterDirection.Output;

            //Executing the SQLCommand
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


            if (count.Value.ToString().Equals("1"))
            {
                //To send response data to the client side (HTML)
                Response.Write("Passed");

                /*ASP.NET session state enables you to store and retrieve values for a user
                as the user navigates ASP.NET pages in a Web application.
                This is how we store a value in the session*/
                Session["field1"] = "HIIII";

                //To navigate to another webpage
                Response.Redirect("Companies.aspx", true);

            }
            else
            {
                Response.Write("Failed");
            }
        }




        protected void addDeal(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["Milestone"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("addTodaysDealOnProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //To read the input from the user
            int dealID = System.Int32.Parse(dIdT.Text);
            int serial = System.Int32.Parse(serialnT.Text);

            //pass parameters to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@deal_id", dealID));
            cmd.Parameters.Add(new SqlParameter("@serial_no", serial));

            //Save the output from the procedure
            SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
            count.Direction = ParameterDirection.Output;

            //Executing the SQLCommand
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


            if (count.Value.ToString().Equals("1"))
            {
                //To send response data to the client side (HTML)
                Response.Write("Passed");

                /*ASP.NET session state enables you to store and retrieve values for a user
                as the user navigates ASP.NET pages in a Web application.
                This is how we store a value in the session*/
                Session["field1"] = "HIIII";

                //To navigate to another webpage
                Response.Redirect("Companies.aspx", true);

            }
            else
            {
                Response.Write("Failed");
            }
        }









        protected void deleteDeal(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["Milestone"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("removeExpiredDeal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //To read the input from the user
            int deal = System.Int32.Parse(dealIDT.Text);


            //pass parameters to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@deal_id", deal));

            //Save the output from the procedure
            SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
            count.Direction = ParameterDirection.Output;

            //Executing the SQLCommand
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


            if (count.Value.ToString().Equals("1"))
            {
                //To send response data to the client side (HTML)
                Response.Write("Passed");

                /*ASP.NET session state enables you to store and retrieve values for a user
                as the user navigates ASP.NET pages in a Web application.
                This is how we store a value in the session*/
                Session["field1"] = "HIIII";

                //To navigate to another webpage
                Response.Redirect("Companies.aspx", true);

            }
            else
            {
                Response.Write("Failed");
            }
        }

    }
}