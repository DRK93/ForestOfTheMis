using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Combat
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;
        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;
        // Start is called before the first frame update
        void Start()
        {
            // expensive method
            _colliders = GetComponentsInChildren<Collider>(true);
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true);
            ToggleRagdoll(false);
        }

        public void ToggleRagdoll(bool isRagdoll)
        {
            foreach(Collider collider in _colliders)
            {
                if(collider.gameObject.CompareTag("Ragdoll"))
                {
                    collider.enabled = isRagdoll;
                }
            }

            foreach(Rigidbody rigidbody in _rigidbodies)
            {
                if (rigidbody.gameObject.CompareTag("Ragdoll"))
                {
                    rigidbody.isKinematic = !isRagdoll;
                    rigidbody.useGravity = isRagdoll;
                }
            }

            controller.enabled = !isRagdoll;
            animator.enabled = !isRagdoll;
        }
    }
}

