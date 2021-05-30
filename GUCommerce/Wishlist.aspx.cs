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
using System.Drawing;

namespace GUCommerce
{
    public partial class Wishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createWish(object sender, EventArgs e)
        {
            try
            {
                //this is how you get data from session variable.
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("createWishlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                string wish = TextBox1.Text;

                if (wish.Equals("") || wish.Equals(" "))
                {
                    throw new Exception("please don't leave wishlist name blank");
                }
                cmd.Parameters.Add(new SqlParameter("@name", wish));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Response.Write("You already have a wishlist with that name :)");
                }
            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
            }
        }

        protected void getWishlist(object sender, EventArgs e)
        {
            try
            {
                //this is how you get data from session variable.
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("showWishlistProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                string wish = TextBox2.Text;

                if (wish.Equals("") || wish.Equals(" "))
                {
                    throw new Exception("please don't leave wishlist name blank");
                }
                cmd.Parameters.Add(new SqlParameter("@name", wish));

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (!(rdr.HasRows))
                {
                    Response.Write("You don't have such wishlist");
                }
                else { 
                while (rdr.Read())
                {
                    try
                    {
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


                            System.Diagnostics.Debug.WriteLine("PRODUCTTTTT " + productName + " " + color + " " + desc + " " + price + " " + fprice);

                            //Create a new label and add it to the HTML form
                            

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

                            Label lbl_sn = new Label();
                            lbl_sn.Text = "Product serial number: ";
                            lbl_sn.ForeColor = Color.Black;
                            Label lblsn = new Label();
                            lblsn.Text = serial;
                            p.Controls.Add(lbl_sn);
                            p.Controls.Add(lblsn);

                            /* Button b = new Button();
                             b.Font.Size = FontUnit.Medium;
                             b.Text = "Remove From Wishlist";
                             b.ForeColor = System.Drawing.Color.White;
                             b.BackColor = Color.Black;
                             b.Font.Bold = true;
                             b.Height = 50;
                             b.Font.Italic = true;
                             b.CommandArgument = serial;
                             //Response.Write(serial + "SERIAL!");
                             b.Click += new System.EventHandler(removeFromSelectedWishlist);
                             b.ID = "The_Bane_of_My_Existence";
                             p.Controls.Add(b);*/

                        }
                    catch (Exception e1)
                    {
                        Response.Write(e1.Message);

                    }
                }
                }

                //cmd.ExecuteNonQuery();
                //conn.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Response.Write("You already have a wishlist with that name :)");
                }
            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
            }
            
        }

       
        /*protected void removeFromSelectedWishlist(object sender, EventArgs e) //not working
        {
            try
            {
                Response.Write("ANA HENA!");
                string field1 = (string)(Session["cname"]);
                Button btn = (Button)sender;
                int serial = Int32.Parse(btn.CommandArgument.ToString());
                String wname=Getwishname(field1, serial);
                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("removefromWishlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                
                cmd.Parameters.Add(new SqlParameter("@wishlistname", wname));
                cmd.Parameters.Add(new SqlParameter("@serial", serial));
                SqlParameter success = cmd.Parameters.Add("@success", SqlDbType.Int);
                success.Direction = ParameterDirection.Output;


                
                   
                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (success.Value.ToString().Equals("1"))
                {
                    Response.Write("Successfully removed");
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

        }*/
    
        protected void removeFromWishlist(object sender, EventArgs e)
        {
            try
            {
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);
               
                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("removefromWishlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                string wname = TextBox3.Text;
                cmd.Parameters.Add(new SqlParameter("@wishlistname", wname));
                SqlParameter success = cmd.Parameters.Add("@success", SqlDbType.Int);
                success.Direction = ParameterDirection.Output;

                
                if (wname.Equals("") || wname.Equals(" "))
                {
                    throw new Exception("please don't leave wishlist name blank");
                }
                if ((TextBox4.Text).Equals("") || (TextBox4.Text).Equals(" "))
                {
                    throw new Exception("please don't leave serial number blank");
                }
                else
                {

                    int serial = int.Parse(TextBox4.Text);
                    cmd.Parameters.Add(new SqlParameter("@serial", serial));
                }
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (success.Value.ToString().Equals("1"))
                {
                    Response.Write("Successfully removed");
                }
                else
                {
                    Response.Write("Check if you have a wishlist with that name or that Product");
                }

            }
            catch (SqlException ex)
            {
                
                    Response.Write(ex.Number);
                
            }
            catch(Exception e2)
            {
                Response.Write(e2.Message);
            }

        }

        protected void homepage(object sender, EventArgs e)
        {
            Response.Redirect("viewproducts.aspx", true);
        }

        /*protected String Getwishname(String customername, int serial)
        {
            String wishname = "";
            Response.Write("HEYY");
            try {
               
           string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("removefromWishlist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@customer", customername));
            cmd.Parameters.Add(new SqlParameter("@serial", serial));
            SqlParameter wish = cmd.Parameters.Add("@wish", SqlDbType.VarChar);
            wish.Direction = ParameterDirection.Output;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            wishname = wish.Value.ToString();
             
            }
            catch (Exception e)
            {
                Response.Write(e.ToString());
            }

            return wishname;
        }*/
    }
}