using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum JellyFishState
{
    Idle,
    Chase,
    Death
}

public class JellyFish : MonoBehaviour
{
    public float speed = 2f;

    public JellyFishState jellyFishState = JellyFishState.Idle;

    public DetectionRange detectionRange;

    public Rigidbody2D rigidbody2D;

    public float colliderRadius = 5f;

    public Vector2 direction;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        detectionRange = gameObject.GetComponentInChildren<DetectionRange>();
        detectionRange.GetComponent<CircleCollider2D>().radius = colliderRadius;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (detectionRange.isPayerInRange)
        {
            jellyFishState = JellyFishState.Chase;
        }
        else
        {
            jellyFishState = JellyFishState.Idle;
        }

        switch (jellyFishState)
        {
            case JellyFishState.Idle:
                rigidbody2D.velocity = Vector2.zero;
                animator.SetFloat("LastInputX", direction.x);
                animator.SetFloat("LastInputY", direction.y);
                animator.SetBool("IsWalking", false);
                break;
            case JellyFishState.Chase:
                Vector2 directionPlayer = (detectionRange.playerPos - (Vector2)this.transform.position).normalized;
                rigidbody2D.velocity = directionPlayer * speed;
                SetAnimator(directionPlayer);
                animator.SetFloat("InputX", direction.x);
                animator.SetFloat("InputY", direction.y);
                animator.SetBool("IsWalking", true);
                break;
        }
    }

    private void SetAnimator(Vector2 dir)
    {
        float dirX = dir.x;
        float dirY = dir.y;

        // 0.5 이상이면 1, -0.5 이하이면 -1, 그 외는 0
        dirX = Mathf.Abs(dirX) > 0.5f ? Mathf.Sign(dirX) : 0f;
        dirY = Mathf.Abs(dirY) > 0.5f ? Mathf.Sign(dirY) : 0f;

        direction = new Vector2(dirX, dirY);
    }

}
