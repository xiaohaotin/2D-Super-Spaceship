using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動スピード
    public float speed = 5;
    //public GameObject bullet;
    // Start is called before the first frame update

    SpaceShip spaceship;
    IEnumerator Start()
    {
        while (true)
        {
            // Spaceshipコンポーネントを取得
            spaceship = GetComponent<SpaceShip>();

            //弾はプレイヤーと同じ位置/角度で作成
            //Instantiate(bullet, transform.position, transform.rotation);
            spaceship.Shot(transform);

            //shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //右、左
        float x = Input.GetAxisRaw("Horizontal");

        //上、下
        float y = Input.GetAxisRaw("Vertical");

        //移動方向向きを求める
        Vector2 direction = new Vector2(x, y).normalized;

        //移動する向きとスピードを代入する
        //GetComponent<Rigidbody2D>().velocity = direction * speed;

        //移動
        spaceship.Move(direction);
    }
}
