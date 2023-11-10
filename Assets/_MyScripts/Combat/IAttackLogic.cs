using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAttackLogic
{
    void TryNextAttack();
    void TryApplyMoveForce();
    void ArmAttack();
    void DisarmAttack();
    void BlockedHit();
    void ParriedHit();
    void ReDoAttack();

}
