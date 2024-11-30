using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int indexScene;
    
    public void LoadSelectLevel(int indexSceneSelect)
    {
        indexScene = indexSceneSelect;
        StartCoroutine("Abrir");
    }

    private IEnumerator Abrir()
    {
        yield return new WaitForSeconds(0.5f);
        
        SceneManager.LoadScene(indexScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}