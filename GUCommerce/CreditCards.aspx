<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditCards.aspx.cs" Inherits="GUCommerce.CreditCards" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
         <asp:Label ID="Label3" runat="server" Text="credit card number:"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
         <asp:Label ID="Label2" runat="server" Text="cvv:"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
         <asp:Label ID="Label1" runat="server" Text="expiry date:"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="Date format should be DD/MM/YYY"></asp:Label>
        <p>
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Addc" />

       
        </p>
        <asp:Button ID="Button2" runat="server" Text="HomePage" OnClick="homepage" />

       
    </form>
</body>
</html>
