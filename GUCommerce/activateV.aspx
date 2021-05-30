<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activateV.aspx.cs" Inherits="GUCommerce.activateV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Activate Vendor"></asp:Label>
           <br />

        <asp:Label ID="adminusername" runat="server" Text="Admin Username"></asp:Label>
           <br />
        <asp:TextBox ID="adminUnameT" runat="server"></asp:TextBox>
           <br />
        <asp:Label ID="vName" runat="server" Text="Vendor Username"></asp:Label>
           <br />
        <asp:TextBox ID="VnameT" runat="server"></asp:TextBox>
           <br />
        <asp:Button ID="activation" runat="server" Text="Activate" />
        
        <div>
        </div>
</body>
</html>
