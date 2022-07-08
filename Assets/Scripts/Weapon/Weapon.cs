// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Weapon : MonoBehaviour
// {
//     public int bulletCount;
//     public float ShootDelay;
//     public float aimSpeed;
//     public Transform bulletStartPos;
//     public float damagePerBullet;
//     public bool canShoot;
//     float shootDistance = 2000f;
//     [SerializeField] internal GameObject shootLight;
//     [SerializeField] GameObject shellPrefab;
//     [SerializeField] Transform shellStartPos;
//     [SerializeField] float spread;
//     float lightDuration = 0.1f;
//     Camera cam;
//     [SerializeField] LayerMask enemyLayermask;
//     private void Start()
//     {
//         canShoot = true;
//         cam = Camera.main;
//     }
//     public void Shoot()
//     {
//         StartCoroutine(ShootCoroutine());
//     }
//     IEnumerator ShootCoroutine()
//     {
//         canShoot = false;

//         Ray ray = new Ray(cam.transform.position, cam.transform.forward);

//         RaycastHit hitInfo;
//         if (Physics.Raycast(ray, out hitInfo, shootDistance, enemyLayermask))
//         {
//             var enemy = hitInfo.collider.GetComponent<IDamagable>();

//             if (enemy != null)
//             {
//                 enemy.TakeDamage(damagePerBullet, hitInfo.point);
//             }
//         }

//         SoundManager.Instance.PlayGunShootSound();
//         bulletCount--;
//         shootLight.gameObject.SetActive(true);
//         var shell = Instantiate(shellPrefab, shellStartPos.position, Quaternion.identity);
//         yield return new WaitForSeconds(lightDuration);
//         shootLight.gameObject.SetActive(false);
//         yield return new WaitForSeconds(ShootDelay - lightDuration);
//         canShoot = true;
//     }
// }
