using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.EnemyStates
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Ragdoll.ToggleRagdoll(true);
            //StateMachine.Weapon.gameObject.SetActive(false);
            StateMachine.WeaponHandlerer.DisableMainWeapon();
            StateMachine.WeaponHandlerer.SecondWeaponDisable();
            GameObject.Destroy(StateMachine.TargetComponent);
        }
        public override void Tick(float deltaTime)
        {

        }
        public override void Exit()
        {

        }
    }
}

