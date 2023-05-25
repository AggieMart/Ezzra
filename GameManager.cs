using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image HealthImage;

    public float health;
    public static bool GameStarted = false;
    public Camera IntroCam, PlayerCam;
    public float IntroLength = 2;
    public GameObject UI;
    public GameObject LoseScreen;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        LoseScreen.SetActive(false);
        PlayerCam.enabled = false;
        IntroCam.enabled = true;
        health = 1;
        UI.SetActive(false);
        FindObjectOfType<CharacterController>().enabled = false;
        yield return new WaitForSeconds(IntroLength);
        StartGame();
    }


   
    // Update is called once per frame
    void Update()
    {
        if(GameStarted)
        {
            if (health > 0)
            {
                health -= Time.deltaTime * 0.01f;
                HealthImage.fillAmount = health;
            }


            if (health <= 0)
            {
                GameOver();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }

    }

    public void StartGame()
    {
        GameStarted = true;
        IntroCam.enabled = false;
        PlayerCam.enabled = true; 
        UI.SetActive(true);
        FindObjectOfType<CharacterController>().enabled = true;
    }
    void GameOver()
    {
        GameStarted = false;
        print("gameover");
        PlayerCam.enabled = false;
        IntroCam.enabled = true;
        LoseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
