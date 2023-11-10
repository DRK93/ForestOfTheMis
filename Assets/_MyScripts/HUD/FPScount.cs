using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace _MyScripts.HUD
{
    public class FPScount : MonoBehaviour
    {
        public TMP_Text fpsText;
        private float _deltaTime;
        void Update () 
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            fpsText.text = Mathf.Ceil (fps).ToString ();
        }
    }
}

