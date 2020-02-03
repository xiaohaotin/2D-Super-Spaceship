using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoAttack : MonoBehaviour
{
    public GameObject Zako;
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    public List<BulletCharacter> tempBullets;
    public float time = 0;
    public float cooldown = 0;
    public bool ShotGun;
    Animator animator;
    public AudioClip shootSE;
    
    // Start is called before the first frame update
    void Start()
    {
        tempBullets = new List<BulletCharacter>();
        time = 3;
        animator = GetComponent<Animator>();
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
        if (Zako)
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
                        AudioSource.PlayClipAtPoint(shootSE, Camera.main.transform.position);
                        ShotGun = false;
                    }

                }
            }
        }
    }
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
