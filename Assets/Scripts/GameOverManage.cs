using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManage : MonoBehaviour
{
   public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenus");
    }
}
