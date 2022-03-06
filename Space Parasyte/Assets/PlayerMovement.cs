using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;

    public float speed;

    public Animator Animator;
    public float dashSpeed;
    public float dashTime;
    public ParticleSystem dashParticles;
    public ParticleSystem cooldownParticles;
    public float dashCooldown;
    bool canDash = true;
    Player playerScript;
    Vector2 input;

    bool isDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<Player>();

    }

    private void Update()
    {
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        

        movement = input.normalized * speed * 10;

        if(Input.GetKeyDown(KeyCode.Space) && !isDashing && canDash)
        {
            StartCoroutine(Dash());
        }

        #region Animator Setting
        if (rb.velocity.sqrMagnitude > 0.2) 
        {
            Animator.SetBool("isRunning", true);
        }
        else
        {
            Animator.SetBool("isRunning", false);
        }
            
        #endregion
    }

    private void FixedUpdate()
    {

        rb.velocity = movement * Time.fixedDeltaTime;
        
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true; // state mangagement
        playerScript.canBeDamaged = false; // make player immune
        speed *= dashSpeed; // dash
        dashParticles.Play(); // play effects
        yield return new WaitForSeconds(dashTime); // for duration
        playerScript.canBeDamaged = true; 
        speed /= dashSpeed;                  // return to normal
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown); // cooldown
        cooldownParticles.Play();  // regain dash
        canDash = true;
    }
 
    public Vector2 GetPlayerDirection()
    {
        return input;
    }

}
