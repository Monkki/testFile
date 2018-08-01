/***
 * UI窗体的父类
 *
 * 定义所有UI窗体的父类
 * 定义四个生命周期
 *  1.Display显示状态
 *  2.Hiding隐藏状态
 *  3.ReDisplay再显示状态
 *  4.Freeze冻结状态
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFrame
{
    public class BaseUIForm : MonoBehaviour
    {
        private UIType _CurrentUIType = new UIType();

        /*封装属性*/
        //当前UI窗体类型
        public UIType CurrentUIType
        {
            get { return _CurrentUIType; }
            set { _CurrentUIType = value; }
        }

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 再显示状态
        /// </summary>
        public virtual void Residplay()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }
    }

}

