using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyScripts : MonoBehaviour
{
    //�G�̈ړ����x
    public float _speed;

    private Animator _enemyAnimator;

    //�U���J�n�̃g���K�[
    private bool _attackTrigger, _attackStart;

    private float _attackInterval;

    //�̗�
    public int _maxEnemyHp = 50;
    public int EnemyHp;

    //Hp�Q�[�W
    [SerializeField]
    private UnityEngine.UI.Slider _enemyHpBar;

    //�|�������ɖႦ�邨��
    public static int _crushing;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAnimator = GetComponent<Animator>();

        _attackInterval = 0;

        EnemyHp = _maxEnemyHp;

        HpManager.CastleHpBar.value = 1;
        _enemyHpBar.value = 1;
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���HP�Q�[�W
        HpManager.CastleHpBar.value = (float)HpManager.CastleHp / (float)HpManager._maxCastleHp;

        //Hp�o�[���J�������_�ɌŒ�
        _enemyHpBar.transform.rotation = Camera.main.transform.rotation;

        if (HpManager.CastleHp <= 0 || HpManager.ClearCount <= 0)
        {
            _speed = 0;           
        }

        //�ړ�
        transform.position += transform.forward * _speed * 0.75f;

        //�U���A�j���[�V�����ֈڍs
        if(_attackTrigger == true)
        {
            StartCoroutine(DelayCoroutine(1.5f, () =>
            {
                _enemyAnimator.SetBool("Standing", false);
                _enemyAnimator.SetBool("Attack", true);

                _attackStart = true;
            }));
        }

        //��ւ̍U��
        if(_attackStart == true) 
        { 
            _attackInterval += Time.deltaTime;

            if( _attackInterval > 1.5 ) 
            {
                HpManager.CastleHp -= HpManager.EnemyAttackPower;
                _attackInterval = 0;

                Debug.Log(HpManager.CastleHp);
            }
        }

        //�̗͂�0�ɂȂ�������ł���
        if(EnemyHp <= 0) 
        {
            HpManager.ClearCount--;
            SpawnPoint.SpawnCount--;
            GameManager.Money = GameManager.Money + _crushing;

            Destroy(this.gameObject);
        }
    }

    //�v���C���[����̍U�����󂯂����̔���
    void OnCollisionEnter(Collision collision)
    {
        //MG
        if (collision.gameObject.tag == "MG")
        {
            Debug.Log("Hit:MG");

            EnemyHp -= HpManager.PlayerAttackMG;

            Debug.Log(EnemyHp);

            _enemyHpBar.value = (float)EnemyHp / (float)_maxEnemyHp;
        }

        //SG
        if(collision.gameObject.tag == "SG")
        {
            Debug.Log("Hit:SG");

            EnemyHp -= HpManager.PlayerAttackSG;

            Debug.Log(EnemyHp);

            _enemyHpBar.value = (float)EnemyHp / (float)_maxEnemyHp;
        }
    }

    //��ւ̍U�����J�n
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject.tag == "Castle")
        {
            Debug.Log("Castle�ɍU�����J�n!");
            _enemyAnimator.SetBool("Standing", true);

            _attackTrigger = true;

            _speed = 0f;
        }
    }


    private void HpController()
    {

    }

    //�w�肵���b����ɔ���������R���[�`��
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
