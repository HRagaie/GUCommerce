<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VendorRegistration.aspx.cs" Inherits="GUCommerce.VendorRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        VENDOR REGISTRATION<div>
            
            First Name: <asp:TextBox ID="firstname" runat="server"></asp:TextBox>
       
         </div>
        <div>
            Last Name: <asp:TextBox ID="lastname" runat="server" ></asp:TextBox>
         </div>
        <div>
            Username: <asp:TextBox ID="username" runat="server"></asp:TextBox>
       </div>
        <div>
            password: <asp:TextBox ID="password" runat="server"></asp:TextBox>
        </div>
        <div>
            Email: <asp:TextBox ID="email" runat="server"></asp:TextBox>
        </div>
        <div>
            bank account number: <asp:TextBox ID="bankno" runat="server"></asp:TextBox>
        </div>
        <div>
            company name: <asp:TextBox ID="comp" runat="server"></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="VendorRegister" Text="Register" />
        </p>
    </form>
</body>
</html>
