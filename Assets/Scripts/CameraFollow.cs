using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float cameraMoveSpeed = 5;
    Vector2 obPosition = new Vector2(0, 0);

    private void Update()
    {
       // transform.position = Vector2.Lerp(transform.position, target.position.y, Time.deltaTime)
    }
}
