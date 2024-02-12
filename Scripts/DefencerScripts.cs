using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefencerScripts : MonoBehaviour
{
    //’e
    [SerializeField]
    private GameObject _bullet;

    //”­ËˆÊ’u
    [SerializeField]
    private Transform _muzzle;

    //’e‘¬
    [SerializeField]
    public float speed = 10000;

    //”­ËŠÔŠu
    [SerializeField]
    public float _interval = 20;

    public float _time;

    //ƒAƒjƒ[ƒVƒ‡ƒ“
    //private Animator _defencerAnimator;

    // “G‚ğŒŸ’m‚·‚é‚½‚ß‚Ì•Ï”
    [SerializeField]
    private GameObject[] _targets;

    private GameObject nearestEnemy = null;
    private GameObject oldNearestEnemy = null;
    float minDis = 1000f;

    //UŒ‚”ÍˆÍ
    [SerializeField]
    private float _range = 100f;

    //©w‚Ìí—Ş
    [SerializeField]
    private bool _gunner;

    // Start is called before the first frame update
    void Start()
    {
        //_defencerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneMoveManager._gameStart == true)
        {
            if (SpawnPoint.SpawnCount >= 0)
            {
                _time += Time.deltaTime;

                if (_time >= _interval)
                {
                    if (_gunner == false)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            GameObject bullets = Instantiate(_bullet) as GameObject;

                            Vector3 force;

                            bullets.transform.position = _muzzle.position;

                            force = this.gameObject.transform.forward * speed;

                            bullets.GetComponent<Rigidbody>().AddForce(force);

                        }
                    }

                    if (_gunner == true)
                    {
                        GameObject bullets = Instantiate(_bullet) as GameObject;

                        Vector3 force;

                        bullets.transform.position = _muzzle.position;

                        force = this.gameObject.transform.forward * speed;

                        bullets.GetComponent<Rigidbody>().AddForce(force);
                    }

                    _time = 0.0f;
                }
            }
        }

        minDis = 1000f;

        _targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in _targets)
        {
            float disWk = Vector3.Distance(transform.position, enemy.transform.position);
            if (disWk <= _range && disWk < minDis)
            {
                minDis = disWk;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy == oldNearestEnemy)
        {
            // minDis = 1000f;
        }

        if (nearestEnemy != null)
        {
            oldNearestEnemy = nearestEnemy;
            transform.rotation = Quaternion.Slerp(transform.rotation
                               , Quaternion.LookRotation(nearestEnemy.transform.position - transform.position), 1.9f);
        }


    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
