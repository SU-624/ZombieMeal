using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 오브젝트에 마우스를 올리고 있으면 빨간 외곽선이 나오게 해주는 함수
/// 마우스가 오브젝트에 올려져있으면 새로운 Material을 List에 추가해주고
/// 쉐이더를 찾아서 새로 만들어진 Material에 붙여준다.
/// 
/// 현재는 쓰지않는 스크립트 - 8/23-
/// 
/// 2022.08.04 Ocean Project1 Room
/// </summary>
public class Check_OutLine : MonoBehaviour
{
    #region _원래 구현했던 부분, 새로운 Material을 만들어서 쉐이더 적용
    //Material outline;
    //Renderer renderers;
    //List<Material> materialList = new List<Material>();

    //private void OnMouseEnter()
    //{
    //    if (GameManager.Instance.isPopUp == false)
    //    {
    //        Debug.Log("오브젝트에 마우스가 올려져있다.");
    //        renderers = this.GetComponent<Renderer>();

    //        materialList.Clear();
    //        materialList.AddRange(renderers.sharedMaterials);
    //        materialList.Add(outline);
    //        renderers.materials = materialList.ToArray();
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    Debug.Log("오브젝트를 눌렀다.");

    //    Renderer renderer = this.GetComponent<Renderer>();

    //    materialList.Clear();
    //    materialList.AddRange(renderer.sharedMaterials);
    //    materialList.Remove(outline);

    //    renderer.materials = materialList.ToArray();
    //}

    //private void OnMouseExit()
    //{
    //    Debug.Log("오브젝트를 빠져나갔다.");

    //    Renderer renderer = this.GetComponent<Renderer>();

    //    materialList.Clear();
    //    materialList.AddRange(renderer.sharedMaterials);
    //    materialList.Remove(outline);

    //    renderer.materials = materialList.ToArray();
    //}


    //// Start is called before the first frame update
    //void Start()
    //{
    //    outline = new Material(Shader.Find("Custom/OutLine"));
    //}
    #endregion

    public GameObject outline;
    MeshRenderer renderers;
    //List<Material> materialList = new List<Material>();

    private void OnMouseEnter()
    {
        if (GameManager.Instance.isPopUp == false) // 밤낮이 바뀌는 동안은 상호작용 가능한 건물에 외곽선이 안나오게 하기 위해 !DayAndNight.dayAndNight.isDay
        {
            Debug.Log("오브젝트에 마우스가 올려져있다.");
            renderers = outline.GetComponent<MeshRenderer>();
            renderers.material.shader = Shader.Find("Custom/OutLine");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("오브젝트를 눌렀다.");

        renderers = outline.GetComponent<MeshRenderer>();
        renderers.material.shader = Shader.Find("Universal Render Pipeline/Lit");
    }

    private void OnMouseExit()
    {
        Debug.Log("오브젝트를 빠져나갔다.");

        renderers = outline.GetComponent<MeshRenderer>();
        renderers.material.shader = Shader.Find("Universal Render Pipeline/Lit");
    }


    //// Start is called before the first frame update
    //void Start()
    //{
    //    //outline = new Material(Shader.Find("Custom/OutLine"));
    //}

}
