using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _MyScripts.Combat
{
    public class ForceReceiver : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float drag = 0.3f;
        private Vector3 _impact;
        private Vector3 _dampingVelocity;
    
        private float _verticalVelocity;
        public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;
        private void Update()
        {
            if(_verticalVelocity < 0f && controller.isGrounded)
            {
                if (Time.timeScale < 0.02f)
                {
                    
                }
                else
                {
                    _verticalVelocity = Physics.gravity.y * Time.deltaTime /Time.timeScale;
                }
            }
            else
            {
                if (Time.timeScale < 0.02f)
                {
                    
                }
                else
                {
                    _verticalVelocity += Physics.gravity.y * Time.deltaTime /Time.timeScale;
                }
            }
            
            _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, drag ) ;
    
            if (agent != null)
            {
                if (_impact.sqrMagnitude < 0.2f * 0.2f)
                {
                    _impact = Vector3.zero;
                    agent.enabled = true;
                }   
            }
        }    
        
        public void AddForce(Vector3 force)
        {
            //Debug.Log("Force: " + force);
            force.y = 0f;
            _impact += force  * 1/Time.timeScale;
            if (agent != null)
            {
                if (Vector3.Distance(agent.transform.position, transform.position) > 0.2f)
                {
                    Debug.Log("NavMeshAgent gone wild");
                }
                //Debug.Log("NavAgent position " + (agent.transform.position - transform.position));
                agent.enabled = false;
            }
        }
    
        public void Jump(float jumpForce)
        {
            _verticalVelocity += jumpForce;
        }
    }
}

