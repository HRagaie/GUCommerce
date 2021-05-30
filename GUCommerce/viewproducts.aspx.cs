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
    public partial class viewproducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) //7agat te7sal lewa7daha when i load the page
        {
            //configuration el ana I saved f el webconfig
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("showProductsbyPrice", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            //IF the output is a table, then we can read the records one at a time
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                try
                {
                    //p.product_name,p.product_description,p.price,p.final_price,p.color
                    //get the value of the attribute product name
                    int serialno = rdr.GetInt32(rdr.GetOrdinal("serial_no"));
                    string serial = serialno.ToString();
                    string productName = rdr.GetString(rdr.GetOrdinal("product_name"));
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
                        lbl_productName.ForeColor= Color.Black;
                        Label lbl = new Label();
                        lbl.Text = productName;
                        p.Controls.Add(lbl_productName);
                        p.Controls.Add(lbl);
                    }

            
                    if (!(desc.Equals(null))){
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
                        lbl_Color.Text = " Color: " ;
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
                    b.Text = "Add to Cart";
                    b.ForeColor = System.Drawing.Color.White;
                    b.BackColor = Color.Black;
                    b.Font.Bold = true;
                    b.Height = 50;
                    b.Font.Italic = true;
                    b.CommandArgument= serial;
                    b.Click += new System.EventHandler(addToCart);
                    p.Controls.Add(b);

                   Button b1 = new Button();
                    b1.Font.Size = FontUnit.Medium;
                    b1.Text = "Add to Wishlist";
                    b1.ForeColor = System.Drawing.Color.White;
                    b1.BackColor = Color.Black;
                    b1.Font.Bold = true;
                    b1.Height = 50;
                    b1.Font.Italic = true;
                    b1.CommandArgument = serial;
                    b1.Click += new System.EventHandler(addToWishlist);
                    p.Controls.Add(b1);



                    

                }
                catch (Exception e1)
                {
                    Response.Write(e1.Message);

                }

            }
  

            

        }
        protected void addMobile(object sender, EventArgs e)
        {
            try {
                //this is how you get data from session variable.
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);


                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("addMobile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@username", field1));
                string mobile = TextBox1.Text;

                if (mobile.Equals("")) {
                    throw new Exception("please enter the mobile number u wish to add to your account");
                }
                if(mobile.Contains(" "))
                {
                    throw new Exception("no spaces are allowed in a mobile number");
                }
                cmd.Parameters.Add(new SqlParameter("@mobile_number", mobile));

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
        protected void AddCredit(object sender, EventArgs e)
        {
            
           
            Response.Redirect("CreditCards.aspx", true);
            
            
        }
        protected void cart(object sender, EventArgs e)
        {
            
            Response.Redirect("Cart.aspx", true);
            
            
        }
        protected void wishlist(object sender, EventArgs e)
        {
           
            Response.Redirect("Wishlist.aspx", true);
            

        }
        protected void addToCart(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int serial = Int32.Parse(btn.CommandArgument.ToString());
                string field1 = (string)(Session["cname"]);
                //Response.Write(field1);

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("AddToCart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                cmd.Parameters.Add(new SqlParameter("@serial", serial));
                

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("viewproducts.aspx", true);
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
        }

        protected void addToWishlist(object sender, EventArgs e)
        {
            try
            {

                String wname = TextBox2.Text;



                

                
                string field1 = (string)(Session["cname"]);

                Button btn = (Button)sender;
                int serial = Int32.Parse(btn.CommandArgument.ToString());
                //Response.Write(serial + " HODA " + wname);

                string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
                //sql connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand cmd = new SqlCommand("AddtoWishlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@customername", field1));
                cmd.Parameters.Add(new SqlParameter("@wishlistname", wname));
                cmd.Parameters.Add(new SqlParameter("@serial", serial));



                if (wname.Equals("") || wname.Equals(" "))
                {
                    throw new Exception("please don't leave wishlist name blank");
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
                Response.Write("Successfully added.");


            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Response.Write("Product already in this wishlist");
                }
                else if (ex.Number == 547)
                {
                    Response.Write("You don't have that wishlist, create it first");
                }

            }
            catch (Exception e2)
            {
                Response.Write(e2.Message);
            }
        }




    }
}