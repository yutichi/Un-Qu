using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpManager : MonoBehaviour
{
    //�X�N���v�g
    public ClearAndGameOverFade _fadeScripts;
    public SoundManager _soundManager;
    public MainBGM _mainBGM;

    //��̗̑�
    public static int CastleHp = 100;
    public static int _maxCastleHp;

    //�̗̓Q�[�W�֘A
    public static UnityEngine.UI.Slider CastleHpBar;

    //���j�ڕW��
    public static int _clearCount;
    public static int ClearCount;

    //�Q�[���I�[�o�[�A�N���A
    [SerializeField]
    private GameObject _gameOver, _clear;

    //HP�Q�[�W
    [SerializeField]
    private UnityEngine.UI.Slider _castleHpBar;

    //�񕜗�
    [SerializeField]
    private int _healing;

    //�񕜃{�^��color
    [SerializeField]
    private Image _healImage;

    //�K�v�Ȃ���(��,�������x��)
    [SerializeField]
    private int _healCost, _moneyLevelCost;
    public static int HealCost;
    public static int MoneyLevelCost;

    //��A�G�̗̑�
    [SerializeField]
    private int _castleHp, _enemyHp;

    //�v���C���[�A�G���ꂼ��̍U����
    [SerializeField]
    private int _playerAttackMG, _playerAttackSG, _enemyAttackPower;
    public static int EnemyAttackPower;
    public static int PlayerAttackMG;
    public static int PlayerAttackSG;

    //�c�茂�j���e�L�X�g
    [SerializeField]
    private Text _clearCountText;


    //SE�d���h�~�p�g���K�[
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
        //�񕜃{�^��(�g�p�s���ɐF�𒲐�����)
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

        //�c�茂�j����ʕ\��
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

        //�Q�[���I�[�o�[
        if (CastleHp <= 0)
        {
            _mainBGM.Stop();

            if (_soundTrigger == false)
            {
                _soundManager.audioSource.PlayOneShot(_soundManager._explosionSE);

                //SE�d���h�~
                _soundTrigger = true;
            }

            StartCoroutine(DelayCoroutine(1.2f, () =>
            {
                _gameOver.SetActive(true);
                _soundManager.audioSource.PlayOneShot(_soundManager._gameOverBGM);
                Time.timeScale = 0f;
            }));
        }

        //�N���A
        if(ClearCount <= 0 && SceneMoveManager._gameStart == true) 
        {
            _clearCountText.text = "Remnant Enemy:00";
            _mainBGM.Stop();

            if (_soundTrigger == false)
            {
                _soundManager.audioSource.PlayOneShot(_soundManager._explosionSE);

                //SE�d���h�~
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

    //��
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

        //HP�o�[
        CastleHpBar.value = (float)CastleHp / (float)_maxCastleHp;

        //����
        GameManager.Money -= HealCost;
        HealCost = HealCost + 1;

        _soundManager.audioSource.PlayOneShot(_soundManager._healSE);

    }

    //�^�C�g���ɖ߂�
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
