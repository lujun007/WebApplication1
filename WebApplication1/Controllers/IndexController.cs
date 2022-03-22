using Aliyun.OSS;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class IndexController : ApiController
    {
        public static string accessKeyId = "";
        public static string accessKeySecret = "";
        public static string endpoint = "oss-cn-beijing.aliyuncs.com";
        private static OssClient client;

        //[HttpGet]
        //public HttpResponseMessage PostExportData()
        //{
        //    // http://jjw-personnel.oss-cn-shenzhen.aliyuncs.com/Image/Personnel/20190731/0lBCZ78mx3zW3l7gZXWteZ.jpg?x-oss-process=style/b_w1125_h843
        //    var file = ExcelStream();
        //    //string csv = _service.GetData(model);
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    result.Content = new StreamContent(file);
        //    //a text file is actually an octet-stream (pdf, etc)
        //    //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel");
        //    //we used attachment to force download
        //    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        //    result.Content.Headers.ContentDisposition.FileName = "file.xlsx";
        //    return result;
        //}

        //[HttpGet]
        //public HttpResponseMessage GetUploadFile(string url)
        //{
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //   // var a = GetSignedUrl("lujuntest", "b58f8c5494eef01fcca8beccecfe9925bc317d7f.jpg");

        //    if (client != null)
        //    {
        //        var obj = client.GetObject("lujuntest", "b58f8c5494eef01fcca8beccecfe9925bc317d7f.jpg");
        //        using (var requestStram = obj.Content)
        //        {
        //            //byte[] buf = new byte[1024];
        //            //var fs = File.Open("b58f8c5494eef01fcca8beccecfe9925bc317d7f.jpg", FileMode.OpenOrCreate);
        //            // result.Content = File.Open("b58f8c5494eef01fcca8beccecfe9925bc317d7f.jpg", FileMode.OpenOrCreate);
        //            //var len = 0;
        //            //while ((len = requestStram.Read(buf, 0, 1024)) != 0)
        //            //{
        //            //    fs.Write(buf, 0, len);
        //            //}
        //            result.Content = new StreamContent(requestStram);
        //            // fs.Close();
        //        }
        //    }
        //    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        //    //we used attachment to force download
        //    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        //    result.Content.Headers.ContentDisposition.FileName = "b58f8c5494eef01fcca8beccecfe9925bc317d7f.jpg";
        //    return result;
        //}

        //[System.Web.Mvc.HttpGet]
        // public ActionResult GetUploadFile()
        // {
        //     string url = "http://jjw-personnel.oss-cn-shenzhen.aliyuncs.com/Image/Personnel/20190731/0lBCZ78mx3zW3l7gZXWteZ.jpg?x-oss-process=style/b_w1125_h843";
        //     //创建请求
        //     WebRequest request = WebRequest.Create(url);
        //     //接收响应
        //     WebResponse response = request.GetResponse();
        //     //输出流
        //     Stream responseStream = response.GetResponseStream();
        //     byte[] bytes = new byte[responseStream.Length];
        //     responseStream.Read(bytes, 0, Convert.ToInt32(responseStream.Length));
        //     responseStream.Seek(0,SeekOrigin.Begin);
        //     return new FileContentResult(bytes, "application/octet-stream");
        // }

        [HttpGet]
        public HttpResponseMessage GetFile()
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string url = "http://jjw-personnel.oss-cn-shenzhen.aliyuncs.com/Image/Personnel/20190731/0lBCZ78mx3zW3l7gZXWteZ.jpg?x-oss-process=style/b_w1125_h843";
            //创建请求
            WebRequest request = WebRequest.Create(url);
            //接收响应
            WebResponse response = request.GetResponse();
            //输出流
            Stream responseStream = response.GetResponseStream();
            result.Content = new StreamContent(responseStream);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            //we used attachment to force download
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "b58f8c5494eef01fcca8beccecfe9925bc317d7f.jpg";
            return result;
        }

        ///// <summary>
        ///// 获取get 链接
        ///// </summary>
        ///// <param name="bucketName">储存空间名称</param>
        ///// <param name="key">文件名</param>
        ///// <returns></returns>

        //public static string GetSignedUrl(string bucketName, string key)
        //{
        //    string restr = "";
        //    try
        //    {
        //        client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        //        var request = new GeneratePresignedUriRequest(bucketName, key, SignHttpMethod.Get);//方式
        //        request.Expiration = DateTime.Now.AddMinutes(60);//有限时间
        //        var signedUrl = client.GeneratePresignedUri(request);
        //        restr = signedUrl.ToString();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return restr;
        //}


        //[HttpGet]
        //public HttpResponseMessage DownLoadFile(string url)
        //{
        //    WebClient client = new WebClient();
        //    client.BaseAddress = "http://www.mrwxk.com";                    //设置WebClient的基URI
        //    client.Encoding = Encoding.UTF8;                                //指定下载字符串的编码格式
        //    //为WebClient类对象添加报头
        //    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        //    //使用OperRead方法获取指定网站的数据，并保存到Stream中
        //    Stream stream = client.OpenRead("http://www.mrwxk.com");

        //    StreamReader sreader = new StreamReader(stream);
        //    string str = string.Empty;
        //    while ((str = sreader.ReadLine()) != null)
        //    {
        //        Console.WriteLine(str);
        //    }
        //    //调用WebClient对象的DownLoadFile方法将指定的网站内容保存到 D  盘中并创建一个新的txt文件
        //    client.DownloadFile("http://www.mrwxk.com", "D:\\" + DateTime.Now.ToFileTime() + ".txt");
        //}

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns></returns>
        //public ActionResult DownLoadFile(string filePath)
        //{
        //    if (filePath == null)
        //    {
        //        return null;
        //    }
        //    //string UrlString = "http://123.123.123.123/abc/123.pdf";
        //    int startIndex = filePath.LastIndexOf("/");
        //    string fileName = filePath.Substring(startIndex + 1);
        //    byte[] fileData;
        //    try
        //    {
        //        WebRequest.Create(filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        //To do something
        //        return null;
        //    }
        //    try
        //    {
        //        using (WebClient client = new WebClient())
        //        {
        //            fileData = client.DownloadData(filePath);
        //            return File(fileData, "text/plain", fileName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //To do something
        //        return null;
        //    }
        //}

        private System.IO.Stream ExcelStream()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields.Add("Name", "姓名");
            fields.Add("Age", "年龄");
            fields.Add("Birthday", "生日");

            NPOIManage npoiManage = new NPOIManage("人事信息" + DateTime.Now.ToString("yyyyMMdd"), "人事信息", fields);

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(string));
            table.Columns.Add("Birthday", typeof(string));

            DataRow newRow;
            newRow= table.NewRow();
            newRow["Name"] = "张三";
            newRow["Age"] = "13";
            newRow["Birthday"] = "1992-01-01";
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["Name"] = "李四";
            newRow["Age"] = "14";
            newRow["Birthday"] = "1992-02-02";
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["Name"] = "王五";
            newRow["Age"] = "15";
            newRow["Birthday"] = "1993-03-03";
            table.Rows.Add(newRow);

            return npoiManage.ExprotTable(table);
        }
    }
}
