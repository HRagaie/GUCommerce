<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="products2.aspx.cs" Inherits="GUCommerce.products2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Offer Amount: "></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>



        <br />
        <br />
        <asp:Label ID="productnamelbl" runat="server" Text="Product Name: "></asp:Label>
        <asp:TextBox ID="productnametxt" runat="server" ></asp:TextBox>
             <br />
        <asp:Label ID="categorylbl" runat="server" Text="Category: "></asp:Label>
        <asp:TextBox ID="categorytxt" runat="server" ></asp:TextBox>
             <br />
        <asp:Label ID="descriptionlbl" runat="server" Text="Description: "></asp:Label>
        <asp:TextBox ID="descriptiontxt" runat="server" ></asp:TextBox>
             <br />
        <asp:Label ID="pricelbl" runat="server" Text="price: "></asp:Label>
        <asp:TextBox ID="pricetxt" runat="server" TextMode="Number" ></asp:TextBox>
                         <br />
        <asp:Label ID="colorlbl" runat="server" Text="Color: "></asp:Label>
        <asp:TextBox ID="colortxt" runat="server" TextMode="Number" ></asp:TextBox>
             <br />
        <asp:Button ID="post" runat="server" Text="Post Product" onclick="postproduct" Width="211px"/>
           <br/>






             <asp:Label ID="vendorNamelbl" runat="server" Text="vendor Name: "></asp:Label>
        <asp:TextBox ID="vendorNametxt" runat="server" ></asp:TextBox>
             <br />
             <asp:Label ID="serialNolbl" runat="server" Text="serialNo: "></asp:Label>
        <asp:TextBox ID="serialNotxt" runat="server" ></asp:TextBox>
             <br />
             <asp:Label ID="productNamelbl2" runat="server" Text="productName: "></asp:Label>
        <asp:TextBox ID="productNametxt2" runat="server" ></asp:TextBox>
             <br />
             <asp:Label ID="categorylbl2" runat="server" Text="category: "></asp:Label>
        <asp:TextBox ID="categorytxt2" runat="server" ></asp:TextBox>
             <br />
             <asp:Label ID="productDescriptionlbl" runat="server" Text="productDescription: "></asp:Label>
        <asp:TextBox ID="productDescriptiontxt" runat="server" ></asp:TextBox>
             <br />
             <asp:Label ID="Label5" runat="server" Text="@price: "></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server" ></asp:TextBox>
             <br />
             <asp:Label ID="colorlbl2" runat="server" Text="color: "></asp:Label>
        <asp:TextBox ID="colortxt2" runat="server" ></asp:TextBox>
             <br />
            <asp:Button ID="editbtn" runat="server" Text="edit Product" onclick="editProduct" Width="211px"/>
            <br/>
            <asp:Label ID="offeridlbl" runat="server" Text="offerid: "></asp:Label>
        <asp:TextBox ID="offeridtxt" runat="server" TextMode="Number" ></asp:TextBox>
                         <br />
            <asp:Label ID="serial_nolbl" runat="server" Text="serial_no: "></asp:Label>
        <asp:TextBox ID="serial_notxt" runat="server" TextMode="Number" ></asp:TextBox>
                         <br />
            <asp:Button ID="createbtn" runat="server" Text="create offer" onclick="applyOffer" Width="211px"/>
            <br/>
            <asp:Label ID="offerAmountlbl" runat="server" Text="offerAmount: "></asp:Label>
        <asp:TextBox ID="offerAmounttxt" runat="server" TextMode="Number" ></asp:TextBox>
                         <br />
            <asp:Label ID="expiry_datelbl" runat="server" Text="expiry_date: "></asp:Label>
        <asp:TextBox ID="expiry_datetxt" runat="server" TextMode="Number" ></asp:TextBox>
                         <br />
            <asp:Button ID="addofferbtn" runat="server" Text="add offer" onclick="addOffer" Width="211px"/>
            <br/>
            <asp:Label ID="Label6" runat="server" Text="offerId: "></asp:Label>
        <asp:TextBox ID="TextBox6" runat="server" TextMode="Number" ></asp:TextBox>
        </div>
    </form>
</body>
</html>

