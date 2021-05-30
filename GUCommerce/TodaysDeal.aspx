<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TodaysDeal.aspx.cs" Inherits="GUCommerce.TodaysDeal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <asp:Label ID="create" runat="server" Text="Create Today's Deal"></asp:Label>
        <br />
        <asp:Label ID="damount" runat="server" Text="Deal Amount"></asp:Label>
                <br />
        <asp:TextBox ID="damountT" runat="server"></asp:TextBox>
                <br />
         <asp:Label ID="admin" runat="server" Text="Admin Username"></asp:Label>
                <br />
        <asp:TextBox ID="adminT" runat="server"></asp:TextBox>
                <br />
        <asp:Label ID="expdate" runat="server" Text="Expiry Date"></asp:Label>
                <br />
        <asp:TextBox ID="expdateT" runat="server"></asp:TextBox>
                <br />
        <asp:Button ID="createD" runat="server" Text="Create Deal" />
                <br />



        


  
        <asp:Label ID="addD" runat="server" Text="Add Deal "></asp:Label>
                <br />
        <asp:Label ID="dId" runat="server" Text="Deal ID"></asp:Label>
                <br />
        <asp:TextBox ID="dIdT" runat="server"></asp:TextBox>
                <br />
        <asp:Label ID="serialn" runat="server" Text="Serial no."></asp:Label>
                <br />
        <asp:TextBox ID="serialnT" runat="server"></asp:TextBox>
                <br />
        <asp:Button ID="addB" runat="server" Text="Add Deal" />
                <br />






        <asp:Label ID="expD" runat="server" Text="Remove Expired Deal"></asp:Label>
                <br />
        <asp:Label ID="dealID" runat="server" Text="Deal ID"></asp:Label>
                <br />
        <asp:TextBox ID="dealIDT" runat="server"></asp:TextBox>
                <br />
        <asp:Button ID="delete" runat="server" Text="Delete" />

























        <div>
        </div>
    </form>
</body>
</html>
