using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletTimmer;

    void Update()
    {
        _bulletTimmer += Time.deltaTime;

        //’e‚ª“–‚½‚ç‚È‚©‚Á‚½Û‚ÉÁ‚¦‚éˆ—
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
