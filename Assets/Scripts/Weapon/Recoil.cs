using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation;
    Vector3 targetRotation;
    float returnSpeed;
    float snappiness;

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);

    }
    public void RecoilFire(float recX, float recY, float recZ)
    {
        targetRotation += new Vector3(recX, recY, recZ);
    }
    public void SetReturnSpeed(float value)
    {
        returnSpeed = value;
    }
    public void Setsnapiness(float value)
    {
        snappiness = value;
    }
}
