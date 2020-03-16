using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HRMS.utils
{
    public class ExportExcelUtil<T>
    {
        public static XSSFWorkbook export(List<T> list, string[][] heads)
        {
            //创建excel对象
            XSSFWorkbook xwb = new XSSFWorkbook();
            //当有数据的时候才创建sheet
            if (list.Count > 0)
            {
                //创建一个sheet
                ISheet sheet = xwb.CreateSheet(list[0].GetType().Name);

                //创建表头
                IRow titleRow = sheet.CreateRow(0);
                for (int i = 0; i < heads.Length; i++)
                {
                    titleRow.CreateCell(i).SetCellValue(heads[i][0]);
                }

                //创建数据
                for (int x = 0; x < list.Count; x++)
                {
                    IRow row = sheet.CreateRow(x + 1);

                    PropertyInfo[] pis = list[x].GetType().GetProperties();
                    for (int y = 0; y < pis.Length; y++)
                    {
                        for (int z = 0; z < heads.Length; z++)
                        {
                            if (pis[y].Name == heads[z][1])
                            {
                                object obj = pis[y].GetValue(list[x]);
                                string value = "";
                                if (obj != null)
                                {
                                    value = obj.ToString();
                                }
                                row.CreateCell(y).SetCellValue(value);
                            }
                        }
                    }
                }

            }


            return xwb;
        }
    }
}