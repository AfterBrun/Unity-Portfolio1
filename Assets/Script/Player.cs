using System.Collections;
using Unity.VisualScripting;
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
    private float _speed = 0.5f;

    [SerializeField]
    public int _hp = 3;

    Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        KeyEvents();
        shoot();
    }

    void KeyEvents() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);

        if(transform.position.x > 2.5f) {
            transform.position = new Vector3(2.5f, transform.position.y, 0f);
        }
        else if(transform.position.x < -2.5f) {
            transform.position = new Vector3(-2.5f, transform.position.y, 0f);
        }
        else {
            transform.position += moveTo * _speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            _animator.SetBool("IsLeft", true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            _animator.SetBool("IsLeft", false);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            _animator.SetBool("IsRight", true);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            _animator.SetBool("IsRight", false);
        }
    }

    void shoot()
    {
        if(Time.time - _lastShootTime > _shootInterval) {
            GameObject gameObject = Instantiate(_missile, _shootPoint.position, Quaternion.identity);
            Weapon weapon = gameObject.GetComponent<Weapon>();
            weapon.SetShootDir(0f);
            _lastShootTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy") {
            _hp--;
            GameManager.instance.DecreaseHpUI();
            if(_hp <= 0) {
                GameManager.instance.SetGameOver();
                Destroy(gameObject);
            }
            StartCoroutine("UnBeatTime");
        }

        if(other.gameObject.tag == "EnemyMissile") {
            _hp--;
            GameManager.instance.DecreaseHpUI();
            if(_hp <= 0) {
                GameManager.instance.SetGameOver();
                GameManager.instance.HideBossHP();
                Destroy(gameObject);
            }
            StartCoroutine("UnBeatTime");
        }


        if(other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Upgrade") {
            _shootInterval /= 2;
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
