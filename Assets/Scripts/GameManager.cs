using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
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
        if (!photonView.IsMine) return;
        Debug.Log("playerHealth--");
    }
}
