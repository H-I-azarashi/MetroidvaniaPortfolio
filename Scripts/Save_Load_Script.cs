using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Save_Load_Script : MonoBehaviour
{
    [Header("使用グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("使用アイテムデータベース")]
    public ItemDataBaseGB itemDataBase;
    [Header("使用経験値テーブル")]
    public ExpTableScript expTable;
    [Header("セーブ・ロード画面UI")]
    public Image saveUI;
    [Header("セーブ・ロード画面選択枠")]
    public Image saveUIwindow;
    [Header("会話アイコン")]
    GameObject kaiwaIcon;

    //プレイヤー
    GameObject player;
    //ファイルNo
    int fileNum = 0;
    //セーブ=false・ロード=true
    bool saveFlag = false;
    //上カーソル変数
    float dy = 0;
    //キー起動用フラグ
    bool kidouFlag = false;

    //ボタン名
    [SerializeField]
    string buttonA = "Fire1";
    [SerializeField]
    string buttonB = "Fire2";

    //Player関係のコンポーネントを取得
    PlayerControlGB playerControl;
    //ファイル名用文字変数
    string file_name;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerControlGB取得
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControlGB>();
        //ファイル名作成
        file_name = "Save/date";
        file_name += globalVariables.fileNum.ToString();
        file_name += ".es3";
        //セーブ・ロード処理
        if (saveFlag == false)
        {
            SaveAction();
        }
        else
        {
            LoadAction();
        }
    }

    private void Update()
    {
        //デバッグ用 キーでセーブ・ロードをテスト
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveAction();
            Debug.Log(file_name.ToString()+"にセーブした");
            playerControl.UIdraw();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadAction();
            Debug.Log(file_name.ToString() + "にロードした");
            playerControl.UIdraw();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //セーブポイントで上キーを押すとセーブ画面が出てくる
        dy = Input.GetAxisRaw("Vertical");
        if (dy == 1 & kidouFlag == false)
        {
            kidouFlag = true;
            saveUI.gameObject.SetActive(true);
        }
            
    }


    public void SaveAction()
    {
        //プレーヤー位置のセーブ
        ES3.Save<Transform>("TransformKey", player.transform, file_name);
        //GlobalVariables_ScriptableObjectのセーブ
        /*
        ES3.Save<int>("lv", globalVariables.lv, file_name);
        ES3.Save<int>("hp_max", globalVariables.hp_max, file_name);
        ES3.Save<int>("hp", globalVariables.hp, file_name);
        ES3.Save<int>("mp_max", globalVariables.mp_max, file_name);
        ES3.Save<int>("mp", globalVariables.mp, file_name);
        ES3.Save<int>("str", globalVariables.str, file_name);
        ES3.Save<int>("def", globalVariables.def, file_name);
        ES3.Save<int>("keiken", globalVariables.keiken, file_name);
        ES3.Save<int>("gold", globalVariables.gold, file_name);
        ES3.Save<int>("exp", globalVariables.exp, file_name);
        ES3.Save<int>("exp_total", globalVariables.exp_total, file_name);
        ES3.Save<int>("inrando", globalVariables.inrando, file_name);
        ES3.Save<int>("h", globalVariables.h, file_name);
        ES3.Save<int>("sekuhara", globalVariables.sekuhara, file_name);
        ES3.Save<int>("fera", globalVariables.fera, file_name);
        ES3.Save<int>("sex", globalVariables.sex, file_name);
        ES3.Save<int>("anal", globalVariables.anal, file_name);
        ES3.Save<bool>("controller", globalVariables.controller, file_name);
        ES3.Save<bool>("obj_controller", globalVariables.obj_controller, file_name);
        ES3.Save<bool>("bukishoji", globalVariables.bukishoji, file_name);
        ES3.Save<int>("menuCount", globalVariables.menuCount, file_name);
        ES3.Save<int>("itemCount", globalVariables.itemCount, file_name);
        ES3.Save<int>("magicCount", globalVariables.magicCount, file_name);
        ES3.Save<int>("bouguCount", globalVariables.bouguCount, file_name);
        ES3.Save<int>("akuseCount", globalVariables.akuseCount, file_name);
        ES3.Save<float>("bgmVolume", globalVariables.bgmVolume, file_name);
        ES3.Save<float>("seVolume", globalVariables.seVolume, file_name);
        ES3.Save<int>("fileNum", globalVariables.fileNum, file_name);
        ES3.Save<bool>("r18", globalVariables.r18, file_name);
        */
        ES3.Save<GlobalVariables_ScriptableObject>("glob", globalVariables, file_name);
        //ItemDataBaseGBのセーブ
        //ES3.Save <string[,,]>("itemName", globalVariables.itemName, file_name);
        ES3.Save<ItemDataBaseGB>("aaa", itemDataBase, file_name);

        /*
        itemDataBase1.itemName = new List<string>(itemDataBase.itemName);
        itemDataBase1.item_setumeibun = new List<string>(itemDataBase.item_setumeibun);
        itemDataBase1.item_hp = new List<int>(itemDataBase.item_hp);
        itemDataBase1.item_mp = new List<int>(itemDataBase.item_mp);
        itemDataBase1.itemYouto = new List<ItemDataBaseGB.ItemYouto>(itemDataBase.itemYouto);

        itemDataBase1.item_shoji = new List<int>(itemDataBase.item_shoji);
        itemDataBase1.buki_lv = itemDataBase.buki_lv;
        itemDataBase1.buki_atk = itemDataBase.buki_atk;
        itemDataBase1.buki_setumeibun = itemDataBase.buki_setumeibun;
        itemDataBase1.buki_shoji = itemDataBase.buki_shoji;

        itemDataBase1.bouguName = new List<string>(itemDataBase.bouguName);
        itemDataBase1.bougu_setumeibun = new List<string>(itemDataBase.bougu_setumeibun);
        itemDataBase1.bougu_lv = new List<int>(itemDataBase.bougu_lv);
        itemDataBase1.bougu_def = new List<int>(itemDataBase.bougu_def);
        itemDataBase1.bougu_shoji = new List<int>(itemDataBase.bougu_shoji);

        itemDataBase1.akuseName = new List<string>(itemDataBase.akuseName);
        itemDataBase1.akuse_setumeibun = new List<string>(itemDataBase.akuse_setumeibun);
        itemDataBase1.akuse_hp = new List<int>(itemDataBase.akuse_hp);
        itemDataBase1.akuse_mp = new List<int>(itemDataBase.akuse_mp);
        itemDataBase1.akuse_atk = new List<int>(itemDataBase.akuse_atk);
        itemDataBase1.akuse_def = new List<int>(itemDataBase.akuse_def);
        itemDataBase1.akuse_inrando = new List<int>(itemDataBase.akuse_inrando);
        itemDataBase1.akuse_shoji = new List<int>(itemDataBase.akuse_shoji);
        itemDataBase1.zokusei = new List<ItemDataBaseGB.Zokusei>(itemDataBase.zokusei);

        itemDataBase1.magicName = new List<string>(itemDataBase.magicName);
        itemDataBase1.magic_setumeibun = new List<string>(itemDataBase.magic_setumeibun);
        itemDataBase1.magic_atk = new List<int>(itemDataBase.magic_atk);
        itemDataBase1.magic_zokusei = new List<ItemDataBaseGB.Zokusei>(itemDataBase.magic_zokusei);
        itemDataBase1.magic_shoji = new List<int>(itemDataBase.magic_shoji);



        itemDataBase1.soubi_bougu = itemDataBase.soubi_bougu;
        itemDataBase1.soubi_akuse = itemDataBase.soubi_akuse;
        itemDataBase1.soubi_magic = itemDataBase.soubi_magic;
        */
    }

    public void LoadAction()
    {
        //プレーヤー位置のロード
        ES3.LoadInto<Transform>("TransformKey", file_name,player.transform);
        //GlobalVariables_ScriptableObjectのロード
        //globalVariables.lv = ES3.Load<int>("lv", file_name);
        globalVariables= ES3.Load<GlobalVariables_ScriptableObject>("glob", file_name);
        itemDataBase= ES3.Load<ItemDataBaseGB>("aaa", file_name);
    }
}
