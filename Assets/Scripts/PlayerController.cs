using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : Common
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private GameObject joystickBg;
    [SerializeField] private float speed;

    private Rigidbody rb;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    
    }

    void Update()
    {
        Move();
    
    }

    void Move()
    {
        if (joystickBg.activeInHierarchy == true)
        {
            Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;

            rb.velocity = direction * speed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);

            anim.SetBool("_isRunning", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("_isRunning", false);
        }

    }

    public override void Collect(GameObject collectable)
    {
        //ozel islmer
    }
}
