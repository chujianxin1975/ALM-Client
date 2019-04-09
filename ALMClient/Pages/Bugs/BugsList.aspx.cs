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
namespace ALMClient
{
    public partial class BugsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.OnClientClick = Window1.GetShowReference("~/Pages/Bugs/BugsNew.aspx", "新增缺陷");

                BindGrid();
            }
        }

        #region LoadData

        private void BindGrid()
        {
            string sortField = Grid1.SortField;
            string sortDirection = Grid1.SortDirection;
            Opers o = new Opers();
            string ret = o.getRest(Session["url"].ToString(), "/qcbin/rest/domains/" + Session["Domain"] + "/projects/" + Session["Project"] + "/defects");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("Entities").ChildNodes;

           // o.ConvertXMLToDataSet(ret);
            DataTable table = new DataTable();
            DataColumn column1 = new DataColumn("ID", typeof(string));
            DataColumn column2 = new DataColumn("Name", typeof(String));
            DataColumn column3 = new DataColumn("CreationTime", typeof(string));
            DataColumn column4 = new DataColumn("Priority", typeof(string));
            DataColumn column5 = new DataColumn("Status", typeof(string));
            DataColumn column6 = new DataColumn("Description", typeof(string));

            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);  
            table.Columns.Add(column5);
            table.Columns.Add(column6);

            for (int i = 0; i < nodelist.Count; i++)
            {
                DataRow row = table.NewRow();
                row[0] = xmldoc.SelectNodes("//Field[@Name='id']")[i].InnerText;
                row[1] = xmldoc.SelectNodes("//Field[@Name='name']")[i].InnerText;
                row[2] = xmldoc.SelectNodes("//Field[@Name='creation-time']")[i].InnerText;
                row[3] = xmldoc.SelectNodes("//Field[@Name='priority']")[i].InnerText;
                row[4] = xmldoc.SelectNodes("//Field[@Name='status']")[i].InnerText;
                row[5] = xmldoc.SelectNodes("//Field[@Name='description']")[i].InnerText;
                table.Rows.Add(row);            
            }          

            DataView view1 = table.DefaultView;
            view1.Sort = String.Format("{0} {1}", sortField, sortDirection);
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        #endregion

        #region Events
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id =  Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();
            
            if (e.CommandName == "Delete")
            {
                Opers o = new Opers();
                string ret = o.deleteRest(Session["url"].ToString(), "/qcbin/rest/domains/" + Session["Domain"] + "/projects/" + Session["Project"] + "/defects/" + id);

                BindGrid();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //labResult.Text = HowManyRowsAreSelected(Grid1);
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;
        }
        
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;

            BindGrid();
        }

        #endregion

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
 

    }
}
