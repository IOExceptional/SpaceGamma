using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Screen.lockCursor = false;
    }

    public void OnPlayClick()
    {
        Application.LoadLevel("main_scene");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
