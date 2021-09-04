using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsOnGround : MonoBehaviour
{
    private LayerMask PFLayerMask;
    public bool isGrounded;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            isGrounded = false;
    }
}
