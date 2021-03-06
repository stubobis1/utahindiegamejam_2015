﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public float jumpForce = 10f;         // Amount of force added when the player jumps.
    
    public float switchCooldown;

    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool isGrounded = false;
    private grounded groundedScript;
    private Animator anim;                  // Reference to the player's animator component.

    
    float currentSwitchCooldown = 0.0f;
    float attackCooldown = 0.0f;

    [HideInInspector]
    public GameManager GM;
    [HideInInspector]
    public int currentLayer;

    void Awake()
    {
        // Setting up references.

        groundCheck = transform.Find("groundCheck");
        groundedScript = groundCheck.GetComponent<grounded>();
        anim = GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.backZone.GetComponent<AudioSource>().mute = true;
        
            
        //GameObject.Find("Normal_Audio").GetComponent<AudioSource>().mute = false;
        currentLayer = LayerMask.NameToLayer("Front");
    }

    void Update()
    {
        // The player is grounded if a raycast from the groundCheck position hits anything on the ground layer.
        if (Input.GetButtonDown("Jump") && groundedScript.isGrounded)
        {
            // Play a random jump audio clip.
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            groundedScript.isGrounded = false;
        }

        if (currentSwitchCooldown <= 0.0f && Input.GetButtonDown("Fire2"))
        {
            SwitchLayer();
            currentSwitchCooldown = switchCooldown;
        }

        if (currentSwitchCooldown > 0)
        {
            currentSwitchCooldown -= Time.deltaTime;
        }

        if(attackCooldown <= 0.0f && Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Roundhouse");

            Vector3 currentPosition = transform.position;

            Collider2D[] collisions;

            if (facingRight)
                collisions = Physics2D.OverlapAreaAll(new Vector2(currentPosition.x + .05f, currentPosition.y + .2f),
                    new Vector2(currentPosition.x + 1.0f, currentPosition.y - .4f));
            else
                collisions = Physics2D.OverlapAreaAll(new Vector2(currentPosition.x - .05f, currentPosition.y + .2f),
                    new Vector2(currentPosition.x - 1.0f, currentPosition.y - .4f));

            foreach (Collider2D c in collisions)
            {
                if (c.gameObject.tag == "Enemy" && c.gameObject.layer == currentLayer)
                    c.gameObject.BroadcastMessage("Death");
            }

            attackCooldown = 0.5f;
        }

        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if (this.transform.position.y < -50)
            Death();
    }
 
    void FixedUpdate()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        anim.SetFloat("vSpeed", body.velocity.y);

        anim.SetBool("Grounded", groundedScript.isGrounded);

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            body.AddForce(Vector2.right * h * moveForce);
        
        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(body.velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            body.velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();
    }

    void Death()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void ChangeMusic(bool frontMusic, AudioSource front, AudioSource back)
    {
        if (frontMusic)
        {
            front.mute = false;
            back.mute = true;
        }
        else
        {
            front.mute = true;
            back.mute = false;
        }
    }


    void SwitchLayer()
    {
        int nextLayer = currentLayer == LayerMask.NameToLayer("Front") ? LayerMask.NameToLayer("Back") : LayerMask.NameToLayer("Front");

        Transform groundCheck = gameObject.transform.FindChild("groundCheck");

        if (groundCheck == null)
            Debug.LogError("GROUNDCHECK MISSING!");

        Vector3 currentPosition = transform.position;

        Collider2D[] otherZoneCollisions = Physics2D.OverlapAreaAll(new Vector2(currentPosition.x - .15f, currentPosition.y + .3f),
            new Vector2(currentPosition.x + .15f, currentPosition.y - .2f),1 << nextLayer);

        foreach (Collider2D c in otherZoneCollisions)
            if (c.tag != "Enemy")
            {
                currentSwitchCooldown = 0.0f;
                return;
            }

        bool isFront = (nextLayer == LayerMask.NameToLayer("Front"));
        ChangeMusic(isFront, GM.frontZone.GetComponent<AudioSource>(), GM.backZone.GetComponent<AudioSource>());

        gameObject.layer = currentLayer = nextLayer;
        groundCheck.gameObject.layer = currentLayer;

        GameObject[] zones = GameObject.FindGameObjectsWithTag("Zone");

        foreach (GameObject go in zones)
        {
            SpriteRenderer[] zoneRenderers = go.GetComponentsInChildren<SpriteRenderer>();

            if (go.GetComponent<zoneScript>().isFront)
            {
                go.GetComponent<zoneScript>().isFront = false;
                foreach (SpriteRenderer sr in zoneRenderers)
                {

                    if (sr.transform.tag == "Background")
                    {
                        sr.color = Color.clear;
                        sr.sortingLayerName = "BackSort";
                    }
                    else
                    {
                        sr.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    }

                }
            }
            else
            {
                go.GetComponent<zoneScript>().isFront = true;

                foreach (SpriteRenderer sr in zoneRenderers)
                {
                    if (sr.transform.tag == "Background")
                    {
                        sr.color = Color.white;
                        sr.sortingLayerName = "BackSort";
                    }
                    else
                    {
                        sr.color = new Color(1f, 1f, 1f, 1f);
                        sr.sortingLayerName = "FrontSort";
                    }
                }
            }
        }
    }
}