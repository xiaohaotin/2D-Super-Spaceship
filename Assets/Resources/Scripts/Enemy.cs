using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpaceShip spaceship;

    public GameObject shotposition;
    public GameObject shotcenter;

    public float angle;

    public int count;
    public float spawnDelay = 0.1f;
    public float releaseDelay = 0.2f;
    float waveDelay = 0f;
    float tick;

    // Start is called before the first frame update
    //IEnumerator Start()
    //{
    //    spaceship = GetComponent<SpaceShip>();

    //    //ローカル座標のY軸のマイナス方向に移動する
    //    spaceship.Move(transform.up * -1);

    //    //canShotがfalseの場合,ここでコルーチンを終了させる
    //    if (spaceship.canShot == false)
    //    {
    //        yield break;
    //    }

    //    while (true)
    //    {
    //        //子要素を全て取得する
    //        for (int i = 0;i < transform.childCount;i++)
    //        {
    //            Transform shotPosition = transform.GetChild(i);

    //            //shotpositionの位置/角度で弾を撃つ
    //            spaceship.Shot(shotPosition);
    //        }

    //        //shotDelay秒待つ
    //        yield return new WaitForSeconds(spaceship.shotDelay);
    //    }
    //}

    void Start()
    {
        tick = 0;
        spaceship = GetComponent<SpaceShip>();
        //ローカル座標のY軸のマイナス方向に移動する
        spaceship.Move(transform.up * -1);

    }

    void Update()
    {


        //for (int i = 0; i < 18; i++)
        //{
        //    shotcenter.transform.Rotate(Vector3.forward, angle);
        //    Spawn();
        //}


        //shotcenter.transform.Rotate(Vector3.forward, angle);
        //tick += Time.deltaTime;
        //if (tick > waveDelay)
        //{
        //    tick = 0;
        //    StartCoroutine(startspawnbullet(count,true,spawnDelay,releaseDelay));
        //    waveDelay = count * spawnDelay + releaseDelay + 1f;
        //    Spawn();
        //}



        tick += Time.deltaTime;
        if (tick > waveDelay)
        {
            tick = 0;
            StartCoroutine(startspawnbullet(count, true, spawnDelay, releaseDelay));
            waveDelay = count * spawnDelay + releaseDelay + 1f;

        }


    }

    private void Spawn()
    {
        spaceship.Shot(shotposition.transform);
    }

    public IEnumerator startspawnbullet(int count, bool clockwise, float shotDelay, float endDelay)
    {

        for (int i = 0; i < count; i++)
        {
            shotcenter.transform.Rotate(Vector3.forward, 360 / count * (clockwise ? 1 : -1));
            Spawn();
            yield return new WaitForSeconds(shotDelay);
        }

        yield return new WaitForSeconds(endDelay);
        spaceship.ShotAll();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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

            //爆発する
            spaceship.Explosion();

            //Enemyを削除する
            Destroy(gameObject);
        }
    }

 
}
