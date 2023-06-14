using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    [ExecuteInEditMode]
    public class TemplateManger : MonoBehaviour
    {
        public BEND_TYPE projectBendType;
        BEND_TYPE bendType_Current;

        public GameObject[] bendTemplates;

  
        void Update()
        {
            if(bendType_Current != projectBendType)
            {
                bendType_Current = projectBendType;


                //Disable all
                for (int i = 0; i < bendTemplates.Length; i++)
                {
                    bendTemplates[i].SetActive(false);
                    bendTemplates[i].hideFlags = HideFlags.HideInHierarchy;
                }

                //Enable
                if (projectBendType != BEND_TYPE.Unknown)
                {
                    bendTemplates[(int)projectBendType].SetActive(true);
                    bendTemplates[(int)projectBendType].hideFlags = HideFlags.None;
                }
            }
        }
    }
}