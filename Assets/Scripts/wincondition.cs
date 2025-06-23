using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wincondition : MonoBehaviour
{

    public GameObject Win;
    public GameObject menu;

    public void Start()
    {
        
        Win.SetActive(false);
        menu.SetActive(false);

    }
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Jogo");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void closemenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }
    public IEnumerator win()
    {
        print("win");
        Win.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");

    }
    public void pause()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(win());
    }
}
