using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ClimbState
{
    None,
    Hanging,
    Moving
}

public class PlatformePlayerClimb : MonoBehaviour
{

    public float climbSpeed = 2f;

    public ClimbState climbState = ClimbState.None;

    public bool isInteraction = false;

    private bool movingTrigger = false;

    private PlatformerPlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlatformerPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(climbState == ClimbState.None)
        return;

        float vertical = Input.GetAxisRaw("Vertical");

        switch (climbState)
        {
            case ClimbState.Hanging:
            HandleHanging(vertical);
            break;
            case ClimbState.Moving:
            HandleMoving(vertical);
            break;
        }
    }

    void HandleHanging(float vertical)
    {
        playerController.rb.velocity = Vector2.zero;
        playerController.rb.gravityScale = 0f;

        if (vertical != 0)
        {
            climbState = ClimbState.Moving;
        }
    }

    void HandleMoving(float vertical)
    {
        playerController.rb.gravityScale = 0f;
        playerController.rb.velocity = new Vector2(0f, vertical * climbSpeed);

        if (vertical == 0)
        {
            climbState = ClimbState.Hanging;
        } 
    }

    void EnterClimb()
    {
        climbState = ClimbState.Hanging;
        playerController.rb.velocity = Vector2.zero;
        playerController.rb.gravityScale = 0f;
    }

    public void ExitClimb()
    {
        climbState = ClimbState.None;
        playerController.rb.gravityScale = playerController.baseGravity;
    }

    public void Interaction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!isInteraction) return;

        // 🔥 None 상태에서만 Hanging 가능
        if (climbState == ClimbState.None)
        {
            EnterClimb();
        }
    }

}