using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Controls : MonoBehaviour
{
    PlayerInput playerInput;
    public PlayerControls playerControls;
    List<Command> playerCommands;
    InputActionMap actionMap;
    Dictionary<string, Command> commandNames;
    string[] deviceNames;
    
    public void Awake()
    {   
        playerCommands = new List<Command>();

        playerControls = new PlayerControls(); //Must Generate a C# Class from controls

        playerInput = GetComponent<PlayerInput>();

        string deviceName = playerInput.devices[0].name;

        if(deviceName.Contains("Mouse") || deviceName.Contains("Keyboard"))
            {
                if(deviceName.Contains("Mouse"))
                {
                    InputUser.PerformPairingWithDevice(Keyboard.current, playerInput.user);
                }
                if(deviceName.Contains("Keyboard"))
                {
                    InputUser.PerformPairingWithDevice(Mouse.current, playerInput.user);
                }
//                playerInput.user.AssociateActionsWithUser((InputActionMap)playerControls.asset.actionMaps[0]);
//                var scheme = InputControlScheme.FindControlSchemeForDevice(playerInput.user.pairedDevices[1], playerInput.user.actions.controlSchemes);

////                print(scheme.ToString());
//                playerInput.user.ActivateControlScheme(scheme.ToString());
            }

        //Add all device names to "list"
        deviceNames = new string[playerInput.user.pairedDevices.Count];
        for(int i = 0; i < deviceNames.Length; i++)
        {
            deviceNames[i] = playerInput.user.pairedDevices[i].name;
        }

        actionMap = (InputActionMap)playerControls.asset.actionMaps[0];
        commandNames = new Dictionary<string, Command>();
        for (int i = 0; i < actionMap.actions.Count; i++)
        {
            Command newCommand = new Command();
            newCommand.AddInput(actionMap.actions[i], deviceNames);
            commandNames.Add(actionMap.actions[i].name, newCommand);
            playerCommands.Add(newCommand);
//           print(actionMap.actions[i].name);
        }
    }

    //public Command GetCommand(string commandName)
    //{
    //    Command cmd;
    //    if(commandNames.TryGetValue(commandName, out cmd) == false)
    //    {
    //        Debug.Log(commandName + " does not exist! Check the spelling.");
    //        return null;
    //    }
    //    return cmd;

    //}

    public Vector2 GetAxes(string commandName)
    {
        Command cmd;
        if (commandNames.TryGetValue(commandName, out cmd) == false)
        {
//            Debug.Log(commandName + " does not exist! Check the spelling.");
            return Vector2.zero;
        }
        return cmd.GetAxes();
    }
    public bool GetButtonDown(string commandName)
    {
        Command cmd;
        if (commandNames.TryGetValue(commandName, out cmd) == false)
        {
            Debug.Log(commandName + " does not exist! Check the spelling.");
            return false;
        }
        return cmd.isPressedThisFrame;
    }

    public bool GetButton(string commandName)
    {
        Command cmd;
        if (commandNames.TryGetValue(commandName, out cmd) == false)
        {
            Debug.Log(commandName + " does not exist! Check the spelling.");
            return false;
        }
        return cmd.isHeldDown;

    }

    public bool GetButtonUp(string commandName)
    {
        Command cmd;
        if (commandNames.TryGetValue(commandName, out cmd) == false)
        {
            Debug.Log(commandName + " does not exist! Check the spelling.");
            return false;
        }
        return cmd.isReleasedThisFrame;
    }
    void OnEnable()
    {  
        actionMap.Enable();  
    }

    void LateUpdate()
    {
        foreach(Command command in playerCommands)
        {
            command.isPressedThisFrame = false;
            command.isReleasedThisFrame = false;
        }
    }

 
}