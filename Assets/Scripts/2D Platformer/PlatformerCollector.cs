using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlatformerIItem item = collision.GetComponent<PlatformerIItem>();
        if(item != null)
        {
            item.Collect();
        }
    }
}
