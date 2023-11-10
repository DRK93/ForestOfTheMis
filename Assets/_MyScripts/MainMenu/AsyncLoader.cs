using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace _MyScripts.MainMenu
{
    public class AsyncLoader : MonoBehaviour
    {
        [Header("Menu Screens")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject mainMenu;
    
        [Header("Slider")]
        [SerializeField] private Slider loadingSlider;

        public void LoadLevelButton(string levelToLoad)
        {
            mainMenu.SetActive(false);
            loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelAsync(levelToLoad));
        }

        IEnumerator LoadLevelAsync(string levelToLoad)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressionValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                loadingSlider.value = progressionValue;
                yield return null;
            }
        }
    }
}

