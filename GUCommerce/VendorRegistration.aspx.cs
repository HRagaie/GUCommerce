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
using System.Diagnostics;

namespace GUCommerce
{
    public partial class VendorRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void VendorRegister(object sender, EventArgs e)
        {

            //configuration el ana I saved f el webconfig
            string connStr = WebConfigurationManager.ConnectionStrings["proj"].ToString();
            //sql connection
            SqlConnection conn = new SqlConnection(connStr);
            String uname = username.Text;
            String passw = password.Text;
            String fname = firstname.Text;
            String lname = lastname.Text;
            String mail = email.Text;
            String company_name = comp.Text;
            String banknumber = bankno.Text;
            try
            {
                if (company_name.Equals("") || company_name.Equals(" ") || passw.Equals("") || passw.Equals(" ")|| uname.Equals("") || uname.Equals(" ")
                   || banknumber.Equals("") || banknumber.Equals(" ") || mail.Equals("") || mail.Equals(" ")
                   || fname.Equals("") || fname.Equals(" ") || lname.Equals("") || lname.Equals(" "))
            {
                    throw new Exception(String.Format("Field cannot be empty"));
            }

            SqlCommand cmd = new SqlCommand("vendorRegister", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@username", uname));
            cmd.Parameters.Add(new SqlParameter("@password", passw));
            cmd.Parameters.Add(new SqlParameter("@first_name", fname));
            cmd.Parameters.Add(new SqlParameter("@last_name", lname));
            cmd.Parameters.Add(new SqlParameter("@email", mail));
            cmd.Parameters.Add(new SqlParameter("@company_name", company_name));
            cmd.Parameters.Add(new SqlParameter("@bank_acc_no", banknumber));


            //execute procedure

           
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                //Response.Write("SUCCESSFUL");
                Response.Redirect("login.aspx", true);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                if (ex.Number == 2627)
                {
                    Response.Write("Vendor username already exists");
                }
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "Error Number: " + ex.Errors[i].Number + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                System.Diagnostics.Debug.WriteLine("EXCEPTIONNNNNNNNNNNNNN " + errorMessages.ToString());

            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
                System.Diagnostics.Debug.WriteLine("ANA HENA YA WELAD " + e1.Message);
            }


        }
    }
}