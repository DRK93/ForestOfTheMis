using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.ShowingObjects
{
    public class GrassEraserShow : MonoBehaviour
    {
        public GameObject grassEraser;

        private ObstructionEraser _obstructionEraser;
        // Start is called before the first frame update
        void Start()
        {
            _obstructionEraser = grassEraser.GetComponent<ObstructionEraser>();
            _obstructionEraser.enabled = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            _obstructionEraser.enabled = true;
        }

        public void OnTriggerExit(Collider other)
        {
            _obstructionEraser.enabled = false;
        }
    }
}

