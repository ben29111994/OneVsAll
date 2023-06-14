using UnityEngine;
using System.Collections.Generic;

namespace VacuumShaders.CurvedWorld.Example
{
    [ExecuteInEditMode]
    public class Follow : MonoBehaviour
    {
        public BEND_TYPE bendType;

        public Transform parent;

        public bool recalculateRotation;

        [Space]
        public bool drawDebugLines;
        public float debugLineLength = 2;




        void Start()
        {
            if (parent == null)
            {
                parent = transform.parent;
            }
        }

        void Update()
        {
            if (parent == null || CurvedWorld_Controller.current == null)
            {
                //Do nothing
            }
            else if (CurvedWorld_Controller.current.enabled == false ||
                     CurvedWorld_Controller.current.gameObject.activeSelf == false ||
                     (CurvedWorld_Controller.current.disableInEditor && Application.isEditor && Application.isPlaying == false))
            {
                transform.position = parent.position;
                transform.rotation = Quaternion.identity;
            }
            else 
            {
                transform.position = CurvedWorld_Controller.current.TransformPosition(parent.position, bendType);

                if (recalculateRotation)
                   transform.rotation = CurvedWorld_Controller.current.TransformRotation(parent.position, parent.forward, parent.right, bendType);
            }
        }

        //[ContextMenu("Copy To All")]
        //private void Reset()
        //{
        //    Follow[] scripts = Resources.FindObjectsOfTypeAll<Follow>();

        //    if(scripts != null && scripts.Length > 0)
        //    {
        //        for (int i = 0; i < scripts.Length; i++)
        //        {
        //            if (scripts[i] != null && scripts[i].gameObject != null)
        //                scripts[i].bendType = bendType;
        //        }
        //    }
        //}

        private void OnDrawGizmos()
        {
            if (drawDebugLines)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * debugLineLength);


                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + transform.up * debugLineLength);


                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + transform.right * debugLineLength);
            }
        }
    }
}
