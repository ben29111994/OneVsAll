using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    [ExecuteInEditMode]
    public class LookAtTarget : MonoBehaviour
    {
        public Transform target;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(target);
        }
    }
}