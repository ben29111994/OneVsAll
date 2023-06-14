using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class Spiral_Car : MonoBehaviour
    {
        public float speed;
        public Vector3 moveDirection;
        public bool isActive;

        Rigidbody rigidBody;

        public int teamID; 
        
        // Use this for initialization
        void Start()
        {
            isActive = true;

            rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {           
            if (isActive)
            {
                //Just move car
                rigidBody.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);


                //Car fall out of the road (may be after collision)
                if (transform.position.x > 8 || transform.position.x < -8)
                    StopMoving();
            }



            //Destory car if it is below -10y
            if (transform.position.y < -10 || Input.GetKey(KeyCode.Space))
                Kill();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
               if(collision.gameObject.GetComponent<Spiral_Car>().teamID != teamID)
                {
                    StopMoving();

                    Vector3 f1 = new Vector3(Random.Range(-2f, -1f), Random.Range(0.1f, 0.5f), 0).normalized * 200;
                    Vector3 f2 = new Vector3(Random.Range( 1f,  2f), Random.Range(0.1f, 0.5f), 0).normalized * 200;

                    rigidBody.AddForce(Random.value > 0.5f ? f1 : f2, ForceMode.Impulse);
                }
            }
        }


        public void StopMoving()
        {
            speed = 0;

            isActive = false;
        }

        public void Kill()
        {
            //Instantiate explosion particles 
            Vector3 worldPosition = CurvedWorld_Controller.current.TransformPosition(transform.position, Spiral_SceneController.get.bendType);
            Instantiate(teamID == 0 ? Spiral_SceneController.get.explosionParticlesGreen : Spiral_SceneController.get.explosionParticlesRed, worldPosition, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}