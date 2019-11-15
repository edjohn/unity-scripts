using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy SFX")]
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] AudioClip enemyShootSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    [Header("Enemy VFX")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject enemyExplosionEffect;
    [SerializeField] float durationOfExplosion = 1f;
    [Header("Enemy Config")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 5f;
 
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyShootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(enemyExplosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
