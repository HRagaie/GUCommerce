<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="GUCommerce.orders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 205px">
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="enter cash: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="enter credit: "></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        
        <p>
             <asp:Label ID="Label3" runat="server" Text="enter credit card number in case of credit payment: "></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
       
        </p>
        
        <p>
            <asp:Label ID="Label6" runat="server" Text="Order Id to cancel: "></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <asp:Button ID="Button4" runat="server" Text="Cancel Order" OnClick="cancelOrder" />
            <asp:Button ID="Button1" runat="server" Text="make order" OnClick="specifyAmount" />
            <asp:Button ID="Button5" runat="server" Text="Show History" OnClick="showhistory" />
        </p>
        <asp:Label ID="Label4" runat="server"></asp:Label>
        <asp:Label ID="Label5" runat="server"></asp:Label>
    </form>
</body>
</html>
