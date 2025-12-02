using LegacyWebFormsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Xml.Linq;

namespace LegacyWebFormsApp
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TrackAction("Visited Dashboard");
                LoadDashboard();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            TrackAction("Refreshed Dashboard");
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var metrics = new DashboardMetrics();

            // Products metrics from JSON
            var productsPath = Server.MapPath(WebConfigurationManager.AppSettings["JsonProductsPath"]);
            var productsJson = File.ReadAllText(productsPath);
            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson) ?? new List<Product>();

            metrics.TotalProducts = products.Count;
            metrics.LowStockProducts = products.Count(p => p.Stock < 10);

            // Users metrics from XML
            var usersPath = Server.MapPath(WebConfigurationManager.AppSettings["XmlUsersPath"]);
            var xdoc = XDocument.Load(usersPath);
            var users = xdoc.Root.Elements("User").ToList();
            metrics.TotalUsers = users.Count;

            metrics.LastUpdated = DateTime.Now;

            lblTotalProducts.Text = metrics.TotalProducts.ToString();
            lblTotalUsers.Text = metrics.TotalUsers.ToString();
            lblLowStockProducts.Text = metrics.LowStockProducts.ToString();
            lblLastUpdated.Text = metrics.LastUpdated.ToString("g");

            BindRecentActions();
        }

        private void TrackAction(string action)
        {
            var actions = Session["RecentActions"] as List<string>;
            if (actions == null)
            {
                actions = new List<string>();
            }

            actions.Insert(0, $"{DateTime.Now:g} - {action}");
            if (actions.Count > 10)
            {
                actions = actions.Take(10).ToList();
            }

            Session["RecentActions"] = actions;
        }

        private void BindRecentActions()
        {
            var actions = Session["RecentActions"] as List<string> ?? new List<string>();
            blRecentActions.DataSource = actions;
            blRecentActions.DataBind();
        }
    }
}
