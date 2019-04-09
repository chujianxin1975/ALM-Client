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
using System.Net;
using System.Text;
using System.Xml.Linq;
namespace ALMClient
{
    public partial class BugsNew : System.Web.UI.Page
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
            getSeverity();
           // Opers o = new Opers();
          //  string ret = o.getRest(Session["url"].ToString(), "/qcbin/rest/domains/"+Session["Domain"]+"/projects/"+Session["Project"]+"/defects" );
 
        }
   
        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            post();
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
        private void getSeverity()
        {
            Opers o = new Opers();
            string ret = o.getRest(Session["url"].ToString(), "/qcbin/rest/domains/" + Session["Domain"] + "/projects/" + Session["Project"] + "/customization/used-lists?name=Severity");
           
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("//Items").ChildNodes;
            for (int i = 0; i < nodelist.Count; i++)
            {
                ddSeverity.Items.Add(nodelist[i].Attributes["value"].Value, nodelist[i].Attributes["value"].Value);
            }
        }
        private void post()
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Session["url"].ToString() + "/qcbin/rest/domains/" + Session["Domain"] + "/projects/" + Session["Project"] + "/defects");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/xml";
                httpWebRequest.Headers.Add(HttpRequestHeader.Cookie, Session["token"].ToString());
                httpWebRequest.Headers.Add(HttpRequestHeader.Cookie, Session["QcSession"].ToString());

                Entity en = new Entity();
                en.Type = "defect";

                EntityField detectedBy = new EntityField();
                detectedBy.Name = "detected-by";
                detectedBy.Value = Session["User"].ToString();

                EntityField creationTime = new EntityField();
                creationTime.Name = "creation-time";
                creationTime.Value = dCreationTime.Text;

                EntityField severity = new EntityField();
                severity.Name = "severity";
                severity.Value =ddSeverity.SelectedText;

                EntityField priority = new EntityField();
                priority.Name = "priority";
                priority.Value = ddPriority.SelectedText;
                
                EntityField name = new EntityField();
                name.Name = "name";
                name.Value = txtName.Text;

                EntityField desc = new EntityField();
                desc.Name = "description";
                desc.Value = hDescription.Text;

                EntityField status = new EntityField();
                status.Name = "status";
                status.Value = ddStatus.SelectedText;

                EntityField[] ens = new EntityField[]
                      {  detectedBy,creationTime,severity,priority,name,desc,status
                      };

                en.Fields = ens;

                string xml = XmlUtil.Serializer(typeof(Entity), en);
                byte[] btBodys = Encoding.UTF8.GetBytes(xml);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                XDocument doc = XDocument.Load(streamReader);
 
                streamReader.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
            }
            catch (Exception)
            {
               
            }
        }
    }
}
