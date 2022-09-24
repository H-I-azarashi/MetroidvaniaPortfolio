using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class MessageMini : MonoBehaviour
{

    [Header("�l�[���E�B���h�E")]
    public Image nameWindow;
    [Header("�l�[���e�L�X�g")]
    public TextMeshProUGUI nemeText;
    [Header("�G�l�~�[���O�e�L�X�g")]
    public TextMeshProUGUI enemy_name;
    [Header("�G�l�~�[���e�L�X�g")]
    public TextMeshProUGUI enemyInformation;
    [Header("�e�L�X�g��������܂ł̎���")]
    public int textTime = 600;
    //�J�E���g
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
            //0�b�ȉ��ŉ摜����
            nameWindow.gameObject.SetActive(false);
            nemeText.gameObject.SetActive(false);
            enemy_name.gameObject.SetActive(false);
            enemyInformation.gameObject.SetActive(false);

        }
    }

    public void ItemGet(string a)
    {

        //�A�C�e���Q�b�g���b�Z�[�W�\��
        //�J�E���g���Z�b�g���ĉ摜�ƃe�L�X�g�\��
        count = textTime;
        nameWindow.gameObject.SetActive(true);
        nemeText.gameObject.SetActive(true);
        enemy_name.gameObject.SetActive(false);
        enemyInformation.gameObject.SetActive(false);

        nemeText.text = a;
    }

    public void EnemyInformation(string a,int lv,int hp,int hp_max)
    {
        //�A�C�e���Q�b�g���b�Z�[�W�\��
        //�J�E���g���Z�b�g���ĉ摜�ƃe�L�X�g�\��
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
