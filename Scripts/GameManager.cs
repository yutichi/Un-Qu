using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�X�N���v�g
    public SoundManager _soundManager;

    [SerializeField] 
    private Transform _enemy, _castle;

    //�����֘A
    [SerializeField]
    private float _plusMoney;
    public static int Money;

    private float _money;

    [SerializeField]
    private int _moneyLevel;

    //�񕜃e�L�X�g
    [SerializeField]
    private Text _healText;

    //�������e�L�X�g
    [SerializeField]
    private Text _moneyText;

    //�������x���e�L�X�g
    [SerializeField]
    private Text _moneyLevelText;

    //�����{�^��color
    [SerializeField]
    private Image _moneyImage;

    //�񕜁A�������x���̐�����
    [SerializeField]
    private GameObject _healDescription, _moneyDescription;

    void Start()
    {
        _moneyLevel = 1;

        _moneyDescription.SetActive(false);
        _healDescription.SetActive(false);

        Money = 0;
    }

    private void Update()
    {
        if (SceneMoveManager._gameStart == true)
        {
            //���Ԗ��ɂ����𑝂₷
            _money += Time.deltaTime;

            if (_money >= _plusMoney)
            {
                Money = Money + 1;
                _money = 0;
            }

            //�񕜂ɕK�v�Ȃ�����\��
            _healText.text = "$:" + HpManager.HealCost;

            //�������\��
            _moneyText.text = "$:" + Money;

            //�������x���\��
            if (_moneyLevel < 5)
            {
                _moneyLevelText.text = "Lv:" + _moneyLevel + "\n$:" + HpManager.MoneyLevelCost;
            }
            else if (_moneyLevel >= 5)
            {
                _moneyLevelText.text = "Lv:MAX";
            }

            // �G����̕����Ɍ�������
            _enemy.LookAt(_castle);

            if (Money < HpManager.MoneyLevelCost)
            {
                Image MoneyImage = _moneyImage.gameObject.GetComponent<Image>();
                MoneyImage.color = new Color(0.64f, 0.64f, 0.64f, 1.0f);
            }
            else
            {
                Image MoneyImage = _moneyImage.gameObject.GetComponent<Image>();
                MoneyImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }


    //�������x�����オ��
    public void MoneyLevel()
    {
        if (Money < HpManager.MoneyLevelCost || _moneyLevel >= 5)
        {
            return;
        }

        //���������܂鑬�x�㏸   
        _plusMoney = _plusMoney - 0.2f;

        //�������g�p
        Money = Money - HpManager.MoneyLevelCost;

        //���x���A�b�v
        _moneyLevel++;

        //�K�v�̂�������
        HpManager.MoneyLevelCost = HpManager.MoneyLevelCost * 2;

        _soundManager.audioSource.PlayOneShot(_soundManager._moneyLevelSE);
    }

    public void MousePointHealIn()
    {
        _healDescription.SetActive(true);
    }
    public void MousePointHealOut()
    {
        _healDescription.SetActive(false);
    }

    public void MousePointMoneyIn()
    {
        _moneyDescription.SetActive(true);
    }
    public void MousePointMoneyOut()
    {
        _moneyDescription.SetActive(false);
    }
}
