using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 伺服运动位置类型
    /// </summary>
    public enum ServoClickType
    {
        /// <summary>
        /// 设置速度
        /// </summary>
        SetSpeed,
        /// <summary>
        /// 运动到指定位置
        /// </summary>
        MoveTo,
        /// <summary>
        /// 开始点动增加
        /// </summary>
        StartDotAdd,
        /// <summary>
        /// 结束点动增加
        /// </summary>
        EndDotAdd,
        /// <summary>
        /// 开始点动减少
        /// </summary>
        StartDotSub,
        /// <summary>
        /// 结束点动减少
        /// </summary>
        EndDotSub,
    }
}
