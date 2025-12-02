using LegacyWebFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.IO;

namespace LegacyWebFormsApp
{
    public partial class Users : Page
    {
        private List<User> UsersList
        {
            get { return ViewState["UsersList"] as List<User> ?? new List<User>(); }
            set { ViewState["UsersList"] = value; }
        }

        private string CurrentSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "Name"; }
            set { ViewState["SortExpression"] = value; }
        }

        private SortDirection CurrentSortDirection
        {
            get { return (SortDirection)(ViewState["SortDirection"] ?? SortDirection.Ascending); }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TrackAction("Visited Users");
                LoadUsers();
                ApplyFilterFromSession();
                BindGrid(UsersList);
            }
        }

        private void LoadUsers()
        {
            var path = Server.MapPath(WebConfigurationManager.AppSettings["XmlUsersPath"]);
            XDocument doc = XDocument.Load(path);

            UsersList = doc.Root.Elements("User")
                .Select(x => new User
                {
                    Id = (int)x.Element("Id"),
                    Name = (string)x.Element("Name"),
                    Email = (string)x.Element("Email")
                })
                .ToList();

            // Read ItemsPerPage from settings.xml to show coupling.
            try
            {
                var settingsPath = Server.MapPath(WebConfigurationManager.AppSettings["XmlSettingsPath"]);
                if (File.Exists(settingsPath))
                {
                    var settings = XDocument.Load(settingsPath);
                    int itemsPerPage;
                    if (int.TryParse(settings.Root.Element("ItemsPerPage")?.Value, out itemsPerPage))
                    {
                        gvUsers.PageSize = itemsPerPage;
                    }
                }
            }
            catch
            {
                // Silent fail – classic legacy style
            }
        }

        private void BindGrid(List<User> users)
        {
            var sorted = SortUsers(users, CurrentSortExpression, CurrentSortDirection);
            gvUsers.DataSource = sorted;
            gvUsers.DataBind();

            var filter = Session["UserFilter"] as string;
            lblFilterInfo.Text = string.IsNullOrEmpty(filter)
                ? "No filter applied."
                : $"Filter: {filter}";
        }

        private List<User> SortUsers(List<User> users, string sortExpression, SortDirection direction)
        {
            Func<User, object> keySelector;

            switch (sortExpression)
            {
                case "Id":
                    keySelector = u => u.Id;
                    break;
                case "Email":
                    keySelector = u => u.Email;
                    break;
                default:
                    keySelector = u => u.Name;
                    break;
            }

            return (direction == SortDirection.Ascending
                ? users.OrderBy(keySelector)
                : users.OrderByDescending(keySelector)).ToList();
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindGrid(UsersList);
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == CurrentSortExpression)
            {
                CurrentSortDirection = CurrentSortDirection == SortDirection.Ascending
                    ? SortDirection.Descending
                    : SortDirection.Ascending;
            }
            else
            {
                CurrentSortExpression = e.SortExpression;
                CurrentSortDirection = SortDirection.Ascending;
            }

            BindGrid(UsersList);
            TrackAction($"Sorted users by {CurrentSortExpression} {CurrentSortDirection}");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var filter = txtSearch.Text.Trim();
            Session["UserFilter"] = filter;
            ApplyFilterFromSession();
            BindGrid(UsersList);
            TrackAction($"Filtered users by '{filter}'");
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            Session["UserFilter"] = null;
            txtSearch.Text = string.Empty;
            LoadUsers();
            BindGrid(UsersList);
        }

        private void ApplyFilterFromSession()
        {
            var filter = Session["UserFilter"] as string;
            if (!string.IsNullOrEmpty(filter))
            {
                txtSearch.Text = filter;
                UsersList = UsersList
                    .Where(u => u.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
        }

        private void TrackAction(string action)
        {
            var actions = Session["RecentActions"] as List<string> ?? new List<string>();
            actions.Insert(0, $"{DateTime.Now:g} - {action}");
            if (actions.Count > 10) actions = actions.Take(10).ToList();
            Session["RecentActions"] = actions;
        }
    }
}
