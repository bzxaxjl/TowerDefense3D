using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnSelectGame()
    {
        SceneManager.LoadScene(3);
    }
    public void OnStartGame1()
    {
        SceneManager.LoadScene(1);
    }
    public void OnStartGame2()
    {
        SceneManager.LoadScene(4);
    }
    public void OnStartGame3()
    {
        SceneManager.LoadScene(5);
    }
    public void OnStartGame4()
    {
        SceneManager.LoadScene(6);
    }
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnHelpGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OnQuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
