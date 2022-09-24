using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class ItemGetScript : MonoBehaviour
{
    [Header("グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("アイテムデータベース")]
    public ItemDataBaseGB itemDataBase;
    public enum Shurui
    {
        Item,Magic,Bougu,Akuse

    }
    public enum Action
    {
        None,Delete

    }
    [Header("接触したら起動")]
    public bool tri = false;
    [Header("種類")]
    public Shurui shurui;
    [Header("アイテム番号")]
    public int num;
    [Header("増減数")]
    public int zougen = 1;
    [Header("アイテムオブジェクト消去処理")]
    public bool action = false;
    [Header("アイテムゲットメッセージ")]
    public bool getMessage = false;

    //ゲームマネージャー
    GameObject gameManager;
    //サウンドマネージャーオブジェクト
    GameObject soundManagerObj;
    //サウンドマネージャー関係の変数
    SoundManagerScipt soundManager;
    AudioSource audioSource;

    MessageMini messageMini;
    string file_name;
    bool del;
    //カウント
    int count = 0;

    private void Start()
    {
        //ゲームマネージャー取得
        gameManager= GameObject.Find("GameManager");
        //サウンドマネージャー取得
        soundManagerObj = GameObject.Find("SoundManager");
        soundManager = soundManagerObj.GetComponent<SoundManagerScipt>();
        audioSource = soundManagerObj.GetComponent<AudioSource>();
        //メッセージ表示ウィンドウ＆テキスト取得
        messageMini = gameManager.GetComponent<MessageMini>();
        //ファイル名作成
        file_name = "Save/date";
        file_name += globalVariables.fileNum.ToString();
        file_name += ".es3";
        //アイテムオブジェクト消去済なら消去状態をロード
        if(del==true) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tri == true & collision.gameObject.tag == "Player")
        {
            //効果音
            audioSource.Stop();
            audioSource.PlayOneShot(soundManager.kettei_sound);
            //アイテムゲット処理
            ItemGet();
        }

    }

    private void Update()
    {

    }

    void ItemGet()
    {
        //アイテム
        if (shurui == Shurui.Item)
        {
            itemDataBase.item_shoji[num] += zougen;
            itemDataBase.item_shoji[num] = Mathf.Clamp(itemDataBase.item_shoji[num], 0, 99);
        }
        //魔法
        if (shurui == Shurui.Magic)
        {
            itemDataBase.magic_shoji[num] += zougen;
            itemDataBase.magic_shoji[num] = Mathf.Clamp(itemDataBase.magic_shoji[num], 0, 99);
        }
        //防具
        if (shurui == Shurui.Bougu)
        {
            itemDataBase.bougu_shoji[num] += zougen;
            itemDataBase.bougu_shoji[num] = Mathf.Clamp(itemDataBase.bougu_shoji[num], 0, 99);
        }
        //アクセサリー
        if (shurui == Shurui.Akuse)
        {
            itemDataBase.akuse_shoji[num] += zougen;
            itemDataBase.akuse_shoji[num] = Mathf.Clamp(itemDataBase.akuse_shoji[num], 0, 99);
        }
        //アイテムオブジェクト消去
        if (action == true)
        {
            Destroy(gameObject);
            del = true;
        }
        //アイテムゲットメッセージ表示
        if (getMessage == true)
        {
            if (shurui == Shurui.Item) messageMini.ItemGet(itemDataBase.itemName[num].ToString());
            if (shurui == Shurui.Magic) messageMini.ItemGet(itemDataBase.magicName[num].ToString());
            if (shurui == Shurui.Bougu) messageMini.ItemGet(itemDataBase.bouguName[num].ToString());
            if (shurui == Shurui.Akuse) messageMini.ItemGet(itemDataBase.akuseName[num].ToString());
        }
    }

}
