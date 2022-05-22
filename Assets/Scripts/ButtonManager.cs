using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnButtonPress()
    {
        switch(gameObject.name)
        {
            case "PlayGameButtonManager":
                SceneManager.LoadScene("WorldStart");
                break;
            case "QuitGameButtonManager":
                Application.Quit();
                break;
            case "PauseButtonManager":
                break;
        }
    }
}
