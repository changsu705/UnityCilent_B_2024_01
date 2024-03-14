using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Name :" + gameData.gameName);
        Debug.Log("Game Score :" + gameData.gameScore);
        Debug.Log("is Game Active :" + gameData.isGameActive);
    }
}
