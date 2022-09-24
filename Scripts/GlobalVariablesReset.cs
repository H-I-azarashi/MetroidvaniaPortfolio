using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariablesReset : MonoBehaviour
{
    [Header("使用グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("リセット先するグローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables1;
    [Header("使用アイテムデータベース")]
    public ItemDataBaseGB itemDataBase;
    [Header("リセット先する使用アイテムデータベース")]
    public ItemDataBaseGB itemDataBase1;
    [Header("使用マップScriptableObject")]
    public Map_ScriptableObject map_ScriptableObject;
    [Header("リセット先するマップScriptableObject")]
    public Map_ScriptableObject map_ScriptableObject1;
    [Header("使用経験値テーブル")]
    public ExpTableScript expTable;
    [Header("プレイヤー")]
    public GameObject player;

    //リセット処理したらシーン遷移してもリセットしないようにするフラグ
    bool resetFlag = false;

    //Player関係のコンポーネントを取得
    PlayerControlGB playerControl;

    public enum ItemYouto
    {
        Kaihuku, //回復
        KeyItem//キーアイテム

    }
    public enum Zokusei
    {
        None, //無属性
        Fire, //火属性
        Ice, //氷属性
        Thunder, //雷属性
        Jump2dan//二段ジャンプ

    }
    List<ItemYouto> itemYouto = new List<ItemYouto>();
    // Start is called before the first frame update
    void Start()
    {
        if (resetFlag == false)
        {
            playerControl = player.GetComponent<PlayerControlGB>();
            ResetVariables();
        }

    }



    private void ResetVariables()
    {

        //GlobalVariables_ScriptableObjectのリセット
        globalVariables1.lv = globalVariables.lv;
        globalVariables1.hp_max = globalVariables.hp_max;
        globalVariables1.hp = globalVariables.hp;
        globalVariables1.mp_max = globalVariables.mp_max;
        globalVariables1.mp = globalVariables.mp;
        globalVariables1.str = globalVariables.str;
        globalVariables1.def = globalVariables.def;
        globalVariables1.keiken = globalVariables.keiken;
        globalVariables1.gold = globalVariables.gold;
        globalVariables1.exp = globalVariables.exp;
        globalVariables1.exp_total = globalVariables.exp_total;
        globalVariables1.inrando = globalVariables.inrando;
        globalVariables1.h = globalVariables.h;
        globalVariables1.sekuhara = globalVariables.sekuhara;
        globalVariables1.fera = globalVariables.fera;
        globalVariables1.sex = globalVariables.sex;
        globalVariables1.anal = globalVariables.anal;
        globalVariables1.controller = globalVariables.controller;
        globalVariables1.obj_controller = globalVariables.obj_controller;
        globalVariables1.bukishoji = globalVariables.bukishoji;
        globalVariables1.menuCount = globalVariables.menuCount;
        globalVariables1.itemCount = globalVariables.itemCount;
        globalVariables1.magicCount = globalVariables.magicCount;
        globalVariables1.bouguCount = globalVariables.bouguCount;
        globalVariables1.akuseCount = globalVariables.akuseCount;
        globalVariables1.bgmVolume = globalVariables.bgmVolume;
        globalVariables1.seVolume = globalVariables.seVolume;
        globalVariables1.fileNum = globalVariables.fileNum;
        globalVariables1.r18 = globalVariables.r18;
        globalVariables1.transitionFlag = globalVariables.transitionFlag;
        globalVariables1.nowScene_name = globalVariables.nowScene_name;
        globalVariables1.delScene_name = globalVariables.delScene_name;

        //ItemDataBaseGBのリセット
        itemDataBase1.itemName= new List<string>(itemDataBase.itemName);
        itemDataBase1.item_setumeibun = new List<string>(itemDataBase.item_setumeibun);
        itemDataBase1.item_hp = new List<int>(itemDataBase.item_hp);
        itemDataBase1.item_mp = new List<int>(itemDataBase.item_mp);
        itemDataBase1.itemYouto= new List<ItemDataBaseGB.ItemYouto>(itemDataBase.itemYouto);

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


        //経験値テーブルをセット
        globalVariables1.exp = expTable.nextExp[globalVariables.lv];

        //マップScriptableObjectのリセット
        map_ScriptableObject1.map00= new List<bool>(map_ScriptableObject.map00);
        map_ScriptableObject1.map01 = new List<bool>(map_ScriptableObject.map01);
        map_ScriptableObject1.map02 = new List<bool>(map_ScriptableObject.map02);
        map_ScriptableObject1.map03 = new List<bool>(map_ScriptableObject.map03);
        map_ScriptableObject1.map04 = new List<bool>(map_ScriptableObject.map04);
        map_ScriptableObject1.map05 = new List<bool>(map_ScriptableObject.map05);
        map_ScriptableObject1.map06 = new List<bool>(map_ScriptableObject.map06);
        map_ScriptableObject1.map07 = new List<bool>(map_ScriptableObject.map07);
        map_ScriptableObject1.map08 = new List<bool>(map_ScriptableObject.map08);
        map_ScriptableObject1.map09 = new List<bool>(map_ScriptableObject.map09);
        map_ScriptableObject1.map10 = new List<bool>(map_ScriptableObject.map10);

        //UI反映
        playerControl.UIdraw();

        Debug.Log("グローバル変数リセット処理");
    }
}
