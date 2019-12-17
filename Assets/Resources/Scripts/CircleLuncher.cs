using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLuncher : MonoBehaviour
{
    public GameObject Bullet;
    public float _Velocity_0, Degree, Angle_Split;

    float _theta;
    float PI = Mathf.PI;


    // Update is called once per frame
    void Update() {
        for (int i = 0; i <= (Angle_Split - 1); i++) {
            //n-way弾の端から端までの角度
            float AngleRange = PI * (Degree / 180);

            //弾インスタンス渡す角度の計算
            if (Angle_Split > 1) {
                _theta = (AngleRange / (Angle_Split - 1)) * i + 0.5f * (PI - AngleRange);
            }
            else 
            {
                _theta = 0.5f * PI;
            }

            //弾インスタンスを取得し、初速度と発射角度を与える
            GameObject Bullet_obj = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
            BulletSc bullet = Bullet_obj.GetComponent<BulletSc>();
            //bullet_cs.theta = _theta;
            //bullet_cs.Velocity_0 = _Velocity_0;
        }
    }
}
