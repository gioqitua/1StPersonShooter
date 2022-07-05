using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IShootable
{
    public int bulletCount;
    public float ShootDelay;
    public Transform bulletStartPos;
    public float damagePerBullet;
    public bool canShoot;
    [SerializeField] internal GameObject shootLight;
    public abstract void Shoot();
}
public interface IShootable
{
    public void Shoot();
}
