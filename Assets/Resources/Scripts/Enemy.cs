using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpaceShip spaceship;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        spaceship = GetComponent<SpaceShip>();

        //ローカル座標のY軸のマイナス方向に移動する
        spaceship.Move(transform.up * -1);

        //canShotがfalseの場合,ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            //子要素を全て取得する
            for (int i = 0;i < transform.childCount;i++)
            {
                Transform shotPosition = transform.GetChild(i);

                //shotpositionの位置/角度で弾を撃つ
                spaceship.Shot(shotPosition);
            }

            //shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
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
