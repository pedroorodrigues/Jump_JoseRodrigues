using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject Win;


    public void Start()
    {
        Win.SetActive(false);

    }
    public void Play()
    {
        SceneManager.LoadScene("Jogo");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
    public IEnumerator win()
    {
        print("win");
        Win.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuInicial");

    }


}
