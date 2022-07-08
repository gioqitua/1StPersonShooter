using System.Collections;
using Photon.Pun;
using UnityEngine;

public class WeaponSystem : MonoBehaviourPunCallbacks
{
    public Gun[] allWeapons;
    public Transform weaponParent;
    Vector3 weaponParentOrigin;
    [SerializeField] GameObject currentWeapon;
    private Vector3 targetWeaponBobPosition;
    private Vector3 weaponParentCurrentPos;
    private float movementCounter;
    private float idleCounter;
    Sway sway;
    [SerializeField] Recoil recoil;
    [SerializeField] Transform anchor;
    [SerializeField] Transform Ads;
    [SerializeField] Transform Hip;
    int index;
    public bool isAiming;
    private bool canShoot;
    public Camera cam;
    [SerializeField] LayerMask shootableLayer;
    float shootTimer;
    InputReader inputReader;
    private bool reloading;

    private void Awake()
    {
        if (!photonView.IsMine) return;
        weaponParentOrigin = weaponParent.localPosition;
        weaponParentCurrentPos = weaponParentOrigin;
    }
    private void Start()
    {
        if (!photonView.IsMine) return;
        
        recoil = GetComponentInChildren<Recoil>();

        photonView.RPC("Equip", RpcTarget.All, 0);

        canShoot = true;

        UIManager.Instance.SetAmmoText(allWeapons[index].currentAmountInClip, allWeapons[index].currentAmountInStash);

        //input Events
        inputReader.ReloadPressed += Reload;
        inputReader.SwitchWeaponPressed += SwitchWeapon;
    }
    void SwitchWeapon()
    {
        photonView.RPC("Equip", RpcTarget.All, index == allWeapons.Length - 1 ? 0 : index + 1);
    }
    public void SetInpReader(InputReader reader)
    {
        inputReader = reader;
    }
    public void UpdateSway(Vector2 inputs)
    {
        sway?.UpdateSway(inputs);
    }
    [PunRPC]
    void Equip(int _index)
    {
        index = _index;
        if (currentWeapon != null) Destroy(currentWeapon);
        GameObject newWeapon = Instantiate(allWeapons[_index].GunPrefab, weaponParent.position,
        weaponParent.rotation, weaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        currentWeapon = newWeapon;
        sway = newWeapon.GetComponent<Sway>();
        allWeapons[_index].Initialize();
        anchor = currentWeapon.transform.Find("Anchor");
        Ads = currentWeapon.transform.Find("States/ADS");
        Hip = currentWeapon.transform.Find("States/Hip");
        recoil.SetReturnSpeed(allWeapons[_index].recoilReturnSpeed);
        recoil.Setsnapiness(allWeapons[_index].recoilSnappiness);

    }
    private void Update()
    {
        if (!photonView.IsMine) return;
        Aim(isAiming);
        ApplyWeaponElacticity();
        idleCounter += Time.deltaTime;
        HeadBob(idleCounter, 0.005f, 0.005f);
        idleCounter += 0;
        weaponParent.localPosition = Vector3.MoveTowards(weaponParent.localPosition,
        targetWeaponBobPosition, Time.deltaTime * 2f * 0.2f);
    }
    private void ApplyWeaponElacticity()
    {
        currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 2f);
    }
    public void Aim(bool aiming)
    {
        if (aiming)
        {
            anchor.position = Vector3.Lerp(anchor.position, Ads.position, allWeapons[index].aimSpeed * Time.deltaTime);
        }
        else
        {
            anchor.position = Vector3.Lerp(anchor.position, Hip.position, allWeapons[index].aimSpeed * Time.deltaTime);
        }
    }
    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        float t_aim_adjust = 1f;
        if (isAiming) t_aim_adjust = 0.1f;
        targetWeaponBobPosition = weaponParentCurrentPos + new Vector3(Mathf.Cos(p_z) * p_x_intensity *
         t_aim_adjust, Mathf.Sin(p_z * 2) * p_y_intensity * t_aim_adjust, 0);
    }
    public void Shoot()
    {
        if (!canShoot) return;
        if (!photonView.IsMine) return;
        if (allWeapons[index].currentAmountInClip <= 0) return;

        photonView.RPC("ShootCoroutine", RpcTarget.All);

    }
    [PunRPC]
    IEnumerator ShootCoroutine()
    {
        var currentGUN = allWeapons[index];

        if (photonView.IsMine)
        {
            canShoot = false;
            currentGUN.GetBullet();
            UIManager.Instance.SetAmmoText(currentGUN.currentAmountInClip, currentGUN.currentAmountInStash);
        }

        //bloom

        Vector3 bloom = cam.transform.position + cam.transform.forward * currentGUN.shootDistance;
        bloom += Random.Range(-currentGUN.bloom, currentGUN.bloom) * cam.transform.up;
        bloom += Random.Range(-currentGUN.bloom, currentGUN.bloom) * cam.transform.right;
        bloom -= cam.transform.position;
        bloom.Normalize();

        //Raycast
        Ray ray = new Ray(cam.transform.position, bloom);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, currentGUN.shootDistance, shootableLayer))
        {
            var enemy = hitInfo.collider.GetComponent<IDamagable>();

            if (enemy != null)
            {
                if (photonView.IsMine)
                {
                    enemy.TakeDamage(currentGUN.damagePerBullet);
                }
            }

        }
        //gunFX

        var hole = Instantiate(currentGUN.bulletHolePrefab, hitInfo.point + hitInfo.normal * 0.002f, Quaternion.identity);
        hole.transform.LookAt(hitInfo.point + hitInfo.normal);
        Destroy(hole, 5f);

        var impactFX = Instantiate(currentGUN.bulletImpactFX, hitInfo.point, Quaternion.identity);

        SoundManager.Instance.PlayGunShootSound();

        //recoil
        var recY = UnityEngine.Random.Range(-currentGUN.recoilY, currentGUN.recoilY);
        var recZ = UnityEngine.Random.Range(-currentGUN.recoilZ, currentGUN.recoilZ);
        recoil.RecoilFire(currentGUN.recoilX, recY, recZ);

        //kickback
        currentWeapon.transform.position -= currentWeapon.transform.forward * currentGUN.kickback;



        yield return new WaitForSeconds(currentGUN.ShootDelay);

        if (photonView.IsMine)
        {
            canShoot = true;
        }
    }
    internal void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        reloading = true;
        yield return new WaitForSeconds(allWeapons[index].reloadSpeed);
        allWeapons[index].Reload();
        reloading = false;
        UIManager.Instance.SetAmmoText(allWeapons[index].currentAmountInClip, allWeapons[index].currentAmountInStash);

    }
}
