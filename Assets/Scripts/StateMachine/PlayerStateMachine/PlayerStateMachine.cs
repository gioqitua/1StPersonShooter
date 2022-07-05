using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public InputReader inputReader;
    public Animator anim;
    public CharacterController controller;
    public float moveSpeed;
    [SerializeField] float shootDistance = 2000f;
    float Xrotation = 0;
    [SerializeField] float Ysensitivity = 20f;
    [SerializeField] float Xsensitivity = 20f;
    Camera playerCam;
    public LayerMask enemyLayerMask;
    [SerializeField] Weapon activeWeapon; 

    private void Start()
    {
        playerCam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        SwitchState(new MoveState(this));
    }
    private void Update()
    {
        Look(inputReader.LookValue);
        Move(inputReader.MovementValue);
    }
    internal void Shoot()
    {
        if (!activeWeapon.canShoot) return;
        activeWeapon.Shoot();
        RaycastHandler();
    }
    private void RaycastHandler()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * shootDistance, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, shootDistance, enemyLayerMask))
        {
            var enemy = hitInfo.collider.GetComponent<IDamagable>();
            if (enemy != null)
            {
                enemy.TakeDamage(activeWeapon.damagePerBullet, hitInfo.point);

            }
        }
    }

    private void Move(Vector2 input)
    {
        Vector3 moveDir = new Vector3();
        moveDir.x = input.x;
        moveDir.y = 0;
        moveDir.z = input.y;
        controller.Move(transform.TransformDirection(moveDir * moveSpeed * Time.deltaTime));
    }

    private void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        Xrotation -= (mouseY * Time.deltaTime) * Ysensitivity;
        Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
        playerCam.transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * Xsensitivity);
    }

}