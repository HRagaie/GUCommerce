<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewproducts.aspx.cs" Inherits="GUCommerce.viewproducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title ></title>
</head>
    
<body>
    <form id="form1" runat="server">
        
        <div>
        <asp:Panel ID="Panel1" runat="server" Height="38px" style="background-color:#bfbdbd">
       
        <asp:Button ID="Button2" runat="server" Text="My WishList" style="float:right; " OnClick="wishlist" BackColor="Black" ForeColor="White" />
        <asp:Button ID="Button4" runat="server" Text="My Cart"  style="float:right; " OnClick="cart" BackColor="Black" ForeColor="White" />
        <asp:Button ID="Button3" runat="server" Text="Add Credit Card"  style="float:right; " OnClick="AddCredit" BackColor="Black" ForeColor="White" />
    
        <asp:TextBox ID="TextBox1" runat="server" Width="167px" style="float:left; margin-left: 43px;" ></asp:TextBox>
        
        <asp:Button ID="Button1" runat="server" Text="Add mobile"  style="float:left; " OnClick="addMobile" BackColor="Black" ForeColor="White" />
         
        </asp:Panel>
        </div>
        <asp:panel ID="Panel2" runat="server" Height="38px" style="background-color:#bfbdbd">
            <asp:Label ID="Label1" runat="server" style="float:left; " Text="Wishlist name you want to add a product into:"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" style="float:left; "></asp:TextBox>
        </asp:panel>

       

        
        

       

        
    </form>
  
</body>
</html>
