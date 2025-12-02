using System;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace LegacyWebFormsApp
{
    public partial class Settings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSettings();
                TrackAction("Visited Settings");
            }
        }

        private void LoadSettings()
        {
            try
            {
                var path = Server.MapPath(WebConfigurationManager.AppSettings["XmlSettingsPath"]);
                XDocument doc = XDocument.Load(path);

                txtCompanyName.Text = doc.Root.Element("CompanyName")?.Value;
                txtSupportEmail.Text = doc.Root.Element("SupportEmail")?.Value;
                txtItemsPerPage.Text = doc.Root.Element("ItemsPerPage")?.Value;
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Error loading settings: " + ex.Message;
            }
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var path = Server.MapPath(WebConfigurationManager.AppSettings["XmlSettingsPath"]);
                XDocument doc;

                if (File.Exists(path))
                {
                    doc = XDocument.Load(path);
                }
                else
                {
                    doc = new XDocument(new XElement("Settings"));
                }

                doc.Root.SetElementValue("CompanyName", txtCompanyName.Text);
                doc.Root.SetElementValue("SupportEmail", txtSupportEmail.Text);
                doc.Root.SetElementValue("ItemsPerPage", txtItemsPerPage.Text);

                doc.Save(path);

                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Settings saved successfully.";

                TrackAction("Updated Settings");
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Error saving settings: " + ex.Message;
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
