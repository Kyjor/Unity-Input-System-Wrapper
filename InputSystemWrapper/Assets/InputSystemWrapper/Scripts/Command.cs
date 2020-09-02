using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[System.Serializable]
public class Command 
{
    public Vector2 axes = new Vector2(0,0);
    public bool isPressedThisFrame;
    public bool isHeldDown;
    public bool isReleasedThisFrame;

    string[] deviceNames;
   
    public void AddInput(InputAction input, string[] devices)
    {
  
        deviceNames = devices;
        if(input.type == InputActionType.Button)
        {
//            Debug.Log("Button Added");
            input.started += Press;
            input.performed += HoldDown;
            input.canceled += Release; 
        }
        else if(input.type == InputActionType.Value || input.type == InputActionType.PassThrough)
        {
            input.performed += SetAxes;
          
        }
    }

    public void RemoveInput(InputAction input)
    {
        //Debug.Log("Input Removed");
        if(input.type == InputActionType.Button)
        {
        
            input.started -=  Press;
            input.performed -= HoldDown;
            input.canceled -= Release; 
        }
        else if(input.type == InputActionType.Value || input.type == InputActionType.PassThrough)
        {
            input.performed -= SetAxes;
        }
    }
    
    public void SetAxes(InputAction.CallbackContext context)
    {
        for(int i = 0; i < deviceNames.Length; i++)
        {
            if(context.control.device.name == deviceNames[i])
            {
                axes = context.ReadValue<Vector2>();
            }
        }
    }

    public Vector2 GetAxes()
    {
        return axes;
    }
    public void Press(InputAction.CallbackContext context) 
    {
        for(int i = 0; i < deviceNames.Length; i++)
        {
            if(context.control.device.name == deviceNames[i])
            {
                isPressedThisFrame = true;
                isHeldDown = true;
                isReleasedThisFrame = false;
                Execute();
            }
        }
    }
    public void HoldDown(InputAction.CallbackContext context) 
    {
        for(int i = 0; i < deviceNames.Length; i++)
        {
            if(context.control.device.name == deviceNames[i])
            {
                isHeldDown = true;
                isReleasedThisFrame = false;
            }
        }
    }
    public void Release(InputAction.CallbackContext context) 
    {
        for(int i = 0; i < deviceNames.Length; i++)
        {
            if(context.control.device.name == deviceNames[i])
            {
                isPressedThisFrame = false;
                isHeldDown = false;
                isReleasedThisFrame = true;
            }
        }
    }

    public virtual void Execute()
    {

    }

}
