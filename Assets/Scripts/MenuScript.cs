using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex
         + 1);
    }
    public void ExitCLicked()
    {
        Application.Quit();
    }
}
