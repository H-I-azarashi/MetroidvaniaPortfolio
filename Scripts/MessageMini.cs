using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class MessageMini : MonoBehaviour
{

    [Header("ネームウィンドウ")]
    public Image nameWindow;
    [Header("ネームテキスト")]
    public TextMeshProUGUI nemeText;
    [Header("エネミー名前テキスト")]
    public TextMeshProUGUI enemy_name;
    [Header("エネミー情報テキスト")]
    public TextMeshProUGUI enemyInformation;
    [Header("テキストが消えるまでの時間")]
    public int textTime = 600;
    //カウント
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 0) count -= 1;
        if (count <= 0)
        {
            //0秒以下で画像消去
            nameWindow.gameObject.SetActive(false);
            nemeText.gameObject.SetActive(false);
            enemy_name.gameObject.SetActive(false);
            enemyInformation.gameObject.SetActive(false);

        }
    }

    public void ItemGet(string a)
    {

        //アイテムゲットメッセージ表示
        //カウントリセットして画像とテキスト表示
        count = textTime;
        nameWindow.gameObject.SetActive(true);
        nemeText.gameObject.SetActive(true);
        enemy_name.gameObject.SetActive(false);
        enemyInformation.gameObject.SetActive(false);

        nemeText.text = a;
    }

    public void EnemyInformation(string a,int lv,int hp,int hp_max)
    {
        //アイテムゲットメッセージ表示
        //カウントリセットして画像とテキスト表示
        count = textTime;
        enemy_name.text = a;
        StringBuilder builder = new StringBuilder();
        builder.Append("LV");
        builder.Append(lv.ToString());
        builder.Append(" HP");
        builder.Append(hp.ToString());
        builder.Append("/");
        builder.Append(hp_max.ToString());
        enemyInformation.text = builder.ToString();
        nameWindow.gameObject.SetActive(true);
        enemy_name.gameObject.SetActive(true);
        enemyInformation.gameObject.SetActive(true);
        nemeText.gameObject.SetActive(false);
    }
}
