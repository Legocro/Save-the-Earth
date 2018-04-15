using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Main
{
    public class Menus : MonoBehaviour
    {

        public GameObject PremiumPanel;
        public GameObject PausePanel;
        public Canvas Canvas;
        public GameObject PauseTint;
        private int IsPaused = 1;
        // Use this for initialization
        void Start()
        {
          //  ClosePremiumMenu();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayGame()
        {
            SceneManager.LoadScene("Game");
        }
        public void MainMenu()
        {
            SceneManager.LoadScene("StartMenu");
        }
        public void ExitGame()
        {
            Application.Quit();
        }
        public int GetState()
        {
            return IsPaused;
        }
        public void PauseGame()
        {
            PauseTint.SetActive(true);
            PausePanel.SetActive(true);
            IsPaused = 0;
        }
        public void UnpauseGame()
        {
            PauseTint.SetActive(false);
            PausePanel.SetActive(false);
            IsPaused = 1;
        }
        public void OpenPremiumMenu()
        {
            PremiumPanel.SetActive(true);
        }

        public void ClosePremiumMenu()
        {
            PremiumPanel.SetActive(false);
        }
    }
}