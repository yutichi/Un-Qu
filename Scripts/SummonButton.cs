using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonButton : MonoBehaviour
{
    //�X�N���v�g
    public SoundManager _soundManager;

    //�����{�^��color
    [SerializeField]
    public Image _buttonImageMG, _buttonImageSG;

    //�����R�X�g
    private int _costMG = 300;
    private int _costSG = 350;


    //�������j�b�g
    [SerializeField]
    private GameObject _defencerSG, _defencerMG;

    private float _summonPosX, _summonPosY, _summonPosZ;

    private int _summonCount;

    //�ő叢����
    [SerializeField]
    private int _maxSummonCount;

    //������
    [SerializeField]
    private GameObject _mgDescription, _sgDescription;

    // Start is called before the first frame update
    void Start()
    {
        _summonCount = 1;

        _mgDescription.SetActive(false);
        _sgDescription.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //�������W
        if (_summonCount == 1)
        {
            _summonPosX = -5;
            _summonPosY = 1;
            _summonPosZ = 10;
        }
        else if (_summonCount == 2)
        {
            _summonPosX = -9f;
            _summonPosY = 1f;
            _summonPosZ = 10f;
        }
        if (_summonCount == 3)
        {
            _summonPosX = -2.5f;
            _summonPosY = 1;
            _summonPosZ = 10;
        }
        if (_summonCount == 4)
        {
            _summonPosX = -7f;
            _summonPosY = 1;
            _summonPosZ = 10;
        }

        if(GameManager.Money < _costMG)
        {
            Image MGImage = _buttonImageMG.gameObject.GetComponent<Image>();
            MGImage.color = new Color(0.64f, 0.64f, 0.64f, 1.0f);
        }
        else
        {
            Image MGImage = _buttonImageMG.gameObject.GetComponent<Image>();
            MGImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        if (GameManager.Money < _costSG)
        {
            Image SGImage = _buttonImageSG.gameObject.GetComponent<Image>();
            SGImage.color = new Color(0.64f, 0.64f, 0.64f, 1.0f);
        }
        else
        {
            Image SGImage = _buttonImageSG.gameObject.GetComponent<Image>();
            SGImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    //MG���j�b�g������
    public void SummonMG()
    {
        if (_maxSummonCount > _summonCount && GameManager.Money > _costMG)
        {
            Instantiate(_defencerMG, new Vector3(_summonPosX, _summonPosY, _summonPosZ), Quaternion.identity);
            GameManager.Money = GameManager.Money - _costMG;

            _summonCount = _summonCount + 1;

            _soundManager.audioSource.PlayOneShot(_soundManager._summonButtonSE);
        }
    }

    //SG���j�b�g������
    public void SummonSG()
    {
        if (_maxSummonCount > _summonCount && GameManager.Money > _costSG)
        {
            Instantiate(_defencerSG, new Vector3(_summonPosX, _summonPosY, _summonPosZ), Quaternion.identity);
            GameManager.Money = GameManager.Money - _costSG;

            _summonCount = _summonCount + 1;

            _soundManager.audioSource.PlayOneShot(_soundManager._summonButtonSE);
        }
    }

    //�J�[�\�������킹������������o�Ă���(MG)
    public void MousePointMGIn()
    {
        _mgDescription.SetActive(true);
    }
    public void MousePointMGOut()
    {
        _mgDescription.SetActive(false);
    }

    //�J�[�\�������킹������������o�Ă���(SG)
    public void MousePointSGIn()
    {
        _sgDescription.SetActive(true);
    }
    public void MousePointSGOut()
    {
        _sgDescription.SetActive(false);
    }
}
