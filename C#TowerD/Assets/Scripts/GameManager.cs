using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject endUI;//显示UI
    public Text endMessage;//显示文字
    public static GameManager Instance;
    private EnemySpawner enemySpawner;
    public AudioSource audioBackSource;
    public AudioSource audioFailSource;
    public AudioSource audioWinSource;
    void Awake()
    {
        Instance = this;
        audioBackSource.Play();
        enemySpawner = GetComponent<EnemySpawner>();
        //游戏结束时控制停止敌人生成
    }

    public void Win()
    {
        endUI.SetActive(true);
        audioBackSource.Stop();
        audioWinSource.Play();
        endMessage.text = "胜 利";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        audioBackSource.Stop();
        audioFailSource.Play();
        endMessage.text = "失 败";
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
