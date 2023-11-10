using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _MyScripts.Player
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue
    { get; private set;}
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action DrawWeaponEvent;
    public event Action TargetEvent;
    public event Action Special;
    public event Action ToggleWalkRun;

    public bool IsAttacking {get; private set;}
    public bool IsBlocking {get; private set;}
    public int SpecialNumber { get; private set; }
    private Controls _controls;
    private bool _mouseOnUI;
    private bool _noMove;
    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            _mouseOnUI = true;
        }
        else
        {
            _mouseOnUI = false;
        }
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        if(!_noMove) 
            JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        
        if(!_noMove)
            DodgeEvent?.Invoke();
    }

    public void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        
        if (!_noMove)
        {
            DrawWeaponEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_noMove)
        {
            MovementValue = context.ReadValue<Vector2>();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        
        if(!_noMove)
            TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_mouseOnUI)
        {
            if(context.canceled)
                IsAttacking = false;
        }
        else
        {
            if (!_noMove)
            {
                if(context.performed)
                    IsAttacking = true;

                else if(context.canceled)
                    IsAttacking = false;
            }
        }

    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (!_noMove)
        {
            if(context.performed)
                IsBlocking = true;

            else if(context.canceled)
                IsBlocking = false;
        }
    }

    public void OnStartPresentation(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        //AnimationPresentation?.Invoke();
    }

    public void OnSpecial1(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}

        if (!_noMove)
        {
            SpecialNumber = 1;
            Special?.Invoke();
        }
    }

    public void OnSpecial2(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        if (!_noMove)
        {
            SpecialNumber = 2;
            Special?.Invoke();
        }

    }

    public void OnSpecial3(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        if (!_noMove)
        {
            SpecialNumber = 3;
            Special?.Invoke();
        }
    }

    public void OnSpecial4(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        if (!_noMove)
        {

            SpecialNumber = 4;
            Special?.Invoke();
        }
    }

    public void OnSpecial5(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        if (!_noMove)
        {
            SpecialNumber = 5;
            Special?.Invoke();
        }
    }

    public void OnWalkOrRun(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        
        if(!_noMove)
            ToggleWalkRun?.Invoke();
    }

    public void StopPlayerMovement()
    {
        MovementValue = Vector2.zero;
        _noMove = true;
    }

    public void LetPlayerMove()
    {
        _noMove = false;
        // event for reset animatorPosition
        //ResetPlayerMovement?.Invoke();
    }
}
}

