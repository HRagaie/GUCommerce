
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="GUCommerce.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Button runat="server" Text="Check out" OnClick="makeorder" />

        </p>
        
        <asp:Button ID="Button1" runat="server" Text="Home Page" OnClick="homepage" />
        
    </form>
</body>
</html>
