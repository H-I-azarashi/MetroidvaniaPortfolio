using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;

public class Transition : MonoBehaviour
{
    //指定したシーンに遷移するスクリプト
    [Header("使用シーン名")]
    public string scene_genzai= "GB_Main";
    [Header("遷移先シーン名")]
    public string scene_name= "GB_Main";
    [Header("現在と同じシーンに遷移")]
    public bool onajiScene = false;
    [Header("遷移先座標")]
    public Vector2 pos;
    [Header("オブジェクト接触で遷移するか？")]
    public bool contact = false;
    [Header("フェードイン・フェードアウトする")]
    public bool fade = true;
    [Header("フェードイン・フェードアウトした場合の待ち時間")]
    public float waitTime = 1.0f;
    [Header("グローバル変数")]
    public GlobalVariables_ScriptableObject globalVariables;

    //遷移させたいゲームオブジェクト（プレイヤー）
    GameObject obj;
    PlayerControlGB playerControl;

    //カメラ
    GameObject maincamera;
    ProCamera2DTransitionsFX proCamera2DTransitionsFX;

    // Start is called before the first frame update
    void Start()
    {
        obj= GameObject.FindWithTag("Player");
        playerControl = obj.GetComponent<PlayerControlGB>();
        maincamera = GameObject.FindWithTag("MainCamera");
        proCamera2DTransitionsFX = maincamera.GetComponent<ProCamera2DTransitionsFX>();
        if (contact == false & SceneManager.GetActiveScene().name == scene_genzai) StartCoroutine(SceneWarp());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerDefence" & contact == true & SceneManager.GetActiveScene().name == scene_genzai) StartCoroutine(SceneWarp());
    }
    IEnumerator SceneWarp()
    {
        if (onajiScene == false)
        {
            //遷移前のシーン名を削除シーン名に格納
            globalVariables.delScene_name = globalVariables.nowScene_name;
            //遷移先のシーン名を現在シーン名に格納
            globalVariables.nowScene_name = scene_name;
        }

        Debug.Log("現在のシーン名"+ globalVariables.nowScene_name+" 削除したシーン名 "+ globalVariables.delScene_name);
        //遷移シーンをロード
        globalVariables.controller = false;
        globalVariables.obj_controller = false;
        if(onajiScene==false) SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.7f);
        //フェードアウト
        if (fade == true)
        {
            
            proCamera2DTransitionsFX.TransitionExit();
            yield return new WaitForSeconds(waitTime);
        }
        playerControl.settiSoundFlag = false;
        obj.transform.position = pos;
        //現在のシーンを削除（もし遷移前がGB_Mainだったら削除しない）
        if (onajiScene == false & globalVariables.delScene_name!= "GB_Main") SceneManager.UnloadSceneAsync(globalVariables.delScene_name);
        //フェードイン
        if (fade == true)
        {
            yield return new WaitForSeconds(waitTime);
            proCamera2DTransitionsFX.TransitionEnter();
        }
        globalVariables.controller = true;
        globalVariables.obj_controller = true;
    }

    public void PrologueWarp()
    {
        SceneManager.LoadScene("GB_Main");
        SceneManager.LoadScene("Forest2", LoadSceneMode.Additive);
        globalVariables.nowScene_name = "Forest2";
        proCamera2DTransitionsFX.TransitionEnter();
    }
}
