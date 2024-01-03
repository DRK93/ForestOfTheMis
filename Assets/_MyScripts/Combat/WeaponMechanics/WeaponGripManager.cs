using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Combat.WeaponMechanics
{
    public class WeaponGripManager : MonoBehaviour
    {
        [SerializeField] private Transform swordOneHandedTransformBase;
        [SerializeField] private Transform axeOneHandedTransformBase;
        [SerializeField] private Transform maceOneHandedTransformBase;
        [SerializeField] private Transform swordTwoHandedTransformBase;
        [SerializeField] private Transform axeTwoHandedTransformBase;
        [SerializeField] private Transform warhammerTransformBase;
        [SerializeField] private Transform spearTransformBase;
        [SerializeField] private Transform halberdTransformBase;
        
        // transforms wiht 0 are basic transforms for movement, after attacks it will just return to it
        [SerializeField] private Transform oneHandedMovementTransform;
        [SerializeField] private Transform oneHandedBlockTransform;
        [SerializeField] private Transform oneHandedTransform0;
        [SerializeField] private Transform oneHandedTransform1;
        [SerializeField] private Transform oneHandedTransform2;
        [SerializeField] private Transform twoHandedMovementTransform;
        [SerializeField] private Transform twoHandedBlockTransform;
        [SerializeField] private Transform twoHandedTransform0;
        [SerializeField] private Transform twoHandedTransform1;
        [SerializeField] private Transform twoHandedTransform2;
        [SerializeField] private Transform twoHandedTransform3;
        [SerializeField] private Transform twoHandedTransform4;
        [SerializeField] private Transform twoHandedTransform5;
        [SerializeField] private Transform spearMovementTransform;
        [SerializeField] private Transform spearBlockTransform;
        [SerializeField] private Transform spearTransform0;
        [SerializeField] private Transform spearTransform1;
        [SerializeField] private Transform spearTransform2;

        private void Start()
        {
            oneHandedTransform0.position = swordOneHandedTransformBase.position;
            oneHandedTransform0.rotation = swordOneHandedTransformBase.rotation;
            
            twoHandedTransform0.position = swordTwoHandedTransformBase.position;
            twoHandedTransform0.rotation = swordTwoHandedTransformBase.rotation;
            
            spearTransform0.position = spearTransform2.position;
            spearTransform0.rotation = spearTransform2.rotation;
        }

        public void ChangeGrip(int number)
        {
            switch (number)
            {
                case 11:
                    swordOneHandedTransformBase.position = oneHandedTransform1.position;
                    swordOneHandedTransformBase.rotation = oneHandedTransform1.rotation;
                    break;
                case 12:
                    swordOneHandedTransformBase.position = oneHandedTransform2.position;
                    swordOneHandedTransformBase.rotation = oneHandedTransform2.rotation;
                    break;
                case 18:
                    swordOneHandedTransformBase.position = oneHandedMovementTransform.position;
                    swordOneHandedTransformBase.rotation = oneHandedMovementTransform.rotation;
                    break;
                case 19:
                    swordOneHandedTransformBase.position = oneHandedBlockTransform.position;
                    swordOneHandedTransformBase.rotation = oneHandedBlockTransform.rotation;
                    break;
                case 21:
                    axeOneHandedTransformBase.position = oneHandedTransform1.position;
                    axeOneHandedTransformBase.rotation = oneHandedTransform1.rotation;
                    break;
                case 22:
                    axeOneHandedTransformBase.position = oneHandedTransform2.position;
                    axeOneHandedTransformBase.rotation = oneHandedTransform2.rotation;
                    break;
                case 28:
                    axeOneHandedTransformBase.position = oneHandedMovementTransform.position;
                    axeOneHandedTransformBase.rotation = oneHandedMovementTransform.rotation;
                    break;
                case 29:
                    axeOneHandedTransformBase.position = oneHandedBlockTransform.position;
                    axeOneHandedTransformBase.rotation = oneHandedBlockTransform.rotation;
                    break;
                case 31:
                    maceOneHandedTransformBase.position = oneHandedTransform1.position;
                    maceOneHandedTransformBase.rotation = oneHandedTransform1.rotation;
                    break;
                case 32:
                    maceOneHandedTransformBase.position = oneHandedTransform2.position;
                    maceOneHandedTransformBase.rotation = oneHandedTransform2.rotation;
                    break;
                case 38:
                    maceOneHandedTransformBase.position = oneHandedMovementTransform.position;
                    maceOneHandedTransformBase.rotation = oneHandedMovementTransform.rotation;
                    break;
                case 39:
                    maceOneHandedTransformBase.position = oneHandedBlockTransform.position;
                    maceOneHandedTransformBase.rotation = oneHandedBlockTransform.rotation;
                    break;
                // digit 4 will be for shield if needed
                
                case 51:
                    swordTwoHandedTransformBase.position = twoHandedTransform1.position;
                    swordTwoHandedTransformBase.rotation = twoHandedTransform1.rotation;
                    break;
                case 52:
                    swordTwoHandedTransformBase.position = twoHandedTransform2.position;
                    swordTwoHandedTransformBase.rotation = twoHandedTransform2.rotation;
                    break;
                case 53:
                    swordTwoHandedTransformBase.position = twoHandedTransform3.position;
                    swordTwoHandedTransformBase.rotation = twoHandedTransform3.rotation;
                    break;
                case 54:
                    swordTwoHandedTransformBase.position = twoHandedTransform4.position;
                    swordTwoHandedTransformBase.rotation = twoHandedTransform4.rotation;
                    break;
                case 55:
                    swordTwoHandedTransformBase.position = twoHandedTransform5.position;
                    swordTwoHandedTransformBase.rotation = twoHandedTransform5.rotation;
                    break;
                case 58:
                    swordTwoHandedTransformBase.position = twoHandedMovementTransform.position;
                    swordTwoHandedTransformBase.rotation = twoHandedMovementTransform.rotation;
                    break;
                case 59:
                    swordTwoHandedTransformBase.position = twoHandedBlockTransform.position;
                    swordTwoHandedTransformBase.rotation = twoHandedBlockTransform.rotation;
                    break;
                case 61:
                    axeTwoHandedTransformBase.position = twoHandedTransform1.position;
                    axeTwoHandedTransformBase.rotation = twoHandedTransform1.rotation;
                    break;
                case 62:
                    axeTwoHandedTransformBase.position = twoHandedTransform2.position;
                    axeTwoHandedTransformBase.rotation = twoHandedTransform2.rotation;
                    break;
                case 68:
                    axeTwoHandedTransformBase.position = twoHandedMovementTransform.position;
                    axeTwoHandedTransformBase.rotation = twoHandedMovementTransform.rotation;
                    break;
                case 69:
                    axeTwoHandedTransformBase.position = twoHandedBlockTransform.position;
                    axeTwoHandedTransformBase.rotation = twoHandedBlockTransform.rotation;
                    break;
                case 71:
                    warhammerTransformBase.position = twoHandedTransform1.position;
                    warhammerTransformBase.rotation = twoHandedTransform1.rotation;
                    break;
                case 72:
                    warhammerTransformBase.position = twoHandedTransform2.position;
                    warhammerTransformBase.rotation = twoHandedTransform2.rotation;
                    break;
                case 78:
                    warhammerTransformBase.position = twoHandedMovementTransform.position;
                    warhammerTransformBase.rotation = twoHandedMovementTransform.rotation;
                    break;
                case 79:
                    warhammerTransformBase.position = twoHandedBlockTransform.position;
                    warhammerTransformBase.rotation = twoHandedBlockTransform.rotation;
                    break;
                case 81:
                    spearTransformBase.position = spearTransform1.position;
                    spearTransformBase.rotation = spearTransform1.rotation;
                    break;
                case 82:
                    spearTransformBase.position = spearTransform2.position;
                    spearTransformBase.rotation = spearTransform2.rotation;
                    break;
                case 88:
                    spearTransformBase.position = spearMovementTransform.position;
                    spearTransformBase.rotation = spearMovementTransform.rotation;
                    break;
                case 89:
                    spearTransformBase.position = spearBlockTransform.position;
                    spearTransformBase.rotation = spearBlockTransform.rotation;
                    break;
                case 91:
                    halberdTransformBase.position = spearTransform1.position;
                    halberdTransformBase.rotation = spearTransform1.rotation;
                    break;
                case 92:
                    halberdTransformBase.position = spearTransform2.position;
                    halberdTransformBase.rotation = spearTransform2.rotation;
                    break;
                case 98:
                    halberdTransformBase.position = spearMovementTransform.position;
                    halberdTransformBase.rotation = spearMovementTransform.rotation;
                    break;
                case 99:
                    halberdTransformBase.position = spearBlockTransform.position;
                    halberdTransformBase.rotation = spearBlockTransform.rotation;
                    break;
                default:
                    break;
            }
        }

        public void BlockGrip(int number)
        {
            
        }
        public void ReturnToBaseGrip(int number)
        {
            switch (number)
            {
                case 1:
                    ChangeGrip(18);
                    break;
                case 2:
                    ChangeGrip(28);
                    break;
                case 3:
                    ChangeGrip(38);
                    break;
                case 5:
                    ChangeGrip(58);
                    break;
                case 6:
                    ChangeGrip(68);
                    break;
                case 7:
                    ChangeGrip(78);
                    break;
                case 8:
                    ChangeGrip(88);
                    break;
                case 9:
                    ChangeGrip(98);
                    break;
                default:
                    break;
            }
        }
    }
}

