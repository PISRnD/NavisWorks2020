using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace PivdcSupportModule
{
    public static class DataConversionSupport
    {
        public static DataTable ConvertListToDataTable(this List<object> dataList, string tableName, Type type)
        {
            DataTable dataTable = new DataTable(tableName);
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                dataTable.Columns.Add(propertyInfo.Name);
            }
            foreach (var dataItem in dataList)
            {
                var values = new object[propertyInfos.Length];
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    values[i] = propertyInfos[i].GetValue(dataItem, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}