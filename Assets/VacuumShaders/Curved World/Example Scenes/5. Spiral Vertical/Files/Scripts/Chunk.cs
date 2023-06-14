using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VacuumShaders.CurvedWorld.Example
{
    public class Chunk : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(SceneController.moveDirection);
        }
    }
}