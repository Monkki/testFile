/**
 * 框架核心参数
 *
 * Description
 *  1.系统常量
 *  2.全局性方法
 *  3.系统枚举类型
 *  4.委托定义
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFrame
{
    #region 系统枚举类型
    /// <summary>
    /// UI窗体（位置）类型
    /// </summary>
    public enum UIFormType
    {
        Normal, //普通
        Fixed,  //固定
        PopUp   //弹出
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        Normal,         //普通
        ReverseChange,  //反向切换
        HideOther       //隐藏所有
    }

    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormLucenyType
    {
        Lucency,         //完全透明，不能穿透
        Translucency,   //半透明，不能穿透
        ImPenetrable,   //低透明度，不能穿透
        Pentrate        //可以穿透
    }

    #endregion
    public class SysDefine : MonoBehaviour
    {
        /* */
        public const string SYS_PATH_CANVAS = "Canvas";

        /* */
        public const string SYS_TAG_CANVAS = "_TagCanvas";

        /* */

    }

}

