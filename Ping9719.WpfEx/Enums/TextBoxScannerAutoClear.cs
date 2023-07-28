using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 自动清空模式
    /// </summary>
    public enum TextBoxScannerAutoClear
    {
        /// <summary>
        /// 默认，不清空
        /// </summary>
        NoClear,
        /// <summary>
        /// 清空
        /// </summary>
        Clear,
        /// <summary>
        /// 下次清空
        /// </summary>
        NextClear
    }
}
