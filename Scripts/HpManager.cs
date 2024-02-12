using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpManager : MonoBehaviour
{
    //スクリプト
    public ClearAndGameOverFade _fadeScripts;
    public SoundManager _soundManager;
    public MainBGM _mainBGM;

    //城の体力
    public static int CastleHp = 100;
    public static int _maxCastleHp;

    //体力ゲージ関連
    public static UnityEngine.UI.Slider CastleHpBar;

    //撃破目標数
    public static int _clearCount;
    public static int ClearCount;

    //ゲームオーバー、クリア
    [SerializeField]
    private GameObject _gameOver, _clear;

    //HPゲージ
    [SerializeField]
    private UnityEngine.UI.Slider _castleHpBar;

    //回復量
    [SerializeField]
    private int _healing;

    //回復ボタンcolor
    [SerializeField]
    private Image _healImage;

    //必要なお金(回復,お金レベル)
    [SerializeField]
    private int _healCost, _moneyLevelCost;
    public static int HealCost;
    public static int MoneyLevelCost;

    //城、敵の体力
    [SerializeField]
    private int _castleHp, _enemyHp;

    //プレイヤー、敵それぞれの攻撃力
    [SerializeField]
    private int _playerAttackMG, _playerAttackSG, _enemyAttackPower;
    public static int EnemyAttackPower;
    public static int PlayerAttackMG;
    public static int PlayerAttackSG;

    //残り撃破数テキスト
    [SerializeField]
    private Text _clearCountText;


    //SE重複防止用トリガー
    private bool _soundTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        _maxCastleHp = _castleHp;

        ClearCount = 0;

        _gameOver.SetActive(false);
        _clear.SetActive(false);

        Time.timeScale = 1f;
    }

    void Awake()
    {
        CastleHpBar = _castleHpBar;
        EnemyAttackPower = _enemyAttackPower;
        PlayerAttackMG = _playerAttackMG;
        PlayerAttackSG = _playerAttackSG;
        CastleHp = _castleHp;
        HealCost = _healCost;
        MoneyLevelCost = _moneyLevelCost;
    }

    // Update is called once per frame
    void Update()
    {
        //回復ボタン(使用不可時に色を調整する)
        if (GameManager.Money < HealCost || CastleHp == _maxCastleHp)
        {
            Image HealImage = _healImage.gameObject.GetComponent<Image>();
            HealImage.color = new Color(0.64f, 0.64f, 0.64f, 1.0f);
        }
        else 
        {
            Image HealImage = _healImage.gameObject.GetComponent<Image>();
            HealImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        //残り撃破数画面表示
        if (ClearCount > 0)
        {
            if (ClearCount >= 10)
            {
                _clearCountText.text = "Remnant Enemy:" + ClearCount;
            }
            if (ClearCount <= 9)
            {
                _clearCountText.text = "Remnant Enemy:0" + ClearCount;
            }
        }

        //ゲームオーバー
        if (CastleHp <= 0)
        {
            _mainBGM.Stop();

            if (_soundTrigger == false)
            {
                _soundManager.audioSource.PlayOneShot(_soundManager._explosionSE);

                //SE重複防止
                _soundTrigger = true;
            }

            StartCoroutine(DelayCoroutine(1.2f, () =>
            {
                _gameOver.SetActive(true);
                _soundManager.audioSource.PlayOneShot(_soundManager._gameOverBGM);
                Time.timeScale = 0f;
            }));
        }

        //クリア
        if(ClearCount <= 0 && SceneMoveManager._gameStart == true) 
        {
            _clearCountText.text = "Remnant Enemy:00";
            _mainBGM.Stop();

            if (_soundTrigger == false)
            {
                _soundManager.audioSource.PlayOneShot(_soundManager._explosionSE);

                //SE重複防止
                _soundTrigger = true;
            }

            StartCoroutine(DelayCoroutine(1.2f, () =>
            {
                _clear.SetActive(true);
                _soundManager.audioSource.PlayOneShot(_soundManager._clearBGM);
                Time.timeScale = 0f;
            }));
        }
    }

    //回復
    public void Heal()
    {
        Image HealImage = _healImage.gameObject.GetComponent<Image>();

        if (GameManager.Money < HealCost || CastleHp == _maxCastleHp)
        {
            return;
        }

        if (CastleHp + _healing <= _maxCastleHp)
        {
            CastleHp = CastleHp + _healing;
        }

        if(CastleHp + _healing > _maxCastleHp)
        {
            CastleHp = _maxCastleHp;
        }

        //HPバー
        CastleHpBar.value = (float)CastleHp / (float)_maxCastleHp;

        //お金
        GameManager.Money -= HealCost;
        HealCost = HealCost + 1;

        _soundManager.audioSource.PlayOneShot(_soundManager._healSE);

    }

    //タイトルに戻る
    public void BackTitle(int _gameScene)
    {
        _soundManager.audioSource.PlayOneShot(_soundManager._selectDifficultSE);
            SceneManager.LoadScene(_gameScene);
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
