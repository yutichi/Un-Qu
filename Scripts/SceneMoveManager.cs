using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMoveManager : MonoBehaviour
{
    //�X�N���v�g
    public ClearAndGameOverFade _fadeScripts;
    public SoundManager _soundManager;
    public MainBGM _mainBGM;

    //�^�C�g����GameObject
    [SerializeField]
    private GameObject _titleScene, _titleLogo, _startButton;

    //��Փx�I�����
    [SerializeField]
    private GameObject _difficulty;

    [SerializeField]
    private Text _easy, _normal, _hard;

    public static bool _gameStart;

    // Start is called before the first frame update
    void Start()
    {
        _gameStart = false;

        _titleScene.SetActive(true);

        _titleLogo.SetActive(true);
        _startButton.SetActive(true);

        _difficulty.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        _titleLogo.SetActive(false);
        _startButton.SetActive(false);

        _soundManager.audioSource.PlayOneShot(_soundManager._startButtonSE);

        StartCoroutine(DelayCoroutine(0.75f, () =>
        {
            Debug.Log("AAA");
            _difficulty.SetActive(true);
        }));
    }

    /// <summary>
    /// �e��Փx�ݒ�(�������z�A���j���ɖႦ����z�A�K�v���j��)
    /// </summary>
    //�ȒP
    public void Easy()
    {
        GameManager.Money = GameManager.Money + 800;
        EnemyScripts._crushing = EnemyScripts._crushing + 150;
        HpManager.ClearCount = HpManager.ClearCount + 10;

        GameStart();
    }

    //����
    public void Normal()
    {
        GameManager.Money = GameManager.Money + 500;
        EnemyScripts._crushing = EnemyScripts._crushing + 100;
        HpManager.ClearCount = HpManager.ClearCount + 15;

        GameStart();
    }

    //���
    public void Hard()
    {
        GameManager.Money = GameManager.Money + 350;
        EnemyScripts._crushing = EnemyScripts._crushing + 50;
        HpManager.ClearCount = HpManager.ClearCount + 20;

        GameStart();
    }

    //�Q�[���X�^�[�g
    public void GameStart()
    {
        _fadeScripts.FadeOutLoadTrigger();
        _soundManager.audioSource.PlayOneShot(_soundManager._selectDifficultSE);

        StartCoroutine(DelayCoroutine(1.25f, () =>
        {
            _titleScene.SetActive(false);
            _difficulty.SetActive(false);
            _gameStart = true;

            _fadeScripts.FadeInTrigger();

            _mainBGM.Play();
        }));
    }


    /// <summary>
    /// �ȉ��A��Փx�I����ʂ̃J�[�\�������킹����F���ύX
    /// </summary>
    public void PointInEasy()
    {
        Text LevelImage = _easy.gameObject.GetComponent<Text>();
        LevelImage.color = new Color(0.9f, 1.0f, 0.0f, 1.0f);
        _soundManager.audioSource.PlayOneShot(_soundManager._pointInSE);
    }
    public void PointOutEasy()
    {
        Text LevelImage = _easy.gameObject.GetComponent<Text>();
        LevelImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void PointInNormal()
    {
        Text LevelImage = _normal.gameObject.GetComponent<Text>();
        LevelImage.color = new Color(0.9f, 1.0f, 0.0f, 1.0f);
        _soundManager.audioSource.PlayOneShot(_soundManager._pointInSE);
    }
    public void PointOutNormal()
    {
        Text LevelImage = _normal.gameObject.GetComponent<Text>();
        LevelImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void PointInHard()
    {
        Text LevelImage = _hard.gameObject.GetComponent<Text>();
        LevelImage.color = new Color(0.9f, 1.0f, 0.0f, 1.0f);
        _soundManager.audioSource.PlayOneShot(_soundManager._pointInSE);
    }
    public void PointOutHard()
    {
        Text LevelImage = _hard.gameObject.GetComponent<Text>();
        LevelImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
