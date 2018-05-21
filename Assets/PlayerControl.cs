using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject projectile;
    public float speed = 15.0f;
    public float padding = 1f;
    public float projectileSpeed = 20f;
    public float fireRate = 0.2f;
    public float health = 250;
    public AudioClip playerFire;
    float xmin;
    float xmax;

    private void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;

    }
    
    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1, 0);
        GameObject laser = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(playerFire, transform.position);
    }
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) InvokeRepeating("Fire", 0.000001f, fireRate);
        else if (Input.GetKeyUp(KeyCode.Space))  CancelInvoke("Fire");

        if (Input.GetKey(KeyCode.LeftArrow)) transform.position += Vector3.left * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.RightArrow)) transform.position += Vector3.right * speed * Time.deltaTime;

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
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
                LevelManager man = GameObject.FindGameObjectWithTag("LevelMenager").GetComponent<LevelManager>();
                man.LoadLevel("Win Screen");
                Destroy(gameObject);
            }
        }
    }
}
