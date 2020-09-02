using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerList : MonoBehaviour
{

    public List<GameObject> Players {get; private set;}

    #region Singleton


	public static PlayerList instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<PlayerList> ();
			}
			return _instance;
		}
	}
	static PlayerList _instance;

	void Awake ()
	{
        Players = new List<GameObject>();
		_instance = this;
       
	}
    #endregion


    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += AddPlayerToList;
        PlayerInputManager.instance.onPlayerLeft += RemovePlayerFromList;
    }
    public void AddPlayerToList(PlayerInput playerInput)
    {  
       Players.Add(playerInput.gameObject);
       if(Players.Count > 1)
       {
           //SortPlayerLayers();
       }
       NamePlayers();
    }
    public void RemovePlayerFromList(PlayerInput playerInput)
    {  
//       Players.Remove(playerInput.gameObject);
       if(Players.Count > 1)
       {
           //SortPlayerLayers();
       }
       NamePlayers();
    }
    void SortPlayerLayers()
    {
        for(int i = 1; i < Players.Count; i++)
        {
            Players[i].layer = LayerMask.NameToLayer("Player" + (i + 1));
        }
    }
    void NamePlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].name = "Player " + (i + 1);
        }
    }
}
