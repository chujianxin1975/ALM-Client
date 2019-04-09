using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FineUIPro;
using System.Xml;
using System.Net;
using System.IO;
using System.Xml.Linq;
using RestSharp;

namespace ALMClient
{
    public partial class login : System.Web.UI.Page
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
            
        }
  
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (ddDomain.SelectedText != "" && ddProject.SelectedText != "")
            {
                Session["Domain"] = ddDomain.SelectedText;
                Session["Project"] = ddProject.SelectedText;
                FineUIPro.PageContext.Redirect("default.aspx");
            }            
        }

        protected void btnAuth_Click(object sender, EventArgs e)
        {
            Session["url"] = txtUrl.Text;
            Session["user"] = txtUser.Text;
            Session["password"] = txtPass.Text;

            string url = Session["url"].ToString();
            frmDomain.Hidden = !getToken(url);

            ddDomain.Items.Clear();
            ddProject.Items.Clear();
            string ret = getRest(url, "/qcbin/rest/domains");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("Domains").ChildNodes;
            for (int i = 0; i < nodelist.Count; i++)
            {
                ddDomain.Items.Add(nodelist[i].Attributes[0].Value, nodelist[i].Attributes[0].Value);
            }            
        }
        private Boolean getToken(string url)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(url + "/qcbin/authentication-point/alm-authenticate");
                var request = new RestRequest(Method.POST);

                request.AddHeader("Accept", "application/xml");

                string body = "<alm-authentication><user>" + txtUser.Text + "</user> <password>" + txtPass.Text + "</password></alm-authentication>";

                request.AddParameter("application/xml", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    return false;

                if (response.ErrorException != null)
                    throw response.ErrorException;

                Session["token"] = response.Headers[2].Value.ToString().Replace(";HTTPOnly", "");

                HttpWebRequest domainreq = (HttpWebRequest)WebRequest.Create(url + "/qcbin/rest/site-session");
                domainreq.Method = "POST";

                domainreq.Headers.Set(HttpRequestHeader.Cookie, Session["token"].ToString());
                HttpWebResponse domainres = (HttpWebResponse)domainreq.GetResponse();
                Session["QcSession"] = domainres.GetResponseHeader("Set-Cookie");

                return true;
            }
            catch (Exception )
            {
         
                return false;
            }

        }
        private string getRest(string url,string req)
        {
            try
            {
                HttpWebRequest domainreq = (HttpWebRequest)WebRequest.Create(url + req);
                domainreq.Method = "GET";

                domainreq.Headers.Add(HttpRequestHeader.Cookie, Session["token"].ToString());
                domainreq.Headers.Add(HttpRequestHeader.Cookie, Session["QcSession"].ToString());

                HttpWebResponse domainres = (HttpWebResponse)domainreq.GetResponse();
                Stream RStream = domainres.GetResponseStream();
                XDocument doc = XDocument.Load(RStream);

                return doc.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        protected void ddDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddProject.Items.Clear();
                string ret = getRest(Session["url"].ToString(),"/qcbin/rest/domains/" + ddDomain.SelectedText + "/projects");
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(ret);
                XmlNodeList nodelist = xmldoc.SelectSingleNode("Projects").ChildNodes;
                for (int i = 0; i < nodelist.Count; i++)
                {
                    ddProject.Items.Add(nodelist[i].Attributes[0].Value, nodelist[i].Attributes[0].Value);
                }
            }
            catch (Exception )
            { }
        }

    }
}
