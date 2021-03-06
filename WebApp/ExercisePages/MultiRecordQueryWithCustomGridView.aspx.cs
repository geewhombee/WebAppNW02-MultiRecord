﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace WebApp.ExercisePages
{
    public partial class MultiRecordQueryWithCustomGridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }
        protected void BindList()
        {
            try
            {
                Controller01 sysmgr = new Controller01();
                List<Entity03> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Entity03.TeamName);
                List01.DataValueField = nameof(Entity03.TeamID);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
                
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
        protected void Fetch_Click(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a Team to view its information";
            }
            else
            {
                
                try
                {
                 
                    Controller02 sysmgr = new Controller02();
                    Controller01 sysmgr2 = new Controller01();
                    List<Entity02> info = null;
                    Entity03 SearchEntity = sysmgr2.FindByID(int.Parse(List01.SelectedValue));
                    info = sysmgr.FindByID(int.Parse(List01.SelectedValue));
                    
                    info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                    List02.DataSource = info;
                    List02.DataBind();
                    Coach.Text = SearchEntity.Coach.ToString();
                    AssistCoach.Text = SearchEntity.AssistantCoach.ToString();
                    Wins.Text = SearchEntity.Wins.ToString();
                    Losses.Text = SearchEntity.Losses.ToString();
                    
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message + "Test";
                }
            }
        }
        protected void List02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List02.PageIndex = e.NewPageIndex;
            Fetch_Click(sender, new EventArgs());
        }
        protected void List02_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow agvrow = List02.Rows[List02.SelectedIndex];
            string playerid = (agvrow.FindControl("PlayerID") as Label).Text;
            Response.Redirect("ReceivingPage.aspx?pid=" + playerid);
        }
    }
}