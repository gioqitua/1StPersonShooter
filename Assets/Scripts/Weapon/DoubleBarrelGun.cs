using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelGun : Weapon
{
    [SerializeField] GameObject shellPrefab;
    [SerializeField] Transform shellStartPos;
    private void Start()
    {
        canShoot = true;
    }
    public override void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }
    IEnumerator ShootCoroutine()
    {
        canShoot = false;
        SoundManager.Instance.PlayGunShootSound();
        bulletCount--;
        shootLight.gameObject.SetActive(true);
        var shell = Instantiate(shellPrefab, shellStartPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        shootLight.gameObject.SetActive(false);
        yield return new WaitForSeconds(ShootDelay - 0.1f);
        canShoot = true;
    }
}
