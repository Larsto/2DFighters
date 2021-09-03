using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxPlayers;
    public List<PlayerController> activePlayers = new List<PlayerController>();

    public GameObject playerSpawnEffect;

    public bool canFight;

    public string[] allLevels;
    private List<string> levelOrder = new List<string>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            GoToNextArena();
        }

    }

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

    public void ActivatePlayers()
    {
        foreach(PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }
    }

    public int CheckActivePlayers()
    {
        int playerAliveCount = 0;

        for(int i = 0; i < activePlayers.Count; i++)
        {
            if (activePlayers[i].gameObject.activeInHierarchy)
            {
                playerAliveCount++;
            }
        }

        return playerAliveCount;
    }

    public void GoToNextArena()
    {
        //SceneManager.LoadScene(allLevels[Random.Range(0, allLevels.Length)]);

        if(levelOrder.Count == 0)
        {
            List<string> allLevelList = new List<string>();
            allLevelList.AddRange(allLevels);

            for(int i = 0; i < allLevels.Length; i++)
            {
                int selected = Random.Range(0, allLevelList.Count);

                levelOrder.Add(allLevelList[selected]);
                allLevelList.RemoveAt(selected);
            }
        }

        string levelToLoad = levelOrder[0];
        levelOrder.RemoveAt(0);

        foreach(PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
