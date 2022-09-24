using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;

public class Transition : MonoBehaviour
{
    //�w�肵���V�[���ɑJ�ڂ���X�N���v�g
    [Header("�g�p�V�[����")]
    public string scene_genzai= "GB_Main";
    [Header("�J�ڐ�V�[����")]
    public string scene_name= "GB_Main";
    [Header("���݂Ɠ����V�[���ɑJ��")]
    public bool onajiScene = false;
    [Header("�J�ڐ���W")]
    public Vector2 pos;
    [Header("�I�u�W�F�N�g�ڐG�őJ�ڂ��邩�H")]
    public bool contact = false;
    [Header("�t�F�[�h�C���E�t�F�[�h�A�E�g����")]
    public bool fade = true;
    [Header("�t�F�[�h�C���E�t�F�[�h�A�E�g�����ꍇ�̑҂�����")]
    public float waitTime = 1.0f;
    [Header("�O���[�o���ϐ�")]
    public GlobalVariables_ScriptableObject globalVariables;

    //�J�ڂ��������Q�[���I�u�W�F�N�g�i�v���C���[�j
    GameObject obj;
    PlayerControlGB playerControl;

    //�J����
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
            //�J�ڑO�̃V�[�������폜�V�[�����Ɋi�[
            globalVariables.delScene_name = globalVariables.nowScene_name;
            //�J�ڐ�̃V�[���������݃V�[�����Ɋi�[
            globalVariables.nowScene_name = scene_name;
        }

        Debug.Log("���݂̃V�[����"+ globalVariables.nowScene_name+" �폜�����V�[���� "+ globalVariables.delScene_name);
        //�J�ڃV�[�������[�h
        globalVariables.controller = false;
        globalVariables.obj_controller = false;
        if(onajiScene==false) SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.7f);
        //�t�F�[�h�A�E�g
        if (fade == true)
        {
            
            proCamera2DTransitionsFX.TransitionExit();
            yield return new WaitForSeconds(waitTime);
        }
        playerControl.settiSoundFlag = false;
        obj.transform.position = pos;
        //���݂̃V�[�����폜�i�����J�ڑO��GB_Main��������폜���Ȃ��j
        if (onajiScene == false & globalVariables.delScene_name!= "GB_Main") SceneManager.UnloadSceneAsync(globalVariables.delScene_name);
        //�t�F�[�h�C��
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
