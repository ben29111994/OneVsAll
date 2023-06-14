using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VacuumShaders.CurvedWorld.Example
{
    public class EllenController : MonoBehaviour
    {
        Animator animator;

        const string AnimationIdle = "idle";
        const string AnimationRun = "run";
        const string AnimationJump = "jump";


        public AudioClip[] footsteps;
        AudioSource audioSource;


        float jumpLength;


        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();

            audioSource = GetComponent<AudioSource>();

            PlayIdle();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayJump();
            }
            else if ((jumpLength -= Time.deltaTime) < 0)
            {
                switch (SceneController.movingState)
                {
                    case SceneController.MOVING_STATE.Idle: PlayIdle(); break;
                    case SceneController.MOVING_STATE.Forward: PlayRunForward(); break;
                    case SceneController.MOVING_STATE.Backward: PlayRunBackward(); break;
                }
            }
        }


        void PlayIdle()
        {
            StopOtherAnimations(AnimationIdle);

            animator.SetBool(AnimationIdle, true);
        }

        void PlayJump()
        {
            StopOtherAnimations(AnimationJump);

            animator.SetBool(AnimationJump, true);

            jumpLength = 0.25f;
        }

        void PlayRunForward()
        {
            StopOtherAnimations(AnimationRun);

            animator.SetBool(AnimationRun, true);

            transform.rotation = Quaternion.Euler(0, -90, 0);


            if (audioSource.clip == null || audioSource.isPlaying == false)
            {
                audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
                audioSource.Play();
            }

        }

        void PlayRunBackward()
        {
            StopOtherAnimations(AnimationRun);

            animator.SetBool(AnimationRun, true);

            transform.rotation = Quaternion.Euler(0, 90, 0);


            if (audioSource.clip == null || audioSource.isPlaying == false)
            {
                audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
                audioSource.Play();
            }
        }


        void StopOtherAnimations(string animationName)
        {
            foreach (AnimatorControllerParameter item in animator.parameters)
            {
                if (item.name != animationName)
                    animator.SetBool(item.name, false);
            }
        }
    }
}