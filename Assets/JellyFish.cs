using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
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
                break;
            case JellyFishState.Chase:
                Vector2 direction = (detectionRange.playerPos -(Vector2)this.transform.position).normalized;
                rigidbody2D.velocity = direction * speed;
                break; 
        }
    }

}
