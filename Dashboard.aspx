<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LegacyWebFormsApp.Dashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Legacy Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Legacy Dashboard</h1>

        <asp:Button ID="btnRefresh" runat="server" Text="Refresh Metrics" OnClick="btnRefresh_Click" />

        <h3>Metrics</h3>
        <table border="1" cellpadding="5">
            <tr>
                <td>Total Products</td>
                <td><asp:Label ID="lblTotalProducts" runat="server" /></td>
            </tr>
            <tr>
                <td>Total Users</td>
                <td><asp:Label ID="lblTotalUsers" runat="server" /></td>
            </tr>
            <tr>
                <td>Low Stock Products (&lt; 10)</td>
                <td><asp:Label ID="lblLowStockProducts" runat="server" /></td>
            </tr>
            <tr>
                <td>Last Updated</td>
                <td><asp:Label ID="lblLastUpdated" runat="server" /></td>
            </tr>
        </table>

        <h3>Recent Actions (Session)</h3>
        <asp:BulletedList ID="blRecentActions" runat="server"></asp:BulletedList>

        <h3>Navigation</h3>
        <ul>
            <li><a href="Products.aspx">Products</a></li>
            <li><a href="Users.aspx">Users</a></li>
            <li><a href="Settings.aspx">Settings</a></li>
        </ul>
    </form>
</body>
</html>
