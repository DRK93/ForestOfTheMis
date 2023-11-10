using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.Targeting
{
    public class RayTargeter : MonoBehaviour
    {
        private List<ObjectOfInterest> _intrestingObjects = new List<ObjectOfInterest>();

        private List<NeutralCharacters> _neutralChars = new List<NeutralCharacters>();
        private LayerMask _objectLayers;
    
        private void Start()
        {
            _objectLayers =  LayerMask.GetMask("NeutralCharacter", "ObjectOfInterest");
        }

        private void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ObjectOfInterest>(out ObjectOfInterest obj))
            {
                _intrestingObjects.Add((obj));
            }
            else if (other.TryGetComponent<NeutralCharacters>(out NeutralCharacters neuChar))
            {
                _neutralChars.Add((neuChar));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ObjectOfInterest>(out ObjectOfInterest obj))
            {
                _intrestingObjects.Remove((obj));
            }
            else if (other.TryGetComponent<NeutralCharacters>(out NeutralCharacters neuChar))
            {
                _neutralChars.Remove((neuChar));
            }
        }

        private void CheckingObjectsForward()
        {
            Physics.SphereCast(transform.position, 1f,transform.forward, out RaycastHit hitInfo, 20f, _objectLayers);
        
        }
    }
}

