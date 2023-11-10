using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            _currentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State newState)
        {
            if(_currentState != null)
                _currentState.Exit();
            //currentState?.Exit();
            _currentState = newState;
            newState?.Enter();
        }
    }
}

