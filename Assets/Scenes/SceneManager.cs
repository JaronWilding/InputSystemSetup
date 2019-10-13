using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;



public class SceneManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public List<UserInputs> allUsers = new List<UserInputs>();
    private bool gamePaused;
    public void Awake()
    {
        canvas.enabled = false;
    }

    public void PauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            gamePaused = true;
            Debug.Log("Paused");
            canvas.enabled = true;
            foreach(UserInputs _inputs in allUsers)
            {
                _inputs.current_Actions.Disable();
                _inputs.current_UI.Enable();
            }
        }
        else
        {
            Time.timeScale = 1f;
            gamePaused = false;
            Debug.Log("UnPaused");
            canvas.enabled = false;
            foreach (UserInputs _inputs in allUsers)
            {
                _inputs.current_Actions.Enable();
                _inputs.current_UI.Disable();
            }
        }
    }

    public void InputA()
    {
        Debug.Log("Button 1 Clicked");
    }

    public void InputB()
    {
        Debug.Log("Button 2 Clicked");
    }
}
