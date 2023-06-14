using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class Spiral_SceneController : MonoBehaviour
    {
        public static Spiral_SceneController get;


        public CurvedWorld.BEND_TYPE bendType;
        public Shader defaultShader;

        [Space(5)]
        public GameObject explosionParticlesRed;
        public GameObject explosionParticlesGreen;

        [Space(5)]
        public Transform spwanPoint1;
        public Transform spwanPoint2;


        [Space(5)]
        public GameObject[] carPrefabs;

        

        float spawnDeltaTime;


        // Use this for initialization
        void Start()
        {
            get = this;

            Physics.gravity = new Vector3(0, -100, 0);
        }

        // Update is called once per frame
        void Update()
        {
            spawnDeltaTime += Time.deltaTime;

            if(spawnDeltaTime > 0)
            {
                int teamID = Random.value > 0.5f ? 0 : 1;


                Vector3 spawnPoint = teamID == 0 ? spwanPoint1.position : spwanPoint2.position;
                spawnPoint.x = Random.Range(-4.6f, 4.6f);
                spawnPoint.z += Random.Range(-10f, 10f);


                GameObject car = Instantiate(carPrefabs[teamID], spawnPoint, teamID == 0 ? Quaternion.Euler(new Vector3(0, 180, 0)) : Quaternion.identity);
                car.GetComponent<Spiral_Car>().speed = Random.Range(50, 80);
                car.GetComponent<Spiral_Car>().moveDirection = teamID == 0 ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);
                car.GetComponent<Spiral_Car>().teamID = teamID;

                //Reset
                spawnDeltaTime = -Random.value;
            }
        }
    }
}