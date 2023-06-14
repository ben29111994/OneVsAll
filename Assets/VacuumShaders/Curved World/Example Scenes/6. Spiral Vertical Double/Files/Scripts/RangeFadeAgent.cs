using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    [ExecuteInEditMode]
    public class RangeFadeAgent : MonoBehaviour
    {
        bool isActive;
     

        void Start()
        {
            isActive = true;
            Toggle(false);
        }

        void FixedUpdate()
        {
            if(RangeFadeController.current != null)
                Toggle(RangeFadeController.current.IsPointVisible(transform.parent.position));           
        }

        void Toggle(bool toggle)
        {
            if(toggle)
            {
                if (isActive == false)
                {
                    isActive = true;

                    foreach (Transform child in transform)
                    {
                        if (child != transform)
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                if (isActive == true)
                {
                    isActive = false;

                    foreach (Transform child in transform)
                    {
                        if (child != transform)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}