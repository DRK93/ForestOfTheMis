using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.ShowingObjects
{
    public class ObjectsDetector : MonoBehaviour
    {
    
        private void OnTriggerEnter()
        {
            ShowObject();
        }
        private void OnTriggerExit()
        {
            HideObject();
        }

        public void ShowObject()
        {
        
        }

        public void HideObject()
        {

        }
    }
}

