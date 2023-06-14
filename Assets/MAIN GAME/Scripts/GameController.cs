using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;
using System.Linq;
using VisCircle;
using UnityEngine.PostProcessing;
using GPUInstancer;

public class GameController : MonoBehaviour
{
    [Header("Variable")]
    public static GameController instance;
    public int maxLevel;
    public bool isStartGame = false;
    public bool isControl = true;
    bool isDrag = false; 
    int maxPlusEffect = 0;
    bool isVibrate = false;
    public float speed;
    public LayerMask clickMask;
    public LayerMask dragMask;
    public static int totalEnemy;
    public static int enemyKilled;
    public static bool isFever = false;
    public float swordSize = 1.5f;
    public GameObject feverText;

    [Header("UI")]
    public Slider progress;
    public GameObject winPanel;
    public GameObject losePanel;
    public Text currentLevelTextBig;
    public Text currentLevelText;
    public Text nextLevelText;
    public int currentLevel;
    public Canvas canvas;
    public GameObject startGameMenu;
    public GameObject shopMenu;
    public Transform ballTab;
    public Transform charTab;
    public Text title;
    static int currentBG = 0;
    public InputField levelInput;
    public Image compliment;
    public List<Sprite> listCompliment = new List<Sprite>();
    public Text taskText;
    public GameObject tutorial;
    public GameObject startButton;
    public Text coinText;
    int coin;
    int tempCoin;
    public List<CoinFlyAnimation> listCoinAnim = new List<CoinFlyAnimation>();

    [Header("Objects")]
    public GameObject mainChar;
    public GameObject plusVarPrefab;
    public GameObject conffeti;
    GameObject conffetiSpawn;
    public List<GameObject> listLevel = new List<GameObject>();
    public GameObject blast;
    public GameObject environment;
    public GameObject dieEffect;

    private void OnEnable()
    {
        // PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        instance = this;
        StartCoroutine(delayStart());
    }

    IEnumerator delayStart()
    {
        yield return new WaitForSeconds(0.001f);
        isFever = false;
        Camera.main.transform.DOMoveX(-200, 0);
        Camera.main.transform.DOMoveX(0, 1);
        maxLevel = listLevel.Count - 1;
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevelTextBig.text = "LEVEL " + (currentLevel + 1).ToString();
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
        listLevel[currentLevel].SetActive(true);
        startGameMenu.SetActive(true);
        title.DOColor(new Color32(255, 255, 255, 0), 3);
        isControl = true;
        coin = PlayerPrefs.GetInt("Coin");
        coinText.text = coin.ToString();
        mainChar.GetComponent<Movement>().anim.speed = speed / 10;
        foreach (Transform child in environment.transform)
        {
            var random = Random.Range(0, 100);
            if(random > 50)
            {
                child.gameObject.SetActive(false);
            }
            child.transform.position = new Vector3(child.transform.position.x, Random.Range(-60, -40), child.transform.position.z);
        }
        yield return new WaitForSeconds(0.1f);
        progress.value = 0;
        progress.maxValue = totalEnemy;
        taskText.text = 0 + "/" + totalEnemy.ToString();
    }

    float h, v;
    Vector3 firstP, lastP, dir;
    public Transform target;

