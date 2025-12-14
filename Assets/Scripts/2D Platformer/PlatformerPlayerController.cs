using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;

    bool isFacingRight = true;

    [Header("Movement")]
    public float moveSpeed = 5f;

    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.49f,0.03f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMutiplier = 2f;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f,0.05f);
    public LayerMask wallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2;
    bool isWallSliding;


    // Wall Jumping
    bool isWallJumping;
    float wallJumpDirection;
    // 벽 점프 상태 유지시간
    float wallJumpTime = 0.5f;
    // 벽 점프 입력 가능 시간
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f,10f);

    private PlatformePlayerClimb playerClimb;

    void Start()
    {
        playerClimb = GetComponent<PlatformePlayerClimb>();
    }

    private bool WallCheck()
    {   
        Collider2D hit = Physics2D.OverlapBox(
            wallCheckPos.position,
            wallCheckSize,
            0,
            wallLayer
            );
        return hit != null; 
    }

    void Update()
    {
        
        if(playerClimb.climbState != ClimbState.None)
            return;
        
        ProcessGravity();

        ProcessWallSlide();

        GroundCheck();

        ProcessWallJump();
    
        // 벽 점프 중이 아니거나 
        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalMovement * moveSpeed,rb.velocity.y);
            Flip();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if(playerClimb.climbState == ClimbState.Hanging || playerClimb.climbState == ClimbState.Moving)
        {
            if(context.performed)
            {
                Debug.Log("밧줄에서 점프함");
                rb.velocity = new Vector2(rb.velocity.x,jumpPower);
                playerClimb.climbState = ClimbState.None;
            }
        }

        if(jumpsRemaining > 0)
        {
            if(context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x,jumpPower);
                jumpsRemaining--;
            }
            else if(context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y* 0.5f);
                jumpsRemaining--;
            }
        }

        // Wall Jump
        // 벽 점프가 가능 하고 
        // 벽 점프 할때 
        if(context.performed && isWallSliding)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x,wallJumpPower.y); // Jump away from wall
            
            // 방향과 다르다면 
            if(transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump),wallJumpTime + 0.1f);
        }
    }

    // 중력 떨어지는 속도 증가함
    public void ProcessGravity ()
    {
        if(rb.velocity.y < 0)
        {
            // 중력 크기 조절 점점 가속도 붙음
            rb.gravityScale = baseGravity * fallSpeedMutiplier;
            // 가속도 붙는 중력에서 최대 범위를 지정함 
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Max(rb.velocity.y,-maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;  
        }
    }

    // 벽에 붙으면 밑으로 슬라이드하게
    private void ProcessWallSlide()
    {
        // 땅에 붙어있지 않고 벽이랑 붙어있고 움직임 방향이 있다면
        if(!isGrounded && WallCheck() && horizontalMovement != 0)
        { 
            isWallSliding = true;
            // 뭔말인지 몰루
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Max(rb.velocity.y,-wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    // 벽 점프 하기전에 벽 점프 가능한지 보는 함수
    private void ProcessWallJump()
    {
        if(isWallSliding && !isWallJumping)
        {
            wallJumpDirection = -transform.localScale.x;
            // wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        } 
        // else if(wallJumpTimer > 0f)
        // {
        //     wallJumpTimer -= Time.deltaTime;
        // }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    // 좌우 반전
    private void Flip()
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    // 캐릭터가 땅 밟고있는지 체크
    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize,0,groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position,groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position,wallCheckSize);
    }
}
