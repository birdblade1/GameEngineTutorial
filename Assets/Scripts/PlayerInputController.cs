using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{

    public PlayerAction inputAction;


    //singleton
    public static PlayerInputController controller;

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    // Start is called before the first frame update
      private void Awake()
    {
        //instance
        if (controller == null)
        {
            controller = this;
        }

        inputAction = new PlayerAction();
    }
    // Update is called once per frame
    void Update()
    {

    }
}