    private void Update()
    {
        if (isStartGame && isControl)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(mainChar.transform.position.x, Camera.main.transform.position.y, mainChar.transform.position.z - 16.5f), Time.deltaTime * 5);
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }

            if (Input.GetMouseButton(0))
            {
                OnMouseDrag();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnMouseUp();
            }
        }
        else if (!isStartGame && isControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ButtonStartGame();
                OnMouseDown();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    //Look at swipe direction
    //void OnMouseDown()
    //{
    //    if (!isDrag)
    //    {
    //        isDrag = true;
    //        target = null;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, 1000, dragMask))
    //        {
    //            firstP = hit.point;
    //        }
    //    }
    //}

    //void OnMouseDrag()
    //{
    //    if (isDrag)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, 1000, dragMask))
    //        {
    //            lastP = hit.point;
    //        }
    //        if (Vector3.Distance(firstP, lastP) > 0.05f)
    //        {
    //            dir = lastP - firstP;
    //            var modDir = dir;
    //            modDir.y = 0.5f;
    //            dir = modDir;
    //            if (dir != Vector3.zero)
    //            {
    //                var targetRot = Quaternion.LookRotation(-dir);
    //                var modTargetRot = targetRot;
    //                modTargetRot.x = 0;
    //                modTargetRot.z = 0;
    //                targetRot = modTargetRot;
    //                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * 3600);
    //            }
    //        }
    //    }
    //}

    //void OnMouseUp()
    //{
    //    if (isDrag)
    //    {
    //        isDrag = false;
    //    }
    //}

    void OnMouseDown()
    {
        if (!isDrag)
        {
            transform.DOKill();
            mainChar.GetComponent<Movement>().weaponL.tag = "Untagged";
            mainChar.GetComponent<Movement>().weaponR.tag = "Untagged";
            //mainChar.GetComponent<Movement>().weaponL.GetComponent<CapsuleCollider>().isTrigger = true;
            //mainChar.GetComponent<Movement>().weaponR.GetComponent<CapsuleCollider>().isTrigger = true;
            mainChar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            mainChar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            mainChar.GetComponent<Movement>().weaponL.transform.localScale = new Vector3(0.1f, 0.75f, 0.1f);
            mainChar.GetComponent<Movement>().weaponR.transform.localScale = new Vector3(0.1f, 0.75f, 0.1f);
            mainChar.GetComponent<Movement>().anim.SetTrigger("NinjaRun");
            isDrag = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, dragMask))
            {
                firstP = hit.point;
            }
        }
    }

    void OnMouseDrag()
    {
        if (isDrag)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 1000, dragMask))
            //{
            //    lastP = hit.point;
            //}
            //if (Vector3.Distance(firstP, lastP) > 0.01)
            //{
#if UNITY_EDITOR
            h = Input.GetAxis("Mouse X");
            v = Input.GetAxis("Mouse Y");
#endif
#if UNITY_IOS
            if (Input.touchCount > 0)
            {
                h = Input.touches[0].deltaPosition.x / 10;
                v = Input.touches[0].deltaPosition.y / 10;
            }
