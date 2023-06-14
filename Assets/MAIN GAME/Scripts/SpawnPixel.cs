using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using GPUInstancer;

public class SpawnPixel : MonoBehaviour
{
    public static SpawnPixel instance;
    public GameObject prefabPixel;
    public GameObject parentObject;
    public Color pixelColor;
    public int totalPixel;
    public GameObject GPUInstance;

    private void OnEnable()
    {
        instance = this;
        for (int i = 0; i < totalPixel; i++)
        {
            var spawnPixel = Instantiate(prefabPixel, parentObject.transform.position, Quaternion.identity);
            //spawnPixel.GetComponent<Renderer>().material.color = pixelColor;
            //if(parentObject != null)
            //spawnPixel.transform.parent = parentObject.transform;
        }
        GameController.totalEnemy += totalPixel;
        //GPUInstance.SetActive(true);
    }
}