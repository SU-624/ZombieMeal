using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampEffect : MonoBehaviour
{
    public List<GameObject> image;
    public bool isComplete;

    CameraShake cameraShake;

    private void Start()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isComplete)
            {
                image[0].SetActive(true);
                cameraShake.VibrateForTime(0.01f);
            }
            else
            {
                image[1].SetActive(true);
                cameraShake.VibrateForTime(0.01f);
            }
        }
    }
}
