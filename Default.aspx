
<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="PlacementDashboard.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Placement Dashboard</title>
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">

<div class="header">
    <h1>College Placement Dashboard</h1>
    <p>Student Placement Management System</p>
</div>

<div class="container">

    <div class="cards">
        <div class="card">
            <h3>Total Students</h3>
            <asp:Label ID="lblTotal" runat="server" />
        </div>

        <div class="card">
            <h3>Placed Students</h3>
            <asp:Label ID="lblPlaced" runat="server" />
        </div>

        <div class="card">
            <h3>Not Placed</h3>
            <asp:Label ID="lblNotPlaced" runat="server" />
        </div>

        <div class="card">
            <h3>Placement Percentage</h3>
            <asp:Label ID="lblPercentage" runat="server" />
        </div>
    </div>

    <h2>Student Placement Records</h2>

    <asp:GridView ID="gvStudents" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="StudentID"
        AutoGenerateSelectButton="True"
        OnSelectedIndexChanged="gvStudents_SelectedIndexChanged"
        CssClass="table"
        EmptyDataText="No student records found">

        <Columns>
            <asp:BoundField DataField="StudentID"
                HeaderText="ID" />

            <asp:BoundField DataField="StudentName"
                HeaderText="Student Name" />

            <asp:BoundField DataField="Department"
                HeaderText="Department" />

            <asp:BoundField DataField="CompanyName"
                HeaderText="Company" />

            <asp:BoundField DataField="Package"
                HeaderText="Package (LPA)" />

            <asp:BoundField DataField="PlacementStatus"
                HeaderText="Status" />
        </Columns>
    </asp:GridView>

    <h2>Selected Student Details</h2>

    <asp:DetailsView ID="dvStudent" runat="server"
        AutoGenerateRows="True"
        CssClass="details"
        EmptyDataText="Select a student from the table above">
    </asp:DetailsView>

    <h2>Company Placement Summary</h2>

    <asp:Repeater ID="rptCompanies" runat="server">
        <HeaderTemplate>
            <div class="companies">
        </HeaderTemplate>

        <ItemTemplate>
            <div class="company">
                <h3><%# Eval("CompanyName") %></h3>
                <p>Students: <%# Eval("TotalStudents") %></p>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

</div>
</form>
</body>
</html>
