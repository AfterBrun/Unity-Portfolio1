using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private GameObject _missile;
    [SerializeField]
    private float _shootInterval = 0.5f;
    private float _lastShootTime = 0.0f;

    [SerializeField]
    public int _hp = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Clamp(mousePosition.x, -2.5f, 2.5f);

        transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        shoot();
    }

    void shoot()
    {
        if(Time.time - _lastShootTime > _shootInterval) {
            Instantiate(_missile, _shootPoint.position, Quaternion.identity);
            _lastShootTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy") {
            _hp--;
            GameManager.instance.DecreaseHpUI();
            if(_hp <= 0) {
                Destroy(gameObject);
            }
            StartCoroutine("UnBeatTime");
        }
        if(other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    IEnumerator UnBeatTime() {
        GetComponent<CircleCollider2D>().enabled = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int count = 0;
        while(count < 10)
        {
            if(count % 2 == 0) {
                spriteRenderer.color = new Color32(255, 255, 255, 90); 
            }
            else {
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.2f);
            count += 1;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        GetComponent<CircleCollider2D>().enabled = true;
        yield return null;
    }

}
