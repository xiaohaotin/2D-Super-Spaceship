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
    public bool FireTurbine_;
    public bool FlowerFireBall_;
    public bool FireBallBulle_;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        tempBullets = new List<BulletCharacter>();
        time = 3;
    }

    // Update is called once per frame
    void Update()
    {
       cooldown += Time.deltaTime;


    }
    private void FixedUpdate()
    {
        Attack();
    }

    public void Attack()
    {
        if (Boss)
        { 
        if (cooldown >= 3)
        {
            if (time == 3)
            {
                ShotGun = true;
                time = 6;

                if (ShotGun)
                {
                    StopAllCoroutines();
                    ClearBulletsList();
                    StartCoroutine(FirShotgun());

                    ShotGun = false;
                }

            }
        }
        if (cooldown >= 10)
        {

            if (time == 6)
            {
                FirRound_ = true;
                time = 9;
                if (FirRound_)
                {
                    StopAllCoroutines();
                    ClearBulletsList();
                    StartCoroutine(FirRound(5, firPoint.transform.position));

                    FirRound_ = false;
                }
            }
        }
            if (cooldown >= 15)
            {
                if (time == 9)
                {
                    FireTurbine_ = true;
                    time = 12;
                    if (FireTurbine_)
                    {
                        StopAllCoroutines();
                        ClearBulletsList();
                        StartCoroutine(FireTurbine());

                        FireTurbine_ = false;
                        
                    }
                }
            }
            if (cooldown >= 20)
            {
                if (time == 12)
                {
                    FlowerFireBall_ = true;
                    time = 15;
                    if (FlowerFireBall_)
                    {
                        StopAllCoroutines();
                        ClearBulletsList();
                        StartCoroutine(FlowerFireBall());

                        FlowerFireBall_ = false;
                        cooldown = -2;
                        time = 3;
                    }
                }
            
            }
            //if (cooldown >= 30)
            //{
            //    if (time == 15)
            //    {
            //        FireBallBulle_ = true;
            //        time = 25;
            //        if (FireBallBulle_)
            //        {
            //            StopAllCoroutines();
            //            ClearBulletsList();
            //            StartCoroutine(FireBallBulle());

            //            FireBallBulle_ = false;

            //        }
            //    }
            //}
        }

    }
    /// <summary>
    /// ShotGun
    /// </summary>
    /// <returns></returns>
    IEnumerator FirShotgun()
    {
        Vector3 bulletDir = firPoint.transform.up; //发射方向は物体のUp轴方向
        Quaternion leftRota = Quaternion.AngleAxis(-30, Vector3.forward);
        Quaternion RightRota = Quaternion.AngleAxis(30, Vector3.forward); //二つのrotationを作ります、Z軸左右30度
        for (int i = 0; i < 10; i++)     //弾を打つ回数
        {
            for (int j = 0; j < 3; j++) //一回三つ弾を打つ
            {
                switch (j)
                {
                    case 0:
                        CreatBullet(bulletDir, firPoint.transform.position);  //最初の弾を打つと方向は変わらない
                        break;
                    case 1:
                        bulletDir = RightRota * bulletDir;//最初の弾を打った後、次の方向へ変更する
                        CreatBullet(bulletDir, firPoint.transform.position);
                        break;
                    case 2:
                        bulletDir = leftRota * (leftRota * bulletDir); //右方向発射終了後、左へ回転2回同じ角度で次の発射方向へ変更する
                        CreatBullet(bulletDir, firPoint.transform.position);
                        bulletDir = RightRota * bulletDir; //1 round発射終了後、右方向回転して、次の発射 roundを準備する
                        break;
                }
            }
            yield return new WaitForSeconds(0.5f); //0.5秒後次の発射 round開始
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
        Quaternion rotateQuate = Quaternion.AngleAxis(10, Vector3.forward);
        for (int i = 0; i < number; i++)    
        {
            for (int j = 0; j < 36; j++)
            {
                CreatBullet(bulletDir, creatPoint);
                bulletDir = rotateQuate * bulletDir; 
            }
            yield return new WaitForSeconds(0.5f); 
        }
        yield return null;
    }

    /// <summary>
    /// 嵐弾幕
    /// </summary>
    /// <returns></returns>
    public IEnumerator FireTurbine()
    {
        Vector3 bulletDir = firPoint.transform.up;      
        Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.forward);
        float radius = 0.6f;        
        float distance = 0.2f;      
        for (int i = 0; i < 18; i++)
        {
            Vector3 firePoint = firPoint.transform.position + bulletDir * radius;   
            StartCoroutine(FirRound(1, firePoint));     
            yield return new WaitForSeconds(0.05f);      
            bulletDir = rotateQuate * bulletDir;        
            radius += distance;     
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
        Quaternion rotateQuate = Quaternion.AngleAxis(10, Vector3.forward);
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
    
        tempBullets.Clear();
    }
}
