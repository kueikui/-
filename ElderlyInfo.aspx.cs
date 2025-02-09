using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace web_1
{
    public partial class ElderlyInfo : System.Web.UI.Page
    {
        static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port = 33061";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cAccount"] == null && Session["homeAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            BindGridView();
            if (!IsPostBack)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                if (Session["LoginType"] == "System")
                {
                    eId_Title.Visible = true;
                    eId_Title.Text = "長者編號:";
                }
                else if(Session["LoginType"] == "Home")
                {
                    search_Btn.Visible = false;
                    Search_Text.Visible = false;
                    eId_Title.Visible = false;
                }
            }

        }
        protected void BindGridView()//gridview elder
        {
            if (Session["LoginType"] == "System") {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT Elder.eId as 長者編號,Elder.eName as 長者姓名, Elder.eGender as 長者性別,Elder.eBirth as 長者生日,Elder.eIdCard as 長者身分證,Elder.ePhone as 長者電話,Elder.eAddress as 長者住所地址,Elder.eHeight as 長者身高,Elder.eWeight as 長者體重,eCreateFile as 建檔日期,Elder.pId as 房號,Elder.cId as 照服員 FROM Elder ";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;

                GridView1.DataBind();
                GridView1.Visible = true;
                connection.Close();
            }
            else if(Session["LoginType"] == "Home")
            {
                string homeUserName = Session["userName"].ToString();

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT HomeElder.heName as 長者姓名, HomeElder.heGender as 長者性別,HomeElder.heBirth as 長者生日,HomeElder.heIdCard as 長者身分證,HomeElder.hePhone as 長者電話,HomeElder.heAddress as 長者地址,HomeElder.heHeight as 長者身高,HomeElder.heWeight as 長者體重 FROM HomeElder WHERE HomeElder.homeUserName = @homeUserName";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeUserName", homeUserName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;

                GridView1.DataBind();
                GridView1.Visible = true;
                connection.Close();
            }
        }

        protected void BindGridView2(string eId)//gridview fall
        {
            if (Session["LoginType"] == "System")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT Fall.fId as 跌倒編號,Fall.eId as 長者編號,Elder.eName as 長者姓名 ,Fall.pId as 跌倒地點,Fall.fTime as 跌倒時間, Fall.fWhy as 跌倒原因 ,Fall.hId as 送醫 FROM Fall " +
                           "INNER JOIN Elder ON Fall.eId = Elder.eId WHERE Fall.eId = @eId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eId", eId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView2.DataSource = dt;

                GridView2.DataBind();
                GridView2.Visible = true;
                connection.Close();
            }
        }
        protected void BindGridView3(string ename)//gridview fall
        {
            if (Session["LoginType"] == "Home")
            {
                string homeUserName = Session["userName"].ToString();

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                //string query = "SELECT HomeElderFall.hfId as 跌倒編號, HomeElderFall.hPlace as 跌倒地點,HomeElderFall.hfTime as 跌倒時間, HomeElderFall.hfWhy as 跌倒原因  FROM HomeElderFall "+
                //"JOIN HomeElder ON HomeElderFall.heName = HomeElder.heName WHERE HomeElderFall.heName = @heName";
                string query = "SELECT HomeElderFall.hfId as 跌倒編號, HomeElderFall.hPlace as 跌倒地點, " +
                      "HomeElderFall.hfTime as 跌倒時間, HomeElderFall.hfWhy as 跌倒原因 " +
                      "FROM HomeElderFall WHERE HomeElderFall.homeUserName = @homeUserName";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeUserName", homeUserName); // 正確地將 homeUserName 參數添加到查詢中
                //command.Parameters.AddWithValue("@heName", ename);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView3.DataSource = dt;

                GridView3.DataBind();
                GridView3.Visible = true;
                connection.Close();
            }
        }
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)//跌倒資訊編輯
        {
            Panel4.Visible = false;
            Panel5.Visible = true;
            Fback.Visible = true;
            Fedit.Visible = true;
            Fsave.Visible = false;

        }
        protected void edit_Click(object sender, EventArgs e)//編輯
        {
            if (Session["LoginType"].ToString() == "System")
            {
                string user = Session["cId"].ToString();
                if (cId_Text.Text == user)
                {
                    edit.Visible = false;
                    save.Visible = true;
                    back.Visible = false;
                    eCreateFile_Text.Enabled = true;
                    eName_Text.ReadOnly = false;
                    cId_Text.Visible = false;
                    pId_Text.Visible = false;
                    cId_list.Visible = true;
                    pId_list.Visible = true;
                    eIdCard_Text.ReadOnly = false;
                    ePhone_Text.ReadOnly = false;
                    eAddress_Text.ReadOnly = false;
                    eGender_list.Enabled = true;
                    eBirth_Text.ReadOnly = false;
                    eHeight_Text.ReadOnly = false;
                    eWeight_Text.ReadOnly = false;

                    rName_Text.ReadOnly = false;
                    rIdCard_Text.ReadOnly = false;
                    rGender_list.Enabled = true;
                    rPhone_Text.ReadOnly = false;
                    rAddress_Text.ReadOnly = false;
                    rJob_Text.ReadOnly = false;
                    rWorkPlace_Text.ReadOnly = false;
                    Required_rName.Enabled = true;
                    Required_rPhone.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('沒有編輯權限');", true);
                }
            }
            else if (Session["LoginType"].ToString() == "Home")
            {

            }
        }

        protected void save_Click(object sender, EventArgs e)//儲存更新
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE Elder SET eName=@eName, eGender=@eGender, eBirth=@eBirth, eIdCard=@eIdCard, " +
            "ePhone=@ePhone, eAddress=@eAddress, eHeight=@eHeight, eWeight=@eWeight WHERE eId=@eId";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@eId", eId_Lebel.Text);
            command.Parameters.AddWithValue("@eName", eName_Text.Text);
            command.Parameters.AddWithValue("@eGender", eGender_list.Text);
            command.Parameters.AddWithValue("@eBirth", eBirth_Text.Text);
            command.Parameters.AddWithValue("@eIdCard", eIdCard_Text.Text);
            command.Parameters.AddWithValue("@ePhone", ePhone_Text.Text);
            command.Parameters.AddWithValue("@eAddress", eAddress_Text.Text);
            command.Parameters.AddWithValue("@eHeight", eHeight_Text.Text);
            command.Parameters.AddWithValue("@eWeight", eWeight_Text.Text);
            command.ExecuteNonQuery();

            if (rName_Text.Text != "")//家屬資料更新||新增
            {
                string checkQuery = "SELECT COUNT(*) FROM Relative WHERE rName=@rName AND rPhone=@rPhone";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@rName", rName_Text.Text);
                checkCommand.Parameters.AddWithValue("@rPhone", rPhone_Text.Text);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)//修改已存在家屬資料
                {
                    string query2 = "UPDATE Relative SET rName=@rName, rGender=@rGender, rIdCard=@rIdCard, " +
                    "rPhone=@rPhone, rAddress=@rAddress, rJob=@rJob,rWorkPlace=@rWorkPlace WHERE rName=@rName and rPhone=@rPhone";

                    MySqlCommand command2 = new MySqlCommand(query2, connection);

                    command2.Parameters.AddWithValue("@rName", rName_Text.Text);
                    command2.Parameters.AddWithValue("@rGender", rGender_list.Text);
                    command2.Parameters.AddWithValue("@rIdCard", rIdCard_Text.Text);
                    command2.Parameters.AddWithValue("@rPhone", rPhone_Text.Text);
                    command2.Parameters.AddWithValue("@rAddress", rAddress_Text.Text);
                    command2.Parameters.AddWithValue("@rJob", rJob_Text.Text);
                    command2.Parameters.AddWithValue("@rWorkPlace", rWorkPlace_Text.Text);
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else//家屬尚未填寫過
                {
                    MySqlConnection connection1 = new MySqlConnection(connectionString);
                    connection1.Open();
                    string query3 = "SELECT  Relative.rName as 家屬姓名, Relative.rPhone as 家屬電話,Relative.rGender as 家屬性別,Relative.rIdCard as 家屬身分證,Relative.rAddress as 家屬住所地址,Relative.rJob as 家屬職業,Relative.rWorkPlace as 家屬工作地 FROM Relative";
                    MySqlCommand command3 = new MySqlCommand(query3, connection1);
                    MySqlDataReader reader3 = command3.ExecuteReader();

                    if (reader3.HasRows)
                    {
                        while (reader3.Read())
                        {
                            if (rName_Text.Text == reader3.GetString("家屬姓名") && rPhone_Text.Text == reader3.GetString("家屬電話"))
                            {
                                Label2.Visible = true;
                                Label2.Text = "新增失敗，此家屬已綁定!";
                                return;
                            }
                            else if (rIdCard_Text.Text == reader3.GetString("家屬身分證"))
                            {
                                Label1.Visible = true;
                                Label1.Text = "新增失敗，已身分已存在!";
                                return;
                            }
                            else
                            {
                                MySqlConnection connection2 = new MySqlConnection(connectionString);
                                connection2.Open();
                                string query4 = "INSERT INTO Relative (rName, rIdCard, rGender, rPhone, rAddress, rJob, rWorkPlace, eId) " +
                                "VALUES (@rName, @rIdCard, @rGender, @rPhone, @rAddress, @rJob, @rWorkPlace, @eId)";
                                MySqlCommand command4 = new MySqlCommand(query4, connection2);

                                command4.Parameters.AddWithValue("@rName", rName_Text.Text);
                                command4.Parameters.AddWithValue("@rIdCard", rIdCard_Text.Text);
                                command4.Parameters.AddWithValue("@rGender", rGender_list.Text);
                                command4.Parameters.AddWithValue("@rPhone", rPhone_Text.Text);
                                command4.Parameters.AddWithValue("@rAddress", rAddress_Text.Text);
                                command4.Parameters.AddWithValue("@rJob", rJob_Text.Text);
                                command4.Parameters.AddWithValue("@rWorkPlace", rWorkPlace_Text.Text);
                                command4.Parameters.AddWithValue("@eId", eId_Lebel.Text);

                                command4.ExecuteNonQuery();
                                connection2.Close();
                            }
                            break;
                        }
                    }
                }
            }

            edit.Visible = true;
            save.Visible = false;
            back.Visible = true;

            eCreateFile_Text.Enabled = false;
            eName_Text.ReadOnly = true;
            cId_Text.Visible = true;
            pId_Text.Visible = true;
            cId_list.Visible = false;
            pId_list.Visible = false;
            eIdCard_Text.ReadOnly = true;
            ePhone_Text.ReadOnly = true;
            eAddress_Text.ReadOnly = true;
            eGender_list.Enabled = false;
            eBirth_Text.ReadOnly = true;
            eHeight_Text.ReadOnly = true;
            eWeight_Text.ReadOnly = true;

            rName_Text.ReadOnly = true;
            rIdCard_Text.ReadOnly = true;
            rGender_list.Enabled = false;
            rPhone_Text.ReadOnly = true;
            rAddress_Text.ReadOnly = true;
            rJob_Text.ReadOnly = true;
            rWorkPlace_Text.ReadOnly = true;
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('更新成功');", true);
            Required_rName.Enabled = false;
            Required_rPhone.Enabled = false;
        }
        protected void back_Click(object sender, EventArgs e)//返回
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            edit.Visible = true;
            save.Visible = false;

            cId_Text.ReadOnly= true;
            pId_Text.ReadOnly= true;
            eName_Text.ReadOnly = true;
            eIdCard_Text.ReadOnly=true;
            eGender_list.Enabled = false;
            eBirth_Text.ReadOnly = true;
            ePhone_Text.ReadOnly = true;
            eAddress_Text.ReadOnly = true;
            eHeight_Text.ReadOnly = true;
            eWeight_Text.ReadOnly = true;
            eCreateFile_Text.ReadOnly = true;
            rName_Text.ReadOnly = true;
            rIdCard_Text.ReadOnly = true;
            rGender_list.Enabled = false;
            rPhone_Text.ReadOnly = true;
            rAddress_Text.ReadOnly = true;
            rJob_Text.ReadOnly = true;
            rWorkPlace_Text.ReadOnly = true;

            rName_Text.Text = "";
            rPhone_Text.Text = "";
            rIdCard_Text.Text = "";
            rAddress_Text.Text = "";
            //rGender_list.Text = "";
            rJob_Text.Text = "";
            rWorkPlace_Text.Text = "";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)//長者詳細資料
        {

            // 获取选定的行的索引
            int Index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[Index];
            if (Session["LoginType"] == "System"){
                if (e.CommandName == "Details")
                {
                    Panel2.Visible = true;
                    Panel3.Visible = true;
                    Panel1.Visible = false;
                    Add_check.Visible = false;
                    Label1.Visible = false;
                    Add_cancel.Visible = false;
                    cId_Text.Visible = true;
                    cId_list.Visible = false;
                    pId_Text.Visible = true;
                    pId_list.Visible = false;
                    eGender_list.Enabled = false;
                    TableCell eBirthCell = selectedRow.Cells[5];
                    TableCell eCreateFile = selectedRow.Cells[11];

                    eId_Lebel.Text = selectedRow.Cells[2].Text;
                    eName_Text.Text = selectedRow.Cells[3].Text;
                    eGender_list.Text = selectedRow.Cells[4].Text;
                    eBirth_Text.Text = Convert.ToDateTime(eBirthCell.Text).ToString("yyyy-MM-dd");
                    eIdCard_Text.Text = selectedRow.Cells[6].Text;
                    ePhone_Text.Text = selectedRow.Cells[7].Text;
                    eAddress_Text.Text = selectedRow.Cells[8].Text;
                    eHeight_Text.Text = selectedRow.Cells[9].Text;
                    eWeight_Text.Text = selectedRow.Cells[10].Text;
                    pId_Text.Text = selectedRow.Cells[12].Text;
                    cId_Text.Text = selectedRow.Cells[13].Text;
                    eCreateFile_Text.Text = Convert.ToDateTime(eCreateFile.Text).ToString("yyyy-MM-dd");

                    //讀取對應到長者的家屬
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT * FROM Relative WHERE Relative.eId=@eId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@eId", eId_Lebel.Text);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (eId_Lebel.Text == reader.GetString("eId"))
                            {
                                rName_Text.Text = reader.GetString("rName");
                                rPhone_Text.Text = reader.GetString("rPhone");
                                rIdCard_Text.Text = reader.GetString("rIdCard");
                                rAddress_Text.Text = reader.GetString("rAddress");
                                rGender_list.Text = reader.GetString("rGender");
                                rJob_Text.Text = reader.GetString("rJob");
                                rWorkPlace_Text.Text = reader.GetString("rWorkPlace");
                                break;
                            }
                        }
                    }
                    reader.Close();

                    string query2 = "SELECT cId FROM Carer";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);

                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
                    DataTable dt2 = new DataTable();
                    adapter2.Fill(dt2);
                    // 將DataTable資料綁定到下拉式選單
                    cId_list.DataSource = dt2;
                    cId_list.DataTextField = "cId"; // 顯示的文字欄位
                                                    //ddlOptions.DataValueField = "ID"; // 資料值欄位
                    cId_list.DataBind();
                    connection.Close();
                }
                else if (e.CommandName == "Fall")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    selectedRow = GridView1.Rows[index];

                    // 獲取長者編號
                    string eId = selectedRow.Cells[2].Text;
                    BindGridView2(eId);
                    Panel4.Visible = true;
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    GridView3.Visible = false;
                }
            }
            else if (Session["LoginType"] == "Home")
            {
                Panel3.Visible = false;
                if (e.CommandName == "Details")
                {
                    Panel2.Visible = true;
                    Panel1.Visible = false;
                    Add_check.Visible = false;
                    Label1.Visible = false;
                    Add_cancel.Visible = false;
                    cId_Text.Visible = true;
                    cId_list.Visible = false;
                    pId_Text.Visible = true;
                    pId_list.Visible = false;
                    eGender_list.Enabled = false;
                    Home_back.Visible = true;

                    TableCell eBirthCell = selectedRow.Cells[4];
                    eBirth_Text.Text = Convert.ToDateTime(eBirthCell.Text).ToString("yyyy-MM-dd");

                    //eId_Lebel.Text = selectedRow.Cells[2].Text;// HomeElder.heId as 長者編號
                    eId_Lebel.Visible = false;
                    eName_Text.Text = selectedRow.Cells[2].Text;//HomeElder.heName as 長者姓名
                    //eGender_list.Text = eGenderCell.Text; HomeElder.heGender as 長者性別
                    string dateString1 = selectedRow.Cells[4].Text;
                    
                    eIdCard_Text.Text = selectedRow.Cells[5].Text;//HomeElder.heIdCard as 長者身分證
                    ePhone_Text.Text = selectedRow.Cells[6].Text;//HomeElder.hePhone as 長者電話
                    eAddress_Text.Text = selectedRow.Cells[7].Text;//HomeElder.heAddress as 長者地址
                    eHeight_Text.Text = selectedRow.Cells[8].Text;//HomeElder.heHeight as 長者身高
                    eWeight_Text.Text = selectedRow.Cells[9].Text;//HomeElder.heWeight as 長者體重
                    //cId_Label.Text = "照顧者:";
                    //cId_Text.Text = selectedRow.Cells[11].Text;//HomeElder.homeNamet as 照護人
                    cId_Label.Visible = false;
                    cId_Text.Visible = false;
                    cId_list.Visible=false;
                    pId_Label.Visible=false;
                    pId_Text.Visible=false;
                    pId_list.Visible = false;
                    createFile_Label.Visible = false;
                    eCreateFile_Text.Visible = false;
                    
                }
                else if (e.CommandName == "Fall")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    selectedRow = GridView1.Rows[index];

                    // 獲取長者編號
                    string ename = selectedRow.Cells[5].Text;
                    BindGridView3(ename);
                    Panel4.Visible = true;
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    GridView2.Visible=false;
                }
            }
            }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)//跌倒資訊編輯
        {
            Panel4.Visible = false;
            Panel5.Visible = true;
            Fback.Visible = true;
            Fedit.Visible = true;
            Fsave.Visible = false;

            // 获取选定的行的索引
            int Index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView2.Rows[Index];

            fId_Label.Text = selectedRow.Cells[1].Text;
            //feId_Label.Text = selectedRow.Cells[2].Text;
            //feName_Text.Text = selectedRow.Cells[3].Text;
            fPlace_Text.Text= selectedRow.Cells[4].Text;
            string dateString = selectedRow.Cells[5].Text;
            DateTime FallDate;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out FallDate))
            {
            fTime_Text.Text = FallDate.ToString("yyyy-MM-dd");
            }
            fWhy_Text.Text = selectedRow.Cells[6].Text;
            //fHospital_Text.Text = selectedRow.Cells[7].Text;

        }
        protected void Add_Click(object sender, EventArgs e)//新增畫面
        {
            if (Session["LoginType"] == "System")
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = false;
                Add_check.Visible = true;
                Label1.Visible = false;
                Add_cancel.Visible = true;
                eCreateFile_Text.Enabled = false;
                eCreateFile_Text.Text = DateTime.Now.ToString("yyyy-MM-dd");

                eId_Lebel.Visible = false;
                eId_Text.Visible = true;
                cId_Text.Visible = false;
                cId_list.Visible = true;
                pId_Text.Visible = false;
                pId_list.Visible = true;
                eName_Text.ReadOnly = false;
                eIdCard_Text.ReadOnly = false;
                eBirth_Text.ReadOnly = false;
                ePhone_Text.ReadOnly = false;
                eAddress_Text.ReadOnly = false;
                eHeight_Text.ReadOnly = false;
                eWeight_Text.ReadOnly = false;
                eGender_list.Enabled = true;

                eId_Text.Text = "";
                eName_Text.Text = "";
                eBirth_Text.Text = "";
                eIdCard_Text.Text = "";
                ePhone_Text.Text = "";
                eAddress_Text.Text = "";
                eHeight_Text.Text = "";
                eWeight_Text.Text = "";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "select cId from Carer";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "cIdlist");
                cId_list.DataValueField = "cId";        //在此輸入的是資料表的欄位名稱
                cId_list.DataTextField = "cId";      //在此輸入的是資料表的欄位名稱

                cId_list.DataSource = ds.Tables["cIdlist"].DefaultView;
                cId_list.DataBind();

                string query2 = "select pId from Place";
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
                DataSet ds2 = new DataSet();
                adapter2.Fill(ds2, "pIdlist");
                pId_list.DataValueField = "pId";        //在此輸入的是資料表的欄位名稱
                pId_list.DataTextField = "pId";      //在此輸入的是資料表的欄位名稱

                pId_list.DataSource = ds2.Tables["pIdlist"].DefaultView;
                pId_list.DataBind();

                connection.Close();
            }
            else if (Session["LoginType"] == "Home")
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = false;
                Add_check.Visible = true;
                Label1.Visible = false;
                Add_cancel.Visible = true;
                cId_Label.Visible = true;
                cId_Label.Text = "照顧者:";
                cId_list.Visible = true;

                eId_Lebel.Visible = false;  
                eName_Text.ReadOnly = false;
                eIdCard_Text.ReadOnly = false;
                eBirth_Text.ReadOnly = false;
                ePhone_Text.ReadOnly = false;
                eAddress_Text.ReadOnly = false;
                eHeight_Text.ReadOnly = false;
                eWeight_Text.ReadOnly = false;
                eGender_list.Enabled = true;
                //cId_Text.ReadOnly = false;
                eName_Text.Text = "";
                eBirth_Text.Text = "";
                eIdCard_Text.Text = "";
                ePhone_Text.Text = "";
                eAddress_Text.Text = "";
                eHeight_Text.Text = "";
                eWeight_Text.Text = "";
                cId_Text.Text = "";
                cId_Text.Visible = false;
                pId_Label.Visible = false;
                pId_Text.Visible = false;
                pId_list.Visible = false;
                createFile_Label.Visible = false;
                eCreateFile_Text.Visible = false;
                cId_list.Visible=false;
                cId_Text.Visible = true;

                // 從 Session 中獲取登入的 homeUserName
                string homeUserName = Session["userName"].ToString();

                // 使用 homeUserName 查詢照顧者的相關資訊
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "select homeUserName from HomeLogin WHERE homeUserName = @homeUserName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeUserName", homeUserName);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // 將查詢到的照顧者名稱顯示在 Label 上
                        cId_Text.Text = homeUserName; // cId_Text 作為 Label 使用
                    }
                }
                connection.Close();
            }
        }

        protected void Add_check_Click(object sender, EventArgs e)//確認新增
        {
            string input = eBirth_Text.Text;
            DateTime birthday;
            Required_eId.Enabled = true;
            Required_eName.Enabled = true;
            Required_eBirth.Enabled = true;
            Required_eIdCard.Enabled = true;
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            if (Session["LoginType"] == "System")
            {
                if (eId_Text.Text != "" && eName_Text.Text != "" && eBirth_Text.Text != "" && eIdCard_Text.Text != "")
                {
                    string query = "SELECT * FROM Elder";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime.TryParse(input, out birthday);

                            DateTime currentDate = DateTime.Now;
                            int result = DateTime.Compare(birthday, currentDate);

                            if (eId_Text.Text == reader.GetString("eId"))
                            {
                                Label1.Visible = true;
                                Label1.Text = "新增失敗，已有此帳號!";
                                return;
                            }
                            else if (ePhone_Text.Text == reader.GetString("ePhone"))
                            {
                                Label1.Visible = true;
                                Label1.Text = "新增失敗，已電話已綁定!";
                                return;
                            }
                            else if (eIdCard_Text.Text == reader.GetString("eIdCard"))
                            {
                                Label1.Visible = true;
                                Label1.Text = "新增失敗，已身分已存在!";
                                return;
                            }
                            else if (result > 0)
                            {
                                Label1.Visible = true;
                                Label1.Text = "新增失敗，生日錯誤";
                                return;
                            }
                            else
                            {
                                query = "INSERT INTO Elder (eId,eName,eIdCard,eGender,eBirth,ePhone,eAddress,eHeight,eWeight,eCreateFile,pId,cId) VALUES (@heId,@heName,@heIdCard,@heGender,@heBirth,@hePhone,@heAddress,@heHeight,@heWeight,@eCreateFile,@pId,@cId)";
                                connection = new MySqlConnection(connectionString);
                                command = new MySqlCommand(query, connection);
                                connection.Open();

                                command.Parameters.AddWithValue("@heId", eId_Text.Text);
                                command.Parameters.AddWithValue("@heName", eName_Text.Text);
                                command.Parameters.AddWithValue("@heIdCard", eIdCard_Text.Text);
                                command.Parameters.AddWithValue("@heGender", eGender_list.Text);
                                command.Parameters.AddWithValue("@heBirth", eBirth_Text.Text);
                                command.Parameters.AddWithValue("@hePhone", ePhone_Text.Text);
                                command.Parameters.AddWithValue("@heAddress", eAddress_Text.Text);
                                command.Parameters.AddWithValue("@heHeight", eHeight_Text.Text);
                                command.Parameters.AddWithValue("@heWeight", eWeight_Text.Text);
                                command.Parameters.AddWithValue("@eCreateFile", eCreateFile_Text.Text);
                                command.Parameters.AddWithValue("@cId", cId_list.Text);
                                command.Parameters.AddWithValue("@pId", pId_list.Text);

                                command.ExecuteNonQuery();
                                connection.Close();

                                Panel1.Visible = true;
                                Panel2.Visible = false;
                                Panel3.Visible = false;
                                Label1.Visible = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('新增成功');", true);
                                break;
                            }
                        }
                        BindGridView();
                    }
                    
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "無填寫資料";
                }
                
            }
            else if (Session["LoginType"] == "Home")
            {
                if (eName_Text.Text != "" && eBirth_Text.Text != "" && eIdCard_Text.Text != "")
                {
                    string query = "SELECT * FROM HomeElder";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime.TryParse(input, out birthday);

                                DateTime currentDate = DateTime.Now;
                                int result = DateTime.Compare(birthday, currentDate);

                                if (ePhone_Text.Text == reader.GetString("hePhone"))
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "新增失敗，已電話已綁定!";
                                    return;
                                }
                                else if (eIdCard_Text.Text == reader.GetString("heIdCard"))
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "新增失敗，已身分已存在!";
                                    return;
                                }
                                else if (result > 0)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "新增失敗，生日錯誤";
                                    return;
                                }
                                else
                                {
                                    query = "INSERT INTO HomeElder (heName,heIdCard,heGender,heBirth,hePhone,heAddress,heHeight,heWeight,homeUserName) VALUES (@heName,@heIdCard,@heGender,@heBirth,@hePhone,@heAddress,@heHeight,@heWeight,@homeUserName)";
                                    connection = new MySqlConnection(connectionString);
                                    command = new MySqlCommand(query, connection);
                                    connection.Open();

                                    //command.Parameters.AddWithValue("@heId", eId_Text.Text);
                                    command.Parameters.AddWithValue("@heName", eName_Text.Text);
                                    command.Parameters.AddWithValue("@heIdCard", eIdCard_Text.Text);
                                    command.Parameters.AddWithValue("@heGender", eGender_list.Text);
                                    command.Parameters.AddWithValue("@heBirth", eBirth_Text.Text);
                                    command.Parameters.AddWithValue("@hePhone", ePhone_Text.Text);
                                    command.Parameters.AddWithValue("@heAddress", eAddress_Text.Text);
                                    command.Parameters.AddWithValue("@heHeight", eHeight_Text.Text);
                                    command.Parameters.AddWithValue("@heWeight", eWeight_Text.Text);
                                    command.Parameters.AddWithValue("@homeUserName", cId_Text.Text);
                                    command.ExecuteNonQuery();
                                    connection.Close();

                                    Panel1.Visible = true;
                                    Panel2.Visible = false;
                                    Panel3.Visible = false;
                                    Label1.Visible = false;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('新增成功');", true);
                                    break;
                                }
                            } 
                            BindGridView();
                        }
                        
                }
                else
                    {
                        Label1.Visible = true;
                        Label1.Text = "無填寫資料";
                    }
            }
           
        }
        
        protected void Add_cancel_Click(object sender, EventArgs e)//取消新增
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Add_cancel.Visible = false;

            eGender_list.Enabled = true;
            eId_Lebel.Visible = true;
            eId_Text.Visible = false;
            cId_Text.ReadOnly = true;
            pId_Text.ReadOnly = true;
            eName_Text.ReadOnly = true;
            eIdCard_Text.ReadOnly = true;
            eBirth_Text.ReadOnly = true;
            ePhone_Text.ReadOnly = true;
            eAddress_Text.ReadOnly = true;
            eHeight_Text.ReadOnly = true;
            eWeight_Text.ReadOnly = true;
            Required_eId.Enabled = false;
            Required_eName.Enabled = false;
            Required_eBirth.Enabled = false;
            Required_eIdCard.Enabled = false;
        }


        //這是什麼
        protected void cId_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT cId FROM Carer";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // 從資料讀取器中取得 pid，並將其加入下拉選單
                        ListItem item = new ListItem(reader["cId"].ToString(), reader["cId"].ToString());
                        cId_list.Items.Add(item);
                    }
                }     
        }

        protected void search_Btn_Click(object sender, EventArgs e)//搜尋
        {
            if (Session["LoginType"] == "System") {
                if (Search_Text.Text == "")
                {
                    return;
                }
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "select Elder.eId as 長者編號,Elder.eName as 長者姓名, Elder.eGender as 長者性別,Elder.eBirth as 長者生日,Elder.eIdCard as 長者身分證,Elder.ePhone as 長者電話,Elder.eAddress as 長者住所地址,Elder.eHeight as 長者身高,Elder.eWeight as 長者體重,eCreateFile as 建檔日期,Elder.pId as 房號,Elder.cId as 照服員 from Elder where eId = @eId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eId", Search_Text.Text);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                connection.Close();

                show_AllGridView.Visible = true;
            }
            else if (Session["LoginType"] == "Home")
            {
                search_Btn.Visible= false;
                Search_Text.Visible= false;
            }
        }

        protected void show_AllGridView_Click(object sender, EventArgs e)
        {
            show_AllGridView.Visible=false;
            BindGridView();
        }

        protected void Fback_Click(object sender, EventArgs e)
        {
            Fedit.Visible = true;
            Fsave.Visible = false;
            Panel4.Visible = true;
            Panel5.Visible = false;
        }
        protected void Fedit_Click(object sender, EventArgs e)
        {
            Fback.Visible = false;
            Fedit.Visible = false;
            Fsave.Visible = true;
            feName_Text.Enabled = true;
            fTime_Text.Enabled = true;
            fPlace_Text.Enabled = true;
            fHospital_Text.Enabled = true;
            fWhy_Text.Enabled = true;
        }

        protected void Fsave_Click(object sender, EventArgs e)
        {
            Fback.Visible = true;
            Fedit.Visible = true;
            Fsave.Visible = false;
            Fsave.Visible = false;
            feName_Text.Enabled = false;
            fTime_Text.Enabled = false;
            fPlace_Text.Enabled = false;
            fHospital_Text.Enabled = false;
            fWhy_Text.Enabled = false;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // 先檢查日期格式是否正確，並轉換成 MySQL 支援的格式
            DateTime fallDate;
            if (DateTime.TryParseExact(fTime_Text.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fallDate))
            {
                // 將日期格式轉換成 "yyyy-MM-dd HH:mm:ss" 格式，並確保有時間部分
                string formattedDate = fallDate.ToString("yyyy-MM-dd 00:00:00");



                string query = "UPDATE Fall SET fTime=@fTime,pId=@pId,fWhy=@fWhy,hId=@hId  WHERE fId = @fId";
            string updateElderQuery = "UPDATE Elder SET eName = @feName WHERE eId = @eId";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@feName", feName_Text.Text);
            command.Parameters.AddWithValue("@eId", feId_Label);
            command.Parameters.AddWithValue("@fTime", formattedDate);
            command.Parameters.AddWithValue("@pId", fPlace_Text.Text);
            command.Parameters.AddWithValue("@hId", fHospital_Text.Text);
            command.Parameters.AddWithValue("@fWhy", fWhy_Text.Text);
            command.Parameters.AddWithValue("@fId", fId_Label.Text);

            MySqlDataReader reader = command.ExecuteReader();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('儲存成功');", true);
            BindGridView();
            }
            else
            {
                // 如果日期格式不正確，顯示錯誤訊息
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('日期格式錯誤，請使用 yyyy-MM-dd 格式');", true);
            }

        }

        protected void FDetail_back_Click(object sender, EventArgs e)
        {
            Panel4.Visible = false;
            Panel1.Visible = true;
        }

        protected void Home_back_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Home_back.Visible = false;
        }
    }
}