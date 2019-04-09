using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Data;
using System.Xml;
using ALMClient.DAL;
using System.IO;
using Newtonsoft.Json.Linq;
using AppBoxPro;

namespace ALMClient
{
    public partial class BugsEdit : PageBase// System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            getStatus();
            getPriority();
            string id = Request.QueryString["id"];
            Opers o = new Opers();
            string ret = o.getRest(Session["url"].ToString(), "/qcbin/rest/domains/"+Session["Domain"]+"/projects/"+Session["Project"]+"/defects/" + id);

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            lblID.Text = xmldoc.SelectSingleNode("//Field[@Name='id']").InnerText;
            lblName.Text = xmldoc.SelectSingleNode("//Field[@Name='name']").InnerText;
            ddStatus.Text = xmldoc.SelectSingleNode("//Field[@Name='status']").InnerText;
            ddPriority.Text = xmldoc.SelectSingleNode("//Field[@Name='priority']").InnerText;
            dCreationTime.Text = xmldoc.SelectSingleNode("//Field[@Name='creation-time']").InnerText;
            hDescription.Text = xmldoc.SelectSingleNode("//Field[@Name='description']").InnerText;
        }
   
        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
 
        private void getStatus()
        {
            Opers o = new Opers();
            string ret = o.getRest(Session["url"].ToString(), "/qcbin/rest/domains/" + Session["Domain"] + "/projects/" + Session["Project"] + "/customization/used-lists?name=Bug%20Status");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("//Items").ChildNodes;
            for (int i = 0; i < nodelist.Count; i++)
            {
                ddStatus.Items.Add(nodelist[i].Attributes["value"].Value, nodelist[i].Attributes["value"].Value);
            }
        }
        private void getPriority()
        {
            Opers o = new Opers();
            string ret = o.getRest(Session["url"].ToString(), "/qcbin/rest/domains/" + Session["Domain"] + "/projects/" + Session["Project"] + "/customization/used-lists?name=Priority");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("//Items").ChildNodes;
            for (int i = 0; i < nodelist.Count; i++)
            {
                ddPriority.Items.Add(nodelist[i].Attributes["value"].Value, nodelist[i].Attributes["value"].Value);
            }
        }
    }
}
