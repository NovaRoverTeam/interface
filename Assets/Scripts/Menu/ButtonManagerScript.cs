using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManagerScript : MonoBehaviour {

    public void GotoScene (string LoadTarget)
    {
        //will create a field in unity for selecting the scene to go to. Ensure that the scene is added to the project under file -> project settings
        SceneManager.LoadScene(LoadTarget);
    }

    public void Exit()
    {
        //will create a field in unity for selecting the scene to go to. Ensure that the scene is added to the project under file -> project settings
        Application.Quit();
    }
}
