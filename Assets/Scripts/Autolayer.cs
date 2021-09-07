using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Autolayer : MonoBehaviour
{
    private SpriteRenderer idk;
    // Start is called before the first frame update
    private void Start()
    {
        idk = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        idk.sortingOrder = -Mathf.FloorToInt(transform.position.z * 100);
    }
}
