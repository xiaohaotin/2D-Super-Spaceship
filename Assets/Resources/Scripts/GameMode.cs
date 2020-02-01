using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    public List<BulletCharacter> tempBullets;
    public float time = 0;
    public float cooldown = 0;
    public bool ShotGun;
    public bool FirRound_;
    // Start is called before the first frame update
    void Start()
    {
        tempBullets = new List<BulletCharacter>();
        //time = 3;
      
    }

    // Update is called once per frame
    void Update()
    {
        //cooldown += Time.deltaTime;


    }
    private void FixedUpdate()
    {
        Attack();
    }

    public void Attack()
    {
        time++;
        if (time>=300)
        {
            time = 0;
        }
        //if (cooldown>=3)
        //{



        //    //if (time == 3)
        //    //{
        //    //    ShotGun = true;
        //    //    time = 6;

        //    //    if (ShotGun)
        //    //    {
        //    //        StopAllCoroutines();
        //    //        ClearBulletsList();
        //    //        StartCoroutine(FirShotgun());

        //    //        ShotGun = false;
        //    //    }

        //    //}
        //}
        //if (cooldown>=15)
        //{
           
        //    if (time == 6)
        //    {
        //        FirRound_ = true;
        //        time = 9;
        //        if (FirRound_)
        //        {
        //            StopAllCoroutines();
        //            ClearBulletsList();
        //            StartCoroutine(FirRound(5, firPoint.transform.position));
        //        }
        //    }
        //}
        if (GUI.Button(new Rect(20,240,200,100),"多重円形弾幕")) 
        {
            StopAllCoroutines();
            ClearBulletsList();
            StartCoroutine(FlowerFireBall());
        }
        if (GUI.Button(new Rect(20, 340, 200, 100), "嵐弾幕"))
        {
            StopAllCoroutines();
            ClearBulletsList();
            StartCoroutine(FireTurbine());
        }
        if (GUI.Button(new Rect(20, 440, 200, 100), "球体弹幕"))
        {
            StopAllCoroutines();
            ClearBulletsList();
            StartCoroutine(FireBallBulle());
        }
    }
    /// <summary>
    /// ShotGun
    /// </summary>
    /// <returns></returns>
    IEnumerator FirShotgun()
    {
        Vector3 bulletDir = firPoint.transform.up; //发射方向为物体的Up轴方向
        Quaternion leftRota = Quaternion.AngleAxis(-30, Vector3.forward);
        Quaternion RightRota = Quaternion.AngleAxis(30, Vector3.forward); //使用四元数制造2个旋转，分别是绕Z轴朝左右旋转30度
        for (int i = 0; i < 10; i++)     //散弹发射次数
        {
            for (int j = 0; j < 3; j++) //一次发射3颗子弹
            {
                switch (j)
                {
                    case 0:
                        CreatBullet(bulletDir, firPoint.transform.position);  //发射第一颗子弹，方向不需要进行旋转
                        break;
                    case 1:
                        bulletDir = RightRota * bulletDir;//第一个方向子弹发射完毕，旋转方向到下一个发射方向
                        CreatBullet(bulletDir, firPoint.transform.position);
                        break;
                    case 2:
                        bulletDir = leftRota * (leftRota * bulletDir); //右边方向发射完毕，得向左边旋转2次相同的角度才能到达下一个发射方向
                        CreatBullet(bulletDir, firPoint.transform.position);
                        bulletDir = RightRota * bulletDir; //一轮发射完毕，重新向右边旋转回去，方便下一波使用
                        break;
                }
            }
            yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
        }
    }

    /// <summary>
    /// 円形弾幕
    /// </summary>
    /// <param name="number"></param>
    /// <param name="creatPoint"></param>
    /// <returns></returns>
    IEnumerator FirRound(int number, Vector3 creatPoint)
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(10, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
        for (int i = 0; i < number; i++)    //发射波数
        {
            for (int j = 0; j < 36; j++)
            {
                CreatBullet(bulletDir, creatPoint);
                bulletDir = rotateQuate * bulletDir; //让发射方向旋转10度，到达下一个发射方向
            }
            yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
        }
        yield return null;
    }

    /// <summary>
    /// 嵐弾幕
    /// </summary>
    /// <returns></returns>
    IEnumerator FireTurbine()
    {
        Vector3 bulletDir = firPoint.transform.up;      //发射方向
        Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.forward);//使用四元数制造绕Z轴旋转20度的旋转
        float radius = 0.6f;        //生成半径
        float distance = 0.2f;      //每生成一次增加的距离
        for (int i = 0; i < 18; i++)
        {
            Vector3 firePoint = firPoint.transform.position + bulletDir * radius;   //使用向量计算生成位置
            StartCoroutine(FirRound(1, firePoint));     //在算好的位置生成一波圆形弹幕
            yield return new WaitForSeconds(0.05f);      //延时较小的时间（为了表现效果），计算下一步
            bulletDir = rotateQuate * bulletDir;        //发射方向改变
            radius += distance;     //生成半径增加
        }
    }
    /// <summary>
    /// 多重円形弾幕
    /// </summary>
    /// <returns></returns>
    IEnumerator FlowerFireBall() 
    {
        Vector3 bulletdir = this.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);
        List<BulletCharacter> bullets = new List<BulletCharacter>();
        for (int i = 0; i < 8; i++)
        {
           
            var tempBullet = CreatBullet(bulletdir, firPoint.transform.position);
            bulletdir = rotateQuate * bulletdir;
            bullets.Add(tempBullet);

            var clones = GameObject.FindGameObjectsWithTag("bullet");
            foreach (var clone in clones)
            {
                Destroy(clone,3f);

            }
        }
        yield return new WaitForSeconds(1.0f);
        for (int i=0;i<bullets.Count;i++) 
        {
            bullets[i].speed = 0;
            StartCoroutine(FirRound(6, bullets[i].transform.position));
        }
    }


    /// <summary>
    /// 球体弾幕
    /// </summary>
    /// <returns></returns>
    IEnumerator FireBallBulle()
    {
        Vector3 bulletDir = firPoint.transform.up;      //发射方向
        Quaternion rotateQuate = Quaternion.AngleAxis(10, Vector3.forward);//使用四元数制造绕Z轴旋转20度的旋转
        float distance = 1.0f;
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 36; i++)
            {
                Vector3 creatPoint = firPoint.transform.position + bulletDir * distance;
                BulletCharacter tempBullet = CreatBullet(bulletDir, creatPoint);
                tempBullet.isMove = false;
                StartCoroutine(tempBullet.DirChangeMoveMode(10.0f, 0.4f, 15));
                bulletDir = rotateQuate * bulletDir;
                var clones = GameObject.FindGameObjectsWithTag("bullet");
                foreach (var clone in clones)
                {
                    Destroy(clone, 5f);

                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }
    public BulletCharacter CreatBullet(Vector3 dir, Vector3 creatPoint)
    {
        BulletCharacter bulletCharacter = Instantiate(bulletTemplate, creatPoint, Quaternion.identity);
        bulletCharacter.gameObject.SetActive(true);
        bulletCharacter.dir = dir;
        tempBullets.Add(bulletCharacter);
        return bulletCharacter;
    }
    public void ClearBulletsList()
    {
    //    if (tempBullets.Count>0)
    //    {
    //        for (int i = (tempBullets.Count -1);i>=0;i--)
    //        {
    //            //Destroy(tempBullets[i].gameObject);
    //        }
    //    }
        tempBullets.Clear();
    }
}
