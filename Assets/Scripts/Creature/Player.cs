using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    public CreatureScript creatureScript;

    public PlayerSettings playerSettings;

    public PlayerMovement playerMovement;

    private Quaternion rotation;
    private void Awake()
    {
        playerSettings = creatureScript.playerSettings;
    }
}
