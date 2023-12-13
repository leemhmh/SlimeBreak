using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]

    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]

    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.1f;

    private float lastShotTime = 0f;

    public SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private int PlayerCoin;
    // Update is called once per frame
    void Update()
    {    
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //x y값 알맞게 변환
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        float toY = Mathf.Clamp(mousePos.y, -4.55f, 4.55f);
        transform.position = new Vector3(toX, toY, transform.position.z);
        PlayerCoin = GameManager.instance.coin;

        if (GameManager.instance.isGameOver == false) {
            Shoot();
        }

        if (RealTime > 0) {
            RealTime -= Time.smoothDeltaTime;
        }
        else{
            if (itemTime == true) {
                shootInterval = 0.1f;
                itemTime = false;
            }
        }
        if (RealTime2 > 0) {
            RealTime2 -= Time.smoothDeltaTime;
        }
        else{
            if (itemTime2 == true) {
                if (PlayerCoin < 33) {
                    weaponIndex = 0;
                }
                else if (PlayerCoin < 66) {
                    weaponIndex = 1;
                }
                else if (PlayerCoin < 99) {
                    weaponIndex = 2;
                }
                else if (PlayerCoin >= 99) {
                    weaponIndex = 3;
                }
                itemTime2 = false;
            }
        }
        if (RealTime3 > 0) {
            RealTime3 -= Time.smoothDeltaTime;
        }
        else{
            if (itemTime3 == true) {
                isUnbeatTime = false;
                itemTime3 = false;
                render.color = new Color32(255,255,255,255);
            }
        }
    }
    void Shoot() 
    {
        if (Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    public float LimitTime = 5f;
    public float RealTime = 0f;
    public float RealTime2 = 0f;
    public float RealTime3 = 0f;
    public bool itemTime = false;
    public bool itemTime2 = false;
    public bool itemTime3 = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isUnbeatTime == false) {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
                GetComponent<Health>().PlayerHp -= 1;
                isUnbeatTime = true;
                StartCoroutine ("UnbeatTime");
                if (GetComponent<Health>().PlayerHp == 0) {
                    GameManager.instance.SetGameOver();
                    Destroy(gameObject);
                }
            }
        } 
        if (isUnbeatTime == true) {
            if (itemTime3 == true) {
                if (other.gameObject.tag == "Enemy") {
                    Destroy(other.gameObject);
                }
            }
        }
        if (other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin(1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "item") {
            shootInterval = 0.02f;
            RealTime = LimitTime;
            Destroy(other.gameObject);
            itemTime = true;
        }
        if (other.gameObject.tag == "item2") {
            RealTime2 = LimitTime;
            Upgrade();
            Destroy(other.gameObject);
            itemTime2 = true;
        }
        if (other.gameObject.tag == "item3") {
            RealTime3 = LimitTime + 2;
            isUnbeatTime = true;
            render.color = new Color32(255, 0, 0, 255);
            Destroy(other.gameObject);
            itemTime3 = true;
        }
    }

    public bool isUnbeatTime = false;

    IEnumerator UnbeatTime()
    {
        int countTime = 0;

        while (countTime < 10) {
            if (countTime%2 == 0)
                render.color = new Color32(255,255,255,90);
            else 
                render.color = new Color32(255,255,255,180);

            yield return new WaitForSeconds(0.2f);

            countTime ++;
        }

        render.color = new Color32(255,255,255,255);

        isUnbeatTime = false;

        yield return null;
    }


    public void Upgrade() {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
