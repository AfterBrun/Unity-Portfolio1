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
        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Clamp(mousePosition.x, -2.5f, 2.5f);

        transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        */
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        transform.position += moveTo * _speed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            _animator.SetBool("IsLeft", true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow)) {
            _animator.SetBool("IsLeft", false);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            _animator.SetBool("IsRight", true);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow)) {
            _animator.SetBool("IsRight", false);
        }

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
