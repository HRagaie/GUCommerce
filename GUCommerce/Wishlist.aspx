<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="GUCommerce.Wishlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
       
        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Create a new Wishlist" OnClick="createWish" />
        <p>
            <asp:Label ID="Label2" runat="server" Text="Name: "></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Get wishlist" OnClick="getWishlist" />
        </p>
         <asp:Label ID="Label3" runat="server" Text="Name: "></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
         <asp:Label ID="Label4" runat="server" Text="serial number: "></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:Button ID="Button3" runat="server" Text="remove from wishlist" OnClick="removeFromWishlist" />
        <p>
            <asp:Button ID="Button4" runat="server" Text="HomePage" OnClick="homepage" />
        </p>
             </div>
    </form>
</body>
</html>
