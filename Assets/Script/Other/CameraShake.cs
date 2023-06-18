using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //카메라 쉐이킹

    public float shakeAmount;
    float shakeTime;
    Vector3 initalPosition;

    // Start is called before the first frame update
    void Start()
    {
        initalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + initalPosition;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0;
            transform.position = initalPosition;
        }
    }

    public void VibrateForTime(float time)
    {
        shakeTime = time;
    }
}
