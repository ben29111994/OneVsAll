using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    [ExecuteInEditMode]
    public class RangeFadeController : MonoBehaviour
    {
        public enum AXIS { X, Z };

        static public RangeFadeController current;

        public float rangeFadeMin = -100;
        public float rangeFadeMax = 100;


        public AXIS axis;
        float rangeFadeMin_Current;
        float rangeFadeMax_Current;

        int rangeFadeID;

        // Use this for initialization
        void Start()
        {
            if (current != null && current != this)
                Debug.LogWarning("There is more than one active 'RangeFadeController' scripts in the scene.\n", current.gameObject);

            current = this;

            LoadID();
        }

        // Update is called once per frame
        void Update()
        {
            current = this;


            if (rangeFadeMin != rangeFadeMin_Current || rangeFadeMax_Current != rangeFadeMax)
            {
                rangeFadeMin_Current = rangeFadeMin;
                rangeFadeMax_Current = rangeFadeMax;

                Shader.SetGlobalVector(rangeFadeID, new Vector2(rangeFadeMin, rangeFadeMax));
            }
        }

        void LoadID()
        {
            rangeFadeID = Shader.PropertyToID("_V_CW_RangeFade");
        }

        public bool IsPointVisible(Vector3 point)
        {
            if (axis == AXIS.X)
            {
                if (point.x < rangeFadeMin || point.x > rangeFadeMax)
                    return false;
                else
                    return true;
            }
            else
            {
                if (point.z < rangeFadeMin || point.z > rangeFadeMax)
                    return false;
                else
                    return true;
            }
        }
    }

}

