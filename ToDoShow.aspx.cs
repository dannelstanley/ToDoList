using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ToDoListApp
{
    public partial class ToDoShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Repository repo = new Repository();
                Session["Repo"] = repo;
            }
        }

        protected void CompleteButton_OnClick(object sender, EventArgs e)
        {
            Repository repo = (Repository) Session["Repo"];
            List<Task> pendingList = repo.GetPending();
            List<int> idList = (from GridViewRow row in PendingGridView.Rows let check = (CheckBox) row.Cells[0].FindControl("PendingCbx") 
                                where check.Checked
                                select pendingList[row.RowIndex].Id).ToList();

            if (idList.Count > 0)
            {
                foreach (var i in idList)
                {
                    repo.CompleteTask(i);
                }
            }
            FormatDataGrid();
        }

        private void FormatDataGrid()
        {
            Repository repo = (Repository) Session["Repo"];
            PendingGridView.DataSource = repo.GetPending();
            CompletedGridView.DataSource = repo.GetCompleted();
            Page.DataBind();
            foreach (GridViewRow gridViewRow in PendingGridView.Rows)
            {
                DateTime rowDate = DateTime.Parse(gridViewRow.Cells[2].Text); 
                if(rowDate == default(DateTime))
                {
                    gridViewRow.Cells[2].Text = "N/A";
                }

                //highlight row if past due date
                else if (rowDate < DateTime.Today)
                {
                    gridViewRow.BackColor = System.Drawing.Color.Yellow;
                    gridViewRow.Cells[2].Text = rowDate.ToShortDateString();
                }
                else
                {
                    gridViewRow.Cells[2].Text = rowDate.ToShortDateString();
                }
            }

            foreach (GridViewRow gridViewRow in CompletedGridView.Rows)
            {
                DateTime rowDate = DateTime.Parse(gridViewRow.Cells[2].Text);
                gridViewRow.Cells[2].Text = rowDate == default(DateTime) ? "N/A" : rowDate.ToShortDateString();
            }
        }

        protected void DeleteButton_OnClick(object sender, EventArgs e)
        {
            Repository repo = (Repository) Session["Repo"];

            List<Task> tempList = repo.GetPending();
            List<int> idList = new List<int>();

            foreach (GridViewRow gridViewRow in PendingGridView.Rows)
            {
                var check = (CheckBox) gridViewRow.Cells[0].FindControl("PendingCbx");
                if (check.Checked)
                {
                   idList.Add(tempList[gridViewRow.RowIndex].Id); 
                }
            }

            if (idList.Count > 0)
            {
                foreach (var i in idList)
                {
                    repo.Delete(i);
                }
            }
            FormatDataGrid();
        }

        protected void AddButton_OnClick(object sender, EventArgs e)
        {
            TasksPanel.Visible = false;
            AddTasksPanel.Visible = true;

            ResetAddFields();
        }

        private void ResetAddFields()
        {
            ErrorLbl.Visible = false;
            TitleTbx.Text = "";
            DetailsTbx.Text = "";
            NoDueDateCbx.Checked = false;
            DueDateCalendar.Visible = true;
            DueDateCalendar.SelectedDate = DateTime.Today;
        }

        protected void CompletedDeleteButton_OnClick(object sender, EventArgs e)
        {
            Repository repo = (Repository)Session["Repo"];
            List<Task> tempList = repo.GetCompleted();
            List<int> idList = (from GridViewRow gridViewRow in CompletedGridView.Rows let check = (CheckBox) gridViewRow.Cells[0].FindControl("CompletedCbx")
                                where check.Checked 
                                select tempList[gridViewRow.RowIndex].Id).ToList();

            if (idList.Count > 0)
            {
                foreach (var i in idList)
                {
                    repo.Delete(i);
                }
            }
            FormatDataGrid();
        }

        protected void NoDueDateCbx_OnCheckedChanged(object sender, EventArgs e)
        {
            DueDateCalendar.Visible = !NoDueDateCbx.Checked;
        }

        protected void FinalizeAddButton_OnClick(object sender, EventArgs e)
        {
            Repository repo = (Repository) Session["Repo"];

            try
            {
                if (NoDueDateCbx.Checked)
                {
                    repo.Add(new Task(TitleTbx.Text, DetailsTbx.Text));
                    FormatDataGrid();

                    TasksPanel.Visible = true;
                    AddTasksPanel.Visible = false;
                }
                else
                {
                    repo.Add(new Task(TitleTbx.Text, DetailsTbx.Text, DueDateCalendar.SelectedDate));
                    FormatDataGrid();

                    TasksPanel.Visible = true;
                    AddTasksPanel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLbl.Visible = true;
                ErrorLbl.Text = ex.Message;
                TitleTbx.Focus();
            }
        }

        protected void CancelButton_OnClick(object sender, EventArgs e)
        {
            TasksPanel.Visible = true;
            AddTasksPanel.Visible = false;

            ResetAddFields();
        }
    }
}