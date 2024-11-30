using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.gameObject.activeSelf)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void QuitGame()
        {
            Time.timeScale = 1; // Garante que o tempo está correndo normalmente
            SceneManager.LoadScene(1); // Carrega a cena de selecionar o level
        }
}
