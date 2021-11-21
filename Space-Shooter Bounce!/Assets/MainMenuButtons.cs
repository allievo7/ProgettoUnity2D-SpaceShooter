using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public MainMenuAnim player;
    public GameObject VaiQui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == VaiQui.transform.position)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void NewGame()
    {
        player.isNewGame = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
