using LegacyWebFormsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;

namespace LegacyWebFormsApp
{
    public partial class Products : Page
    {
        private List<Product> ProductsList
        {
            get
            {
                return ViewState["ProductsList"] as List<Product> ?? new List<Product>();
            }
            set
            {
                ViewState["ProductsList"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
                BindGrid(ProductsList);
                TrackAction("Visited Products");
            }
        }

        private void LoadProducts()
        {
            var path = Server.MapPath(WebConfigurationManager.AppSettings["JsonProductsPath"]);
            var json = File.ReadAllText(path);
            ProductsList = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        }

        private void BindGrid(List<Product> products)
        {
            gvProducts.DataSource = products;
            gvProducts.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var term = txtSearch.Text.ToLower();
            var filtered = ProductsList.Where(p => p.Name.ToLower().Contains(term)).ToList();
            BindGrid(filtered);
            lblMessage.Text = $"Filtered by: {term}";
            TrackAction($"Searched products for '{term}'");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindGrid(ProductsList);
            lblMessage.Text = "";
        }

        protected void gvProducts_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            BindGrid(ProductsList);
        }

        protected void gvProducts_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            BindGrid(ProductsList);
        }

        protected void gvProducts_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = (int)e.Keys["Id"];

            var row = gvProducts.Rows[e.RowIndex];
            var txtName = row.Cells[1].Controls[0] as System.Web.UI.WebControls.TextBox;
            var txtPrice = row.Cells[2].Controls[0] as System.Web.UI.WebControls.TextBox;
            var txtStock = row.Cells[3].Controls[0] as System.Web.UI.WebControls.TextBox;

            var product = ProductsList.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = txtName.Text;
                decimal price;
                if (decimal.TryParse(txtPrice.Text, out price))
                {
                    product.Price = price;
                }

                int stock;
                if (int.TryParse(txtStock.Text, out stock))
                {
                    product.Stock = stock;
                }
            }

            gvProducts.EditIndex = -1;
            BindGrid(ProductsList);
            lblMessage.Text = "Row updated in memory (not yet saved to file).";
            TrackAction($"Updated product #{id}");
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            var path = Server.MapPath(WebConfigurationManager.AppSettings["JsonProductsPath"]);
            var json = JsonConvert.SerializeObject(ProductsList, Formatting.Indented);
            File.WriteAllText(path, json);

            lblMessage.Text = "All changes saved to products.json.";
            TrackAction("Saved products to JSON file");
        }

        private void TrackAction(string action)
        {
            var actions = Session["RecentActions"] as List<string> ?? new List<string>();
            actions.Insert(0, $"{DateTime.Now:g} - {action}");
            if (actions.Count > 10)
            {
                actions = actions.Take(10).ToList();
            }
            Session["RecentActions"] = actions;
        }
    }
}
