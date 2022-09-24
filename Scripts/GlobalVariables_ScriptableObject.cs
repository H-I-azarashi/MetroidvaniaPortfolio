using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GlobalVariables", menuName = "ScriptableObjects/GlobalVariables", order = 1)]
public class GlobalVariables_ScriptableObject : ScriptableObject
{
    //グローバル変数スクリプト

    [Header("レベル")]
    public int lv = 1;
    [Header("最大HP")]
    public int hp_max = 1;
    [Header("現在HP")]
    public int hp = 1;
    [Header("最大MP")]
    public int mp_max = 1;
    [Header("現在MP")]
    public int mp = 1;
    [Header("攻撃力")]
    public int str = 1;
    [Header("防御力")]
    public int def = 1;
    [Header("経験回数")]
    public int keiken = 0;
    [Header("所持金")]
    public int gold = 0;
    [Header("EXP")]
    public int exp = 0;
    [Header("累積EXP")]
    public int exp_total = 0;
    [Header("淫乱度")]
    public int inrando = 0;
    [Header("エッチ")]
    public int h = 0;
    [Header("セクハラ")]
    public int sekuhara = 0;
    [Header("フェラ")]
    public int fera = 0;
    [Header("セックス")]
    public int sex = 0;
    [Header("アナル")]
    public int anal = 0;
    [Header("遷移座標")]
    public Vector2 transitionPos;
    [Header("コントローラーの受付判定（trueで受け付け可能に）")]
    public bool controller = true;
    [Header("オブジェクト動作の受付判定（trueで受け付け可能に）")]
    public bool obj_controller = true;
    [Header("オブジェクト復活")]
    public bool reborn = false;
    [Header("オブジェクト全消去")]
    public bool dead = false;
    [Header("武器所持フラグ")]
    public bool bukishoji = false;
    [Header("メニューカーソルカウント")]
    public int menuCount = 0;
    [Header("アイテムカーソルカウント")]
    public int itemCount = 0;
    [Header("魔法カーソルカウント")]
    public int magicCount = 0;
    [Header("防具カーソルカウント")]
    public int bouguCount = 0;
    [Header("アクセサリーカーソルカウント")]
    public int akuseCount = 0;
    [Header("BGM音量")]
    public float bgmVolume = 1f;
    [Header("SE音量")]
    public float seVolume = 1f;
    [Header("ファイルNo")]
    public int fileNum = 0;
    [Header("健全モード（trueでR18モード）")]
    public bool r18;
    [Header("遷移フラグ")]
    public bool transitionFlag = false;
    [Header("現在シーン名")]
    public string nowScene_name = "GB_Main";
    [Header("削除シーン名")]
    public string delScene_name = "GB_Main";







}
