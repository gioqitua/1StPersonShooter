using Photon.Pun;
using UnityEngine;

public class Sway : MonoBehaviourPunCallbacks
{
    public float intensity;
    public float smooth;
    private Quaternion origin_rotation;

    private void Start()
    {
        if (!photonView.IsMine) return;
        origin_rotation = transform.localRotation;
    }

    public void UpdateSway(Vector2 inputs)
    {
        //controls
        float t_x_mouse = inputs.x;
        float t_y_mouse = inputs.y;

        //calculate target rotation
        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * t_x_adj * t_y_adj;

        //rotate towards target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
    }
}