using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFrame;


namespace DemoProject
{
    public class StartProject : MonoBehaviour
    {
        private void Start()
        {
            UIManager.GetInstance().ShowUIForms("LogontPanel");
        }

    }
}

