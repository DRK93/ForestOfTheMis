using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _MyScripts.HUD
{
    public class CompassManager : MonoBehaviour
    {
        public GameObject compassArrow;
        [field: SerializeField] private GameObject playerChar;
        [field: SerializeField] private float arrowTick;
        private float _compassTimer;
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            _compassTimer += Time.deltaTime;
            if (_compassTimer > arrowTick)
            {
                _compassTimer = 0f;
                RotateCompassArrow();
            }
        }

        private void RotateCompassArrow()
        {
            compassArrow.LeanRotateZ(CalculateAngle(),0.5f);
        }

        private float CalculateAngle()
        {
            float angle = playerChar.transform.localEulerAngles.y - 90f ;
            return angle;
        }
        
    }
}

