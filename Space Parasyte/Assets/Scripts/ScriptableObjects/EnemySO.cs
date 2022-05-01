using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy" , menuName ="Enemy")]
public class EnemySO : ScriptableObject
{
    public enum EnemyType
    {
        Melle,
        Ranged
    }

    public EnemyType enemyType;
    public int health;
    public int rewardedCoins;
    public  GameObject[] loot;

    public float maxSight;
    public float speed;

    public int damage = 5;
    public float attackCooldown = 1.5f;
    public float projectileForce;
    public GameObject projectile;

    public bool leavesPoisonTrail;
    public GameObject poisonTrail;
    public float trailDuration;
    public float trailcooldown;



}
