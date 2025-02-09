<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Admini.aspx.cs" Inherits="web_1.Web.Admini" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <style>
         body {
            background-image: url(../Images/b-4.jpg);
            background-size:cover;
         }
         .panel-common{
             border-radius: 10px;
             box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
             padding:10px;
         }
        
     </style>



    <main aria-labelledby="title">
         <h1>管理員資料</h1>

    <asp:Panel ID="Panel2" runat="server" BackColor="PowderBlue" CssClass="panel-common"> <!--BackColor="#FFFFCC"-->
        <span style="font-size:20px;font-weight: bold;">管理員資料</span><br /> 
        <table style="width:100%; box-sizing:border-box;">
            <tr>
                <td>管理員編號: &nbsp;<asp:Label ID="cId_Text" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>管理員姓名: <asp:TextBox ID="cName_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                <td>身分證字號: <asp:TextBox ID="cIdCard_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                <td>性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 別:&nbsp;&nbsp;<asp:DropDownList ID="cGender_list" runat="server" ReadOnly="True" Enabled="False">
                    <asp:ListItem>male</asp:ListItem>
                    <asp:ListItem>female</asp:ListItem>
                </asp:DropDownList></td>
           </tr>
           <tr>
                <td>生&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 日: <asp:TextBox ID="cBirth_Text" runat="server" Width="160px" TextMode="Date" ReadOnly="True"></asp:TextBox></td>
                <td>電&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 話: <asp:TextBox ID="cPhone_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                <td>通訊地址:&nbsp; <asp:TextBox ID="cAddress_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
           </tr>
           <tr>
               <td>電子信箱: <asp:Label ID="cEmail_Text" runat="server"></asp:Label></td>
           </tr>
       </table>
    </asp:Panel>
        <Br />
    <asp:Panel ID="Panel3" runat="server" BackColor="powderblue" CssClass="panel-common"><!--BackColor="#99FFCC"-->
      負責的長者<br /> 
      <asp:GridView ID="GridView2" runat="server">
      </asp:GridView>
      <br /> 
     
      <asp:Button ID="edit" runat="server" OnClick="edit_Click1" Text="編輯" />
     
      <asp:Button ID="save" runat="server" Text="儲存" OnClick="save_Click" Visible="False" />
      <asp:Button ID="changePW" runat="server" OnClick="changePW_Click" Text="更改密碼" />
      <asp:Button ID="changeEmail" runat="server" OnClick="changeEmail_Click" Text="更改信箱" />
  
    </asp:Panel>
        <br /> 
        <asp:Panel ID="Panel1" runat="server" BackColor="PowderBlue" CssClass="panel-common"> <!--BackColor="#FFFFCC"-->
            <span style="font-size:20px;font-weight: bold;">用戶資料</span><br />
            <table style="width:100%; box-sizing:border-box;">
                <tr>
                    <td>暱    稱: <asp:TextBox ID="homeUserName_Text" runat="server" Width="200px" ReadOnly="True"></asp:TextBox></td>
                    <td>真實姓名: <asp:TextBox ID="homeRealName_Text" runat="server" ReadOnly="True" Width="200px"></asp:TextBox></td>
                    <td>電    話: <asp:TextBox ID="homePhone_Text" runat="server" Width="200px" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                     <td>性   別:&nbsp;&nbsp;<asp:DropDownList ID="homeGender_List" runat="server" ReadOnly="True" Enabled="False">
                        <asp:ListItem>male</asp:ListItem>
                        <asp:ListItem>female</asp:ListItem>
                    </asp:DropDownList></td>
                    <td>通訊地址: <asp:TextBox ID="homeAddress_Text" runat="server" Width="200px" ReadOnly="True"></asp:TextBox></td>
                    <td>Email:<asp:TextBox ID="homeEmail_Text" runat="server" ReadOnly="True" Width="295px"></asp:TextBox></td>
                </tr>
            </table>
            <asp:Button ID="homeEdit" runat="server" OnClick="homeEdit_Click1" Text="編輯" />
            <asp:Button ID="homeSave" runat="server" OnClick="homeSave_Click" Text="儲存" Visible="False" />
            <br />
</asp:Panel>
    </main>
</asp:Content>
