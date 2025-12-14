using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerRope : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Rope")
        {
            PlatformePlayerClimb playerClimb = collision.GetComponentInParent<PlatformePlayerClimb>();
            playerClimb.isInteraction = true; 
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Rope")
        {
            PlatformePlayerClimb playerClimb = collision.GetComponentInParent<PlatformePlayerClimb>();
            playerClimb.ExitClimb();
            playerClimb.isInteraction = false; 
        }
    }

}
