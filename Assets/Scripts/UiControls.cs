using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiControls : MonoBehaviour
{
    
    GunController gunController;

    public GameObject gameOverPanel, gameWinPanel;

    public static UiControls instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gunController = FindObjectOfType<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowGameWinPanel()
    {
        gameWinPanel.SetActive(true);
    }
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
