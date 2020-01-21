using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : MonoBehaviour
{

    List<GameObject> bullets = new List<GameObject>();

    //移動スピード
    public float speed;

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
        //bullets.Add(bulletInst);
        //bulletInst.transform.SetParent(this.transform);
    }

    //機体の移動
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }


    //public void ShotAll()
    //{
    //    foreach (var b in bullets)
    //    {
    //        b.GetComponent<Bullet>().release();
    //        b.transform.SetParent(null);
    //    }

    //    bullets.Clear();
    //}
}
