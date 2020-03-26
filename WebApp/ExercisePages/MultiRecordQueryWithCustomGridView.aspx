﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiRecordQueryWithCustomGridView.aspx.cs" Inherits="WebApp.ExercisePages.MultiRecordQueryWithCustomGridView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h1> Multi Record Query with Custom GridView</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Select a Team: "></asp:Label>&nbsp;&nbsp;   
        <asp:DropDownList ID="List01" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Button ID="Fetch" runat="server" Text="Fetch" 
             CausesValidation="false" OnClick="Fetch_Click"/>
        <br /><br />

        <br />
        <asp:Label ID="MessageLabel" runat="server" ></asp:Label>
        <br />
        <asp:GridView ID="List02" runat="server" 
            AutoGenerateColumns="False"
            CssClass="table table-striped" GridLines="Horizontal"
            BorderStyle="None" AllowPaging="True"
            OnPageIndexChanging="List02_PageIndexChanging" PageSize="10"
            OnSelectedIndexChanged="List02_SelectedIndexChanged">

            <Columns>
                <asp:CommandField SelectText="View" ShowSelectButton="True" 
                    ButtonType="Button" CausesValidation="false">
                </asp:CommandField>
                <asp:TemplateField HeaderText="ID" Visible="True">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="PlayerID" runat="server" 
                            Text='<%# Eval("PlayerID") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="FullName" runat="server" 
                            Text='<%# Eval("FullName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Age">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                     <ItemTemplate>
                        <asp:Label ID="Age" runat="server" 
                            Text='<%# Eval("Age") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gender">
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                     <ItemTemplate>
                        <asp:Label ID="Gender" runat="server" 
                            Text='<%# Eval("Gender")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Med Alert">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                     <ItemTemplate>
                         <asp:Label ID="MedicalAlertDetails" runat="server" 
                              Text='<%# Eval("MedicalAlertDetails") == null ? " " : Eval("MedicalAlertDetails") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                no data to display
            </EmptyDataTemplate>
            <PagerSettings FirstPageText="Start" LastPageText="End" Mode="NumericFirstLast" PageButtonCount="3" />
        </asp:GridView>
    </div>
</asp:Content>
