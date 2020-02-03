using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpaceShip spaceship;
    public GameObject Enemy1;
    public AudioClip expolisionSE;
    // ヒットポイント
    public int hp = 1;
    public int point = 100;
    void Start()
    {
        //tick = 0;
        spaceship = GetComponent<SpaceShip>();
        //ローカル座標のY軸のマイナス方向に移動する
        spaceship.Move(transform.up * -1);

    }

    void Update()
    {


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(Enemy1)
        {
            // ヒットポイントを減らす
            hp = hp - spaceship.power;
            //layer名を取得
            string layerName = LayerMask.LayerToName(collision.gameObject.layer);

            //layer名がBullet(Player)以外の時は何も行わない
            if (layerName != "Bullet(Player)")
            {
                return;
            }
            else
            {
                //弾を削除する
                Destroy(collision.gameObject);
                if (hp<=0) 
                {
                    // スコアコンポーネントを取得してポイントを追加
                    FindObjectOfType<Score>().AddPoint(point);
                    //爆発する
                    spaceship.Explosion();
                    AudioSource.PlayClipAtPoint(expolisionSE, Camera.main.transform.position);
                    //Enemyを削除する
                    Destroy(gameObject);
                }
            }
        }
       
    }

 
}
