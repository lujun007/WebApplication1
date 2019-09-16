using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace WebApplication1
{
    /// <summary>
    /// Excel常用的表格导出逻辑封装
    /// 单表写入
    /// </summary>
    public class NPOIManage
    {
        #region 私有属性
        private string _fileName = DateTime.Now.ToString("yyyyMMdd");
        private string _sheetName = "Sheet1";
        private XSSFWorkbook _xssFWorkbook=null;
        private ISheet _sheet = null;
        /// <summary>
        /// 导出文件名称
        /// </summary>
        public string FileName
        {
            get { if (string.IsNullOrWhiteSpace(_fileName)) { _fileName = DateTime.Now.ToString("yyyyMMdd"); } return _fileName; }
            set { _fileName = value; }
        }

        /// <summary>
        /// Sheet页名称
        /// </summary>
        public string SheetName
        {
            get { if (string.IsNullOrWhiteSpace(_sheetName)) { _sheetName = "Sheet1"; } return _sheetName; }
            set { _sheetName = value; }
        }
        
        /// <summary>
        /// 导出字段
        /// </summary>
        public Dictionary<string, string> Fieds { get; set; }
        #endregion

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        public NPOIManage(string fileName,string sheetName, Dictionary<string, string> fieds)
        {
            this.FileName = fileName;
            this.SheetName = sheetName;
            this.Fieds = fieds;
            _xssFWorkbook = new XSSFWorkbook();
            _sheet = _xssFWorkbook.CreateSheet(sheetName);
        }

        /// <summary>
        /// 创建表头
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private void SettingHeader()
        {
            if (Fieds != null)
            {
                IRow cells = _sheet.CreateRow(0);
                int i = 0;//计数器
                foreach (string value in Fieds.Values)
                {
                    cells.CreateCell(i).SetCellValue(value);
                    i++;
                }
            }
        }

        ///// <summary>
        ///// 多个Sheet页,table转换Eexcel
        ///// </summary>
        ///// <param name="dataSet">数据内容</param>
        ///// <returns></returns>
        //public XSSFWorkbook GetXSSFWorkbook(DataSet dataSet)
        //{
        //    XSSFWorkbook xSSFWorkbook = new XSSFWorkbook();
        //    if (dataSet.Tables.Count > 0)
        //    {
        //        for(int i = 0; i < dataSet.Tables.Count; i++)
        //        {
        //            DataTable tab = dataSet.Tables[i];
        //            ISheet sheet = xSSFWorkbook.CreateSheet(dataSet.Tables[i].TableName.ToString().Trim());
        //            for(int j = 0; j < tab.Rows.Count; i++)
        //            {
        //                IRow cells = sheet.CreateRow(j);
        //                for (int k = 0; k < tab.Columns.Count; k++)
        //                {
        //                    cells.CreateCell(k).SetCellValue(tab.Rows[j][k].ToString());
        //                }
        //            }
        //        }
        //    }
        //    xSSFWorkbook.Close();
        //    return xSSFWorkbook;
        //}

        ///// <summary>
        ///// 单个Sheet页,table转换Eexcel
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="sheetName"></param>
        ///// <returns></returns>
        //public XSSFWorkbook GetXSSFWorkbookSingleSheet(DataTable dt,string sheetName="Sheet1")
        //{
        //    XSSFWorkbook xSSFWorkbook = new XSSFWorkbook();
        //    if (dt!=null)
        //    {
        //        ISheet sheet = xSSFWorkbook.CreateSheet(sheetName);
        //        for(int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            IRow cells = sheet.CreateRow(i);
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                cells.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
        //            }
        //        }
        //    }
        //    xSSFWorkbook.Close();
        //    return xSSFWorkbook;
        //}
        
        /// <summary>
        /// DataTable数据导出到Excel中
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public System.IO.Stream ExprotTable(DataTable dt)
        {
            if (dt != null)
            {
                SettingHeader();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow cells = _sheet.CreateRow(i+1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cells.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    
                }
                //设置自适应列宽
                for (int i = 0; i < this.Fieds.Count; i++)
                {
                    _sheet.AutoSizeColumn(i);
                }
            }

            var file = new NPOIMemoryStream();
            file.AllowClose = false;
            _xssFWorkbook.Write(file);
            file.Seek(0, SeekOrigin.Begin);
            file.AllowClose = true;
            return file;
        }
    }

    public class NPOIMemoryStream : MemoryStream
    {
        public bool AllowClose { get; set; }
        public NPOIMemoryStream()
        {
            AllowClose = true;
        }

        public override void Close()
        {
            if (AllowClose)
            {
                base.Close();
            }
        }
    }
}