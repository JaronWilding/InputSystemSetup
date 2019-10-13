using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.UI;

public class UserInputs
{
    public InputUser currentUser;
    public InputActionMap current_UI;
    public InputActionMap current_Actions;


    public UserInputs(InputUser _currentUser, InputActionMap _current_UI, InputActionMap _current_Actions)
    {
        this.currentUser = _currentUser;
        this.current_UI = _current_UI;
        this.current_Actions = _current_Actions;
    }
}
public class Input : MonoBehaviour
{
    [SerializeField] private int gamePadId;
    [SerializeField] private InputSystemUIInputModule uiInput;
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private UnityEvent PauseEvent;
    [SerializeField] private Canvas canvas;

    private InputActionMap _actions;
    private InputActionMap _UI;

    private Vector2 dir;
    private bool gamePaused;
    
    private InputUser newIP;

    void Awake()
    {
        InputActionAsset input = Instantiate(uiInput.actionsAsset);

        _actions = input.FindActionMap("Player");
        _UI = input.FindActionMap("UI");

        newIP = InputUser.PerformPairingWithDevice(Gamepad.all[gamePadId]);
        newIP.AssociateActionsWithUser(input);
        
        _sceneManager.allUsers.Add(new UserInputs(newIP, _UI, _actions));


        InputAction movement = _actions.FindAction("Movement");
        InputAction pause = _actions.FindAction("Pause Menu");
        InputAction unPause = _UI.FindAction("Pause Menu");
        
        movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        movement.canceled += ctx => dir = Vector2.zero;

        pause.performed += PauseMenuBLAh;
        unPause.performed += PauseMenuBLAh;
        canvas.enabled = false;
    }
    
    public void OnEnable()
    {
        _actions.Enable();
        _UI.Disable();
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            gamePaused = true;
            canvas.enabled = true;
            _actions.Disable();
            _UI.Enable();
        }
        else
        {
            Time.timeScale = 1f;
            gamePaused = false;
            canvas.enabled = false;
            _actions.Enable();
            _UI.Disable();
        }
    }

    public void OnDisable()
    {
        _actions.Disable();
        _UI.Disable();
    }

    private void PauseMenuBLAh(InputAction.CallbackContext context)
    {
        PauseEvent.Invoke();
    }
    
    void Update()
    {

        transform.position += new Vector3(dir.x, transform.position.y, dir.y) * 4.0f * Time.deltaTime;
    }
}