#endif
            dir = new Vector3(h, 0, v);
            if (dir != Vector3.zero)
            {
                //var targetRot = Quaternion.LookRotation(dir);
                mainChar.transform.rotation = Quaternion.Lerp(mainChar.transform.rotation, Quaternion.LookRotation(dir), 3.5f * Time.deltaTime);
                //mainChar.transform.rotation = Quaternion.RotateTowards(mainChar.transform.rotation, targetRot, Time.deltaTime * 360);
            }
            mainChar.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (!mainChar.GetComponent<Movement>().anim.GetCurrentAnimatorStateInfo(0).IsName("NinjaRun"))
            {
                mainChar.GetComponent<Movement>().anim.SetTrigger("NinjaRun");
            }
            //}
        }
        else
        {
            if (!mainChar.GetComponent<Movement>().anim.GetCurrentAnimatorStateInfo(0).IsName("Sad"))
            {
                mainChar.GetComponent<Movement>().anim.SetTrigger("Sad");
            }
        }
    }

    void OnMouseUp()
    {
        if (isDrag)
        {
            isDrag = false;
            mainChar.GetComponent<Movement>().weaponL.transform.localScale = new Vector3(0.1f, 0.75f, 0.1f);
            mainChar.GetComponent<Movement>().weaponR.transform.localScale = new Vector3(0.1f, 0.75f, 0.1f);
            mainChar.GetComponent<Movement>().anim.SetTrigger("Attack");
            mainChar.transform.DOKill();
            mainChar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            mainChar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            mainChar.GetComponent<Movement>().weaponL.tag = "Weapon";
            mainChar.GetComponent<Movement>().weaponR.tag = "Weapon";
            mainChar.GetComponent<Movement>().weaponL.transform.DOScaleY(swordSize, 0.5f).SetLoops(2, LoopType.Yoyo);
            mainChar.GetComponent<Movement>().weaponR.transform.DOScaleY(swordSize, 0.5f).SetLoops(2, LoopType.Yoyo);
            //mainChar.GetComponent<Movement>().weaponL.GetComponent<CapsuleCollider>().isTrigger = false;
            //mainChar.GetComponent<Movement>().weaponR.GetComponent<CapsuleCollider>().isTrigger = false;
            mainChar.GetComponent<Rigidbody>().AddForce(mainChar.transform.forward.normalized * 5000);
            transform.DOMove(transform.position, 1f).OnComplete(() =>
            {
                DOTween.To(() => mainChar.GetComponent<Rigidbody>().velocity, x => mainChar.GetComponent<Rigidbody>().velocity = x, new Vector3(0, 0, 0), 0.5f);
                mainChar.GetComponent<Movement>().weaponL.transform.localScale = new Vector3(0.1f, 0.75f, 0.1f);
                mainChar.GetComponent<Movement>().weaponR.transform.localScale = new Vector3(0.1f, 0.75f, 0.1f);
                mainChar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                mainChar.GetComponent<Movement>().weaponL.tag = "Untagged";
                mainChar.GetComponent<Movement>().weaponR.tag = "Untagged";
                //mainChar.GetComponent<Movement>().weaponL.GetComponent<CapsuleCollider>().isTrigger = true;
                //mainChar.GetComponent<Movement>().weaponR.GetComponent<CapsuleCollider>().isTrigger = true;
            });
        }
    }

    IEnumerator PlusEffect(Vector3 pos)
    {
        maxPlusEffect++;
        if (!UnityEngine.iOS.Device.generation.ToString().Contains("5") && !isVibrate)
        {
            isVibrate = true;
            StartCoroutine(delayVibrate());
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
        var plusVar = Instantiate(plusVarPrefab);
        plusVar.transform.SetParent(canvas.transform);
        plusVar.transform.localScale = new Vector3(1, 1, 1);
        plusVar.transform.position = new Vector3(pos.x + Random.Range(-50, 50), pos.y + Random.Range(-100, -75), pos.z);
        plusVar.GetComponent<Text>().DOColor(new Color32(255, 255, 255, 0), 1f);
        plusVar.SetActive(true);
        plusVar.transform.DOMoveY(plusVar.transform.position.y + Random.Range(50, 90), 0.5f);
        Destroy(plusVar, 0.5f);
        yield return new WaitForSeconds(0.01f);
        maxPlusEffect--;
    }

    public void OnGetHurt()
    {
        if (!isFever)
        {
            mainChar.GetComponent<Movement>().healthBar.value -= 1;
            mainChar.GetComponent<Movement>().healthBar.transform.DOKill();
            mainChar.GetComponent<Movement>().healthBar.transform.localScale = Vector3.one;
            mainChar.GetComponent<Movement>().healthBar.transform.DOScale(Vector3.one * 1.5f, 0.25f).SetLoops(2, LoopType.Yoyo);
            if (mainChar.GetComponent<Movement>().healthBar.value <= 0)
            {
                mainChar.GetComponent<Movement>().enabled = false;
                Instantiate(dieEffect, mainChar.transform.position, Quaternion.identity);
                mainChar.SetActive(false);
                Lose();
            }
        }
    }

    public void OnKill()
    {
        enemyKilled++;
        taskText.text = enemyKilled + "/" + totalEnemy;
        progress.value = enemyKilled;
        if(!isFever)
        mainChar.GetComponent<Movement>().feverBar.value++;
        if(mainChar.GetComponent<Movement>().feverBar.value >= 9)
        {
            isFever = true;
            StartCoroutine(FeverMode());
        }
        if(enemyKilled >= totalEnemy)
        {
            Win();
        }
    }

    public void ChangeSpeed(float speedValue)
    {
        speed = speedValue;
    }

    IEnumerator FeverMode()
    {
        feverText.SetActive(true);
        speed = 20;
        mainChar.GetComponent<Movement>().feverEffect.SetActive(true);
        swordSize = 3;
        while (isFever)
        {
            yield return null;
        }
        feverText.SetActive(false);
        speed = 12;
        mainChar.GetComponent<Movement>().feverEffect.SetActive(false);
        swordSize = 1.5f;
        isFever = false;
    }

    IEnumerator delayVibrate()
    {
        yield return new WaitForSeconds(0.2f);
        isVibrate = false;
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        return parentCanvas.transform.TransformPoint(movePos);
    }

    public void ButtonStartGame()
    {
        startGameMenu.SetActive(false);
        isStartGame = true;
        tutorial.SetActive(false);
    }

    bool isCheckShop = false;
    //public void ButtonShopMenu()
    //{
    //    shopMenu.SetActive(true);
    //    if (!isCheckShop)
    //    {
    //        isCheckShop = true;
    //        for (int i = 0; i < listShopBall.Count; i++)
    //        {
    //            var checkAgain = PlayerPrefs.GetInt("Ball" + i.ToString());
    //            if (checkAgain == 1)
    //            {
    //                var checkCurrent = PlayerPrefs.GetInt("CurrentBall");
    //                if (i == checkCurrent)
    //                {
    //                    listShopBall[i].transform.GetChild(checkAgain).SetAsLastSibling();
    //                }
    //                else
    //                {
    //                    PlayerPrefs.SetInt("Ball" + i.ToString(), 2);
    //                    listShopBall[i].GetComponent<ItemManager>().listStatus[checkAgain].SetSiblingIndex(1);
    //                }
    //            }
    //            else
    //            {
    //                listShopBall[i].transform.GetChild(checkAgain).SetAsLastSibling();
    //            }
    //        }

    //        for (int i = 0; i < listShopChar.Count; i++)
    //        {
    //            var checkAgain = PlayerPrefs.GetInt("Player" + i.ToString());
    //            if (checkAgain == 1)
    //            {
    //                var checkCurrent = PlayerPrefs.GetInt("CurrentPlayer");
    //                if (i == checkCurrent)
    //                {
    //                    listShopChar[i].transform.GetChild(checkAgain).SetAsLastSibling();
    //                }
    //                else
    //                {
    //                    PlayerPrefs.SetInt("Player" + i.ToString(), 2);
    //                    listShopChar[i].GetComponent<ItemManager>().listStatus[checkAgain].SetSiblingIndex(1);
    //                }
    //            }
    //            else
    //            {
    //                listShopChar[i].transform.GetChild(checkAgain).SetAsLastSibling();
    //            }
    //        }
    //    }
    //}

    //public void ExitShop()
    //{
    //    shopMenu.SetActive(false);
    //}

    //public void CharTabButton()
    //{
    //    charTab.SetAsLastSibling();
    //}
    
    //public void BallTabButton()
    //{
    //    ballTab.SetAsLastSibling();
    //}

    public void Win()
    {
        enemyKilled = 0;
        totalEnemy = 0;
        StopAllCoroutines();
        StartCoroutine(DelayWin());
    }

    IEnumerator DelayWin()
    {
        if (isStartGame)
        {
            isStartGame = false;
            isControl = false;
            isDrag = false;
            tutorial.SetActive(false);
            losePanel.SetActive(false);
            //conffetiSpawn = Instantiate(conffeti);
            //conffetiSpawn.transform.position = mainChar.transform.position;
            //conffetiSpawn.SetActive(true);
            currentLevel++;
            if (currentLevel > maxLevel)
            {
                currentLevel = 0;
            }
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            yield return new WaitForSeconds(2);
            Camera.main.transform.DOMove(new Vector3(mainChar.transform.position.x, Camera.main.transform.position.y, mainChar.transform.position.z - 16.5f), 0.5f);
            winPanel.SetActive(true);
            tempCoin = totalEnemy;           
            var nextButton = winPanel.transform.GetChild(2).gameObject;
            nextButton.SetActive(false);
            var textWinCoin = winPanel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
            textWinCoin.text = tempCoin.ToString();
            blast.SetActive(false);
            blast.transform.position = new Vector3(mainChar.transform.position.x, 20, mainChar.transform.position.z);
            blast.SetActive(true);
            yield return new WaitForSeconds(1);
            foreach (var item in listCoinAnim)
            {
                item.Move();
            }
            textWinCoin.DOText(" 0 ", 1.2f, true, ScrambleMode.Numerals);
            yield return new WaitForSeconds(1);
            coin += tempCoin;
            PlayerPrefs.SetInt("Coin", coin);
            coinText.text = coin.ToString();
            coinText.transform.parent.DOScale(Vector3.one * 1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
            nextButton.SetActive(true);
        }
    }

    public void Lose()
    {
        if (isStartGame)
        {
            Debug.Log("Lose");
            StopAllCoroutines();
            tutorial.SetActive(false);
            isStartGame = false;
            isControl = false;
            StartCoroutine(DelayLose());
            enemyKilled = 0;
            totalEnemy = 0;
        }
    }

    IEnumerator DelayLose()
    {
        yield return new WaitForSeconds(1);
        losePanel.SetActive(true);
    }

    public void LoadScene()
    {
        totalEnemy = 0;
        StartCoroutine(delayLoadScene());
    }

    IEnumerator delayLoadScene()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        Camera.main.transform.DOMoveX(200, 1);
        yield return new WaitForSeconds(1);
        var temp = conffetiSpawn;
        Destroy(temp);
        SceneManager.LoadScene(0);
    }

    public void OnChangeMap()
    {
        if (levelInput != null)
        {
            enemyKilled = 0;
            totalEnemy = 0;
            int level = int.Parse(levelInput.text.ToString());
            if (level <= maxLevel)
            {
                PlayerPrefs.SetInt("currentLevel", level);
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ButtonNextLevel()
    {
        title.DOKill();
        isStartGame = true;
        currentLevel++;
        if (currentLevel > maxLevel)
        {
            currentLevel = 0;
        }
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        SceneManager.LoadScene(0);
    }

    public void ChoosePlayerButton()
    {
        int index = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        int check = PlayerPrefs.GetInt("Player" + index.ToString());
        if(check == 0)
        {
            var objectTarget = EventSystem.current.currentSelectedGameObject.GetComponent<ItemManager>().listStatus[0];
            string priceS = objectTarget.transform.GetChild(0).GetComponent<Text>().text;
            int price = int.Parse(priceS);
            if (coin >= price)
            {
                coin -= price;
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.SetInt("Player" + index.ToString(), 1);
                objectTarget.transform.SetSiblingIndex(0);
                PlayerPrefs.SetInt("CurrentPlayer", index);
            }
        }
        if(check == 1)
        {
            PlayerPrefs.SetInt("CurrentPlayer", index);
            PlayerPrefs.SetInt("Player" + index, 2);

            //if (isPass)
            //{
            //    foreach(var item in listStop)
            //    {
            //        item.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = listChars[index];
            //    }
            //}
            //else
            //{
            //    foreach (var item in listPass)
            //    {
            //        item.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.mainTexture = listChars[index];
            //    }
            //}
        }

        //for (int i = 0; i < listShopChar.Count; i++)
        //{
        //    var checkAgain = PlayerPrefs.GetInt("Player" + i.ToString());
        //    if (checkAgain == 2)
        //    {
        //        var checkCurrent = PlayerPrefs.GetInt("CurrentPlayer");
        //        if (i == checkCurrent)
        //        {
        //            listShopChar[i].GetComponent<ItemManager>().listStatus[checkAgain].SetAsLastSibling();
        //        }
        //        else
        //        {
        //            PlayerPrefs.SetInt("Player" + i.ToString(), 1);
        //            listShopChar[i].GetComponent<ItemManager>().listStatus[checkAgain].SetSiblingIndex(1);
        //        }
        //    }
        //}
    }

    public void ChooseBallButton()
    {
        int index = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        int check = PlayerPrefs.GetInt("Ball" + index.ToString());
        if (check == 0)
        {
            var objectTarget = EventSystem.current.currentSelectedGameObject.transform.GetComponent<ItemManager>().listStatus[0];
            string priceS = objectTarget.transform.GetChild(0).GetComponent<Text>().text;
            int price = int.Parse(priceS);
            if (coin >= price)
            {
                coin -= price;
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.SetInt("Ball" + index.ToString(), 1);
                objectTarget.transform.SetSiblingIndex(0);
                PlayerPrefs.SetInt("CurrentBall", index);
            }
        }
        if (check == 2)
        {
            PlayerPrefs.SetInt("CurrentBall", index);
            PlayerPrefs.SetInt("Ball" + index, 1);
        }

        //for (int i = 0; i < listShopBall.Count; i++)
        //{
        //    var checkAgain = PlayerPrefs.GetInt("Ball" + i.ToString());
        //    if(checkAgain == 2)
        //    {
        //        var checkCurrent = PlayerPrefs.GetInt("CurrentBall");
        //        if(i == checkCurrent)
        //        {
        //            listShopBall[i].GetComponent<ItemManager>().listStatus[checkAgain].SetAsLastSibling();
        //        }
        //        else
        //        {
        //            PlayerPrefs.SetInt("Ball" + i.ToString(), 1);
        //            listShopBall[i].GetComponent<ItemManager>().listStatus[checkAgain].SetSiblingIndex(1);
        //        }
        //    }
        //}
    }
}
