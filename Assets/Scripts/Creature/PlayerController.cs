using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, InputControls.IPlayerActions
{
    public Player controlPlayer;

    public Cinemachine.CinemachineFreeLook virtualCamera;

    private PlayerInput playerInput;

    private InputControls actions;

    private Vector2 MoveInfo;

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInfo = context.ReadValue<Vector2>();
    }

    public void OnPick(InputAction.CallbackContext context)
    {
        
    }

    // Use this for initialization
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        actions = new InputControls();
        playerInput.actions = actions.asset;
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        actions.Player.SetCallbacks(this);
    }

    void Start() 
    {
        virtualCamera.Follow = controlPlayer.transform;
        virtualCamera.LookAt = controlPlayer.transform;
    }

    void OnEnable()
    {
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Disable();
    }

    void OnDestroy()
    {
        actions.Player.SetCallbacks(null);
        actions.Dispose();
    }

    // Update is called once per frame
    void Update()
    {

        controlPlayer.playerMovement.Move(ConvertFromCameraLocalToWorld(MoveInfo));
    }

    private Vector3 ConvertFromCameraLocalToWorld(Vector2 moveInfo)
    {
        var Cam = Camera.main;
        Vector3 direction = Cam.transform.TransformDirection(new Vector3(moveInfo.x,0,moveInfo.y));
        direction.y = 0; 
        direction.Normalize();
        return direction;
    }
}
