<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFToImage.aspx.cs" Inherits="PDF_to_Image_MVC.UploadPDF" %>
<!DOCTYPE html>
<html>
<head>
    <title>PDF to Image</title>
</head>
<body>
    <form id="uploadForm" runat="server" enctype="multipart/form-data">
        <div>
            <label for="pdfFile">Select a PDF File:</label>
            <input type="file" id="pdfFile" name="pdfFile" runat="server" accept=".pdf" />
            <br />
            <asp:Button ID="PDFToImage" runat="server" Text="PDF to Image" OnClick="PDFToImage_Click" />
        </div>
        
    </form>
</body>
</html>
