using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace CSM.MedicalInsurance.Assignment.Utility
{
    public static class Extensions
    {
        #region Html Templates
        static readonly string headerFormat = "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>{0}</th>";
        static readonly string BeginTableTag = "<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>";
        static readonly string EndTableTag = "</table>";
        static readonly string BeginRowTag = "<tr>";
        static readonly string EndRowTag = "</tr>";
        static readonly string HtmlBeginTag = "<html>";
        static readonly string HtmlEndTag = "</html>";
        static readonly string BodyBeginTag = "<body>";
        static readonly string BodyEndTag = "</body>";
        static readonly string HeadBeginTag = "<head>";
        static readonly string HeadEndTag = "</head>";

        static readonly string ColumnFormat = "<td style='border: 1px solid #ccc'>{0}</td>";
        #endregion
        private static T CreateItemFromRow<T>(DataRow row, List<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    if (row[property.Name] != DBNull.Value)
                        property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }
        public static T ConvertFromDataRow<T>(this DataRow row) where T : new()
        {
            T item = new T();
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    if (row[property.Name] != DBNull.Value)
                        property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> results = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                results.Add(item);
            }
            return results;
        }

        public static List<SelectListItem> ToSelectList<T>(this List<T> list, string idPropertyName, string namePropertyName = "Name")
       where T : class, new()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            list.ForEach(item =>
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.GetType().GetProperty(namePropertyName).GetValue(item).ToString(),
                    Value = item.GetType().GetProperty(idPropertyName).GetValue(item).ToString()
                });
            });

            return selectListItems;
        }
        public static int ParseToInteger(this string str)
        {
            if(int.TryParse(str, out int result)) return result;
            return 0;
        }
        public static int ParseToInteger(this Object obj)
        {
            if(obj == null) return -1;
            if (int.TryParse(obj.ParseToText(), out int result)) return result;
            return -1;
        }
        public static string ParseToText(this Object obj)
        {
            if (obj==null)
                return string.Empty;
            return  Convert.ToString(obj);
        }

        public static string GenerateHtmlTable<T>(this List<T> list, IEnumerable<string> requiredFields = null) where T : new()
        {
            try
            {
                if (list == null || list.Count <= 0) { return string.Empty; }
                if (requiredFields == null || requiredFields.Count() <= 0)
                {
                    requiredFields = typeof(T).GetProperties().Select(p => p.Name);
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(HtmlBeginTag)
                    .Append(HeadBeginTag)
                    .Append(BodyBeginTag);
                sb.Append(BeginTableTag).Append(BeginRowTag);
                foreach (var field in requiredFields)
                {
                    sb.Append(string.Format(headerFormat, field));
                }
                sb.Append(EndRowTag);
                foreach (var row in list)
                {
                    sb.Append(BeginRowTag);
                    foreach (var field in requiredFields)
                    {
                        var property = row.GetType().GetProperty(field);
                        sb.Append(string.Format(ColumnFormat, property.GetValue(row, null)));
                    }
                    sb.Append(EndRowTag);
                }
                sb.Append(EndTableTag).Append(BodyEndTag).Append(HeadEndTag).Append(HtmlEndTag);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static byte[] GetBytes(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}