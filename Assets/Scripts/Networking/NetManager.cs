using UnityEngine;
using Photon.Pun;

public class NetManager : MonoBehaviour
{
    public static NetManager Instance;
    [SerializeField] string Player;
    [SerializeField] Transform[] spawnPoints;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        var randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate("Player", spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
