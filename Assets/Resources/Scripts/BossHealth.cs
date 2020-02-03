using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float Hp = 1000;
    public float Nowhp = 1000;
    public Slider healthSlider;
    SpaceShip spaceship;
    // Start is called before the first frame update
    void Start()
    {
        Nowhp = Hp;
        healthSlider.value = Nowhp;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = Nowhp;
        Nowhp-=10;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //layer名を取得
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
         
        
        //layer名がBullet(Player)以外の時は何も行わない
        if (layerName != "Bullet(Player)")
        {
            Nowhp -= 10;
           
            return;
            
        }
        else
        {
            //弾を削除する
            Destroy(collision.gameObject);

          Debug.Log("-30");
            if (Nowhp <= 0)
            {
                spaceship.Explosion();
                //Enemyを削除する
                Destroy(gameObject);
            }
            
        }
    }

}
