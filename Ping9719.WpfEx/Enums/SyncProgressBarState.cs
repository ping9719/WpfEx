using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping9719.WpfEx
{
    /// <summary>
    /// 同步进度条状态
    /// </summary>
    public enum SyncProgressBarState
    {
        /// <summary>
        /// 同步成功并且显示
        /// </summary>
        OkVisible,
        /// <summary>
        /// 同步成功不显示控件
        /// </summary>
        OkCollapsed,
        /// <summary>
        /// 同步中
        /// </summary>
        SyncIn,
        /// <summary>
        /// 同步失败
        /// </summary>
        SyncErr
    }
}
