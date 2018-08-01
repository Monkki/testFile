using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    Dictionary<string, string> _dicTest = new Dictionary<string, string>();

    private void Start()
    {
        string strResult = string.Empty;

        _dicTest.Add("zhangsan", "三");
        _dicTest.Add("lisi","四");

        _dicTest.TryGetValue("zhangsan", out strResult);
        print("查询结果 strRes = " + strResult);
    }
}
