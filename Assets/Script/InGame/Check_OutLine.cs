using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ư�� ������Ʈ�� ���콺�� �ø��� ������ ���� �ܰ����� ������ ���ִ� �Լ�
/// ���콺�� ������Ʈ�� �÷��������� ���ο� Material�� List�� �߰����ְ�
/// ���̴��� ã�Ƽ� ���� ������� Material�� �ٿ��ش�.
/// 
/// ����� �����ʴ� ��ũ��Ʈ - 8/23-
/// 
/// 2022.08.04 Ocean Project1 Room
/// </summary>
public class Check_OutLine : MonoBehaviour
{
    #region _���� �����ߴ� �κ�, ���ο� Material�� ���� ���̴� ����
    //Material outline;
    //Renderer renderers;
    //List<Material> materialList = new List<Material>();

    //private void OnMouseEnter()
    //{
    //    if (GameManager.Instance.isPopUp == false)
    //    {
    //        Debug.Log("������Ʈ�� ���콺�� �÷����ִ�.");
    //        renderers = this.GetComponent<Renderer>();

    //        materialList.Clear();
    //        materialList.AddRange(renderers.sharedMaterials);
    //        materialList.Add(outline);
    //        renderers.materials = materialList.ToArray();
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    Debug.Log("������Ʈ�� ������.");

    //    Renderer renderer = this.GetComponent<Renderer>();

    //    materialList.Clear();
    //    materialList.AddRange(renderer.sharedMaterials);
    //    materialList.Remove(outline);

    //    renderer.materials = materialList.ToArray();
    //}

    //private void OnMouseExit()
    //{
    //    Debug.Log("������Ʈ�� ����������.");

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
        if (GameManager.Instance.isPopUp == false) // �㳷�� �ٲ�� ������ ��ȣ�ۿ� ������ �ǹ��� �ܰ����� �ȳ����� �ϱ� ���� !DayAndNight.dayAndNight.isDay
        {
            Debug.Log("������Ʈ�� ���콺�� �÷����ִ�.");
            renderers = outline.GetComponent<MeshRenderer>();
            renderers.material.shader = Shader.Find("Custom/OutLine");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("������Ʈ�� ������.");

        renderers = outline.GetComponent<MeshRenderer>();
        renderers.material.shader = Shader.Find("Universal Render Pipeline/Lit");
    }

    private void OnMouseExit()
    {
        Debug.Log("������Ʈ�� ����������.");

        renderers = outline.GetComponent<MeshRenderer>();
        renderers.material.shader = Shader.Find("Universal Render Pipeline/Lit");
    }


    //// Start is called before the first frame update
    //void Start()
    //{
    //    //outline = new Material(Shader.Find("Custom/OutLine"));
    //}

}
