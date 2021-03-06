﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    //Waveプレハブを格納する
        public GameObject[] waves;
    private Manager manager;

    //現在のWave
    private int currentWave;

    private void Start()
    {
        StartCoroutine(emitWave());
    }
    IEnumerator emitWave()
    {
        //waveが存在しなければコルーチンを終了する
        if (waves.Length == 0)
        {
            yield break;
        }
        // Managerコンポーネントをシーン内から探して取得する
        manager = FindObjectOfType<Manager>();
        while (true)
        {
            while (manager.IsPlaying() == false)
            {
                yield return new WaitForEndOfFrame();
            }
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
