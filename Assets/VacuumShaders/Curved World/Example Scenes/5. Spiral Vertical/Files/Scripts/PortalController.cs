using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class PortalController : MonoBehaviour
    {
        public Transform movementLimiter;

        public float limit;
        public bool isLeftPortal;


        public GameObject light1;
        public GameObject light2;
        public ParticleSystem ps;

        public Material distortionMat;


        float deltaTime;


        bool isActive;


        // Use this for initialization
        void Start()
        {
            Color tintColor = distortionMat.GetColor("_TintColor");
            tintColor.a = 0.2f;
            distortionMat.SetColor("_TintColor", tintColor);

            distortionMat.SetFloat("_BumpAmt", 7000);



            isActive = false;
            Activate();
        }

        // Update is called once per frame
        void Update()
        {
            if (isLeftPortal)
            {
                if (movementLimiter.position.x > limit)
                    Deactivate();
                else
                    Activate();
            }
            else
            {
                if (movementLimiter.position.x > limit)
                    Activate();
                else
                    Deactivate();
            }


            if(deltaTime > 0)
            {
                deltaTime = Mathf.Clamp01(deltaTime - Time.deltaTime);

                if (isActive)
                {
                    Color tintColor = distortionMat.GetColor("_TintColor");
                    tintColor.a = (1 - deltaTime) * 0.2f;
                    distortionMat.SetColor("_TintColor", tintColor);


                    distortionMat.SetFloat("_BumpAmt", (1 - deltaTime) * 7000);
                }
                else
                {
                    Color tintColor = distortionMat.GetColor("_TintColor");
                    tintColor.a = deltaTime * 0.2f;
                    distortionMat.SetColor("_TintColor", tintColor);


                    distortionMat.SetFloat("_BumpAmt", deltaTime * 7000);
                }
            }
        }

        void Activate()
        {
            if (isActive == false)
            {
                ps.Play();

                light1.SetActive(true);
                light2.SetActive(true);

                isActive = !isActive;

                deltaTime = 1;
            }
        }

        void Deactivate()
        {
            if (isActive == true)
            {
                ps.Stop();

                 light1.SetActive(true);
                light2.SetActive(true);

                isActive = !isActive;

                deltaTime = 1;
            }
        }
    }
}