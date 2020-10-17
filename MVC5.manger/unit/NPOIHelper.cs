using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace System
{
    public class NPOIHelper
    {

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <param name="list">todo: describe list parameter on ExportByWeb</param>
        /// <param name="titleDic">todo: describe titleDic parameter on ExportByWeb</param>
        public static void ExportByWeb<T>(List<T> list, string strFileName, Dictionary<string, string> titleDic)
        {
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            strFileName = strFileName + DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = HttpUtility.UrlEncode(strFileName, Encoding.UTF8) + ".xls";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + fileName);

            curContext.Response.BinaryWrite(ListToExcel(list, strFileName, titleDic).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// List导出到Excel的MemoryStream
        /// </summary>
        /// <param name="list">需要导出的泛型List</param>
        /// <param name="strHeaderText">第一行标题头</param>
        /// <param name="titleDictionaries">列名称字典映射</param>
        /// <param name="title">todo: describe title parameter on ListToExcel</param>
        /// <param name="titleDic">todo: describe titleDic parameter on ListToExcel</param>
        /// <returns>内存流</returns>
        private static MemoryStream ListToExcel<T>(List<T> list, string strHeaderText = null,
            Dictionary<string, string> titleDic = null)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            //设置文件属性
            SetFileSummary(strHeaderText, workbook);

            //获取列宽
            int[] arrColWidth = GetColumnWidth(titleDic);

            //日期风格
            ICellStyle dateStyle = GetDateStyle(workbook);

            //Excel标题风格
            HSSFCellStyle headStyle = GetHeadStyle(workbook);

            /*在第一行创建标题行*/
            CreateHeadRow(titleDic, sheet, arrColWidth, headStyle);

            //通过反射得到对象的属性集合  
            Type type = null;
            if (list != null && list.Count > 0)
            {
                type = list.First().GetType();
                for (int row = 0; row < list.Count; row++)
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(row + 1);

                    int cellIndex = 0;
                    foreach (var dicItem in titleDic)
                    {
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(cellIndex);

                        string drValue = string.Empty;

                        PropertyInfo propInfo = type.GetProperty(dicItem.Key);

                        var propValue = type.GetProperty(dicItem.Key).GetValue(list[row]);
                        if (propValue != null)
                        {
                            drValue = propValue.ToString();
                        }
                        SetCellValueByType(newCell, drValue, propInfo, dateStyle);

                        cellIndex = cellIndex + 1;
                    }
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook.Close();
                return ms;
            }
        }

        private static void SetCellValueByType(HSSFCell newCell, string drValue, PropertyInfo propInfo, ICellStyle dateStyle)
        {
            if (string.IsNullOrEmpty(drValue))
            {
                return;
            }

            string propertyName = GetPropertyFullName(propInfo);

            switch (propertyName)
            {
                case "System.String": //字符串类型
                    newCell.SetCellValue(drValue);
                    break;
                case "System.DateTime": //日期类型
                    DateTime dateV;
                    DateTime.TryParse(drValue, out dateV);
                    newCell.SetCellValue(dateV);

                    newCell.CellStyle = dateStyle; //格式化显示
                    break;
                case "System.Boolean": //布尔型
                    bool boolV = false;
                    bool.TryParse(drValue, out boolV);
                    newCell.SetCellValue(boolV);
                    break;
                case "System.Int16": //整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    int.TryParse(drValue, out intV);
                    newCell.SetCellValue(intV);
                    break;
                case "System.Decimal": //浮点型
                case "System.Double":
                    double doubV = 0;
                    double.TryParse(drValue, out doubV);
                    newCell.SetCellValue(doubV);
                    break;
                case "System.DBNull": //空值处理
                    newCell.SetCellValue("");
                    break;
                default:
                    newCell.SetCellValue(drValue);
                    break;
            }
        }

        private static string GetPropertyFullName(PropertyInfo propInfo)
        {
            var propertyName = propInfo.PropertyType.FullName;
            if (propInfo.PropertyType.IsGenericType && propInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyName = propInfo.PropertyType.GetGenericArguments()[0].FullName;
            }

            return propertyName;
        }

        private static object GetCellValueByType(ICell cell, PropertyInfo propInfo)
        {
            if (string.IsNullOrWhiteSpace(cell.ToString()))
            {
                return string.Empty;
            }
            if (propInfo.PropertyType.IsEnum)
            {
                return Enum.Parse(propInfo.PropertyType, cell.ToString());
            }
            string propertyName = GetPropertyFullName(propInfo);
            switch (propertyName)
            {
                case "System.String": //字符串类型
                    return cell.ToString();
                case "System.DateTime": //日期类型
                    return cell.DateCellValue;
                case "System.Boolean": //布尔型
                    return cell.BooleanCellValue;
                case "System.Int16": //整型
                case "System.Int32":
                case "System.Int64":
                    int.TryParse(cell.ToString(), out int value);
                    return value;
                case "System.Byte":
                case "System.Decimal": //浮点型
                case "System.Double":
                    return cell.NumericCellValue;
                case "System.Single":
                    return Convert.ToSingle(cell.ToString());
                default:
                    return cell.ToString();
            }
        }

        private static void CreateHeadRow(Dictionary<string, string> titleDic, HSSFSheet sheet, int[] arrColWidth, HSSFCellStyle headStyle)
        {
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

            int colIndex = 0;
            foreach (var dicItem in titleDic)
            {
                string columnName = dicItem.Value;
                headerRow.CreateCell(colIndex).SetCellValue(columnName);
                headerRow.GetCell(colIndex).CellStyle = headStyle;
                //设置列宽
                sheet.SetColumnWidth(colIndex, (arrColWidth[colIndex] + 1) * 500);
                colIndex++;
            }
        }

        private static HSSFCellStyle GetHeadStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 14;
            font.Boldweight = 500;
            font.FontName = "宋体";
            headStyle.SetFont(font);
            headStyle.VerticalAlignment = VerticalAlignment.Center;
            headStyle.Alignment = HorizontalAlignment.Center;//水平对齐
            return headStyle;
        }

        private static HSSFCellStyle GetDateStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd HH:mm:ss");
            return dateStyle;
        }

        /// <summary>
        /// 设置文件属性信息
        /// </summary>
        /// <param name="strHeaderText"></param>
        /// <param name="workbook"></param>
        private static void SetFileSummary(string strHeaderText, HSSFWorkbook workbook)
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = ""; //填加xls文件作者信息
            si.ApplicationName = "管理系统"; //填加xls文件创建程序信息
            si.LastAuthor = ""; //填加xls文件最后保存者信息
            si.Comments = ""; //填加xls文件作者信息
            si.Title = strHeaderText; //填加xls文件标题信息
            si.Subject = ""; //填加文件主题信息
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;
        }

        /// <summary>
        /// 获取列宽
        /// </summary>
        /// <param name="titleDic"></param>
        /// <returns></returns>
        private static int[] GetColumnWidth(Dictionary<string, string> titleDic)
        {
            int fieldsCount = titleDic.Count;
            int[] arrColWidth = new int[fieldsCount];
            int index = 0;
            foreach (var item in titleDic)
            {
                arrColWidth[index] = Encoding.GetEncoding(936).GetBytes(item.Value).Length;
                index++;
            }

            return arrColWidth;
        }

        private static IWorkbook workbook = null;
        private static FileStream fs = null;
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName">excel文件路径</param>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; i++)
                        {
                            DataColumn column = new DataColumn(i.ToString());
                            data.Columns.Add(column);
                        }
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }

}