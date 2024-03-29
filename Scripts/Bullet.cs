using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletTimmer;

    void Update()
    {
        _bulletTimmer += Time.deltaTime;

        //弾が当たらなかった際に消える処理
        if(_bulletTimmer >= 8 )
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
