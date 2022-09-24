using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class Menu_mainScript : MonoBehaviour
{
    [Header("UI�p�l��")]
    public GameObject uiPanel;
    [Header("���j���[�E�B���h�E")]
    public Image menuImage;
    [Header("���j���[�J�[�\��")]
    public Image menuCursor;
    [Header("���j���[���̃E�B���h�E")]
    public Image menuNameImage;
    [Header("���j���[���̃e�L�X�g")]
    public TextMeshProUGUI menuName;
    [Header("�A�C�e���E�B���h�E")]
    public Image ListImage;
    [Header("�A�C�e���J�[�\��")]
    public Image ListCursor;
    [Header("�A�C�e�����̃e�L�X�g")]
    public TextMeshProUGUI itemName;
    [Header("�������t�H���g")]
    public TextMeshProUGUI setumeibun;
    [Header("�X�e�[�^�X�E�B���h�E")]
    public Image StateImage;

    [Header("�O���[�o���ϐ�")]
    public GlobalVariables_ScriptableObject globalVariables;
    [Header("�A�C�e���f�[�^�x�[�X")]
    public ItemDataBaseGB itemDataBase;
    [Header("���j���[�J�[�\���ʒu")]
    public List<Vector2> menuCursorPos = new List<Vector2>();
    [Header("�A�C�e���J�[�\���ʒu")]
    public List<Vector2> itemCursorPos = new List<Vector2>();
    [Header("���j���[����")]
    public List<string> menuNameStr = new List<string>();
    [Header("�A�C�e���A�C�R��")]
    public List<Image> itemIcon = new List<Image>();
    [Header("���@�A�C�R��")]
    public List<Image> magicIcon = new List<Image>();
    [Header("�h��A�C�R��")]
    public List<Image> bouguIcon = new List<Image>();
    [Header("�A�N�Z�T���[�A�C�R��")]
    public List<Image> akuseIcon = new List<Image>();
    [Header("�T�E���h�}�l�[�W���[�I�u�W�F�N�g")]
    public GameObject soundManagerObj;
    [Header("�v���C���[�R���g���[��")]
    public PlayerControlGB playerControl;

    //�}�b�v�C���[�W
    GameObject mapobj;
    GameObject MapImage;

    MessageMini messageMini;
    //�T�E���h�}�l�[�W���[�̃R���|�[�l���g�擾

    SoundManagerScipt soundManager;
    AudioSource audioSource;

    //�\���E��\���t���O
    int menuFlag=0;

    //�E�F�C�g
    float WaitTime = 0.1f;
    //�\���L�[�󂯕t���E�F�C�g
    int WaitKeyTime = 45;
    //����p
    int count = 0;
    float dy = 0;
    float dx = 0;

    //�}�b�v�ǂݍ��݃t���O
    bool mapLoad= true;

    //�\���L�[�I���̃E�F�C�g�t���O
    bool WaitKeyFlag = false;

    //�{�^����
    [SerializeField]
    string buttonName = "Start";
    [SerializeField]
    string buttonA = "Fire1";
    [SerializeField]
    string buttonB = "Fire2";

    //�{�^���������̃t���O�i�\���h�~�j
    bool ButtonDown = false;




    //�J�[�\��RectTransform����p
    RectTransform menuC, listC;

    // Start is called before the first frame update
    void Start()
    {
        //�T�E���h�}�l�[�W���[�擾
        soundManager = soundManagerObj.GetComponent<SoundManagerScipt>();
        audioSource = soundManagerObj.GetComponent<AudioSource>();

        //�e�J�[�\���̏����ʒu��ݒ�
        globalVariables.menuCount = 0;
        menuC = menuCursor.GetComponent<RectTransform>();
        listC = ListCursor.GetComponent<RectTransform>();
        menuC.localPosition = new Vector3(menuCursorPos[globalVariables.menuCount].x - 640, menuCursorPos[globalVariables.menuCount].y + 360, 0f);
        menuName.text = menuNameStr[globalVariables.menuCount];
        Debug.Log("menuCursorPos�̐�"+menuCursorPos.Count);

        //���b�Z�[�W�\���E�B���h�E���e�L�X�g�擾
        messageMini = GetComponent<MessageMini>();

        //�}�b�v�C���[�W�擾
        MapImage = GameObject.FindWithTag("Map_Panel");
        MapImage.gameObject.SetActive(false);
        Debug.Log("�}�b�v�̖��O" + MapImage);
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
            //���j���[�\��
            if (Input.GetButtonDown(buttonName))
            {
                //�I�[�v�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.kettei_sound);
                //���j���[��ʍĕ`��
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
            //���j���[��ʍĕ`��
            StateImage.gameObject.SetActive(true);
            menuNameImage.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(false);
            MapImage.gameObject.SetActive(false);
            //���j���[�Z���N�g
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
                //Debug.Log("�������փL�[�󂯕t����" + globalVariables.menuCount);
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
                //Debug.Log("������փL�[�󂯕t����" + globalVariables.menuCount);
                WaitKeyFlag = true;
                
            }

            if (WaitKeyFlag == true)
            {
                WaitKey();
            }
            //���j���[����
            if (Input.GetButtonDown(buttonName) | Input.GetButtonDown(buttonB))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }
            //���j���[����
            if (Input.GetButtonDown(buttonA))
            {
                //�A�C�e����
                if (globalVariables.menuCount == 0)
                {
                    //�I�[�v�����ʉ�
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //���݃J�[�\���������`��
                    ItemNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 2;

                }
                //���@��
                if (globalVariables.menuCount == 1)
                {
                    //�I�[�v�����ʉ�
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //���݃J�[�\���������`��
                    MagicNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 3;

                }
                //�h���
                if (globalVariables.menuCount == 2)
                {
                    //�I�[�v�����ʉ�
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //���݃J�[�\���������`��
                    BouguNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 4;

                }
                //�A�N�Z�T���[��
                if (globalVariables.menuCount == 3)
                {
                    //�I�[�v�����ʉ�
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //���݃J�[�\���������`��
                    AkuseNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 5;

                }
                //�}�b�v��
                if (globalVariables.menuCount == 4)
                {
                    //�I�[�v�����ʉ�
                    audioSource.Stop();
                    audioSource.PlayOneShot(soundManager.kettei_sound);
                    //���݃J�[�\���������`��
                    AkuseNameAndCursor();
                    ButtonDown = true;
                    menuFlag = 6;

                }
            }
            
        }
        if (menuFlag == 2)
        {

            //�A�C�e����
            //Debug.Log("�A�C�e�����J����");
            //�ĕ`��
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Itemicon();
            //Y�����Z���N�g
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

            //X�����Z���N�g
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

            //�A�C�e������
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.item_shoji[globalVariables.itemCount] > 0)
                {
                    if (itemDataBase.itemYouto[globalVariables.itemCount] == ItemDataBaseGB.ItemYouto.Kaihuku)
                    {
                        //�񕜌��ʉ��iitemDataBase�Ŏw��j
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cursor_sound);
                        //�񕜏���
                        globalVariables.hp += itemDataBase.item_hp[globalVariables.itemCount];
                        globalVariables.hp = Mathf.Clamp(globalVariables.hp, 0, globalVariables.hp_max);
                        globalVariables.mp += itemDataBase.item_mp[globalVariables.itemCount];
                        globalVariables.mp = Mathf.Clamp(globalVariables.mp, 0, globalVariables.mp_max);
                        //UI�ĕ`��
                        playerControl.UIdraw();
                        //��������1���炷
                        itemDataBase.item_shoji[globalVariables.itemCount] -= 1;
                        //�A�C�R���ĕ`��
                        ItemNameAndCursor();
                    }
                    else
                    {
                        //�L�����Z�����ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        //���b�Z�[�W�\��
                        messageMini.ItemGet("�����Ă邾���Ō��ʃA���ł�");

                    }
                }

            }

            //���j���[1�O�ɖ߂�
            if (Input.GetButtonDown(buttonB))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //���j���[�S����
            if (Input.GetButtonDown(buttonName))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //�{�^���𗣂�����\���t���O����
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 3)
        {

            //�A�C�e����
            //Debug.Log("�A�C�e�����J����");
            //�ĕ`��
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Magicicon();
            //Y�����Z���N�g
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

            //X�����Z���N�g
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

            //���@��������
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.magic_shoji[globalVariables.magicCount] > 0)
                {


                    //�����i���������Ă�A�C�e�����ēx�N���b�N����ƊO���j
                    if(itemDataBase.soubi_magic== globalVariables.magicCount)
                    {
                        //���ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        itemDataBase.soubi_magic = -1;
                        Debug.Log("�������O����");
                    }
                    else
                    {
                        //���ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.kettei_sound);
                        itemDataBase.soubi_magic = globalVariables.magicCount;
                        Debug.Log(itemDataBase.soubi_magic+"�𑕔�����");
                    }
                }
                //UI�ĕ`��
                playerControl.UIdraw();
                playerControl.Soubi();
            }

            //���j���[1�O�ɖ߂�
            if (Input.GetButtonDown(buttonB))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //���j���[�S����
            if (Input.GetButtonDown(buttonName))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //�{�^���𗣂�����\���t���O����
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 4)
        {

            //�A�C�e����
            //Debug.Log("�A�C�e�����J����");
            //�ĕ`��
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Bouguicon();
            //Y�����Z���N�g
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

            //X�����Z���N�g
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

            //�h�������
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.bougu_shoji[globalVariables.bouguCount] > 0)
                {


                    //�����i���������Ă�A�C�e�����ēx�N���b�N����ƊO���j
                    if (itemDataBase.soubi_bougu == globalVariables.bouguCount)
                    {
                        //���ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        itemDataBase.soubi_bougu = -1;
                        Debug.Log("�������O����");
                    }
                    else
                    {
                        //���ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.kettei_sound);
                        itemDataBase.soubi_bougu = globalVariables.bouguCount;
                        Debug.Log(itemDataBase.soubi_bougu + "�𑕔�����");
                    }
                }
                //UI�ĕ`��
                playerControl.UIdraw();
                playerControl.Soubi();
            }

            //���j���[1�O�ɖ߂�
            if (Input.GetButtonDown(buttonB))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //���j���[�S����
            if (Input.GetButtonDown(buttonName))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //�{�^���𗣂�����\���t���O����
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 5)
        {

            //�A�C�e����
            //Debug.Log("�A�C�e�����J����");
            //�ĕ`��
            StateImage.gameObject.SetActive(false);
            menuNameImage.gameObject.SetActive(false);
            ListCursor.gameObject.SetActive(true);
            ListImage.gameObject.SetActive(true);
            IconFalse();
            Akuseicon();
            //Y�����Z���N�g
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

            //X�����Z���N�g
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

            //�A�N�Z�T���[��������
            if (Input.GetButtonDown(buttonA) & ButtonDown == false)
            {
                if (itemDataBase.akuse_shoji[globalVariables.akuseCount] > 0)
                {


                    //�����i���������Ă�A�C�e�����ēx�N���b�N����ƊO���j
                    if (itemDataBase.soubi_akuse == globalVariables.akuseCount)
                    {
                        //���ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.cancel_sound);
                        itemDataBase.soubi_akuse = -1;
                        Debug.Log("�������O����");
                    }
                    else
                    {
                        //���ʉ�
                        audioSource.Stop();
                        audioSource.PlayOneShot(soundManager.kettei_sound);
                        itemDataBase.soubi_akuse = globalVariables.akuseCount;
                        Debug.Log(itemDataBase.soubi_akuse + "�𑕔�����");
                    }
                }
                //UI�ĕ`��
                playerControl.UIdraw();
                playerControl.Soubi();
            }

            //���j���[1�O�ɖ߂�
            if (Input.GetButtonDown(buttonB))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //���j���[�S����
            if (Input.GetButtonDown(buttonName))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //�{�^���𗣂�����\���t���O����
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 6)
        {

            //���j���[��
            //Debug.Log("�A�C�e�����J����");
            //�ĕ`��
            MapImage.gameObject.SetActive(true);
            StateImage.gameObject.SetActive(true);
            menuNameImage.gameObject.SetActive(true);
            ListCursor.gameObject.SetActive(false);
            ListImage.gameObject.SetActive(false);
            IconFalse();

            //���j���[1�O�ɖ߂�
            if (Input.GetButtonDown(buttonB))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                WaitKey();
                menuFlag = 1;
            }
            //���j���[�S����
            if (Input.GetButtonDown(buttonName))
            {
                //���j���[��\���t���OON
                //�L�����Z�����ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(soundManager.cancel_sound);
                yield return new WaitForSeconds(WaitTime);
                menuFlag = 99;
            }

            //�{�^���𗣂�����\���t���O����
            if (Input.GetButtonUp(buttonA)) ButtonDown = false;
        }
        if (menuFlag == 99)
        {

            //���j���[��\��
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
        //�J�[�\���\��
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.itemCount].x - 640, itemCursorPos[globalVariables.itemCount].y + 360, 0f);
        //�A�C�e���������Ă�����\��
        if (itemDataBase.item_shoji[globalVariables.itemCount] == 0)
        {
            //���́A��������`��
            itemName.text = "";
            setumeibun.text = "";
            itemIcon[globalVariables.itemCount].gameObject.SetActive(false);
        }
        else
        {
            //���́A��������`��
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.itemName[globalVariables.itemCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.item_shoji[globalVariables.itemCount].ToString());
            builder.Append("��");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.item_setumeibun[globalVariables.itemCount];
            itemIcon[globalVariables.itemCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }

    void MagicNameAndCursor()
    {
        //�J�[�\���\��
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.magicCount].x - 640, itemCursorPos[globalVariables.magicCount].y + 360, 0f);
        //���@�������Ă�����\��
        if (itemDataBase.magic_shoji[globalVariables.magicCount] == 0)
        {
            //���́A��������`��
            itemName.text = "";
            setumeibun.text = "";
            magicIcon[globalVariables.magicCount].gameObject.SetActive(false);
        }
        else
        {
            //���́A��������`��
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.magicName[globalVariables.magicCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.magic_shoji[globalVariables.magicCount].ToString());
            builder.Append("��");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.magic_setumeibun[globalVariables.magicCount];
            magicIcon[globalVariables.magicCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }

    void BouguNameAndCursor()
    {
        //�J�[�\���\��
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.bouguCount].x - 640, itemCursorPos[globalVariables.bouguCount].y + 360, 0f);
        //�h��������Ă�����\��
        if (itemDataBase.bougu_shoji[globalVariables.bouguCount] == 0)
        {
            //���́A��������`��
            itemName.text = "";
            setumeibun.text = "";
            bouguIcon[globalVariables.bouguCount].gameObject.SetActive(false);
        }
        else
        {
            //���́A��������`��
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.bouguName[globalVariables.bouguCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.bougu_shoji[globalVariables.bouguCount].ToString());
            builder.Append("��");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.bougu_setumeibun[globalVariables.bouguCount];
            bouguIcon[globalVariables.bouguCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }

    void AkuseNameAndCursor()
    {
        //�J�[�\���\��
        listC.localPosition = new Vector3(itemCursorPos[globalVariables.akuseCount].x - 640, itemCursorPos[globalVariables.akuseCount].y + 360, 0f);
        //���@�������Ă�����\��
        if (itemDataBase.akuse_shoji[globalVariables.akuseCount] == 0)
        {
            //���́A��������`��
            itemName.text = "";
            setumeibun.text = "";
            akuseIcon[globalVariables.akuseCount].gameObject.SetActive(false);
        }
        else
        {
            //���́A��������`��
            StringBuilder builder = new StringBuilder();
            builder.Append(itemDataBase.akuseName[globalVariables.akuseCount]);
            builder.Append(" ");
            builder.Append(itemDataBase.akuse_shoji[globalVariables.akuseCount].ToString());
            builder.Append("��");
            itemName.text = builder.ToString();
            setumeibun.text = itemDataBase.akuse_setumeibun[globalVariables.akuseCount];
            akuseIcon[globalVariables.akuseCount].gameObject.SetActive(true);
        }

        WaitKeyFlag = true;
    }
    void WaitKey()
    {

        //�҂�����
        count++;
        if (count >= WaitKeyTime)
        {
            count = 0;
            WaitKeyFlag = false;
        }
        Debug.Log("���[�v" + count);

    }

    IEnumerator Wait()
    {
        //()�̃t���[�����҂�
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
