using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Targeting
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnDestroyed;
        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}

