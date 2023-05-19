using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Type
    {
        HealthPotion,
        Coin
    }

    public Type pickupType;
    public int value;
    bool goToPlayer;

    private void Start()
    {
        transform.root.GetComponent<Room>().coinsList.Add(this);
    }

    private void OnDestroy()
    {
        transform.root.GetComponent<Room>().coinsList.Remove(this);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

            if (pickupType == Type.HealthPotion)
            {
                collision.GetComponent<Player>().AddHealth(value);
            }
            else if(pickupType == Type.Coin)
            {
                collision.GetComponent<Player>().AwardCoins(value);
            }
            Destroy(gameObject);
        }
    }
}
