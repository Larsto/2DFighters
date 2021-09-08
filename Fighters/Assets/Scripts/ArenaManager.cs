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

    public GameObject[] pickUps;
    public float timeBetweenPowerUps;
    private float powerUpCounter;


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

        powerUpCounter = timeBetweenPowerUps * Random.Range(.75f, 1.25f);
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

        if(powerUpCounter > 0)
        {
            powerUpCounter -= Time.deltaTime;

            if(powerUpCounter <= 0)
            {
                int randomPoint = Random.Range(0, spawnPoints.Count);
                Instantiate(pickUps[Random.Range(0, pickUps.Length)], spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

                powerUpCounter = timeBetweenPowerUps *Random.Range(.75f, 1.25f);
            }
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
