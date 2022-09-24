using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class Menu_mainScript : MonoBehaviour
{
    [Header("UIパネル")]
    public GameObject uiPanel;
    [Header("メニューウィンドウ")]
    public Image menuImage;
    [Header("メニューカーソル")]
    public Image menuCursor;
    [Header("メニュー名称ウィンドウ")]
    public Image menuNameImage;
    [Header("メニュー名称テキスト")]
    public TextMeshProUGUI menuName;
    [Header("アイテムウィンドウ")]
    public Image ListImage;
    [Header("アイテムカーソル")]
    public Image ListCursor;
    [Header("アイテム名称テキスト")]
    public TextMeshProUGUI itemName;
    [Header("説明文フォント")]
    public TextMeshProUGUI setumeibun;
    [Header("ステータスウィンドウ")]
    public Image StateImage;

    [Header("グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("アイテムデータベース")]
    public ItemDataBaseGB itemDataBase;
    [Header("メニューカーソル位置")]
    public List<Vector2> menuCursorPos = new List<Vector2>();
    [Header("アイテムカーソル位置")]
    public List<Vector2> itemCursorPos = new List<Vector2>();
    [Header("メニュー名称")]
    public List<string> menuNameStr = new List<string>();
    [Header("アイテムアイコン")]
    public List<Image> itemIcon = new List<Image>();
    [Header("魔法アイコン")]
    public List<Image> magicIcon = new List<Image>();
    [Header("防具アイコン")]
    public List<Image> bouguIcon = new List<Image>();
    [Header("アクセサリーアイコン")]
    public List<Image> akuseIcon = new List<Image>();
    [Header("サウンドマネージャーオブジェクト")]
    public GameObject soundManagerObj;
    [Header("プレイヤーコントロール")]
    public PlayerControlGB playerControl;

    //マップイメージ
    GameObject mapobj;
    GameObject MapImage;

    MessageMini messageMini;
    //サウンドマネージャーのコンポーネント取得

    SoundManagerScipt soundManager;
    AudioSource audioSource;

    //表示・非表示フラグ
    int menuFlag=0;

    //ウェイト
    float WaitTime = 0.1f;
    //十字キー受け付けウェイト
    int WaitKeyTime = 45;
    //代入用
    int count = 0;
    float dy = 0;
    float dx = 0;

    //マップ読み込みフラグ
    bool mapLoad= true;

    //十字キー選択のウェイトフラグ
    bool WaitKeyFlag = false;

    //ボタン名
    [SerializeField]
    string buttonName = "Start";
    [SerializeField]
    string buttonA = "Fire1";
    [SerializeField]
    string buttonB = "Fire2";

    //ボタン押し中のフラグ（暴発防止）
    bool ButtonDown = false;




    //カーソルRectTransform代入用
    RectTransform menuC, listC;

    // Start is called before the first frame update
    void Start()
    {
        //サウンドマネージャー取得
        soundManager = soundManagerObj.GetComponent<SoundManagerScipt>();
        audioSource = soundManagerObj.GetComponent<AudioSource>();

        //各カーソルの初期位置を設定
        globalVariables.menuCount = 0;
        menuC = menuCursor.GetComponent<RectTransform>();
        listC = ListCursor.GetComponent<RectTransform>();
        menuC.localPosition = new Vector3(menuCursorPos[globalVariables.menuCount].x - 640, menuCursorPos[globalVariables.menuCount].y + 360, 0f);
        menuName.text = menuNameStr[globalVariables.menuCount];
        Debug.Log("menuCursorPosの数"+menuCursorPos.Count);

        //メッセージ表示ウィンドウ＆テキスト取得
        messageMini = GetComponent<MessageMini>();

        //マップイメージ取得
        MapImage = GameObject.FindWithTag("Map_Panel");
        MapImage.gameObject.SetActive(false);
        Debug.Log("マップの名前" + MapImage);
        //MapImage = mapobj.GetComponent<GameObject>();

    }




    // Update is called once per frame
    void Update()
    {

        StartCoroutine(MenuAction());
        
    }

    IEnumerator MenuAction()
    {
        if (menuFlag == 0)
        {
            //Debug.Log(menuFlag);
            //メニュー表示
            if (Input.GetButtonDown(buttonName))
            {
                //オープン効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.kettei_sound);
                //メニュー画面再描画
                uiPanel.gameObject.SetActive(true);
                menuCursor.gameObject.SetActive(true);
                ListImage.gameObject.SetActive(false);
                MapImage.gameObject.SetActive(false);
                IconFalse();

                globalVariables.controller = false;
                globalVariables.obj_controller = false;
                WaitKeyFlag = false;
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 1;
            }
            
            
        }
        if (menuFlag == 1)
        {
            //メニュー画面再描画
            StateImage.gameObject.SetActive(true);
            menuNameImage.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(false);
            MapImage.gameObject.SetActive(false);
            //メニューセレクト
            dy = Input.GetAxisRaw("Vertical");
            if (dy == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);
                globalVariables.menuCount++;
                if (globalVariables.menuCount >= menuCursorPos.Count)
                {
                    globalVariables.menuCount = 0;
                }
                menuC.localPosition = new Vector3(menuCursorPos[globalVariables.menuCount].x - 640, menuCursorPos[globalVariables.menuCount].y + 360, 0f);
                menuName.text = menuNameStr[globalVariables.menuCount];
                //Debug.Log("下方向へキー受け付け中" + globalVariables.menuCount);
                WaitKeyFlag = true;
            }
            if (dy == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);
                globalVariables.menuCount--;
                if (globalVariables.menuCount < 0)
                {
                    globalVariables.menuCount = menuCursorPos.Count - 1;
                }
                menuC.localPosition = new Vector3(menuCursorPos[globalVariables.menuCount].x - 640, menuCursorPos[globalVariables.menuCount].y + 360, 0f);
                menuName.text = menuNameStr[globalVariables.menuCount];
                //Debug.Log("上方向へキー受け付け中" + globalVariables.menuCount);
                WaitKeyFlag = true;
                
            }

            if (WaitKeyFlag == true)
            {
                WaitKey();
            }
            //メニュー消去
            if (Input.GetButtonDown(buttonName) | Input.GetButtonDown(buttonB))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }
            //メニュー決定
            if (Input.GetButtonDown(buttonA))
            {
                //アイテムへ
                if (globalVariables.menuCount == 0)
                {
                    //オープン効果音
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //現在カーソル＆文字描画
                    ItemNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 2;

                }
                //魔法へ
                if (globalVariables.menuCount == 1)
                {
                    //オープン効果音
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //現在カーソル＆文字描画
                    MagicNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 3;

                }
                //防具へ
                if (globalVariables.menuCount == 2)
                {
                    //オープン効果音
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //現在カーソル＆文字描画
                    BouguNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 4;

                }
                //アクセサリーへ
                if (globalVariables.menuCount == 3)
                {
                    //オープン効果音
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //現在カーソル＆文字描画
                    AkuseNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 5;

                }
                //マップへ
                if (globalVariables.menuCount == 4)
                {
                    //オープン効果音
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //現在カーソル＆文字描画
                    AkuseNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 6;

                }
            }
            
        }
        if (menuFlag == 2)
        {

            //アイテム欄
            //Debug.Log("アイテム欄開いた");
            //再描画
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Itemicon();
            //Y方向セレクト
            dy = Input.GetAxisRaw("Vertical");
            if (dy == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.itemCount)
                {
                    case 0:
                        globalVariables.itemCount = 4;
                        break;
                    case 1:
                        globalVariables.itemCount = 5;
                        break;
                    case 2:
                        globalVariables.itemCount = 6;
                        break;
                    case 3:
                        globalVariables.itemCount = 3;
                        break;
                    case 4:
                        globalVariables.itemCount = 0;
                        break;
                    case 5:
                        globalVariables.itemCount = 1;
                        break;
                    case 6:
                        globalVariables.itemCount = 2;
                        break;
                    case 7:
                        globalVariables.itemCount = 3;
                        break;
                }

                ItemNameAndCursor();
            }
            if (dy == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.itemCount)
                {
                    case 0:
                        globalVariables.itemCount = 4;
                        break;
                    case 1:
                        globalVariables.itemCount = 5;
                        break;
                    case 2:
                        globalVariables.itemCount = 6;
                        break;
                    case 3:
                        globalVariables.itemCount = 3;
                        break;
                    case 4:
                        globalVariables.itemCount = 0;
                        break;
                    case 5:
                        globalVariables.itemCount = 1;
                        break;
                    case 6:
                        globalVariables.itemCount = 2;
                        break;
                    case 7:
                        globalVariables.itemCount = 3;
                        break;
                }

                ItemNameAndCursor();
            }

            //X方向セレクト
            dx = Input.GetAxisRaw("Horizontal");
            if (dx == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.itemCount)
                {
                    case 0:
                        globalVariables.itemCount = 1;
                        break;
                    case 1:
                        globalVariables.itemCount = 2;
                        break;
                    case 2:
                        globalVariables.itemCount = 3;
                        break;
                    case 3:
                        globalVariables.itemCount = 4;
                        break;
                    case 4:
                        globalVariables.itemCount = 5;
                        break;
                    case 5:
                        globalVariables.itemCount = 6;
                        break;
                    case 6:
                        globalVariables.itemCount = 0;
                        break;
                    case 7:
                        globalVariables.itemCount = 0;
                        break;
                }

                ItemNameAndCursor();
            }
            if (dx == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.itemCount)
                {
                    case 0:
                        globalVariables.itemCount = 6;
                        break;
                    case 1:
                        globalVariables.itemCount = 0;
                        break;
                    case 2:
                        globalVariables.itemCount = 1;
                        break;
                    case 3:
                        globalVariables.itemCount = 2;
                        break;
                    case 4:
                        globalVariables.itemCount = 3;
                        break;
                    case 5:
                        globalVariables.itemCount = 4;
                        break;
                    case 6:
                        globalVariables.itemCount = 5;
                        break;
                    case 7:
                        globalVariables.itemCount = 6;
                        break;
                }

                ItemNameAndCursor();
            }

                if (WaitKeyFlag == true)
            {
                WaitKey();
            }

            //アイテム決定
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.item_shoji[globalVariables.itemCount] > 0)
                {
                    if (itemDataBase.itemYouto[globalVariables.itemCount] == ItemDataBaseGB.ItemYouto.Kaihuku)
                    {
                        //回復効果音（itemDataBaseで指定）
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cursor_sound);
                        //回復処理
                        globalVariables.hp += itemDataBase.item_hp[globalVariables.itemCount];
                        globalVariables.hp = Mathf.Clamp(globalVariables.hp, 0, globalVariables.hp_max);
                        globalVariables.mp += itemDataBase.item_mp[globalVariables.itemCount];
                        globalVariables.mp = Mathf.Clamp(globalVariables.mp, 0, globalVariables.mp_max);
                        //UI再描画
                        playerControl.UIdraw();
                        //所持数を1つ減らす
                        itemDataBase.item_shoji[globalVariables.itemCount] -= 1;
                        //アイコン再描画
                        ItemNameAndCursor();
                    }
                    else
                    {
                        //キャンセル効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        //メッセージ表示
                        messageMini.ItemGet("持ってるだけで効果アリです");

                    }
                }

            }

            //メニュー1つ前に戻る
            if (Input.GetButtonDown(buttonB))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //メニュー全消去
            if (Input.GetButtonDown(buttonName))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //ボタンを離したら暴発フラグ解除
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 3)
        {

            //アイテム欄
            //Debug.Log("アイテム欄開いた");
            //再描画
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Magicicon();
            //Y方向セレクト
            dy = Input.GetAxisRaw("Vertical");
            if (dy == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.magicCount)
                {
                    case 0:
                        globalVariables.magicCount = 0;
                        break;
                    case 1:
                        globalVariables.magicCount = 1;
                        break;
                    case 2:
                        globalVariables.magicCount = 2;
                        break;
                    case 3:
                        globalVariables.magicCount = 3;
                        break;
                    case 4:
                        globalVariables.magicCount = 0;
                        break;
                    case 5:
                        globalVariables.magicCount = 1;
                        break;
                    case 6:
                        globalVariables.magicCount = 2;
                        break;
                    case 7:
                        globalVariables.magicCount = 3;
                        break;
                }

                MagicNameAndCursor();
            }
            if (dy == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.magicCount)
                {
                    case 0:
                        globalVariables.magicCount = 0;
                        break;
                    case 1:
                        globalVariables.magicCount = 1;
                        break;
                    case 2:
                        globalVariables.magicCount = 2;
                        break;
                    case 3:
                        globalVariables.magicCount = 3;
                        break;
                    case 4:
                        globalVariables.magicCount = 0;
                        break;
                    case 5:
                        globalVariables.magicCount = 1;
                        break;
                    case 6:
                        globalVariables.magicCount = 2;
                        break;
                    case 7:
                        globalVariables.magicCount = 3;
                        break;
                }

                MagicNameAndCursor();
            }

            //X方向セレクト
            dx = Input.GetAxisRaw("Horizontal");
            if (dx == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.magicCount)
                {
                    case 0:
                        globalVariables.magicCount = 1;
                        break;
                    case 1:
                        globalVariables.magicCount = 2;
                        break;
                    case 2:
                        globalVariables.magicCount = 3;
                        break;
                    case 3:
                        globalVariables.magicCount = 0;
                        break;
                    case 4:
                        globalVariables.magicCount = 5;
                        break;
                    case 5:
                        globalVariables.magicCount = 6;
                        break;
                    case 6:
                        globalVariables.magicCount = 0;
                        break;
                    case 7:
                        globalVariables.magicCount = 0;
                        break;
                }

                MagicNameAndCursor();
            }
            if (dx == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.magicCount)
                {
                    case 0:
                        globalVariables.magicCount = 3;
                        break;
                    case 1:
                        globalVariables.magicCount = 0;
                        break;
                    case 2:
                        globalVariables.magicCount = 1;
                        break;
                    case 3:
                        globalVariables.magicCount = 2;
                        break;
                    case 4:
                        globalVariables.magicCount = 3;
                        break;
                    case 5:
                        globalVariables.magicCount = 4;
                        break;
                    case 6:
                        globalVariables.magicCount = 5;
                        break;
                    case 7:
                        globalVariables.magicCount = 6;
                        break;
                }

                MagicNameAndCursor();
            }

            if (WaitKeyFlag == true)
            {
                WaitKey();
            }

            //魔法装備決定
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.magic_shoji[globalVariables.magicCount] > 0)
                {


                    //装備（今装備してるアイテムを再度クリックすると外す）
                    if(itemDataBase.soubi_magic== globalVariables.magicCount)
                    {
                        //効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        itemDataBase.soubi_magic = -1;
                        Debug.Log("装備を外した");
                    }
                    else
                    {
                        //効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.kettei_sound);
                        itemDataBase.soubi_magic = globalVariables.magicCount;
                        Debug.Log(itemDataBase.soubi_magic+"を装備した");
                    }
                }
                //UI再描画
                playerControl.UIdraw();
                playerControl.Soubi();
            }

            //メニュー1つ前に戻る
            if (Input.GetButtonDown(buttonB))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //メニュー全消去
            if (Input.GetButtonDown(buttonName))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //ボタンを離したら暴発フラグ解除
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 4)
        {

            //アイテム欄
            //Debug.Log("アイテム欄開いた");
            //再描画
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Bouguicon();
            //Y方向セレクト
            dy = Input.GetAxisRaw("Vertical");
            if (dy == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.bouguCount)
                {
                    case 0:
                        globalVariables.bouguCount = 4;
                        break;
                    case 1:
                        globalVariables.bouguCount = 5;
                        break;
                    case 2:
                        globalVariables.bouguCount = 2;
                        break;
                    case 3:
                        globalVariables.bouguCount = 3;
                        break;
                    case 4:
                        globalVariables.bouguCount = 0;
                        break;
                    case 5:
                        globalVariables.bouguCount = 1;
                        break;
                    case 6:
                        globalVariables.bouguCount = 2;
                        break;
                    case 7:
                        globalVariables.bouguCount = 3;
                        break;
                }

                BouguNameAndCursor();
            }
            if (dy == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.bouguCount)
                {
                    case 0:
                        globalVariables.bouguCount = 4;
                        break;
                    case 1:
                        globalVariables.bouguCount = 5;
                        break;
                    case 2:
                        globalVariables.bouguCount = 2;
                        break;
                    case 3:
                        globalVariables.bouguCount = 3;
                        break;
                    case 4:
                        globalVariables.bouguCount = 0;
                        break;
                    case 5:
                        globalVariables.bouguCount = 1;
                        break;
                    case 6:
                        globalVariables.bouguCount = 2;
                        break;
                    case 7:
                        globalVariables.bouguCount = 3;
                        break;
                }

                BouguNameAndCursor();
            }

            //X方向セレクト
            dx = Input.GetAxisRaw("Horizontal");
            if (dx == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.bouguCount)
                {
                    case 0:
                        globalVariables.bouguCount = 1;
                        break;
                    case 1:
                        globalVariables.bouguCount = 2;
                        break;
                    case 2:
                        globalVariables.bouguCount = 3;
                        break;
                    case 3:
                        globalVariables.bouguCount = 4;
                        break;
                    case 4:
                        globalVariables.bouguCount = 5;
                        break;
                    case 5:
                        globalVariables.bouguCount = 0;
                        break;
                    case 6:
                        globalVariables.bouguCount = 0;
                        break;
                    case 7:
                        globalVariables.bouguCount = 0;
                        break;
                }

                BouguNameAndCursor();
            }
            if (dx == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.bouguCount)
                {
                    case 0:
                        globalVariables.bouguCount = 5;
                        break;
                    case 1:
                        globalVariables.bouguCount = 0;
                        break;
                    case 2:
                        globalVariables.bouguCount = 1;
                        break;
                    case 3:
                        globalVariables.bouguCount = 2;
                        break;
                    case 4:
                        globalVariables.bouguCount = 3;
                        break;
                    case 5:
                        globalVariables.bouguCount = 4;
                        break;
                    case 6:
                        globalVariables.bouguCount = 5;
                        break;
                    case 7:
                        globalVariables.bouguCount = 6;
                        break;
                }

                BouguNameAndCursor();
            }

            if (WaitKeyFlag == true)
            {
                WaitKey();
            }

            //防具装備決定
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.bougu_shoji[globalVariables.bouguCount] > 0)
                {


                    //装備（今装備してるアイテムを再度クリックすると外す）
                    if (itemDataBase.soubi_bougu == globalVariables.bouguCount)
                    {
                        //効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        itemDataBase.soubi_bougu = -1;
                        Debug.Log("装備を外した");
                    }
                    else
                    {
                        //効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.kettei_sound);
                        itemDataBase.soubi_bougu = globalVariables.bouguCount;
                        Debug.Log(itemDataBase.soubi_bougu + "を装備した");
                    }
                }
                //UI再描画
                playerControl.UIdraw();
                playerControl.Soubi();
            }

            //メニュー1つ前に戻る
            if (Input.GetButtonDown(buttonB))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //メニュー全消去
            if (Input.GetButtonDown(buttonName))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //ボタンを離したら暴発フラグ解除
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 5)
        {

            //アイテム欄
            //Debug.Log("アイテム欄開いた");
            //再描画
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Akuseicon();
            //Y方向セレクト
            dy = Input.GetAxisRaw("Vertical");
            if (dy == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.akuseCount)
                {
                    case 0:
                        globalVariables.akuseCount = 4;
                        break;
                    case 1:
                        globalVariables.akuseCount = 5;
                        break;
                    case 2:
                        globalVariables.akuseCount = 6;
                        break;
                    case 3:
                        globalVariables.akuseCount = 7;
                        break;
                    case 4:
                        globalVariables.akuseCount = 0;
                        break;
                    case 5:
                        globalVariables.akuseCount = 1;
                        break;
                    case 6:
                        globalVariables.akuseCount = 2;
                        break;
                    case 7:
                        globalVariables.akuseCount = 3;
                        break;
                }

                AkuseNameAndCursor();
            }
            if (dy == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.akuseCount)
                {
                    case 0:
                        globalVariables.akuseCount = 4;
                        break;
                    case 1:
                        globalVariables.akuseCount = 5;
                        break;
                    case 2:
                        globalVariables.akuseCount = 6;
                        break;
                    case 3:
                        globalVariables.akuseCount = 7;
                        break;
                    case 4:
                        globalVariables.akuseCount = 0;
                        break;
                    case 5:
                        globalVariables.akuseCount = 1;
                        break;
                    case 6:
                        globalVariables.akuseCount = 2;
                        break;
                    case 7:
                        globalVariables.akuseCount = 3;
                        break;
                }

                AkuseNameAndCursor();
            }

            //X方向セレクト
            dx = Input.GetAxisRaw("Horizontal");
            if (dx == 1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.akuseCount)
                {
                    case 0:
                        globalVariables.akuseCount = 1;
                        break;
                    case 1:
                        globalVariables.akuseCount = 2;
                        break;
                    case 2:
                        globalVariables.akuseCount = 3;
                        break;
                    case 3:
                        globalVariables.akuseCount = 4;
                        break;
                    case 4:
                        globalVariables.akuseCount = 5;
                        break;
                    case 5:
                        globalVariables.akuseCount = 6;
                        break;
                    case 6:
                        globalVariables.akuseCount = 7;
                        break;
                    case 7:
                        globalVariables.akuseCount = 0;
                        break;
                }

                AkuseNameAndCursor();
            }
            if (dx == -1 & WaitKeyFlag == false)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cursor_sound);

                switch (globalVariables.akuseCount)
                {
                    case 0:
                        globalVariables.akuseCount = 7;
                        break;
                    case 1:
                        globalVariables.akuseCount = 0;
                        break;
                    case 2:
                        globalVariables.akuseCount = 1;
                        break;
                    case 3:
                        globalVariables.akuseCount = 2;
                        break;
                    case 4:
                        globalVariables.akuseCount = 3;
                        break;
                    case 5:
                        globalVariables.akuseCount = 4;
                        break;
                    case 6:
                        globalVariables.akuseCount = 5;
                        break;
                    case 7:
                        globalVariables.akuseCount = 6;
                        break;
                }

                AkuseNameAndCursor();
            }

            if (WaitKeyFlag == true)
            {
                WaitKey();
            }

            //アクセサリー装備決定
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.akuse_shoji[globalVariables.akuseCount] > 0)
                {


                    //装備（今装備してるアイテムを再度クリックすると外す）
                    if (itemDataBase.soubi_akuse == globalVariables.akuseCount)
                    {
                        //効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        itemDataBase.soubi_akuse = -1;
                        Debug.Log("装備を外した");
                    }
                    else
                    {
                        //効果音
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.kettei_sound);
                        itemDataBase.soubi_akuse = globalVariables.akuseCount;
                        Debug.Log(itemDataBase.soubi_akuse + "を装備した");
                    }
                }
                //UI再描画
                playerControl.UIdraw();
                playerControl.Soubi();
            }

            //メニュー1つ前に戻る
            if (Input.GetButtonDown(buttonB))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //メニュー全消去
            if (Input.GetButtonDown(buttonName))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //ボタンを離したら暴発フラグ解除
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 6)
        {

            //メニュー欄
            //Debug.Log("アイテム欄開いた");
            //再描画
            MapImage.gameObject.SetActive(true);
            StateImage.gameObject.SetActive(true);
            menuNameImage.gameObject.SetActive(true);
            ListCursor.gameObject.SetActive(false);
            ListImage.gameObject.SetActive(false);
            IconFalse();

            //メニュー1つ前に戻る
            if (Input.GetButtonDown(buttonB))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //メニュー全消去
            if (Input.GetButtonDown(buttonName))
            {
                //メニュー非表示フラグON
                //キャンセル効果音
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //ボタンを離したら暴発フラグ解除
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 99)
        {

            //メニュー非表示
            uiPanel.gameObject.SetActive(false);
            menuCursor.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(false);
            MapImage.gameObject.SetActive(false);
            globalVariables.controller = true;
            globalVariables.obj_controller = true;
            yield return new WaitForSeconds(WaitTime);
            menuFlag = 0;
        }
    }

    void ItemNameAndCursor()
    {
        //カーソル表示
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.itemCount].x - 640, itemCursorPos[globalVariables.itemCount].y + 360, 0f);
        //アイテムを持っていたら表示
        if (itemDataBase.item_shoji[globalVariables.itemCount] == 0)
        {
            //名称、説明文を描画
            itemName.text = "";
            setumeibun.text = "";
            itemIcon[globalVariables.itemCount].gameObject.SetActive(false);
        }
        else
        {
            //名称、説明文を描画
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.itemName[globalVariables.itemCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.item_shoji[globalVariables.itemCount].ToString());
            builder.Append("個");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.item_setumeibun[globalVariables.itemCount];
            itemIcon[globalVariables.itemCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }

    void MagicNameAndCursor()
    {
        //カーソル表示
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.magicCount].x - 640, itemCursorPos[globalVariables.magicCount].y + 360, 0f);
        //魔法を持っていたら表示
        if (itemDataBase.magic_shoji[globalVariables.magicCount] == 0)
        {
            //名称、説明文を描画
            itemName.text = "";
            setumeibun.text = "";
            magicIcon[globalVariables.magicCount].gameObject.SetActive(false);
        }
        else
        {
            //名称、説明文を描画
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.magicName[globalVariables.magicCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.magic_shoji[globalVariables.magicCount].ToString());
            builder.Append("個");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.magic_setumeibun[globalVariables.magicCount];
            magicIcon[globalVariables.magicCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }

    void BouguNameAndCursor()
    {
        //カーソル表示
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.bouguCount].x - 640, itemCursorPos[globalVariables.bouguCount].y + 360, 0f);
        //防具を持っていたら表示
        if (itemDataBase.bougu_shoji[globalVariables.bouguCount] == 0)
        {
            //名称、説明文を描画
            itemName.text = "";
            setumeibun.text = "";
            bouguIcon[globalVariables.bouguCount].gameObject.SetActive(false);
        }
        else
        {
            //名称、説明文を描画
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.bouguName[globalVariables.bouguCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.bougu_shoji[globalVariables.bouguCount].ToString());
            builder.Append("個");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.bougu_setumeibun[globalVariables.bouguCount];
            bouguIcon[globalVariables.bouguCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }

    void AkuseNameAndCursor()
    {
        //カーソル表示
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.akuseCount].x - 640, itemCursorPos[globalVariables.akuseCount].y + 360, 0f);
        //魔法を持っていたら表示
        if (itemDataBase.akuse_shoji[globalVariables.akuseCount] == 0)
        {
            //名称、説明文を描画
            itemName.text = "";
            setumeibun.text = "";
            akuseIcon[globalVariables.akuseCount].gameObject.SetActive(false);
        }
        else
        {
            //名称、説明文を描画
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.akuseName[globalVariables.akuseCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.akuse_shoji[globalVariables.akuseCount].ToString());
            builder.Append("個");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.akuse_setumeibun[globalVariables.akuseCount];
            akuseIcon[globalVariables.akuseCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }
    void WaitKey()
    {

        //待ち時間
        count++;
        if (count >= WaitKeyTime)
        {
            count = 0;
            WaitKeyFlag = false;
        }
        Debug.Log("ループ" + count);

    }

    IEnumerator Wait()
    {
        //()のフレーム数待つ
        yield return new WaitForSeconds(WaitTime);
    }

    void IconFalse()
    {
        for(int i=0;i< itemIcon.Count; i++)
        {
            itemIcon[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < magicIcon.Count; i++)
        {
            magicIcon[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < bouguIcon.Count; i++)
        {
            bouguIcon[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < akuseIcon.Count; i++)
        {
            akuseIcon[i].gameObject.SetActive(false);
        }

    }

    void Itemicon()
    {
        for (int i = 0; i < itemIcon.Count; i++)
        {
            if(itemDataBase.item_shoji[i] == 0)
            {
                itemIcon[i].gameObject.SetActive(false);
            }
            else
            {
                itemIcon[i].gameObject.SetActive(true);
            }
            
        }
    }

    void Magicicon()
    {
        for (int i = 0; i < magicIcon.Count; i++)
        {
            if (itemDataBase.magic_shoji[i] == 0)
            {
                magicIcon[i].gameObject.SetActive(false);
            }
            else
            {
                magicIcon[i].gameObject.SetActive(true);
            }

        }
    }

    void Bouguicon()
    {
        for (int i = 0; i < bouguIcon.Count; i++)
        {
            if (itemDataBase.bougu_shoji[i] == 0)
            {
                bouguIcon[i].gameObject.SetActive(false);
            }
            else
            {
                bouguIcon[i].gameObject.SetActive(true);
            }

        }
    }

    void Akuseicon()
    {
        for (int i = 0; i < akuseIcon.Count; i++)
        {
            if (itemDataBase.akuse_shoji[i] == 0)
            {
                akuseIcon[i].gameObject.SetActive(false);
            }
            else
            {
                akuseIcon[i].gameObject.SetActive(true);
            }

        }
    }

    public void MapLoad()
    {
        MapImage = GameObject.FindWithTag("Map_Panel");
    }
}
