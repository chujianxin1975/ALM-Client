using System;
using System.Web.Security;
using System.Web.UI;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
 

using FineUIPro;

namespace AppBoxPro
{
    public class PageBase : System.Web.UI.Page
    {
        #region 只读静态变量

        // Session key
        private static readonly string SK_ONLINE_UPDATE_TIME = "OnlineUpdateTime";
        //private static readonly string SK_USER_ROLE_ID = "UserRoleId";

        private static readonly string CHECK_POWER_FAIL_PAGE_MESSAGE = "您无权访问此页面！";
        private static readonly string CHECK_POWER_FAIL_ACTION_MESSAGE = "您无权进行此操作！";



        #endregion
            
        #region 浏览权限

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public virtual string ViewPower
        {
            get
            {
                return String.Empty;
            }
        }

        #endregion

        #region 页面初始化

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            // 设置主题
            //if (PageManager.Instance != null)
            //{
            //    var pm = PageManager.Instance;
            //    var themeValue = ConfigHelper.Theme;
            //    // 是否为内置主题
            //    if (IsSystemTheme(themeValue))
            //    {
            //        pm.CustomTheme = String.Empty;
            //        pm.Theme = (Theme)Enum.Parse(typeof(Theme), themeValue, true);
            //    }
            //    else
            //    {
            //        pm.CustomTheme = themeValue;
            //    }
            //}

            // 设置页面标题
            //Page.Title = ConfigHelper.Title;

            // 禁用表单的自动完成功能
            Form.Attributes["autocomplete"] = "off";
        }

        private bool IsSystemTheme(string themeName)
        {
            themeName = themeName.ToLower();
            string[] themes = Enum.GetNames(typeof(Theme));
            foreach (string theme in themes)
            {
                if (theme.ToLower() == themeName)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 请求参数

        /// <summary>
        /// 获取查询字符串中的参数值
        /// </summary>
        protected string GetQueryValue(string queryKey)
        {
            return Request.QueryString[queryKey];
        }


        /// <summary>
        /// 获取查询字符串中的参数值
        /// </summary>
        protected int GetQueryIntValue(string queryKey)
        {
            int queryIntValue = -1;
            try
            {
                queryIntValue = Convert.ToInt32(Request.QueryString[queryKey]);
            }
            catch (Exception)
            {
                // TODO
            }

            return queryIntValue;
        }

        #endregion

        #region 表格相关

        protected int GetSelectedDataKeyID(Grid grid)
        {
            int id = -1;
            int rowIndex = grid.SelectedRowIndex;
            if (rowIndex >= 0)
            {
                id = Convert.ToInt32(grid.DataKeys[rowIndex][0]);
            }
            return id;
        }

        protected string GetSelectedDataKey(Grid grid, int dataIndex)
        {
            string data = String.Empty;
            int rowIndex = grid.SelectedRowIndex;
            if (rowIndex >= 0)
            {
                data = grid.DataKeys[rowIndex][dataIndex].ToString();
            }
            return data;
        }

        /// <summary>
        /// 获取表格选中项DataKeys的第一个值，并转化为整型列表
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected List<int> GetSelectedDataKeyIDs(Grid grid)
        {
            List<int> ids = new List<int>();
            foreach (int rowIndex in grid.SelectedRowIndexArray)
            {
                ids.Add(Convert.ToInt32(grid.DataKeys[rowIndex][0]));
            }

            return ids;
        }

        #endregion
       
        #region 产品版本

        public string GetProductVersion()
        {
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            return String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
        }

        #endregion


    }
}
