using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //スクリプト
    public SoundManager _soundManager;

    [SerializeField] 
    private Transform _enemy, _castle;

    //お金関連
    [SerializeField]
    private float _plusMoney;
    public static int Money;

    private float _money;

    [SerializeField]
    private int _moneyLevel;

    //回復テキスト
    [SerializeField]
    private Text _healText;

    //所持金テキスト
    [SerializeField]
    private Text _moneyText;

    //お金レベルテキスト
    [SerializeField]
    private Text _moneyLevelText;

    //お金ボタンcolor
    [SerializeField]
    private Image _moneyImage;

    //回復、お金レベルの説明文
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
            //時間毎にお金を増やす
            _money += Time.deltaTime;

            if (_money >= _plusMoney)
            {
                Money = Money + 1;
                _money = 0;
            }

            //回復に必要なお金を表示
            _healText.text = "$:" + HpManager.HealCost;

            //所持金表示
            _moneyText.text = "$:" + Money;

            //お金レベル表示
            if (_moneyLevel < 5)
            {
                _moneyLevelText.text = "Lv:" + _moneyLevel + "\n$:" + HpManager.MoneyLevelCost;
            }
            else if (_moneyLevel >= 5)
            {
                _moneyLevelText.text = "Lv:MAX";
            }

            // 敵を城の方向に向かせる
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


    //お金レベルが上がる
    public void MoneyLevel()
    {
        if (Money < HpManager.MoneyLevelCost || _moneyLevel >= 5)
        {
            return;
        }

        //お金が貯まる速度上昇   
        _plusMoney = _plusMoney - 0.2f;

        //お金を使用
        Money = Money - HpManager.MoneyLevelCost;

        //レベルアップ
        _moneyLevel++;

        //必要のお金増加
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
