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
    public float dashDistance;
    Collider2D playerCollider;
    public bool isDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
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

        //Cast a collider
        CastDashSphere();




        yield return new WaitForSeconds(dashTime); // for duration
        playerCollider.isTrigger = false;
        playerScript.canBeDamaged = true; 
        speed /= dashSpeed;                  // return to normal
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown); // cooldown
        cooldownParticles.Play();  // regain dash
        canDash = true;

    }
    
    void CastDashSphere()
    {
        ContactFilter2D filter = new ContactFilter2D();

        RaycastHit2D[] results = new RaycastHit2D[15];
        Physics2D.CircleCast(transform.position, dashDistance, Vector2.zero, filter, results);
        
        foreach (RaycastHit2D hit in results)
        {
            if (hit.transform != null)
            {
                //Debug.Log(hit.transform.childCount);
            }
            if (hit.transform != null)
            {
                if (hit.transform.gameObject.layer == 3) // 3 is the level layer
                {
                    //we hit the level
                    return;

                }
                
            }
        }
        playerCollider.isTrigger = true;
    }

    public Vector2 GetPlayerDirection()
    {
        return input;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, movement.normalized);
    }
}
