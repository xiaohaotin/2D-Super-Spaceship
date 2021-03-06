﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : MonoBehaviour
{

    List<GameObject> bullets = new List<GameObject>();

    //移動スピード
    public float speed;
    public GameObject Player;
    public GameObject Enemy;
    // ヒットポイント
    public int power = 1;

    //弾を撃つ間隔
    public float shotDelay;

    //弾のprefab
    public GameObject bullet;

    //弾を撃つかどうか
    public bool canShot;

    //爆発のprefab
    public GameObject explosion;

    //爆発の作成
    public void Explosion() 
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    // Start is called before the first frame update

    //弾の作成
    public void Shot(Transform origin)
    {
        GameObject bulletInst = Instantiate(bullet, origin.position, origin.rotation);
        
    }

    //機体の移動
    public void Move(Vector2 direction)
    {
        if (Player)
        {
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        if (Enemy)
        {
            GetComponent<Rigidbody2D>().velocity = direction * speed * -1;
        }
       
    }


   
}
