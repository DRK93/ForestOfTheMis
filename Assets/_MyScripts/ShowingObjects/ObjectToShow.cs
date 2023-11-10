using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.ShowingObjects
{
    public class ObjectToShow : MonoBehaviour
{
    [Header("Special use for not LOD objects")]
    [SerializeField] private List<GameObject> additionalObject;
    private void Start()
    {
        if (TryGetComponent<LODGroup>(out LODGroup lodGroup))
        {
            for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
            {
                transform.GetChild(childIndex).gameObject.SetActive(false);
            }
        }
        else
        {
            if (transform.parent.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            {
                meshRenderer.enabled = false;
                if (transform.parent.TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
                    meshCollider.enabled = false;
            }
            

            if (additionalObject.Count != 0)
            {
                foreach (var objectToShow in additionalObject)
                {
                    objectToShow.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent<LODGroup>(out LODGroup lodGroup))
        {
            for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
            {
                transform.GetChild(childIndex).gameObject.SetActive(true);
            }
        }
        else
        {
            if (transform.parent.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            {
                meshRenderer.enabled = true;
                if (transform.parent.TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
                    meshCollider.enabled = true;
            }
            if (additionalObject.Count != 0)
            {
                foreach (var objectToShow in additionalObject)
                {
                    objectToShow.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TryGetComponent<LODGroup>(out LODGroup lodGroup))
        {
            for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
            {
                transform.GetChild(childIndex).gameObject.SetActive(false);
            }
        }
        else
        {
            if (transform.parent.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            {
                meshRenderer.enabled = false;
                if (transform.parent.TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
                    meshCollider.enabled = false;
            }
            if (additionalObject.Count != 0)
            {
                foreach (var objectToShow in additionalObject)
                {
                    objectToShow.SetActive(false);
                }
            }
        }
    }
}
}

