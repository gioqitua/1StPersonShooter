using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
            Instance = this;
    }

    public void PlayerGetHit(float value)
    {
        Debug.Log("playerHealth--");
    }
}
