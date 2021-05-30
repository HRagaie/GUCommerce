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
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //configuration el ana I saved f el webconfig
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("viewMyCart", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            string field1 = (string)(Session["cname"]);
            cmd.Parameters.Add(new SqlParameter("@customer", field1));

            conn.Open();
            //IF the output is a table, then we can read the records one at a time
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                try
                {
                    //p.product_name,p.product_description,p.price,p.final_price,p.color
                    //get the value of the attribute product name
                    string productName = rdr.GetString(rdr.GetOrdinal("product_name"));
                    int serialno = rdr.GetInt32(rdr.GetOrdinal("serial_no"));
                    string serial = serialno.ToString();
                    //get the value of the attribute field in the color
                    string color = rdr.GetString(rdr.GetOrdinal("color"));
                    string desc = rdr.GetString(rdr.GetOrdinal("product_description"));
                    decimal price = rdr.GetDecimal(rdr.GetOrdinal("price"));
                    decimal fprice = rdr.GetDecimal(rdr.GetOrdinal("final_price"));
                    Panel p = new Panel();
                    p.ForeColor = Color.DeepPink;
                    //p.BackColor = Color.DimGray;
                    p.BorderWidth = 2;
                    // p.BorderColor = Color.SkyBlue;
                    p.Font.Size = FontUnit.Large;
                    p.Font.Name = "Comic Sans MS";
                    p.HorizontalAlign = HorizontalAlign.Left;
                    form1.Controls.Add(p);




                    if (!(productName.Equals(null)))
                    {
                        Label lbl_productName = new Label();
                        lbl_productName.Text = "Product Name: ";
                        lbl_productName.ForeColor = Color.Black;
                        Label lbl = new Label();
                        lbl.Text = productName;
                        p.Controls.Add(lbl_productName);
                        p.Controls.Add(lbl);
                    }


                    if (!(desc.Equals(null)))
                    {
                        Label lbl_description = new Label();
                        lbl_description.Text = " Description: ";
                        lbl_description.ForeColor = Color.Black;
                        Label label2 = new Label();
                        label2.Text = desc;
                        p.Controls.Add(lbl_description);
                        p.Controls.Add(label2);
                    }
                    if (!(color.Equals(null)))
                    {
                        Label lbl_Color = new Label();
                        Label label4 = new Label();
                        lbl_Color.Text = " Color: ";
                        label4.Text = color;
                        lbl_Color.ForeColor = Color.Black;
                        p.Controls.Add(lbl_Color);
                        p.Controls.Add(label4);
                    }

                    // Label lbl_price = new Label();
                    // lbl_price.Text = price + "  and price after discount is ";
                    // p.Controls.Add(lbl_price);

                    Label lbl_fprice = new Label();
                    lbl_fprice.Text = " Final Price: ";
                    lbl_fprice.ForeColor = Color.Black;
                    Label label3 = new Label();
                    label3.Text = fprice + "  <br /> <br />";
                    p.Controls.Add(lbl_fprice);
                    p.Controls.Add(label3);


                    Button b = new Button();
                    b.Font.Size = FontUnit.Medium;
                    b.Text = "Remove From Cart";
                    b.ForeColor = System.Drawing.Color.White;
                    b.BackColor = Color.Black;
                    b.Font.Bold = true;
                    b.Height = 50;
                    b.Font.Italic = true;
                    b.CommandArgument = serial;
                    b.Click += new System.EventHandler(removeFromCart);
                    p.Controls.Add(b);

                }
                catch (Exception e1)
                {
                    Response.Write(e1.Message);

                }

            }
        }

        /*protected void addToCart(object sender, EventArgs e)
        {
            try
            {
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("AddToCart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));

                if ((TextBox1.Text).Equals("") || (TextBox1.Text).Equals(" "))
                {
                    throw new Exception("please don't leave serial number blank");
                }
                else
                {
                    int serial = int.Parse(TextBox1.Text);
                    cmd.Parameters.Add(new SqlParameter("@serial", serial));
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Cart.aspx", true);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Response.Write("Product already in this cart");
                }

            }
            catch (Exception e2)
            {
                Response.Write(e2.Message);
            }
        }*/

        protected void removeFromCart(object sender, EventArgs e)
        {
            try
            {
                string field1 = (string)(Session["cname"]);
                Button btn = (Button)sender;
                int serial = Int32.Parse(btn.CommandArgument.ToString());

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("removefromCart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                cmd.Parameters.Add(new SqlParameter("@serial", serial));
                SqlParameter success = cmd.Parameters.Add("@success", SqlDbType.Int);
                success.Direction = ParameterDirection.Output;

                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (success.Value.ToString().Equals("1"))
                {
                    //Response.Write("Successfully removed");
                    Response.Redirect("Cart.aspx", true);
                }
                else
                {
                    Response.Write("Check if you have that product in your cart");
                }
                

            }
            catch (SqlException ex)
            {

                Response.Write(ex.ToString());

            }
            catch (Exception e2)
            {
                Response.Write(e2.Message);
            }
        }

        protected void makeorder(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("viewMyCart", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            string field1 = (string)(Session["cname"]);
            cmd.Parameters.Add(new SqlParameter("@customer", field1));

            conn.Open();
            //IF the output is a table, then we can read the records one at a time
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (rdr.HasRows)
            {
                Response.Redirect("orders.aspx");
            }
            else
            {
                Response.Write("Nothing in cart!");
            }

            
        }

        protected void homepage(object sender, EventArgs e)
        {
            Response.Redirect("viewproducts.aspx", true);
        }
    }
}