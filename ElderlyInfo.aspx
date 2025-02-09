<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ElderlyInfo.aspx.cs" Inherits="web_1.ElderlyInfo" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
       body {
           /*font-family: 'Microsoft YaHei', Arial, sans-serif;*/
           font-size: 14px;
           color: #333;
           background-image: url(../Images/b-4.jpg);
           background-size: cover;
       }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .gridview th, .gridview td {
            padding: 10px;
            border-bottom: 1px solid #ccc;
        }

        .gridview th {
            /*background-color: #f4f4f4;*/
            font-weight: bold;
            text-align: left;
        }

        .gridview tr:hover {
            background-color: red;
        }

       .button {
           padding: 5px 10px;
           margin: 5px;
           border-width: 1px;
           color: forestgreen;
           cursor: pointer;
           border-radius: 3px;
           background-color: #006699;
       }
       .textbox {
           width: 200px;
           padding: 5px;
           margin-right: 10px;
       }

       .panel {
           background-color: #fff;
           padding: 15px;
           border-radius: 5px;
           box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
           margin-bottom: 20px;
       }
       .panel-common {
           border-radius: 10px;
           box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
           padding: 10px;
       }
       .data-table td {
           padding: 5px;
       }

        /* 設置表格行的懸停效果 */
       .gridview tr:hover {
           background-color: #ccffff; /* 懸停時的背景顏色 */
           color: white; /* 懸停時的文字顏色 */
       }
        .auto-style1 {
            height: 37px;
        }
        .auto-style9 {
            height: 37px;
            width: 400px;
        }
        .auto-style10 {
            width: 400px;
        }
   </style>
    <main aria-labelledby="title">
          <h1>長者資料</h1>
    <asp:Panel ID="Panel1" CssClass="panel-common" runat="server" BackColor="PowderBlue">新增一筆資料<asp:Button ID="Add" runat="server" CssClass="button" OnClick="Add_Click" Text="新增" ForeColor="White" BackColor="#006699" />
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Search_Text" runat="server"></asp:TextBox>

        <asp:Button ID="search_Btn" runat="server" Text="搜尋編號" CssClass="button" OnClick="search_Btn_Click" ForeColor="White" BackColor="#006699" />
        <asp:Button ID="show_AllGridView" runat="server" Text="顯示全部" CssClass="button" OnClick="show_AllGridView_Click" Visible="False" ForeColor="White" BackColor="#006699" />
        <asp:GridView ID="GridView1" CssClass="gridview" runat="server" OnRowCommand="GridView1_RowCommand" EnableSortingAndPagingCallbacks="True" CellPadding="3" AllowPaging="True" PageSize="5" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:ButtonField ButtonType="Button" Text="基本資料" CommandName="Details"/>
                <asp:ButtonField Text="跌倒資料" CommandName="Fall" ButtonType="Button"/>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" BackColor="#b0e0e6" CssClass="panel-common" style="width:100%;" Visible="False">
        <span style="font-size:20px;font-weight: bold;">長者資料</span>
        <table style="width:100%; box-sizing:border-box;">
            <tr>
                <td class="auto-style10"><asp:Label ID="eId_Title" runat="server"></asp:Label>&nbsp;<asp:Label ID="eId_Lebel" runat="server"></asp:Label>
                    &nbsp;<asp:TextBox ID="eId_Text" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td class="auto-style10"><asp:Label ID="cId_Label" runat="server" Text="管理員編號:"></asp:Label>&nbsp;<asp:TextBox ID="cId_Text" runat="server" ReadOnly="True" Width="160px"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="cId_list" runat="server" OnSelectedIndexChanged="cId_list_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td><asp:Label ID="pId_Label" runat="server" Text="房&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; 號:"></asp:Label>&nbsp;<asp:TextBox ID="pId_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="pId_list" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">長者姓名: <asp:TextBox ID="eName_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style10">身分證字號: <asp:TextBox ID="eIdCard_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
                <td>性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 別: <asp:DropDownList ID="eGender_list" runat="server" Enabled="False">
                    <asp:ListItem>male</asp:ListItem>
                    <asp:ListItem>female</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">生&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 日: <asp:TextBox ID="eBirth_Text" runat="server" Width="160px" TextMode="Date" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style9">電&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 話: <asp:TextBox ID="ePhone_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style1">通訊地址:&nbsp; <asp:TextBox ID="eAddress_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style10"> 身&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 高: <asp:TextBox ID="eHeight_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style9">體&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; 重: <asp:TextBox ID="eWeight_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="createFile_Label" runat="server" Text="建檔日期:"></asp:Label>&nbsp;<asp:TextBox ID="eCreateFile_Text" runat="server" TextMode="Date" Width="160px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr> 
        </table>
        <asp:Button ID="Home_back" runat="server" Text="返回" OnClick="Home_back_Click" Visible="False" />
        <asp:Button ID="Add_cancel" runat="server" OnClick="Add_cancel_Click" Text="取消新增" Visible="False" />
        <asp:Button ID="Add_check" runat="server" OnClick="Add_check_Click" Text="新增" Visible="False" />
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        <asp:RequiredFieldValidator ID="Required_eId" runat="server" ControlToValidate="eId_Text" ErrorMessage="長者編號不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="Required_eName" runat="server" ControlToValidate="eName_Text" ErrorMessage="長者姓名不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="Required_eBirth" runat="server" ControlToValidate="eBirth_Text" ErrorMessage="長者生日不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="Required_eIdCard" runat="server" ControlToValidate="eIdCard_Text" ErrorMessage="長者身分證不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
    </asp:Panel>
        <br />
    <asp:Panel ID="Panel3" runat="server" BackColor="#b8f0e0" CssClass="panel-common" Visible="False">
        <span style="font-size:20px;font-weight: bold;">家屬資料</span>
        <table style="width:100%; box-sizing:border-box;">
            <tr>
                <td class="auto-style10"> 家屬姓名: <asp:TextBox ID="rName_Text" runat="server" ReadOnly="True"></asp:TextBox></td>
                <td class="auto-style9">身分證字號:&nbsp; <asp:TextBox ID="rIdCard_Text" runat="server" ReadOnly="True"></asp:TextBox></td>
                <td>性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 別: <asp:DropDownList ID="rGender_list" runat="server" Enabled="False">
                    <asp:ListItem>male</asp:ListItem>
                    <asp:ListItem>female</asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="auto-style10">電&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 話: <asp:TextBox ID="rPhone_Text" runat="server" ReadOnly="True"></asp:TextBox></td>
                <td class="auto-style10">通 訊&nbsp; 地 址 : <asp:TextBox ID="rAddress_Text" runat="server" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="auto-style10">工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 作: <asp:TextBox ID="rJob_Text" runat="server" ReadOnly="True"></asp:TextBox></td>
                <td class="auto-style10">工 作&nbsp; 地 點 : <asp:TextBox ID="rWorkPlace_Text" runat="server" ReadOnly="True"></asp:TextBox></td>
            </tr>
        </table>        
        <asp:Button ID="back" runat="server" Text="返回" OnClick="back_Click"/>
        <asp:Button ID="edit" runat="server" Text="編輯" OnClick="edit_Click"/>
        <asp:Button ID="save" runat="server" Text="儲存" OnClick="save_Click" Visible="False" />
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        <asp:RequiredFieldValidator ID="Required_rName" runat="server" ControlToValidate="rName_Text" Enabled="False" ErrorMessage="家屬姓名不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="Required_rPhone" runat="server" ControlToValidate="rPhone_Text" Enabled="False" ErrorMessage="家屬電話不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="Required_rPhone0" runat="server" ControlToValidate="rIdCard_Text" Enabled="False" ErrorMessage="家屬身分證不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="Required_rPhone1" runat="server" ControlToValidate="rAddress_Text" Enabled="False" ErrorMessage="家屬通訊地址不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    </asp:Panel> 
    <asp:Panel ID="Panel4" runat="server" BackColor="#b0e0e6" CssClass="panel-common" Visible="False">
        <asp:GridView ID="GridView2" CssClass="gridview" runat="server" OnRowCommand="GridView2_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" EnableSortingAndPagingCallbacks="True">
            <Columns>
                <asp:ButtonField ButtonType="Button" Text="編輯" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:GridView ID="GridView3" CssClass="gridview" runat="server" OnRowCommand="GridView3_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" EnableSortingAndPagingCallbacks="True">
    <Columns>
        <asp:ButtonField ButtonType="Button" Text="編輯" />
    </Columns>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#007DBB" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>
        <asp:Button ID="FDetail_back" runat="server" Text="返回" OnClick="FDetail_back_Click" />
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server" BackColor="#b0e0e6" CssClass="panel-common" Visible="False">
        跌倒編號:
        <asp:Label ID="fId_Label" runat="server" Text=""></asp:Label>
        <br />
        長者編號:&nbsp;&nbsp;<asp:Label ID="feId_Label" runat="server" Text=""></asp:Label>
        &nbsp;<br /> 長者姓名:
        <asp:TextBox ID="feName_Text" runat="server" Enabled="False" Width="150px"></asp:TextBox>
        <br />
        跌倒時間:&nbsp;<asp:TextBox ID="fTime_Text" runat="server" Enabled="False" TextMode="Date" Width="150px"></asp:TextBox>
        &nbsp;
        <br />
        跌倒地點:
        <asp:TextBox ID="fPlace_Text" runat="server" Enabled="False" Width="150px"></asp:TextBox>
        &nbsp;&nbsp;
        <br />
        送&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 醫:
        <asp:TextBox ID="fHospital_Text" runat="server" Enabled="False" Width="150px"></asp:TextBox>
        <br />
        跌倒原因:&nbsp;<asp:TextBox ID="fWhy_Text" runat="server" TextMode="MultiLine" Enabled="False" Width="150px"></asp:TextBox>
        &nbsp;<br />
        <asp:Button ID="Fback" runat="server" OnClick="Fback_Click" Text="返回" />
        <asp:Button ID="Fedit" runat="server" OnClick="Fedit_Click" Text="編輯" />
        <asp:Button ID="Fsave" runat="server" OnClick="Fsave_Click" Text="儲存" Visible="False" />
    </asp:Panel>
    </main>
   
</asp:Content>