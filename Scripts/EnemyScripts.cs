using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyScripts : MonoBehaviour
{
    //敵の移動速度
    public float _speed;

    private Animator _enemyAnimator;

    //攻撃開始のトリガー
    private bool _attackTrigger, _attackStart;

    private float _attackInterval;

    //体力
    public int _maxEnemyHp = 50;
    public int EnemyHp;

    //Hpゲージ
    [SerializeField]
    private UnityEngine.UI.Slider _enemyHpBar;

    //倒した時に貰えるお金
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
        //城のHPゲージ
        HpManager.CastleHpBar.value = (float)HpManager.CastleHp / (float)HpManager._maxCastleHp;

        //Hpバーをカメラ視点に固定
        _enemyHpBar.transform.rotation = Camera.main.transform.rotation;

        if (HpManager.CastleHp <= 0 || HpManager.ClearCount <= 0)
        {
            _speed = 0;           
        }

        //移動
        transform.position += transform.forward * _speed * 0.75f;

        //攻撃アニメーションへ移行
        if(_attackTrigger == true)
        {
            StartCoroutine(DelayCoroutine(1.5f, () =>
            {
                _enemyAnimator.SetBool("Standing", false);
                _enemyAnimator.SetBool("Attack", true);

                _attackStart = true;
            }));
        }

        //城への攻撃
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

        //体力が0になったら消滅する
        if(EnemyHp <= 0) 
        {
            HpManager.ClearCount--;
            SpawnPoint.SpawnCount--;
            GameManager.Money = GameManager.Money + _crushing;

            Destroy(this.gameObject);
        }
    }

    //プレイヤーからの攻撃を受けた時の判定
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

    //城への攻撃を開始
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject.tag == "Castle")
        {
            Debug.Log("Castleに攻撃を開始!");
            _enemyAnimator.SetBool("Standing", true);

            _attackTrigger = true;

            _speed = 0f;
        }
    }


    private void HpController()
    {

    }

    //指定した秒数後に発動させるコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
