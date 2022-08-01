using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlatformmer : MonoBehaviour
{

    [SerializeField] private Collider2D _platformerCollider;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), _platformerCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), _platformerCollider, false);
        }
    }



}
