using UnityEngine;
using System.Collections;

namespace VacuumShaders.CurvedWorld.Example
{
    public class Runner_Car : MonoBehaviour
    {
        //////////////////////////////////////////////////////////////////////////////
        //                                                                          // 
        //Variables                                                                 //                
        //                                                                          //               
        //////////////////////////////////////////////////////////////////////////////

        Rigidbody rigidBody;
        public float speed = 1;

        //////////////////////////////////////////////////////////////////////////////
        //                                                                          // 
        //Unity Functions                                                           //                
        //                                                                          //               
        //////////////////////////////////////////////////////////////////////////////
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();

            transform.position = new Vector3(Random.Range(-3.5f, 3.5f), 1, Random.Range(140, 240));
            speed = Random.Range(2f, 6f);

        }

        void FixedUpdate()
        {
            if (Runner_SceneManager.get != null)
            {
                rigidBody.MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime * speed);


                if (transform.position.y < -15)
                {
                    Runner_SceneManager.get.DestroyCar(this);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        //                                                                          // 
        //Custom Functions                                                          //
        //                                                                          //               
        //////////////////////////////////////////////////////////////////////////////
    }
}
