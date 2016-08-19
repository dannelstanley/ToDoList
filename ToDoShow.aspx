<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToDoShow.aspx.cs" Inherits="ToDoListApp.ToDoShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Honey Do List</title>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:Panel runat="server" ID="TasksPanel">
            <asp:Label runat="server" ID="PendingLbl" Text="Tasks Pending" Font-Bold="True"></asp:Label>
            <br/>
            <asp:GridView runat="server" ID="PendingGridView" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="PendingCbx"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Task" DataField="Title"/>
                    <asp:BoundField HeaderText="Due Date" DataField="DueDate"/>
                    <asp:BoundField HeaderText="Details" DataField="Description" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="AddButton" runat="server" Text="Add New Task" OnClick="AddButton_OnClick"/>
            &nbsp;&nbsp;
            <asp:Button runat="server" ID="CompleteButton" Text=" Mark Checked Tasks as Completed" OnClick="CompleteButton_OnClick" />
            &nbsp;&nbsp;
            <asp:Button runat="server" ID="DeleteButton" OnClick="DeleteButton_OnClick" Text="Delete Selected Tasks"/>
            <br/>
            <br/>
            <asp:Label runat="server" ID="CompletedLbl" Text="Completed Tasks" Font-Bold="True"></asp:Label>
            <br/>
            <asp:GridView runat="server" ID="CompletedGridView" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="CompletedCbx"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Task" DataField="Title"/>
                    <asp:BoundField HeaderText="Due Date" DataField="DueDate"/>
                    <asp:BoundField HeaderText="Details" DataField="Description"/>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="CompletedDeleteButton" Text="Delete Selected" OnClick="CompletedDeleteButton_OnClick"/>
        </asp:Panel>
        <br/>
        <asp:Panel runat="server" ID="AddTasksPanel" Visible="False">
            <asp:Label runat="server" ID="TitleLabel" Text="Title: "></asp:Label>    
            <asp:TextBox runat="server" ID="TitleTbx" Width="500px"></asp:TextBox>
            <br/>
            <asp:Label runat="server" ID="DetailsLabel" Text="Details: "></asp:Label>
            <asp:TextBox runat="server" ID="DetailsTbx" Width="500 px"></asp:TextBox>
            <br/>
            <asp:Label runat="server" ID="lbl2" Text="Due Date: "></asp:Label>
            <asp:CheckBox runat="server" ID="NoDueDateCbx" ViewStateMode="Inherit" Text="N/A" AutoPostBack="True" OnCheckedChanged="NoDueDateCbx_OnCheckedChanged"/>
            <asp:Calendar runat="server" ID="DueDateCalendar"></asp:Calendar>
            <asp:Label runat="server" ID="ErrorLbl" ForeColor="Red" Visible="False"></asp:Label>
            <br/>
            <asp:Button runat="server" ID="FinalizeAddButton" OnClick="FinalizeAddButton_OnClick" Text="Add Task"/>
            &nbsp;
            <asp:Button runat="server" ID="CancelButton" OnClick="CancelButton_OnClick" Text="Cancel"/>
            <br/>
        </asp:Panel>
    </form>
</body>
</html>

