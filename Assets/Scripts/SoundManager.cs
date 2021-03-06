using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gunSound;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void PlayGunShootSound()
    {
        Debug.Log("shootSound");
        audioSource.PlayOneShot(gunSound);
    }
}
