using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 0f;
    [SerializeField] float zoomMax = 0f;
    [SerializeField] float zoomMin = 0f;

    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float horizontal = 0f;
    [SerializeField] float vertical = 0f;

    Camera cam;
    //Vector2 clickPoint;

    Vector3 beginMousePos = Vector3.zero;
    Vector3 preMousePos = Vector3.zero;
    Vector3 beginCamPos = Vector3.zero;

    float temp_Value;


    public void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    public void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (!GameManager.Instance.isErrorPopUp && !GameManager.Instance.isPopUp)
        {
            // 최대 크기로 줌인(스크롤 다운)
            if (cam.orthographicSize <= zoomMax && scroll > 0)
            {
                temp_Value = cam.orthographicSize;
                cam.orthographicSize = temp_Value;
            }
            else if (cam.orthographicSize >= zoomMin && scroll < 0)
            {
                temp_Value = cam.orthographicSize;
                cam.orthographicSize = temp_Value;
            }
            else
            {
                cam.orthographicSize -= scroll * 0.5f;
            }
        }

    }

    void CameraMove()
    {
        if (!GameManager.Instance.isErrorPopUp && !GameManager.Instance.isPopUp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                beginMousePos = Input.mousePosition;
                beginCamPos = transform.position;
            }
            else if (Input.GetMouseButton(0))
            {
                preMousePos = -(Input.mousePosition - beginMousePos) * moveSpeed;
                Vector3 newCamPos = beginCamPos + preMousePos; ;
                newCamPos.x = Mathf.Clamp(newCamPos.x, -vertical, vertical);
                newCamPos.y = Mathf.Clamp(newCamPos.y, -horizontal, horizontal);

                transform.position = newCamPos;
            }
        }
    }

    public void Update()
    {
        /// 이벤트가 실행 되었을 때, UI가 떠 있다면 카메라의 줌 기능을 못쓰게 막는다.
        ////if (!InteractionEvent.Instance.isOpenType1 || !InteractionEvent.Instance.isOpenType2)
        if (InteractionEvent.Instance.isEvent == true)
        {
            Zoom();
            CameraMove();
        }

    }
}
