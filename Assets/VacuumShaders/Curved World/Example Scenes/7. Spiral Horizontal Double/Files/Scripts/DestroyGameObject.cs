using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VacuumShaders.CurvedWorld.Example
{
    public class DestroyGameObject : MonoBehaviour
    {
        public float time = 1;
        float deltaTime;


        void Update()
        {
            deltaTime += Time.deltaTime;

            if(deltaTime > time)
            {
                Destroy(gameObject);
            }
        }
    }
}