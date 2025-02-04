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
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Coin") {
            Debug.Log("coin +1");
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

}
