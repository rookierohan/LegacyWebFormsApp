<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="LegacyWebFormsApp.Products" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Legacy Products</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <h1>Products (JSON + ViewState + UpdatePanel)</h1>

        <a href="Dashboard.aspx">Back to Dashboard</a>
        <br /><br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <br />

                <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search by name"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                <br /><br />

                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"
                    OnRowEditing="gvProducts_RowEditing"
                    OnRowCancelingEdit="gvProducts_RowCancelingEdit"
                    OnRowUpdating="gvProducts_RowUpdating"
                    DataKeyNames="Id">

                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>

                <br />

                <asp:Button ID="btnSaveChanges" runat="server" Text="Save All Changes"
                    OnClick="btnSaveChanges_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
