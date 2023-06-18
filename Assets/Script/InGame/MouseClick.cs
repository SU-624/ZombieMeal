using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    #region _원래 구현했던 raycast
    //Camera mainCamer = null;

    //private bool mouseState;

    //private GameObject target;
    //private Vector3 mousePos;

    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //RaycastHit hit;

    //private void Start()
    //{
    //    //mainCamer = Camera.main;
    //}

    //private void Update()
    //{
    //    // Ray를 그리는 부분
    //    mousePos = Input.mousePosition;
    //    mousePos.z = 100f;
    //    mousePos = mainCamer.ScreenToWorldPoint(mousePos);
    //    Debug.DrawRay(transform.position, hit.point - transform.position, Color.blue);

    //    //if (Input.GetMouseButtonDown(0))
    //    //{
    //    //    target = GetClickObject();

    //    //    if (target.Equals(gameObject))
    //    //    {
    //    //        mouseState = true;
    //    //    }
    //    //}

    //    //if (mouseState)
    //    //{
    //    //    GameManager.Instance.Start_Captrue();
    //    //}
    //    //else
    //    //{
    //    //}


    //}

    //private GameObject GetClickObject()
    //{
    //    GameObject target = null;

    //    Ray ray = mainCamer.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray.origin, ray.direction * 10 , out hit))
    //    {
    //        target = hit.collider.gameObject;
    //        Debug.Log(hit.transform.name);
    //    }

    //    return target;
    //}
    #endregion

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);

                if (hit.transform.gameObject.tag == "CaptureRoom" && !GameManager.Instance.isCapturing 
                    && !DayAndNight.dayandNight.isNight && InteractionEvent.Instance.isEvent)
                {
                    GameManager.Instance.Start_Captrue();
                }
                else if (hit.transform.gameObject.tag == "CookieRoom" && !GameManager.Instance.isMakingCookie 
                    && !DayAndNight.dayandNight.isNight && InteractionEvent.Instance.isEvent)
                {
                    GameManager.Instance.Start_MakeCookies();
                }
            }

        }

    }

}
