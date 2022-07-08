using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public GameObject GunPrefab;
    public GameObject bulletHolePrefab;
    public GameObject bulletImpactFX;
    public float ShootDelay;
    public float aimSpeed;
    public int damagePerBullet;
    public float bloom;
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float kickback;
    public float reloadSpeed;
    public float shootDistance;
    public int stashSize; //180
    public int clipSize; //30
    public int currentAmountInStash { get; private set; }
    public int currentAmountInClip { get; private set; }
    //recoil settings
    public float recoilReturnSpeed;
    public float recoilSnappiness;

    public void Initialize()
    {
        currentAmountInClip = clipSize;
        currentAmountInStash = stashSize;
    }
    public void GetBullet()
    {
        if (currentAmountInClip > 0)
        {
            currentAmountInClip--;            
        }
        
    }

    public void Reload()
    {
        if (currentAmountInStash > 0)
        {
            currentAmountInStash -= clipSize - currentAmountInClip;
            currentAmountInClip = clipSize;
        }
    }
}
