using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //弾の移動スピード
    public float speed = 10;

    // Object生成から削除するまでの時間
    public float lifeTime = 5;

    //弾は追跡かどうか
    public bool cantrackplayer;

    // Start is called before the first frame update
    void Start()
    {
        if (cantrackplayer == false)
        {
            //ローカル座標のY軸方向に移動する
            GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
        }
        else
        {

        }
        //lifeTime秒後に削除
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
