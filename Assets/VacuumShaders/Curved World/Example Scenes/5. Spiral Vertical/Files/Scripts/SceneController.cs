using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class SceneController : MonoBehaviour
    {
        public enum MOVING_STATE { Idle, Forward, Backward };

        static public Vector3 moveDirection;
        static public MOVING_STATE movingState = MOVING_STATE.Idle;


        public float moveSpeed = 1;      
        public Transform movementLimiter;
        public float movingRangeMin, movingRangeMax;

        public Transform rotateGear;
        public Transform rotateGear2;
        public float rotateSpeed = 30;

        public GameObject[] cameras;
        int activeCameraIndex;




        Vector3 forward = new Vector3(1, 0, 0);
        Vector3 backward = new Vector3(-1, 0, 0);

        // Use this for initialization
        void Start()
        {
            QualitySettings.vSyncCount = 0;

            activeCameraIndex = 0;

            MoveIdle();
        }

        // Update is called once per frame
        void Update()
        {
            //Move world and character
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
                MoveForward();
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
                MoveBackward();
            else
                MoveIdle();
                                    
            //Change camera
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeCamera();
            }
        }


        void MoveForward()
        {
            if (movementLimiter.position.x < movingRangeMax)
            {
                movingState = MOVING_STATE.Forward;

                moveDirection = forward * moveSpeed * Time.deltaTime;


                rotateGear.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);

                if (rotateGear2)
                    rotateGear2.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            }
            else
                MoveIdle();
        }

        void MoveBackward()
        {
            if (movementLimiter.position.x >= movingRangeMin)
            {
                movingState = MOVING_STATE.Backward;

                moveDirection = backward * moveSpeed * Time.deltaTime;


                rotateGear.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

                if (rotateGear2)
                    rotateGear2.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            }
            else
                MoveIdle();
        }

        void MoveIdle()
        {
            movingState = MOVING_STATE.Idle;

            moveDirection = Vector3.zero;
        }

        void ChangeCamera()
        {
            cameras[activeCameraIndex].SetActive(false);

            activeCameraIndex += 1;
            if (activeCameraIndex >= cameras.Length)
                activeCameraIndex = 0;

            cameras[activeCameraIndex].SetActive(true);
        }
    }
}