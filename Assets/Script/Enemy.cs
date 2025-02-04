using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject _coin;

    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    public int _hp = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        if(transform.position.y < -7.0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Missile") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            _hp -= weapon._damage;
            if(_hp <= 0) {
                Instantiate(_coin, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }

    public void SetHp(int hp) {
        _hp = hp;
    }

    public void SetSpeed(float speed) {
        _speed = speed;
    }
}
