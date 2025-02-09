using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;       

namespace web_1.Web
{
    public partial class Admini : System.Web.UI.Page
    { 
        static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port = 33061";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cAccount"] == null && Session["homeAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }          
            if (!IsPostBack)
            {    BindGridView();
                
            }
        }
        protected void BindGridView()//gridview elder
        {
            if (Session["LoginType"] == "System")
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible=true;
                string account = Session["cAccount"].ToString();


                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT Carer.cId, Carer.cName, Carer.cIdCard, Carer.cGender, Carer.cBirth, Carer.cPhone, Carer.cAddress,CarerLogin.cAccount,CarerLogin.cEmail FROM Carer JOIN CarerLogin ON Carer.cId=CarerLogin.cId WHERE CarerLogin.cAccount = @account";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@account", account);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    cId_Text.Text = reader["cId"].ToString();
                    cName_Text.Text = reader["cName"].ToString();
                    cIdCard_Text.Text = reader["cIdCard"].ToString();
                    cGender_list.SelectedValue = reader["cGender"].ToString();
                    cBirth_Text.Text = Convert.ToDateTime(reader["cBirth"]).ToString("yyyy-MM-dd");
                    cPhone_Text.Text = reader["cPhone"].ToString();
                    cAddress_Text.Text = reader["cAddress"].ToString();
                    cEmail_Text.Text = reader["cEmail"].ToString();  // 加入這行以顯示電子郵件
                }
                reader.Close();
                connection.Close();
                BindGridView2(cId_Text.Text);
            }
            else if (Session["LoginType"] == "Home"){
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                string homeEmail= Session["homeAccount"].ToString();
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT HomeLogin.homeUserName, HomeLogin.HomeRealName, HomeLogin.homePhone, HomeLogin.homeGender, HomeLogin.homeAddress,HomeLogin.homeEmail FROM HomeLogin WHERE HomeLogin.homeEmail = @homeEmail";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeEmail", homeEmail);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    homeUserName_Text.Text = reader["homeUserName"].ToString();
                    homeRealName_Text.Text = reader["homeRealName"].ToString();
                    homePhone_Text.Text = reader["homePhone"].ToString();
                    homeGender_List.SelectedValue = reader["homeGender"].ToString();
                    homeAddress_Text.Text = reader["homeAddress"].ToString();
                    homeEmail_Text.Text = reader["homeEmail"].ToString();
                }
                reader.Close();
                connection.Close();
                BindGridView2(cId_Text.Text);
            }
        }
        protected void edit_Click1(object sender, EventArgs e)//編輯
        {
            edit.Visible = false;
            save.Visible = true;
            cName_Text.ReadOnly = false;
            cIdCard_Text.ReadOnly = false;
            cGender_list.Enabled = true;
            cBirth_Text.ReadOnly = false;
            cPhone_Text.ReadOnly = false;
            cAddress_Text.ReadOnly = false;
        }

        protected void save_Click(object sender, EventArgs e)//儲存
        {
            edit.Visible = true;
            save.Visible = false;
            cName_Text.ReadOnly = true;
            cIdCard_Text.ReadOnly = true;
            cGender_list.Enabled = false;
            cBirth_Text.ReadOnly = true;
            cPhone_Text.ReadOnly = true;
            cAddress_Text.ReadOnly = true;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE Carer SET cName = @cName, cGender = @cGender, cBirth = @cBirth, cIdCard = @cIdCard, cPhone = @cPhone, cAddress = @cAddress WHERE cId = @cId";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@cName", cName_Text.Text);
            command.Parameters.AddWithValue("@cGender", cGender_list.SelectedValue);
            command.Parameters.AddWithValue("@cBirth", Convert.ToDateTime(cBirth_Text.Text));
            command.Parameters.AddWithValue("@cIdCard", cIdCard_Text.Text);
            command.Parameters.AddWithValue("@cPhone", cPhone_Text.Text);
            command.Parameters.AddWithValue("@cAddress", cAddress_Text.Text);
            command.Parameters.AddWithValue("@cId", cId_Text.Text);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('儲存成功');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('儲存失敗，沒有資料被更新');", true);
            }
            BindGridView();
        }

        protected void BindGridView2(string cId)//管理員負責長者
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT Elder.eId as 長者編號, Elder.eName as 長者姓名, Elder.eGender as 長者性別, Elder.pId as 房間編號, Elder.cId as 管理員姓名 FROM Elder, Carer WHERE Elder.cId = Carer.cId AND Carer.cId = @cId";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@cId", cId);

            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            GridView2.DataSource = dt;
            GridView2.DataBind();
            connection.Close();

        }  

        protected void changePW_Click(object sender, EventArgs e)
        {
            string token = Session["cAccount"].ToString();
            //Response.Redirect("changePassword.aspx");

            string url = "changePassword.aspx?token=" + HttpUtility.UrlEncode(token);
            Response.Redirect(url);
        }
        protected void changeEmail_Click(object sender, EventArgs e)
        {
            Response.Redirect("changeEmail.aspx");
        }
        protected void homeEdit_Click1(object sender, EventArgs e)
        {
            homeEdit.Visible = false;
            homeSave.Visible = true;
            homeUserName_Text.ReadOnly = true;
            homeRealName_Text.ReadOnly = false;
            homeGender_List.Enabled = true;
            homePhone_Text.ReadOnly = false;
            homeAddress_Text.ReadOnly = false;
            homeEmail_Text.ReadOnly = true;
        }

        protected void homeSave_Click(object sender, EventArgs e)
        {
            homeEdit.Visible = true;
            homeSave.Visible = false;
            homeUserName_Text.ReadOnly = true;
            homeRealName_Text.ReadOnly = true;
            homeGender_List.Enabled = false;
            homePhone_Text.ReadOnly = true;
            homeAddress_Text.ReadOnly = true;
            homeEmail_Text.ReadOnly = true;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE HomeLogin SET homeUserName = @homeUserName, homeRealName = @homeRealName,homeGender = @homeGender, homePhone = @homePhone, homeAddress = @homeAddress WHERE homeUserName = @homeUserName";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@homeUserName", homeUserName_Text.Text);
            command.Parameters.AddWithValue("@homeRealName", homeRealName_Text.Text);
            command.Parameters.AddWithValue("@homeGender", homeGender_List.SelectedValue);
            command.Parameters.AddWithValue("@homePhone", homePhone_Text.Text);
            command.Parameters.AddWithValue("@homeAddress", homeAddress_Text.Text);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('儲存成功');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('儲存失敗，沒有資料被更新');", true);
            }
            BindGridView();
        }
    }
}