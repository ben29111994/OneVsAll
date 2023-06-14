using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    [ExecuteInEditMode]
    public class MaterialCutout : MonoBehaviour
    {
        Material material;

        const float MIN = -30f;
        const float MAX = 9.1f;

        // Use this for initialization
        void Start()
        {
            material = GetComponent<Renderer>().sharedMaterial;
        }

        // Update is called once per frame
        void Update()
        {
            //Range[0, 1]
            float value = (transform.position.y - MIN) / (MAX - MIN);

            material.SetFloat("_Cutoff", 1 - value * 0.75f);
        }
    }
}