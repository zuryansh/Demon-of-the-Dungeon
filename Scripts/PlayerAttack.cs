
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown;
    Animator animatior;
    public bool isAttackng;

    public int powerCharge;
    public int maxPowerCharge;
    public int spreadFactor;
    public Slider PowerBar;
    public int skullCount;
    public GameObject skullPrefab;
    public int skullSpeed;
    public GameObject flameTrail;



    private void Start()
    {
        animatior = GetComponent<Animator>();
        PowerBar.maxValue = maxPowerCharge;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
        else if (Input.GetMouseButtonDown(1) && powerCharge>=maxPowerCharge)
        {
            powerCharge = 0;
            ChargeAttack();
        }

        PowerBar.value = powerCharge;
        flameTrail.SetActive(powerCharge >= maxPowerCharge);

    }

    void ChargeAttack()
    {
        for (int i = 0; i < skullCount; i++)
        {
            Vector3 mousePos = Utilities.MainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 target = mousePos - new Vector3(Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor),0);
            Projectile skull = Instantiate(skullPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            skull.velocity = target - transform.position;
            skull.velocity.Normalize();
            skull.velocity *= skullSpeed;

        }
    }

    public IEnumerator Attack()
    {

        animatior.SetTrigger("Attack");
        isAttackng = true;

        yield return new WaitForSeconds(0.2f);

        isAttackng = false;
    }
}
