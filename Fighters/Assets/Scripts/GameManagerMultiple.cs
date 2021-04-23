using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerMultiple : MonoBehaviour
{

    public static GameManagerMultiple instance;
    /*
    public int maxPlayers;
    public List<PlayerController> activePlayers = new List<PlayerController>();

    public GameObject playerSpawnEffect;
    */
    public List<GameObject> p1Players = new List<GameObject>();
    public List<GameObject> p2Players = new List<GameObject>();
    private int p1Next = 0;
    private int p2Next = 0;

    public GameObject p1CurrentPlayer;
    public GameObject p2CurrentPlayer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        P1FirstPlayer();
        P2FirstPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        P1RemoveFromList();
        P2RemoveFromList();
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            P1ChangePlayer();
        }

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            P2ChangePlayer();
        }
    }
    /*
    public void AddPlayer(PlayerController newPlayer)
    {
        if(activePlayers.Count < maxPlayers)
        {
            activePlayers.Add(newPlayer);
            Instantiate(playerSpawnEffect, newPlayer.transform.position, newPlayer.transform.rotation);
        } else
        {
            Destroy(newPlayer.gameObject);
        }
       
    }
    */

    void P1FirstPlayer()
    {
        for (int i = 1; i < p1Players.Count; i++)
        {
            p1Players[i].GetComponent<PlayerControllerMultiple>().selected = false;
        }

        p1CurrentPlayer = p1Players[0];
        p1CurrentPlayer.GetComponent<PlayerControllerMultiple>().selected = true;
    }
    public void P1ChangePlayer()
    {

        p1CurrentPlayer.GetComponent<PlayerControllerMultiple>().selected = false;
        p1Next++;
        if (p1Next >= p1Players.Count)
        {
            p1Next = 0;
        }
        p1CurrentPlayer = p1Players[p1Next];
        p1CurrentPlayer.GetComponent<PlayerControllerMultiple>().selected = true;

    }
    void P1RemoveFromList()
    {
        if (p1Players.Count > 0)
        {
            for (int i = 0; i < p1Players.Count; i++)
            {
                if (p1Players[i] == null)
                {
                    p1Players.RemoveAt(i);

                    i--;
                }
            }

        }
        /* Win condition
        if (players.Count == 0)
        {
            Wingame
        }
        */
    }

    void P2FirstPlayer()
    {
        for (int i = 1; i < p2Players.Count; i++)
        {
            p2Players[i].GetComponent<PlayerControllerMultiple>().selected = false;
        }

        p2CurrentPlayer = p2Players[0];
        p2CurrentPlayer.GetComponent<PlayerControllerMultiple>().selected = true;
    }

    public void P2ChangePlayer()
    {
        p2CurrentPlayer.GetComponent<PlayerControllerMultiple>().selected = false;
        p2Next++;
        if (p2Next >= p2Players.Count)
        {
            p2Next = 0;
        }
        p2CurrentPlayer = p2Players[p2Next];
        p2CurrentPlayer.GetComponent<PlayerControllerMultiple>().selected = true;
    }
    void P2RemoveFromList()
    {
        if (p2Players.Count > 0)
        {
            for (int i = 0; i < p2Players.Count; i++)
            {
                if (p2Players[i] == null)
                {
                    p2Players.RemoveAt(i);

                    i--;
                }
            }

        }
        /* Win condition
        if (players.Count == 0)
        {
            Wingame
        }
        */
    }

}
