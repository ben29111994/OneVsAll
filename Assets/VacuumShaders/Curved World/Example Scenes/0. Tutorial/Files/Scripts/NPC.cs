using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VacuumShaders.CurvedWorld.Example
{

    public class NPC : MonoBehaviour
    {
        NavMeshAgent agent;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (agent.velocity.magnitude < 0.5f)
                SetDestination();
        }

        void SetDestination()
        {
            if(agent != null)
                agent.SetDestination(new Vector3(Random.Range(-35f, 35), 0, Random.Range(-35f, 35)));
        }
    }
}