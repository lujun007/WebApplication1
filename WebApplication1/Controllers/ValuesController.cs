using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }



        [HttpGet]
        public HttpResponseMessage PostExportData()
        {
            var file = ExcelStream();
            //string csv = _service.GetData(model);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(file);
            //a text file is actually an octet-stream (pdf, etc)
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel");
            //we used attachment to force download
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "file.xls";
            return result;
        }


        private System.IO.Stream ExcelStream()
        {
            //var list = dc.v_bs_dj_bbcdd1.Where(eps).ToList();
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            ISheet sheet1 = hssfworkbook.CreateSheet("保税订单");


            IRow rowHeader = sheet1.CreateRow(0);

            //生成excel标题
            rowHeader.CreateCell(0).SetCellValue("汇通单号");
            rowHeader.CreateCell(1).SetCellValue("单据日期");
            rowHeader.CreateCell(2).SetCellValue("订单号");
            rowHeader.CreateCell(3).SetCellValue("收件人");
            rowHeader.CreateCell(4).SetCellValue("收件人电话");
            rowHeader.CreateCell(5).SetCellValue("收件人地址");
            rowHeader.CreateCell(6).SetCellValue("物流公司");
            rowHeader.CreateCell(7).SetCellValue("运单号");
            rowHeader.CreateCell(8).SetCellValue("数量");
            rowHeader.CreateCell(9).SetCellValue("状态");

            //生成excel内容
            //for (int i = 0; i < list.Count; i++)
            //{
            //    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
            //    rowtemp.CreateCell(0).SetCellValue(list[i].bh_user);
            //    rowtemp.CreateCell(1).SetCellValue(list[i].rq.Value.ToString("yyyy-MM-dd HH:mm:dd"));
            //    rowtemp.CreateCell(2).SetCellValue(list[i].bh_khdd);
            //    rowtemp.CreateCell(3).SetCellValue(list[i].re_name);
            //    rowtemp.CreateCell(4).SetCellValue(list[i].re_tel);
            //    rowtemp.CreateCell(5).SetCellValue(list[i].re_fulladdress);
            //    rowtemp.CreateCell(6).SetCellValue(list[i].bm_kdgs);
            //    rowtemp.CreateCell(7).SetCellValue(list[i].kddh);
            //    rowtemp.CreateCell(8).SetCellValue((int)list[i].sl_total);
            //    rowtemp.CreateCell(9).SetCellValue(list[i].mc_state_dd);
            //}

            for (int i = 0; i < 10; i++)
                sheet1.AutoSizeColumn(i);

            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            file.Seek(0, SeekOrigin.Begin);

            return file;

            //return File(file, "application/vnd.ms-excel", "保税订单.xls");
        }
    }
}
