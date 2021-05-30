using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

private object descriptiontxt;
private object productNametxt;
private object categorytxt2;
private object productDescriptiontxt;

namespace GUCommerce
{
    public partial class products2 : System.Web.UI.Page
    {
        //private object offeramnt;
        //private object TextBox3;
        //private object offer_id;
        //private object _category;
        //private object amount;
        //private object offeramount;

        protected void Page_Load(object sender, EventArgs e)

        {
        }


        protected void postproduct(object sender, EventArgs e)
        {
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("postProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //To read the input from the user
            string vendorName = (string)(Session["x"]);
            string productN = productnametxt.Text;
            string description = descriptiontxt.Text;
            string category = categorytxt.Text;
            string price1 = pricetxt.Text;
            string color1 = colortxt.Text;


            //pass parameters to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@vendor_name", vendorName));
            cmd.Parameters.Add(new SqlParameter("@productname", productN));
            cmd.Parameters.Add(new SqlParameter("@category", category));
            cmd.Parameters.Add(new SqlParameter("@description", description));
            cmd.Parameters.Add(new SqlParameter("@price", price1));
            cmd.Parameters.Add(new SqlParameter("@color", color1));




            //Save the output from the procedure
            SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
            count.Direction = ParameterDirection.Output;

            //Executing the SQLCommand
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();



        }

    }


    protected void VendorViewProducts(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd = new SqlCommand("VendorViewProducts", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        conn.Open();

        //IF the output is a table, then we can read the records one at a time
        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (rdr.Read())
        {
            //Get the value of the attribute name in the Company table
            string vendor_name = rdr.GetString(rdr.GetOrdinal("vendorName"));
            //Get the value of the attribute field in the Company table
            string product_description = rdr.GetString(rdr.GetOrdinal("field"));

            //Create a new label and add it to the HTML form
            Label lbl_vendorName = new Label();
            lbl_vendorName.Text = vendor_name;
            form1.Controls.Add(lbl_vendorName);


            //Label lbl_CompanyField = new Label();
            //lbl_CompanyField.Text = companyField + "  <br /> <br />";
            //form1.Controls.Add(lbl_CompanyField);
        }
        //this is how you retrive data from session variable.
        //string field1 = (string)(Session["field1"]);
        //Response.Write(field1);
    }

    protected void EditProduct(object sender, EventArgs e)
    {
        //Get the information of the connection to the database
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("EditProduct", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string vendorName = (string)(Session["x"]);
        string serialNo = serialNotxt.Text;
        string productName = productNametxt.Text;
        string category = categorytxt2.Text;
        string productDescription = productDescriptiontxt.Text;


        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@vendor_name", vendorName));
        cmd.Parameters.Add(new SqlParameter("@serialNo", serialNo));
        cmd.Parameters.Add(new SqlParameter("@productName", productName));
        cmd.Parameters.Add(new SqlParameter("@category", category));
        cmd.Parameters.Add(new SqlParameter("@productDescription", productDescription));





        //Save the output from the procedure
        SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
        count.Direction = ParameterDirection.Output;

        //Executing the SQLCommand
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();



    }




    protected void applyOffer(object sender, EventArgs e)
    {
        //Get the information of the connection to the database
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("applyOffer", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string vendorName = (string)(Session["x"]);
        int offerid = offeridtxt.Text;
        int @serial_no = serial_notxt.Text;


        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@vendor_name", vendorName));
        cmd.Parameters.Add(new SqlParameter("@offerid", offerid));
        cmd.Parameters.Add(new SqlParameter("@serial_no", serial_no));





        //Save the output from the procedure
        SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
        count.Direction = ParameterDirection.Output;

        //Executing the SQLCommand
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();



    }




    protected void applyOffer(object sender, EventArgs e)
    {
        //Get the information of the connection to the database
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("applyOffer", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string vendorName = (string)(Session["x"]);
        int offerid = offeridtxt.Text;
        int @serial_no = serial_notxt.Text;


        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@vendor_name", vendorName));
        cmd.Parameters.Add(new SqlParameter("@offerid", offerid));
        cmd.Parameters.Add(new SqlParameter("@serial_no", serial_no));





        //Save the output from the procedure
        SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
        count.Direction = ParameterDirection.Output;

        //Executing the SQLCommand
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();



    }
    protected void addOffer(object sender, EventArgs e)
    {
        //Get the information of the connection to the database
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("addOffer", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        int offerAmount = offerAmounttxt.Text;
        int expiry_date = expiry_datetxt.Text;



        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@offerAmount", offerAmount));
        cmd.Parameters.Add(new SqlParameter("@expiry_date", expiry_date));





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
protected void checkandremoveExpiredOffer(object sender, EventArgs e)
{
    //Get the information of the connection to the database
    string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

    //create a new connection
    SqlConnection conn = new SqlConnection(connStr);

    /*create a new SQL command which takes as parameters the name of the stored procedure and
     the SQLconnection name*/
    SqlCommand cmd = new SqlCommand("checkandremoveExpiredOffer", conn);
    cmd.CommandType = CommandType.StoredProcedure;

    //To read the input from the user

    int offerid = offerIdtxt.Text;


    //pass parameters to the stored procedure

    cmd.Parameters.Add(new SqlParameter("@offer_id", offerid));




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
        // Session["x"] = username;

        //To navigate to another webpage
        Response.Redirect("Companies.aspx", true);

    }
    else
    {
        Response.Write("Failed");
    }
}
}