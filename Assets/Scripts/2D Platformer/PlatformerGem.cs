using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGem : MonoBehaviour, PlatformerIItem
{
    public void Collect()
    {   
        Destroy(gameObject);
    }
}
