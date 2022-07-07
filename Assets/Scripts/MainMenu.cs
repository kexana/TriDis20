using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject SetupScreen;
    public GameObject OptionsMenu;
    public GameObject HelpScreen;
    public GameObject ControlsScreen;
    public GameObject InfoScreen;
    public string GameScene;
    public Text FieldDepth;
    private int FinalFieldDepth;
    public void Start()
    {
        OptionsMenu.SetActive(false);
        HelpScreen.SetActive(false);
        SetupScreen.SetActive(false);
        ControlsScreen.SetActive(false);
        InfoScreen.SetActive(false);
        FinalFieldDepth = 1;
    }
    public void OpenSetup()
    {
        SetupScreen.SetActive(true);
    }
    public void CloseSetup()
    {
        SetupScreen.SetActive(false);
    }
    public void StartGame()
    {
        InstantiateField.FieldDepth = FinalFieldDepth;
        SceneManager.LoadScene(GameScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        OptionsMenu.SetActive(true);
    }
    public void CloseOptions()
    {
        OptionsMenu.SetActive(false);
    }
    public void OpenHelp() {
        HelpScreen.SetActive(true);
    }
    public void CloseHelp()
    {
        HelpScreen.SetActive(false);
    }
    public void OpenCtrlScreen()
    {
        ControlsScreen.SetActive(true);
    }
    public void CloseCtrlScreen()
    {
        ControlsScreen.SetActive(false);
    }
    public void OpenInfoScreen()
    {
        InfoScreen.SetActive(true);
    }
    public void CloseInfoScreen()
    {
        InfoScreen.SetActive(false);
    }
    public void SliderChange(float value)
    {
        FieldDepth.text = value.ToString();
        FinalFieldDepth = (int)value;
    }
}
