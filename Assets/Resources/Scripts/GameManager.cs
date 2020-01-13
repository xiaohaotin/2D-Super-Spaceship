using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverTextobj;
    [SerializeField] GameObject gameClearTextobj;
    [SerializeField] Text scoreText;

    const int MAX_SCORE = 9999;
    int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }
    public void AddScore(int val)
    {
        score += val;
        if (score > MAX_SCORE)
        {
            score = MAX_SCORE;
        }
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        gameOverTextobj.SetActive(true);
        Invoke("ReStartThisScene", 1f);
        //ReStartThisScene();
    }

    public void GameClear()
    {
        gameClearTextobj.SetActive(true);
        Invoke("ReStartThisScene", 1f);
        //ReStartThisScene();
    }

    void ReStartThisScene()
    {
        Scene ThisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(ThisScene.name);
    }
}
