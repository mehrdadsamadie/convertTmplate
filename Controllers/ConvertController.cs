using convertTmplate.Models.Entity;
using convertTmplate.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using convert.Models;

namespace convertTmplate.Controllers
{
    public class ConvertController : Controller
    {
        File1Repository File1Repository = new File1Repository();
        File2Repository File2Repository = new File2Repository();
        File3Repository File3Repository = new File3Repository();
        // GET: Convert
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        DataTable dt = new DataTable();
                        if (upload.ContentLength > 0)
                        {

                            string fileName = Path.GetFileName(upload.FileName);
                            string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                            try
                            {
                                upload.SaveAs(path);
                                ProcessCSV(path);
                            }
                            catch (Exception ex)
                            {

                                ViewData["Feedback"] = ex.Message;
                            }
                        }

                        dt.Dispose();

                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "This file format is not supported");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            return View();
        }


        private void ProcessCSV(string fileName)
        {

            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            StreamReader sr = new StreamReader(fileName);
            line = sr.ReadLine();
            strArray = r.Split(line);
            if (strArray.Length == 5 && strArray[0].ToLower() == "identifier")
            {
                Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        var _file1 = new File1();
                        var _itemArray = r.Split(line);
                        _file1.Identifier = _itemArray[0];
                        _file1.Name = _itemArray[1];
                        _file1.AccountType = string.IsNullOrEmpty(_itemArray[2]) == true ? (int?)null : (int)(AccountType)Enum.Parse(typeof(AccountType), _itemArray[2]);
                        _file1.Opened = string.IsNullOrEmpty(_itemArray[3]) == true ? (DateTime?)null : DateTime.Parse(_itemArray[3]);
                        _file1.CurrencyType = string.IsNullOrEmpty(_itemArray[4]) == true ? (int?)null : (int)(CurrencyType)Enum.Parse(typeof(CurrencyType), _itemArray[4]);
                        File1Repository.Add(_file1);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
            else if (strArray.Length <= 4)
            {
                Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));
                while ((line) != null)
                {
                    try
                    {
                        var _file2 = new File2();
                        var _itemArray = r.Split(line);
                        _file2.Name = _itemArray[0];
                        _file2.AccountType = string.IsNullOrEmpty(_itemArray[1]) == true ? (int?)null : (int)(AccountType)Enum.Parse(typeof(AccountType), _itemArray[1]);
                        _file2.CurrencyType = string.IsNullOrEmpty(_itemArray[2]) == true ? (int?)null : (int)(CurrencyType)Enum.Parse(typeof(CurrencyType), _itemArray[2]);
                        _file2.CustodianCode = _itemArray.Length < 4 ? null : _itemArray[3];
                        File2Repository.Add(_file2);
                        line = sr.ReadLine();
                    }
                    catch (Exception e)
                    {
                        line = sr.ReadLine();
                        continue;
                    }

                }
            }
            else if (strArray.Length == 5 && strArray[0].ToLower() != "identifier")
            {
                Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        var _file3 = new File3();
                        var _itemArray = r.Split(line);
                        _file3.AccountCode = _itemArray[0];
                        _file3.Name = _itemArray[1];
                        _file3.AccountType = string.IsNullOrEmpty(_itemArray[2]) == true ? (int?)null : (int)(AccountType)Enum.Parse(typeof(AccountType), _itemArray[2]);
                        _file3.Date = string.IsNullOrEmpty(_itemArray[3]) == true ? (DateTime?)null : DateTime.Parse(_itemArray[3]);
                        _file3.CurrencyType = string.IsNullOrEmpty(_itemArray[4]) == true ? (int?)null : (int)(CurrencyType)Enum.Parse(typeof(CurrencyType), _itemArray[4]); ;
                        File3Repository.Add(_file3);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                }
            }

            sr.Dispose();

        }
    }

}