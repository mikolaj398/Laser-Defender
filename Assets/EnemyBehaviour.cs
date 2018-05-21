using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject enemyProjectile;
    public float enemyProjectileSpeed = 10f;
    public float health = 150f;
    public float fireRatePerSecond = 0.5f;
    private int pointePerEnemy = 150;
    public AudioClip enemyFire;
    public AudioClip enemyDies;
    ScoreKeeper scoreKeeper;
    private void Start()
    {
        scoreKeeper = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreKeeper>();
    }
    private void Update()
    {
        float probability = Time.deltaTime * fireRatePerSecond;
        if (Random.value < probability) Fire();
    }
    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject enemyMissle = Instantiate(enemyProjectile, startPosition, Quaternion.identity);
        enemyMissle.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
        AudioSource.PlayClipAtPoint(enemyFire, transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missle = collision.gameObject.GetComponent<Projectile>();
        if (missle)
        {
            health -= missle.GetDamage();
            missle.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                scoreKeeper.AddPoints(pointePerEnemy);
                AudioSource.PlayClipAtPoint(enemyDies, transform.position);
            }
        }
    }
}
