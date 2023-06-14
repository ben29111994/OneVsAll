using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO.IsolatedStorage;

public class Movement : MonoBehaviour
{
    public Color botColor;
    Vector3 look;
    public Rigidbody rigid;
    public GameObject target;
    public float speed;
    bool isRun = false;
    public Animator anim;
    public GameController gameController;
    public GameObject canvasPrefab;
    public int AILevel;
    public SkinnedMeshRenderer bodyRender;
    public GameObject weaponL;
    public GameObject weaponR;
    public GameObject fireEffect;
    public GameObject bullet;
    public Slider healthBar;
    public Slider feverBar;
    Quaternion infoRot;
    //Quaternion infoRotFever;
    public GameObject gunBot;
    bool isShoot = false;
    public GameObject bulletExplode;
    public GameObject feverEffect;
    int currentLevel;

    void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        isRun = false;
        anim = GetComponent<Animator>();
        anim.Play("Idle");
        rigid = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        if (CompareTag("Bot") || CompareTag("BotGun"))
        {
            speed = 4 + currentLevel / 10;
            bodyRender.material.color = Color.black;
            if (CompareTag("BotGun"))
            {
                speed = 2 + currentLevel / 10;
                bodyRender.material.color = Color.yellow;
                GameController.totalEnemy++;
            }
        }
        else
        {
            infoRot = healthBar.transform.parent.rotation;
            //infoRotFever = feverBar.transform.parent.rotation;
            healthBar.maxValue = 40;
            healthBar.value = 40;
            feverBar.maxValue = 10;
            feverBar.value = 0;
            StartCoroutine(FeverCalm());
            bodyRender.material.color = Color.green;
        }
    }

    IEnumerator FeverCalm()
    {
        yield return new WaitForSeconds(1);
        if (feverBar.value > 0)
        {
            feverBar.value--;
        }
        else
        {
            GameController.isFever = false;
            feverEffect.SetActive(false);
        }
        StartCoroutine(FeverCalm());
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Bot"))
        {
            look = GameController.instance.mainChar.transform.position - transform.position;
            if (look != Vector3.zero)
            {
                var targetRot = Quaternion.LookRotation(look);
                var modTargetRot = targetRot;
                modTargetRot.x = 0;
                modTargetRot.z = 0;
                targetRot = modTargetRot;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * 50 * speed);
                //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            }
            if (GameController.instance.isStartGame)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
                {
                    if(!anim.GetCurrentAnimatorStateInfo(0).IsName("BotFight"))
                    anim.SetTrigger("Run");
                }
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Win"))
                    anim.SetTrigger("Win");
            }
        }

        if (CompareTag("BotGun"))
        {
            look = GameController.instance.mainChar.transform.position - transform.position;
            if (look != Vector3.zero)
            {
                var targetRot = Quaternion.LookRotation(look);
                var modTargetRot = targetRot;
                modTargetRot.x = 0;
                modTargetRot.z = 0;
                targetRot = modTargetRot;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * 50 * speed);
                //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            }
            if (GameController.instance.isStartGame)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PistolRun"))
                {
                    anim.SetTrigger("PistolRun");
                }
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                if (!isShoot)
                {
                    isShoot = true;
                    StartCoroutine(Shooting());
                }
            }
            else
            {
                StopAllCoroutines();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Win"))
                    anim.SetTrigger("Win");
            }
        }
    }

    private void LateUpdate()
    {
        if (CompareTag("Player"))
        {
            healthBar.transform.parent.rotation = infoRot;
            //feverBar.transform.parent.rotation = infoRotFever;
        }
    }

    IEnumerator Shooting()
    {
        fireEffect.SetActive(true);
        var bulletSpawn = Instantiate(bullet, fireEffect.transform.position, Quaternion.identity);
        bulletSpawn.SetActive(true);
        bulletSpawn.transform.DOMove(transform.forward.normalized * 100, 35 - currentLevel);
        yield return new WaitForSeconds(Random.Range(4,6));
        StartCoroutine(Shooting());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Bullet")) && CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Bot"))
            {
                collision.gameObject.GetComponent<Movement>().anim.SetTrigger("BotFight");
                StartCoroutine(delayFightAnim(collision.gameObject));
            }
            GameController.instance.OnGetHurt();
        }

        if (collision.gameObject.CompareTag("Wall") && CompareTag("Player"))
        {
            GameController.instance.ChangeSpeed(5);
        }
    }

    IEnumerator delayFightAnim(GameObject target)
    {
        yield return new WaitForSeconds(1.5f);
        target.GetComponent<Movement>().anim.SetTrigger("Run");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && CompareTag("Bot"))
        {
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("BotFight"))
            anim.SetTrigger("Run");
        }
        if (collision.gameObject.CompareTag("Wall") && CompareTag("Player"))
        {
            float speedValue = 12;
            if(GameController.isFever)
            {
                speedValue = 20;
            }
            GameController.instance.ChangeSpeed(speedValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet") && CompareTag("Player"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                other.tag = "Untagged";
                other.transform.DOKill();
                var dir = other.transform.position - transform.position;
                other.transform.DOMove(dir.normalized * 100, 5);
                Destroy(other.gameObject, 5);
            }
            else
            {
                Instantiate(bulletExplode, other.transform.position, Quaternion.identity);
                other.gameObject.SetActive(false);
                Destroy(other.gameObject, 5);
                GameController.instance.OnGetHurt();
            }
        }
    }
}
