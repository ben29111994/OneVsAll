using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class LightIntensity : MonoBehaviour
    {
        Light lightSource;

        const float MIN = -30f;
        const float MAX = 9.1f;

        float intensityMyltiplier = 20;


        // Use this for initialization
        void Start()
        {
            lightSource = GetComponent<Light>();

            intensityMyltiplier = lightSource.intensity;
        }

        // Update is called once per frame
        void Update()
        {
            float value = (transform.position.y - MIN) / (MAX - MIN);

            lightSource.intensity = value * intensityMyltiplier;
        }
    }
}
