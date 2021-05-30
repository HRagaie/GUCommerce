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
    public partial class order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Milestone"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("reviewOrders", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            //IF the output is a table, then we can read the records one at a time
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                //Get the value of the attribute name in the Company table
                int orderno = rdr.GetInt32(rdr.GetOrdinal("order_no"));
                DateTime orderdate = rdr.GetDateTime(rdr.GetOrdinal("order_date"));
                int totalamount = rdr.GetInt32(rdr.GetOrdinal("total_name"));
                int cashamount = rdr.GetInt32(rdr.GetOrdinal("cash_amount"));
                int creditamount = rdr.GetInt32(rdr.GetOrdinal("credit_amount"));
                string paymentType = rdr.GetString(rdr.GetOrdinal("payment_type"));
                string orderstatus = rdr.GetString(rdr.GetOrdinal("order_status"));
                int remainingdays = rdr.GetInt32(rdr.GetOrdinal("remaining_days"));
                int timelimit = rdr.GetInt32(rdr.GetOrdinal("time_limit"));
                string GiftCardcodeused = rdr.GetString(rdr.GetOrdinal("Gift_Card_code_used"));
                int deliveryid = rdr.GetInt32(rdr.GetOrdinal("delivery_id"));
                string creditCard_number = rdr.GetString(rdr.GetOrdinal("creditCard_number"));
                string customer_name = rdr.GetString(rdr.GetOrdinal("customer_name "));
                //Create a new label and add it to the HTML form
                Label lbl_orderno = new Label();
                lbl_orderno.Text = orderno.ToString();
                form1.Controls.Add(lbl_orderno);

                Label lbl_orderdate = new Label();
                lbl_orderdate.Text = orderdate.ToString();
                form1.Controls.Add(lbl_orderdate);

                Label lbl_totalamount = new Label();
                lbl_totalamount.Text = totalamount.ToString();
                form1.Controls.Add(lbl_totalamount);

                Label lbl_cashamount = new Label();
                lbl_cashamount.Text = cashamount.ToString();
                form1.Controls.Add(lbl_cashamount);

                Label lbl_creditamount = new Label();
                lbl_creditamount.Text = creditamount.ToString();
                form1.Controls.Add(lbl_creditamount);

                Label lbl_paymenttype = new Label();
                lbl_paymenttype.Text = paymentType;
                form1.Controls.Add(lbl_paymenttype);

                Label lbl_orderstatus = new Label();
                lbl_orderstatus.Text = orderstatus;
                form1.Controls.Add(lbl_orderstatus);

                Label lbl_remainingdays = new Label();
                lbl_remainingdays.Text = remainingdays.ToString();
                form1.Controls.Add(lbl_remainingdays);

                Label lbl_timelimit = new Label();
                lbl_timelimit.Text = timelimit.ToString();
                form1.Controls.Add(lbl_timelimit);


                Label lbl_GiftCardcodeused = new Label();
                lbl_GiftCardcodeused.Text = GiftCardcodeused;
                form1.Controls.Add(lbl_GiftCardcodeused);

                Label lbl_deliveryid = new Label();
                lbl_deliveryid.Text = deliveryid.ToString();
                form1.Controls.Add(lbl_deliveryid);

                Label lbl_creditCard_number = new Label();
                lbl_creditCard_number.Text = creditCard_number;
                form1.Controls.Add(lbl_creditCard_number);

                Label lbl_customer_name = new Label();
                lbl_customer_name.Text = customer_name;
                form1.Controls.Add(lbl_customer_name);

            }
            //this is how you retrive data from session variable.
            string field1 = (string)(Session["field1"]);
            Response.Write(field1);
        }
        }
    }