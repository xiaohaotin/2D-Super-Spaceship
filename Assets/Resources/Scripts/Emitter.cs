using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    //Waveプレハブを格納する
        public GameObject[] waves;
    [SerializeField] GameManager gameManager;

    //現在のWave
    private int currentWave;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //waveが存在しなければコルーチンを終了する
        if (waves.Length == 0)
        {
            yield break;
        }

        while (true)
        {
            //waveを作成する
            GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);

            //waveをEmitterの子要素にする
            wave.transform.parent = transform;

            //waveの子要素のEnemyが全て削除されるまで待機する
            while (wave.transform.childCount !=0)
            {
                yield return new WaitForEndOfFrame();
            }
            // Waveの削除
            Destroy(wave);

            // 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
            if (waves.Length <= ++currentWave)
            {
                currentWave = 0;
            }

        }
    }
    

   
}
