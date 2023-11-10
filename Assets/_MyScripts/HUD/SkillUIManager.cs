using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace _MyScripts.HUD
{
    public class SkillUIManager : MonoBehaviour
    {
        [SerializeField] public RectTransform attackArrow;
        [SerializeField] public Image arrowImage;
        private float _baseArrowPosX;
        private float _arrowSizeX;

    
        // Start is called before the first frame update
        void Start()
        {
            _baseArrowPosX = attackArrow.localPosition.x;
            _arrowSizeX = attackArrow.rect.width;
   
        }
    
        public void MoveAttackArrow(float timer, int index)
        {
            float xDir = _baseArrowPosX + (index + timer) * _arrowSizeX;
            Vector3 nextPos = new Vector3(_baseArrowPosX + timer * 65f + index * 80f,  attackArrow.transform.position.y, attackArrow.transform.position.z);
            LeanTween.move(attackArrow, nextPos, Time.deltaTime);

        }

        public void LightAttackArrowOn()
        {
            arrowImage.color = Color.yellow;
        }
        public void LightAttackArrowRed()
        {
            arrowImage.color = Color.red;
        }
        public void LightAttackArrowOff()
        {
            arrowImage.color = Color.white;
        }
    }
}

