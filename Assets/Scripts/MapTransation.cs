using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MapTransation : MonoBehaviour
{

    [SerializeField]
    PolygonCollider2D mapBoundry;
    CinemachineConfiner confiner;

    [SerializeField] Direction direction;
    [SerializeField] float additivePos = 2f;


    enum Direction { Up, Down, Left, Right }

    void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = mapBoundry;
            confiner.InvalidatePathCache();
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += additivePos;
                break;
            case Direction.Down:
                newPos.y -= additivePos;
                break;
            case Direction.Left:
                newPos.x += additivePos;
                break;
            case Direction.Right:
                newPos.x -= additivePos;
                break;
        }

        player.transform.position = newPos;

    }

}
