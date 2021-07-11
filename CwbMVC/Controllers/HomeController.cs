using CwbMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
/*
 * 本專案目的:建立天氣預報系統
 * Auther:Nelson
 * AddDate:2021/07/10
 * Function:
 * 1.取得各城市36小時預報資訊 (\Home\Index)
 * 2.顯示各城市36小時預報資訊列表 (\WeatherForecasts\Index)
 * 3.顯示各城市36小時單筆預報資訊 (\WeatherForecasts\Detail)
 * 4.DB Name:CWBSys
 * 5.table Name:WeatherForecast
 */

namespace CwbMVC.Controllers
{
    public class HomeController : Controller
    {
        private CWBsysEntities1 db = new CWBsysEntities1();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string GetInfo(string areaVal)
        {
            //ViewBag.Message = "取得氣象資訊";
            string strResult = "\"MainResult\":\"fail\"";
            //取得氣象局氣象資訊
            //string strGetUrl = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-5392B170-7480-429F-9505-0A9C0A4052BE&limit=100&locationName=%E6%96%B0%E5%8C%97%E5%B8%82";
            string strGetUrl = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?";
            string strAuth = "CWB-5392B170-7480-429F-9505-0A9C0A4052BE";
            string strLimit = "100";
            string strLocationName = areaVal;
            strGetUrl += "Authorization=" + strAuth;
            strGetUrl += "&limit=" + strLimit;
            strGetUrl += "&locationName=" + strLocationName;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strGetUrl);
            req.Method = "GET";
            req.Credentials = CredentialCache.DefaultNetworkCredentials;

            List<WeatherForecast> oWs = new List<WeatherForecast>();

            using (WebResponse wr = req.GetResponse())
            {
                StreamReader reader = new StreamReader(wr.GetResponseStream());
                string jsonData = reader.ReadToEnd();

                WeatherJsonData weatherData = JsonConvert.DeserializeObject<WeatherJsonData>(jsonData);
                Console.WriteLine(weatherData.records.datasetDescription);
                string strDatasetDescription = weatherData.records.datasetDescription;
                string strLocationNameVal = weatherData.records.location[0].locationName.ToString();

                foreach (Weatherelement ep in weatherData.records.location[0].weatherElement)
                {
                    string strelementName = ep.elementName;
                    string strstartTime = ep.time[0].startTime;
                    string strendTime = ep.time[0].endTime;
                    string strparameterName = ep.time[0].parameter.parameterName;
                    string strparameterValue = ep.time[0].parameter.parameterValue;
                    string strparameterUnit = ep.time[0].parameter.parameterUnit;
                    WeatherForecast data = new WeatherForecast();
                    data.datasetDescription = strDatasetDescription;
                    data.locationName = strLocationNameVal;
                    data.elementName = strelementName;
                    data.startTime = Convert.ToDateTime(strstartTime);
                    data.endTime = Convert.ToDateTime(strendTime);
                    data.parameterName = strparameterName;
                    data.parameterValue = strparameterValue;
                    data.parameterUnit = strparameterUnit;
                    data.createTime = DateTime.Now;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.WeatherForecast.Add(data);
                    db.SaveChanges();
                    db.Configuration.ValidateOnSaveEnabled = true;
                }
                //寫入資料庫成功
                strResult = "\"MainResult\":\"success\"";
                

            }
            
            strResult = "[{" + strResult + "}]";
          
            return strResult;
        }

        
    }
}