using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonButton : MonoBehaviour
{
    //スクリプト
    public SoundManager _soundManager;

    //召喚ボタンcolor
    [SerializeField]
    public Image _buttonImageMG, _buttonImageSG;

    //召喚コスト
    private int _costMG = 300;
    private int _costSG = 350;


    //召喚ユニット
    [SerializeField]
    private GameObject _defencerSG, _defencerMG;

    private float _summonPosX, _summonPosY, _summonPosZ;

    private int _summonCount;

    //最大召喚数
    [SerializeField]
    private int _maxSummonCount;

    //説明文
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

        //召喚座標
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

    //MGユニットを召喚
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

    //SGユニットを召喚
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

    //カーソルを合わせたら説明文が出てくる(MG)
    public void MousePointMGIn()
    {
        _mgDescription.SetActive(true);
    }
    public void MousePointMGOut()
    {
        _mgDescription.SetActive(false);
    }

    //カーソルを合わせたら説明文が出てくる(SG)
    public void MousePointSGIn()
    {
        _sgDescription.SetActive(true);
    }
    public void MousePointSGOut()
    {
        _sgDescription.SetActive(false);
    }
}
