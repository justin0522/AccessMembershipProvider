<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HF.MembershipProvider.Test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <div>
        <div>用户名<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
      <div> 密码<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div> 
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

        </div>
    </form>
</body>
</html>
