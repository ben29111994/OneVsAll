using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPUInstancer;

public class Tile : MonoBehaviour
{
    public Color tileColor;
    private Renderer meshRenderer;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<Renderer>();
        ChangeColor(tileColor);
    }

    public void SetTransfrom(Vector3 pos,Vector3 scale)
    {
        transform.localPosition = pos;
        transform.localScale = new Vector3(scale.x,scale.y,scale.z);
    }

    public void Remove()
    {
        AddRemoveInstances.instance.instancesList.Remove(GetComponent<GPUInstancerPrefab>());
    }

    public void ChangeColor(Color currentColor)
    {
        GetComponent<MeshRenderer>().material.color = currentColor;
    }
}
