using Photon.Pun;
using UnityEngine; 

public class PlayerHealth : MonoBehaviourPunCallbacks, IDamagable
{
    const float maxHealth = 100;
    [SerializeField] float currentHealth;
    private void Start()
    {
        if (!photonView.IsMine) return;
        currentHealth = maxHealth;
        UIManager.Instance.SetHealthSlider(currentHealth / maxHealth);
    }
    public void TakeDamage(int value)
    {
        photonView.RPC("RpcTakeDmg", RpcTarget.All, value);
    }
    [PunRPC]
    void RpcTakeDmg(int value)
    {
        if (!photonView.IsMine) return;
        currentHealth -= value;
        UIManager.Instance.SetHealthSlider(currentHealth / maxHealth);
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            NetManager.Instance.SpawnPlayer();
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

}
