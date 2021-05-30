<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="GUCommerce.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 299px">
    <form id="form1" runat="server">
    <asp:Label ID="username" runat="server" Text="username"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox1" runat="server" style="margin-top: 16px"></asp:TextBox>
        <div>
            <asp:Label ID="password" runat="server" Text="password"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="userLogin" Width="74px" />
        </p>
        <asp:Button ID="Button2" runat="server" OnClick="redirectC" Text="Register as Customer" Width="146px" />
        <asp:Button ID="Button3" runat="server" style="margin-left: 28px" Text="Register as Vendor" Width="151px" OnClick="redirectV" />
    </form>
</body>
</html>
