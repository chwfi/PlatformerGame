using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoarder : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            transform.parent.GetComponent<Enemy_Ball>().MoveDir = -1;
        }
    }

}
