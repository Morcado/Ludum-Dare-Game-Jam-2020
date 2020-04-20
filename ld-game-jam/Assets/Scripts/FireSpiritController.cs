﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritController : MonoBehaviour
{
    [SerializeField] private bool facingLeft = false;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float moveSpeedFactor = 0.5f;
    [SerializeField] private GameObject fireball;
    private SpriteRenderer spriteRend;
    private Rigidbody2D enemy;
    private Animator animator;
    /* Four different styles for enemies. Maybe add a falling state*/
    private enum State {idle, attack, death};
    private State state = State.idle;
    private float timeLeft = 5.0f;

    // Start is called before the first frame update
    void Start() {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        /* this defines if the enemy starts moving towards left or towards right.
         it checks the center of the screen. This has to be changed to check the 
         tree x position */
        facingLeft = transform.position.x < 0 ? true : false;
       
    }

    // Update is called once per frame
    void Update() {
        if (state != State.attack)
            Shoot();
        animator.SetInteger("state", (int)state);
        Debug.Log((int)state);
    }

    public void Shoot() {
     
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0) {
            state = State.attack;
            if (facingLeft) {
                Instantiate(fireball, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
            }
            else {
                Instantiate(fireball, new Vector3(transform.position.x - 1, transform.position.y, 0), Quaternion.identity);
            }
        }
    }

    public void ReturnIdle() {
        timeLeft = 2.0f;
        state = State.idle;
        Debug.Log("idle");
    }
}
