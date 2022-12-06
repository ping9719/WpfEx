using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 枚举帮助扩展类
    /// </summary>
    public static class EnumHelp
    {
        /// <summary>
        /// 得到枚举特性Description
        /// </summary>
        /// <param name="obj">枚举</param>
        /// <returns>返回特性中的说明文本，没有找到特性返回null</returns>
        public static string GetAttributeDescription(Enum obj)
        {
            if (obj == null)
                return null;

            var type = obj.GetType();
            //获取到:Admin
            var enumName = Enum.GetName(type, obj);
            var field = type.GetField(enumName);
            //获取到:0
            //var val = (int)field.GetValue(null);
            var atts = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (atts != null && atts.Length > 0)
            {
                //获取到：超级管理员
                var att = ((DescriptionAttribute[])atts)[0];
                var des = att.Description;
                return des;
            }
            return null;
        }
    }
}
