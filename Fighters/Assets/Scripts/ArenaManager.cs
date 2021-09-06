using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    private bool roundOver;

    public TMP_Text playerWinText;
    public GameObject winBar, roundCompleteText;


    // Start is called before the first frame update
    void Start()
    {
        foreach(PlayerController player in GameManager.instance.activePlayers)
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            player.transform.position = spawnPoints[randomPoint].position;

            if(GameManager.instance.activePlayers.Count <= spawnPoints.Count)
            {
                spawnPoints.RemoveAt(randomPoint);
            }       
        } 

        GameManager.instance.canFight = true;
        GameManager.instance.ActivatePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CheckActivePlayers() == 1 && !roundOver)
        {
            roundOver = true;

            //GameManager.instance.GoToNextArena();
            StartCoroutine(EndRoundCo());
        }
    }

    IEnumerator EndRoundCo()
    {
        winBar.SetActive(true);
        roundCompleteText.SetActive(true);
        playerWinText.gameObject.SetActive(true);
        playerWinText.text = "Player " + (GameManager.instance.lastPlayerNumber + 1) + " wins!";

        GameManager.instance.AddRoundWin();

        yield return new WaitForSeconds(2f);

        GameManager.instance.GoToNextArena();
    }
}
