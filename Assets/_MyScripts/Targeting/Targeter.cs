using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using _MyScripts.Combat;

namespace _MyScripts.Targeting
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup cinemaTargetGroup;
        [SerializeField] private CinemachineTargetGroup cinemaSecondTargetGroup;
        [SerializeField] private List<Target> targets = new List<Target>();
        [SerializeField] private Damageable _damageable;
        private Camera _mainCamera;
        private int _targetCameraNumber;
        public Target CurrentTarget {get; private set;}
        public Action MainTarget;
        public Action NoTarget;

        private void Start()
        {
            _mainCamera = Camera.main;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Target>(out Target target))
            {
                if (targets.Count != 0)
                {
                    targets.Add(target);
                    target.GetComponent<EnemyHighlight>().AddedToTargetList(this);
                    MainTarget?.Invoke();
                    target.OnDestroyed += RemoveTarget;
                }
                else
                {
                    targets.Add(target);
                    target.GetComponent<EnemyHighlight>().AddedToTargetList(this);
                    CurrentTarget = target;
                    MainTarget?.Invoke();
                    target.OnDestroyed += RemoveTarget;
                    _damageable.InBattle?.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent<Target>(out Target target)) 
            {
                RemoveTarget(target);
            }
        }

        // this works with target being on the screen, and the middle of the screen is focus point for distance calculation
        // I need secondway to find by distance, so while fighting in close combat there will be easy change to next target
        // to survive ganking from rear etc.
        public void SelectTargetByDistance()
        {
            if (targets.Count == 0) {return;}

            Target temporaryTarget = null;
            float closestTargetDistance = Mathf.Infinity;
            foreach(Target target in targets)
            {
                if (CurrentTarget == target)
                {

                }
                else
                {
                    Vector3 targetPosition = target.transform.position;
                    float distance = Vector3.Distance(targetPosition, transform.position);
                    if (distance < closestTargetDistance)
                    {
                        temporaryTarget = target;
                    }
                }
            }
            //cinemaTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = temporaryTarget;
            MainTarget?.Invoke();
            //cinemaSecondTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        }
        public bool SelectTarget()
        {
            if (targets.Count == 0) {return false;}
            Target closestTarget = null;
            float closestTargetDistance = Mathf.Infinity;
            Debug.Log("Targeting");
            foreach (Target target in targets)
            {
                Vector2 viewPos = _mainCamera.WorldToViewportPoint(target.transform.position);
                //if(viewPos.x < 0f || viewPos.x > 1f || viewPos.y < 0f || viewPos.y > 1f)
                // renderers are in childrens of main component 
                if(!target.GetComponentInChildren<Renderer>().isVisible)
                {
                    continue;
                }
                if(CurrentTarget == target) 
                {
                        //Debug.Log("currentTarget");
                }
                else
                {
                        Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
                        if (toCenter.sqrMagnitude < closestTargetDistance)
                        {   
                            //Debug.Log("other target");
                            closestTarget = target;
                            closestTargetDistance = toCenter.sqrMagnitude;
                        }
                }
            }

            if(closestTarget == null) 
            {return false;}
            
            CurrentTarget = closestTarget;
            MainTarget?.Invoke();
            //CurrentTarget.GetComponent<EnemyHighlight>().
            //cinemaTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
            return true;
        }

        public bool IsOnlyOneTarget()
        {
            if (targets.Count > 1)
                return false;
            else
            {
                return !IsTargetListEmpty();
            }
        }
        public bool IsTargetListEmpty()
        {
            if (targets.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void NextTarget()
        {
            if (targets.Count > 1)
            {
                //Debug.Log("NextTarget");
                {
                    SelectTargetByDistance();
                }
            }
            else if (targets.Count == 1)
            {
                CurrentTarget = targets[0];
                MainTarget?.Invoke();

            }
            else
            {
                Debug.Log("No other target");

            }
            //cinemaTargetGroup.RemoveMember(CurrentTarget.transform);
            //CurrentTarget = null;
            //SelectTarget();
            
            // changing target while fighting, for example to block in coming attack from rear
        }
        public void Cancel()
        {
            if (CurrentTarget == null) {return;}
            //cinemaTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
            NoTarget?.Invoke();
        }

        private void RemoveTarget(Target target)
        {
            if ( CurrentTarget == target)
            {
                //cinemaTargetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
                target.OnDestroyed -= RemoveTarget;
                target.GetComponent<EnemyHighlight>().RemoveFromTargetList();
                targets.Remove(target);
                
                if (targets.Count != 0)
                {
                    NextTarget();
                }
                else
                {
                    _damageable.OutOfBattle?.Invoke();
                    NoTarget?.Invoke();
                }
            }
            else
            {
                target.OnDestroyed -= RemoveTarget;
                target.GetComponent<EnemyHighlight>().RemoveFromTargetList();
                targets.Remove(target);
            }

        }
    }
}

