using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace HF.MembershipProvider
{
    public static class ListGenerater
    {
        //加入DataTable扩展方法
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            Type type = typeof(T);
            var fieldBinding = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            //得到数据库字段名和类名的映射关系
            var mappings = type.GetProperties()
                              .Where((item) =>
                              {
                                  var attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), false);
                                  return attrs.Length > 0;
                              })
                               .Select((item) =>
                               {
                                   var attr = item.GetCustomAttributes(typeof(DBColumnAttribute), false)[0] as DBColumnAttribute;
                                   var dbName = (string.IsNullOrEmpty(attr.DbName) ? item.Name : attr.DbName);
                                   var storage = string.IsNullOrEmpty(attr.Storage) ? null : type.GetField(attr.Storage, fieldBinding);
                                   return new
                                   {
                                       Type = item.PropertyType,
                                       DbName = dbName,
                                       Property = item,
                                       StorageField = storage
                                   };
                               });

            //动态生成类,根据映射关系得到datatable里的数据,再赋值到类中
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T temp = Activator.CreateInstance<T>();
                foreach (var mapping in mappings)
                {
                    if (mapping.StorageField == null)
                    {
                        mapping.Property.SetValue(temp, row[mapping.DbName], null);
                    }
                    else
                    {
                        mapping.StorageField.SetValue(temp, row[mapping.DbName]);
                    }
                }
                list.Add(temp);
            }
            return list;
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DBColumnAttribute : Attribute
    {
        /// <summary>
        /// 对应数据集中的字段名
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// 存储的字段，如果设置，会绕过属性存取器，直接对字段赋值
        /// </summary>
        public string Storage { get; set; }
    }
}