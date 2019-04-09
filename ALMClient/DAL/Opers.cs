using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ALMClient.DAL
{
    public class Opers : System.Web.UI.Page
    {
        public Boolean getToken(string url)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(url + "/qcbin/authentication-point/alm-authenticate");
                var request = new RestRequest(Method.POST);

                request.AddHeader("Accept", "application/xml");

                string body = "<alm-authentication><user>" + Session["user"] + "</user> <password>" + Session["password"] + "</password></alm-authentication>";

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
            catch (Exception)
            {

                return false;
            }

        }
        public string getRest(string url, string req)
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
            catch (Exception )
            {
                return "";
            }
        }
        public string deleteRest(string url, string req)
        {
            try
            {
                HttpWebRequest domainreq = (HttpWebRequest)WebRequest.Create(url + req);
                domainreq.Method = "DELETE";

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
        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

        }
    }
}