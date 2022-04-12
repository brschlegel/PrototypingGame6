// Base code source: Katarina Tretter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Tooltip("UI Manager")][SerializeField] private GameObject uI;
    [Tooltip("Game Manager")][SerializeField] private GameObject gM;

    //---------GENERAL BUTTONS---------

    public void OnStart()
    {
        Debug.Log("Clicked Start");
        Time.timeScale = 1;
        SceneManager.LoadScene("InfoScene");
    }

    public void OnResume()
    {
        Debug.Log("Clicked Resume");
        uI.GetComponent<UIManager>().ButtonPress("resume");
    }

    public void OnPause()
    {
        Debug.Log("Clicked Pause");
        uI.GetComponent<UIManager>().ButtonPress("pause");
    }

    public void OnOptions()
    {
        Debug.Log("Clicked Options");
        uI.GetComponent<UIManager>().ButtonPress("options");
    }

    public void OnBack()
    {
        Debug.Log("Clicked Back");
        uI.GetComponent<UIManager>().ButtonPress("back");
    }

    public void OnCredits()
    {
        Debug.Log("Clicked Credits");
        SceneManager.LoadScene("Credits");
    }

    public void OnMainMenu()
    {
        Debug.Log("Clicked Main Menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
        Debug.Log("Clicked Quit");
        Application.Quit();
    }
}
