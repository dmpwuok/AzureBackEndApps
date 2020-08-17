<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ImageLoader.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        Image Uploader
    </title>
    <style type="text/css">
        #content
        {
            width: 400px;
            margin: 20px auto;
            border: 1px solid #000111;
        }

        form div
        {
            margin: 15px auto;
        }

        #img_div
        {
            width: 200px;
            padding: 5px;
            margin: 15px auto;
            border: 1px solid #000115;
        }

        #img_div:after
        {
            content: "";
            display: block;
            clear: both;
        }

        img
        {
            float: left;
            margin: 5px;
            width: 300px;
            height: 140px;
        }
    </style>
</head>
<body>
    <div id = "content">
        <form id ="form" runat="server">
            <asp:FileUpload ID="FileUpload" runat="server"/>
            <div>
                <input id="File1" type="file" name="image" />
                <br />
                <br />
            </div>
            <div>
                <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click1"/>
            </div>
            <div>
                <asp:Label ID="Message"  runat="server"></asp:Label>
                <br />
                <asp:HyperLink ID="HLink"  runat="server">View Image</asp:HyperLink>
            </div>
        </form>
    </div>
   
</body>
</html>
