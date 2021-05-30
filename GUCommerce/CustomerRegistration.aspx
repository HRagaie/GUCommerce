<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerRegistration.aspx.cs" Inherits="GUCommerce.CustomerRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        CUSTOMER REGISTRATION<div>
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
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="customerRegister" Text="Register" />
        </p>
    </form>
</body>
</html>
