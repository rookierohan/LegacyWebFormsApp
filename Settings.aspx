<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="LegacyWebFormsApp.Settings" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Legacy Settings</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Settings (XML Read/Write)</h1>

        <a href="Dashboard.aspx">Back to Dashboard</a>
        <br /><br />

        <asp:Label ID="lblStatus" runat="server" ForeColor="Green"></asp:Label>
        <br /><br />

        <table>
            <tr>
                <td>Company Name:</td>
                <td><asp:TextBox ID="txtCompanyName" runat="server" Width="250" /></td>
            </tr>
            <tr>
                <td>Support Email:</td>
                <td><asp:TextBox ID="txtSupportEmail" runat="server" Width="250" /></td>
            </tr>
            <tr>
                <td>Items Per Page (Users):</td>
                <td><asp:TextBox ID="txtItemsPerPage" runat="server" Width="50" /></td>
            </tr>
        </table>

        <br />
        <asp:Button ID="btnSaveSettings" runat="server" Text="Save Settings" OnClick="btnSaveSettings_Click" />
    </form>
</body>
</html>
