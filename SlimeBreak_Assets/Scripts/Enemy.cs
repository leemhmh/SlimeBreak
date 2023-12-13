using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject coin;

    [SerializeField]
    public GameObject[] items;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;
    public void SetMoveSpeed (float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }
    // Update is called once per frame
    private bool moveRight = true;
    private bool moveLeft = true;
    void Update()
    {
        if (gameObject.tag == "Boss") {
            transform.position += Vector3.down * moveSpeed * Time.smoothDeltaTime;
            if (moveLeft == true) {
                transform.position += Vector3.left * 2 * (moveSpeed + 1.2f) * Time.smoothDeltaTime;   
            }
            if (moveRight == true) {
                transform.position += Vector3.right * (moveSpeed + 1.2f) * Time.smoothDeltaTime; 
            }
            if (transform.position.x < -1.6) {
                moveLeft = false;
                //moveRight = true;
            }
            if (transform.position.x > 1.6) {
                moveLeft = true;
                //moveRight = false;
            }
            if (transform.position.y < minY) {
                Destroy(gameObject);
            }
        }
        else {
            transform.position += Vector3.down * moveSpeed * Time.smoothDeltaTime;
            if (transform.position.y < minY) {
                Destroy(gameObject);
            }
        }
    }
    private int percentage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0) {
                if (gameObject.tag == "Boss") {
                    GameManager.instance.IncreaseBossDeath();
                    GameManager.instance.SetGameOver();
                }
                Destroy(gameObject);
                percentage = Random.Range(0, 75);
                if (percentage > 3) {
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
                else if (percentage > 2 && percentage <= 3) {
                    Instantiate(items[0], transform.position, Quaternion.identity);
                }
                else if (percentage > 1 && percentage <= 2) {
                    Instantiate(items[1], transform.position, Quaternion.identity);
                }
                else if (percentage > 0 && percentage <= 1) {
                    Instantiate(items[2], transform.position, Quaternion.identity);
                }
            } 
            Destroy(other.gameObject);
        }
    }
}
