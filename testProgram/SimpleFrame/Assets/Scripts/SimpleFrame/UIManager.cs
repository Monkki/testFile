/***
 * UI管理器
 *
 * UI框架的核心
 * 用户通过脚本实现框架多数的功能实现
 * 
 * 
 * 
 * 软件开发原则
 * 1、高内阻低耦合
 * 2、方法的单一职责
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFrame
{
    public class UIManager : MonoBehaviour
    {

        private static UIManager _Instance = null;
         
        //UI窗体预设路径(参数1：窗体预设名称，参数2：窗体预设路径)
        private Dictionary<string, string> _DicFormsPaths;
        //缓存所有UI窗体
        private Dictionary<string, BaseUIForm> _DicAllUIForms;
        //当前显示的UI窗体
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;

        //UI根节点
        private Transform _TraCanvasTrasfrom = null;
        //全屏幕显示的节点
        private Transform _TraNormal = null;
        //固定显示的节点
        private Transform _TraFixed = null;
        //弹出节点
        private Transform _TraPopUp = null;
        //UI管理脚本的节点
        private Transform _TraUIScripts = null;

        public static UIManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
            }

            return _Instance;
        }
        
        //初始化核心数据
        public void Awake()
        {
            //字段初始化
            _DicAllUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _DicFormsPaths = new Dictionary<string, string>();

            //初始化加载Canvas
            InitRootCanvasLoading();

            //得到UI根节点，全屏节点等
            _TraCanvasTrasfrom = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
            _TraNormal = _TraCanvasTrasfrom.Find("Normal");
            _TraFixed = _TraCanvasTrasfrom.Find("Fixed");
            _TraPopUp = _TraCanvasTrasfrom.Find("PopUp");
            _TraUIScripts = _TraCanvasTrasfrom.Find("_ScriptMgr");

            //把本脚本作为“根UI窗体”的子节点
            this.gameObject.transform.SetParent(_TraUIScripts, false);

            //“根UI窗体”在场景转换时候不销毁
            DontDestroyOnLoad(_TraCanvasTrasfrom);

            //初始化“UI窗体预设”路径数据
            if (_DicFormsPaths != null)
            {
                _DicFormsPaths.Add("LogontPanel",@"UIPrefabs\LogontPanel");
            }
        }

        /// <summary>
        /// 显示UI窗体
        /// </summary>
        /// 加载与判断指定的UI窗体的名称，加载到“_DicAllUIForm”缓存集合中
        /// 根据不同的UI窗体的显示模式，分别作不同的加载处理
        /// <param name="uiFormName"></param>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUiForm;                  //UI窗体基类

            //参数的检查
            if (string.IsNullOrEmpty(uiFormName)) return;

            //指定的UI窗体的名称，加载到“_DicAllUIForm”缓存集合中
            baseUiForm = LoadFormsToAllUIFormCatch(uiFormName);
            if (baseUiForm == null) return;

            // 根据不同的UI窗体的显示模式，分别作不同的加载处理
            switch (baseUiForm.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormShowMode.Normal:
                    LoadUIToCurrentCache(uiFormName);       //把当前窗体加载到“当前窗体”集合中
                    //todo..
                    break;
                case UIFormShowMode.ReverseChange:
                    //todo..
                    break;
                case UIFormShowMode.HideOther:
                    //todo...
                    break;
                default:
                    break;
            }
        }

        //初始化加载Canvas
        private void InitRootCanvasLoading()
        {
            //Resources.Load(SysDefine.SYS_PATH_CANVAS);
            ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
        }


        //指定的UI窗体的名称，加载到“_DicAllUIForm”缓存集合中      
        //检查集合中是否已经加载过，否则才加载
        private BaseUIForm LoadFormsToAllUIFormCatch(string uiFormsName)
        {
            BaseUIForm baseUIResult = null;

            _DicAllUIForms.TryGetValue(uiFormsName,out baseUIResult);
            if (baseUIResult == null)
            {
                //加载指定名称的UI窗体
                baseUIResult = LoadUIForm(uiFormsName);
            }

            return baseUIResult;
        }

        //加载指定名称的UI窗体
        //根据UI窗体名称加载prefabs
        //根据不同prefab加载到对应的'根窗体'的不同的节点
        //隐藏刚创建的UIprefab
        //将prefab加入到ui窗体的缓存集合中
        private BaseUIForm LoadUIForm(string uiFormName)
        {

            string strUIFormPaths = null;       //UI窗体路径
            GameObject goCloneUIPrefabs = null;     //创建的UIPrefab
            BaseUIForm baseUIForm = null;           //窗体基类


            //根据UI窗体名称获取加载路径
            _DicFormsPaths.TryGetValue(uiFormName,out strUIFormPaths);

            //根据UI窗体名称加载prefab
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths, false);
            }

            //设置UIprefab父节点
            if (_TraCanvasTrasfrom != null && goCloneUIPrefabs != null)
            {
                baseUIForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
                if (baseUIForm == null)
                {
                    Debug.Log("baseUIForm is null,确认窗体预设对象上是否加载了baseUIForm的子类脚本：参数：uiFormName = "+ uiFormName);
                    return null;
                }
                switch(baseUIForm.CurrentUIType.UIForms_Type)
                {
                    case UIFormType.Normal:
                        goCloneUIPrefabs.transform.SetParent(_TraNormal,false);
                        break;
                    case UIFormType.Fixed:
                        goCloneUIPrefabs.transform.SetParent(_TraFixed, false);
                        break;
                    case UIFormType.PopUp:
                        goCloneUIPrefabs.transform.SetParent(_TraPopUp,false);
                        break;
                    default:
                        break;
                }

                goCloneUIPrefabs.SetActive(false);

                //将prefab加入到ui窗体缓存集合中
                _DicAllUIForms.Add(uiFormName, baseUIForm);

                //return baseUIForm;
            }
            else
            {
                Debug.Log("_TraCanvasTrasform or goCloneUIPrefabs is null");
                return null;
            }

            //return null
            return baseUIForm;
        }


        /// <summary>
        /// 把当前窗体加载到“当前窗体集合中”
        /// </summary>
        /// <param name="uiFormName"></param>
        private void LoadUIToCurrentCache(string uiFormName)
        {
            BaseUIForm baseUIForm;                      //UI窗体基类
            BaseUIForm baseUIFormFromAllCache;          //从“所有窗体集合”中得到的窗体
            //如果“正在显示”的集合中，存在整个UI窗体，直接返回
            _DicCurrentShowUIForms.TryGetValue(uiFormName,out baseUIForm);
            if (baseUIForm != null) return;

            //把当前窗体，加载到“正在显示”集合中
            _DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();           //显示当前窗体
            }
        }
    }
}

