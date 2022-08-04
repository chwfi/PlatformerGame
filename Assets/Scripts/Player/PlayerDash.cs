using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

}
