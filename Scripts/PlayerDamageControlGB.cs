using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerDamageControlGB : MonoBehaviour
{
    //プレイヤー取得用変数
    //GameObject plyer_object;
    //PlayerControlGB player;
    //無敵フラグ
    public bool muteki;
    [Header("ダメージ受けてからの無敵時間")]
    public int muteki_time = 60;
    public int count = 0;

    //PlayerControlGB
    PlayerControlGB playerControl;
    [Header("使用グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("ダメージポップアップ")]
    public GameObject damagePopUp;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControlGB>();
        muteki = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(muteki==true& count <= muteki_time)
        {
            count++;
            playerControl.Tween_damage_Play();
        }
        else
        {
            count = 0;
            muteki = false;
            playerControl.Tween_damage_Pause();


        }
    }

    public void Damage(int damage)
    {
        if(muteki==false)
        {
            //HP書き換え
            
            globalVariables.hp = Mathf.Clamp((globalVariables.hp- damage), 0, 999);
            //ダメージポップアップ
            string ddd = damage.ToString();
            GameObject pop = Instantiate(damagePopUp, transform.position, transform.rotation);
            pop.GetComponentInChildren<Text>().text = ddd;
            //UI再描画
            playerControl.UIdraw();




            Debug.Log("プレイヤーダメージ受けて無敵時間入る");
            muteki = true;
        }
    }
}
