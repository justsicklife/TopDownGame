using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float power;
    public Vector2 direction;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2d.velocity += direction * power * Time.deltaTime;
    }
}