using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player controlPlayer;

    private Animator animator;

    private float turnSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        turnSpeed = controlPlayer.playerSettings.turnSpeed;
    }

    public void Move(Vector3 moveDirection)
    {
        var magnitude = moveDirection.magnitude;
        if (magnitude > 0)
        {
            UpdateTurn(moveDirection);
        }
        animator.SetBool("isRunning", magnitude > 0);
    }

    private void UpdateTurn(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        controlPlayer.transform.rotation = Quaternion.RotateTowards(controlPlayer.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        var deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0; // 确保只在水平面上移动
        if (deltaPosition.magnitude < 0.01f)
        {
            return; // 如果移动距离很小，则不进行移动
        }
        controlPlayer.transform.position += controlPlayer.transform.forward * deltaPosition.magnitude;
    }
}
