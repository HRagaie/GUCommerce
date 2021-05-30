using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Drawing;

namespace GUCommerce
{
    public partial class orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string field1 = (string)(Session["cname"]);
            //configuration el ana I saved f el webconfig
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("makeOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@customername", field1));

            conn.Open();
            //IF the output is a table, then we can read the records one at a time
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                try
                {
                    int orderno = rdr.GetInt32(rdr.GetOrdinal("order_no"));
                    Session["orderid"] = orderno;
                    decimal totalamount = rdr.GetDecimal(rdr.GetOrdinal("total_amount"));
                    Session["orderamount"] = totalamount;
                    Label4.Text = "Order no: " + orderno + "<br></br>";
                    
                    Label5.Text ="Total amount: "+ totalamount + "<br></br>";
                   

                }

                catch (Exception e1)
                {
                    Response.Write(e1.Message);

                }

            }
           
        }

        protected void specifyAmount(object sender, EventArgs e)
        {

            try
            {
                Label5.Visible = true;
                Label4.Visible = true;
                Boolean check = false;
                string field1 = (string)(Session["cname"]);
                int field2 = (int)(Session["orderid"]);
                decimal field3 = (decimal)(Session["orderamount"]);
                // Response.Write(field2 + "is your order number and total amount is" + field3);

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

               


                    SqlCommand cmd = new SqlCommand("specifyAmount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@customername", field1));
                    cmd.Parameters.Add(new SqlParameter("@orderID", field2));


                    if (!((TextBox1.Text).Equals("")) & !((TextBox3.Text).Equals(""))) //both cash and credit fields filled
                    {
                        throw new Exception("please choose one method of payment ONLY");
                    }
                    else if (((TextBox1.Text).Equals("")) & ((TextBox3.Text).Equals(""))) //both cash and credit fields empty
                    {
                        throw new Exception("please enter the amount you'll be paying");
                    }
                    else if ((TextBox3.Text).Equals(""))//credit empty
                    {

                        decimal cash = decimal.Parse(TextBox1.Text);
                        decimal credit = 0;
                        if (cash > field3)
                        {
                            throw new Exception("Please enter an appropriate amount");
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@cash", cash));
                            cmd.Parameters.Add(new SqlParameter("@credit", credit));
                        }
                    }
                    else //cash empty
                    {
                        decimal cash = 0;
                        decimal credit = decimal.Parse(TextBox3.Text);
                        if (cash > field3)
                        {
                            throw new Exception("Please enter an appropriate amount");
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@cash", cash));
                            cmd.Parameters.Add(new SqlParameter("@credit", credit));
                            check = true;
                        }
                    }


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    if (check) //credit field not empty
                    {
                        if (TextBox2.Text.Equals(""))//credit card number field empty
                        {
                            Label4.Text = "Order Id: " + field2 + "<br></br>";
                            Label5.Text = "Total amount: " + field3 + "<br></br>";
                            throw new Exception("You have to enter a credit Card");

                        }
                        else
                        {
                            String cc = TextBox2.Text;
                            addcredit(cc, field2);
                        }
                    }
               

            }
            catch (SqlException ex)
            {

                Response.Write(ex.Number);


            }
            catch (Exception e2)
            {
                Response.Write(e2.Message);
            }
        }

        protected void addcredit(String creditCardNumber, int field2)
        {
            try
            {

                Label5.Visible = true;
                Label4.Visible = true;
                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand("ChooseCreditCard", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@creditcardnumber", creditCardNumber));
                cmd.Parameters.Add(new SqlParameter("@orderid", field2));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (SqlException ex)
            {

                if (ex.Number == 547)
                {
                    Response.Write("Please enter a credit card that's was previously saved on the website.");
                }
                else
                {
                    Response.Write(ex.Number);
                }


            }
            catch (Exception e2)
            {
                Response.Write(e2.Message);
            }
        }

        protected void cancelOrder(object sender, EventArgs e) 
        {
            try
            {
                if (TextBox4.Text.Equals(""))
                {
                    throw new Exception("Please ente rorder number you wish to cancel");
                }

                int field2 = Int32.Parse(TextBox4.Text);
                string field1 = (string)(Session["cname"]);
                if (!(orderExists(field1, field2)))
                {
                    Response.Write("Order does not exist");
                }
                else
                {


                    string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                    SqlConnection conn = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand("cancelOrder", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@orderid", field2));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Label4.Visible = false;
                    Label5.Visible = false;
                    Response.Write("order canceled successfully");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }
        protected Boolean orderExists(string customer, int order)
        {
            try { 
            string field1 = (string)(Session["cname"]);
            int field2 = (int)(Session["orderid"]);
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("orderExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@customer", customer));
            cmd.Parameters.Add(new SqlParameter("@orderID", order));

                SqlParameter success = cmd.Parameters.Add("@exist", SqlDbType.Int);
                
                success.Direction = ParameterDirection.Output;
               
                conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
              

            if (success.Value.ToString().Equals("1"))
            {
                    return true;
            }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            return false;
        }


        protected void showhistory(object sender, EventArgs e)
        {
            
            string cname = (string)(Session["cname"]);
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("ordersHistory", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@customer", cname));

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (!(rdr.HasRows))
            {
                Response.Write("You don't have any orders");
            }
            else
            {
                while (rdr.Read())
                {
                    try
                    {
                        int orderno = rdr.GetInt32(rdr.GetOrdinal("order_no"));
                        decimal totalamount = rdr.GetDecimal(rdr.GetOrdinal("total_amount"));
                        string status = rdr.GetString(rdr.GetOrdinal("order_status"));

                        Label lbl_orderno = new Label();
                        lbl_orderno.Text = " Order ID : ";
                        lbl_orderno.ForeColor = Color.Black;
                        Label lbl = new Label();
                        lbl.Text = orderno.ToString();
                        lbl.ForeColor = Color.DarkGray;
                        form1.Controls.Add(lbl_orderno);
                        form1.Controls.Add(lbl);

                        Label lbl_totalamount = new Label();
                        lbl_totalamount.Text = " Total Amount : ";
                        lbl_totalamount.ForeColor = Color.Black;
                        Label lb = new Label();
                        lb.Text = totalamount.ToString();
                        lb.ForeColor = Color.DarkGray;
                        form1.Controls.Add(lbl_totalamount);
                        form1.Controls.Add(lb);

                        Label lbl_status = new Label();
                        lbl_status.Text = " Order Status: ";
                        lbl_status.ForeColor = Color.Black;
                        Label lb2 = new Label();
                        lb2.Text = status.ToString() +"  <br /> <br />";
                        lb2.ForeColor = Color.DarkGray;
                        form1.Controls.Add(lbl_status);
                        form1.Controls.Add(lb2);


                    }
                    catch (Exception e2)
                    {
                        Response.Write(e2.ToString());
                    }
                }
            }
        }
    }
}