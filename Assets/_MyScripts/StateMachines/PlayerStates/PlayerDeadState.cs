using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines.PlayerStates
{
    public class PlayerDeadState : PlayerBaseState
    {
        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.WeaponChangeAllowed = false;
            StateMachine.Ragdoll.ToggleRagdoll(true);
            //StateMachine.WeaponStats.gameObject.SetActive(false);
        }
        public override void Tick(float deltaTime)
        {

        }
        public override void Exit()
        {

        }


    }
}

