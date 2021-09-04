using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpTrigger : MonoBehaviour
{
    private LayerMask PFLayerMask;
    public bool isGrounded;
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.transform.position=new Vector2(collision.gameObject.transform.position.x, 12);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
