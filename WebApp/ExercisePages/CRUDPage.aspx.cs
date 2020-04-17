using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBSystem.BLL;
using DBSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace WebApp.ExercisePages
{
    public partial class CRUDPage : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        static string add = "";
        List<string> errormsgs = new List<string>();
        private static List<Entity02> Entity02List = new List<Entity02>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Message.DataSource = null;
            Message.DataBind();
            if (!Page.IsPostBack)
            {
                pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];
                add = Request.QueryString["add"];
                BindCategoryList();
                BindSupplierList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                else if (add == "yes")
                {
                    
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;

                }
                else
                {
                    AddButton.Enabled = false;
                    Controller02 sysmgr = new Controller02();
                    Entity02 info = null;
                    info = sysmgr.FindByPLID(int.Parse(pid));
                    if (info == null)
                    {
                        errormsgs.Add("Record is no longer on file.");
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                        Clear_Click(sender, e);
                    }
                    else
                    {
                        ID.Text = info.PlayerID.ToString();
                        FirstName.Text = info.FirstName;
                        LastName.Text = info.LastName;
                        Age.Text = info.Age.ToString();
                        Gender.Text = info.Gender;
                        AlbertaHealthCareNumber.Text = info.AlbertaHealthCareNumber;
                        MedicalAlertDetails.Text = info.MedicalAlertDetails;

                       
                        if (info.GuardianID.HasValue)
                        {
                            CategoryList.SelectedValue = info.GuardianID.ToString();
                        }
                        else
                        {
                            CategoryList.SelectedIndex = 0;
                        }
                        if (info.TeamID.HasValue)
                        {
                            SupplierList.SelectedValue = info.TeamID.ToString();
                        }
                        else
                        {
                            SupplierList.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        protected Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }
        protected void BindCategoryList()
        {
            try
            {
                Controller03 sysmgr = new Controller03();
                List<Entity01> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                CategoryList.DataSource = info;
                CategoryList.DataTextField = nameof(Entity01.FullName);
                CategoryList.DataValueField = nameof(Entity01.GuardianID);
                CategoryList.DataBind();
                CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void BindSupplierList()
        {
            try
            {
                Controller01 sysmgr = new Controller01();
                List<Entity03> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
                SupplierList.DataSource = info;
                SupplierList.DataTextField = nameof(Entity03.TeamName);
                SupplierList.DataValueField = nameof(Entity03.TeamID);
                SupplierList.DataBind();
                SupplierList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void Validation(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName.Text))
            {
                errormsgs.Add("First Name is required");
            }
            if (string.IsNullOrEmpty(LastName.Text))
            {
                errormsgs.Add("Last Name is required");
            }
            if (string.IsNullOrEmpty(Age.Text))
            {
                errormsgs.Add("Age is required.");
            }
            if (string.IsNullOrEmpty(Gender.Text))
            {
                errormsgs.Add("Gender is required.");
            }
            if (string.IsNullOrEmpty(AlbertaHealthCareNumber.Text))
            {
                errormsgs.Add("Alberta Health Number is required.");
            }
            string gender = null;
            if (!string.IsNullOrEmpty(Gender.Text))
            {
                if (!Gender.Text.ToUpper().StartsWith("F") && !Gender.Text.ToUpper().StartsWith("M"))
                {
                    errormsgs.Add("Gender Must be M or F");
                }
            }
            int age = 0;
            if (!string.IsNullOrEmpty(Age.Text))
            {
                if (int.TryParse(Age.Text, out age))
                {
                    if (age < 6 || age > 14)
                    {
                        errormsgs.Add("Age must be between 6 and 14");
                    }
                }
                else
                {
                    errormsgs.Add("Age Must be a real whole number");
                }                                
            }
            long healthnumber = 0;
            if (!string.IsNullOrEmpty(AlbertaHealthCareNumber.Text))
            {
                if (long.TryParse(AlbertaHealthCareNumber.Text, out healthnumber))
                {
                    if (healthnumber < 1000000000 || healthnumber > 9999999999)
                    {
                        errormsgs.Add("Alberta Health Number must start with a value between 1 and 9 and be 10 digits long.");
                    }
                }
            }
            if (CategoryList.SelectedIndex == 0)
            {
                errormsgs.Add("Guardian is required");
            }
            if (SupplierList.SelectedIndex == 0)
            {
                errormsgs.Add("Team is required");
            }



        }
        protected void Back_Click(object sender, EventArgs e)
        {
            if (pagenum == "4")
            {
                Response.Redirect("Ex09and10.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            ID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Age.Text = "";
            Gender.Text = "";
            AlbertaHealthCareNumber.Text = "";
            MedicalAlertDetails.Text = "";            
            CategoryList.ClearSelection();
            SupplierList.ClearSelection();
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            Validation(sender, e);
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    Controller02 sysmgr = new Controller02();
                    Entity02 item = new Entity02();
                    item.FirstName = FirstName.Text.Trim();
                    item.LastName = LastName.Text.Trim();                    
                    item.TeamID = int.Parse(SupplierList.SelectedValue);
                    item.GuardianID = int.Parse(CategoryList.SelectedValue);
                    item.Age = int.Parse(Age.Text);
                    item.Gender = Gender.Text.ToUpper();
                    item.AlbertaHealthCareNumber = AlbertaHealthCareNumber.Text.Trim();
                    item.MedicalAlertDetails = MedicalAlertDetails.Text.Trim();
                    int newID = sysmgr.Add(item);
                    ID.Text = newID.ToString();
                    errormsgs.Add("Player has been added");
                    LoadMessageDisplay(errormsgs, "alert alert-success");
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                    
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (string.IsNullOrEmpty(ID.Text))
            {
                errormsgs.Add("Search for a Player to update");
            }
            else if (!int.TryParse(ID.Text, out id))
            {
                errormsgs.Add("Id is invalid");
            }
            Validation(sender, e);
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    Controller02 sysmgr = new Controller02();
                    Entity02 item = new Entity02();
                    item.PlayerID = int.Parse(ID.Text);
                    item.FirstName = FirstName.Text.Trim();
                    item.LastName = LastName.Text.Trim();
                    item.Gender = Gender.Text.ToUpper().Trim();
                    item.Age = int.Parse(Age.Text);
                    item.AlbertaHealthCareNumber = AlbertaHealthCareNumber.Text.Trim();
                    item.MedicalAlertDetails = MedicalAlertDetails.Text.Trim();

                    if (SupplierList.SelectedIndex == 0)
                    {
                        item.TeamID = null;
                    }
                    else
                    {
                        item.TeamID = int.Parse(SupplierList.SelectedValue);
                    }
                    item.GuardianID = int.Parse(CategoryList.SelectedValue);
                                        
                    int rowsaffected = sysmgr.Update(item);
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Record has been updated");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (string.IsNullOrEmpty(ID.Text))
            {
                errormsgs.Add("Search for a record to delete");
            }
            else if (!int.TryParse(ID.Text, out id))
            {
                errormsgs.Add("Id is invalid");
            }
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    Controller02 sysmgr = new Controller02();
                    int rowsaffected = sysmgr.Delete(id);
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Record has been deleted");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                        Clear_Click(sender, e);
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;                    
                    AddButton.Enabled = true;

                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
    }
}