<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="LegacyWebFormsApp.Users" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Legacy Users</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Users (XML + Paging + Sorting + Session Filter)</h1>

        <a href="Dashboard.aspx">Back to Dashboard</a>
        <br /><br />

        <asp:Label ID="lblFilterInfo" runat="server"></asp:Label>
        <br />

        <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search by name"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Apply Filter" OnClick="btnSearch_Click" />
        <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" OnClick="btnClearFilter_Click" />

        <br /><br />

        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" AllowSorting="True"
            PageSize="3"
            OnPageIndexChanging="gvUsers_PageIndexChanging"
            OnSorting="gvUsers_Sorting">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
