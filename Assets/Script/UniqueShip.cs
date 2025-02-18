using System.Collections;
using UnityEngine;

public class UniqueShip : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject _upgradeItem;
    [SerializeField]
    GameObject _missile;

    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    public int _hp = 1;
    private float _downPoint = 0f;
    private float _rightMax = 2.28f;
    private float _leftMax = -2.28f;
    private float _xPos;
    private float _yPos;

    private bool _inPositionState = false;
    private bool _patternStarted = false;

    Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _xPos = _leftMax;
        _yPos = Random.Range(1f, 3.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if(_inPositionState == false) {
            transform.position += Vector3.down * _speed * Time.deltaTime;
            if(transform.position.y <= _downPoint) _inPositionState = true;
        }
        else if(transform.position.y < -7.0f) {
            Destroy(gameObject);
        }

        if(_inPositionState == true && _patternStarted == false) {
            StartCoroutine("StartPattern");
            _patternStarted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Missile") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            _hp -= weapon._damage;
            if(_hp <= 0) {
                GetComponent<BoxCollider2D>().enabled = false;
                Instantiate(_upgradeItem, transform.position, Quaternion.identity);
                _animator.SetTrigger("IsDie");
            }
            Destroy(other.gameObject);
        }
    }

    public void SetDownPoint(float downPoint) {
        _downPoint = downPoint;
    }

    IEnumerator StartPattern() {
        while(true) {
            yield return new WaitForSeconds(0.001f);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_xPos, _yPos, transform.position.z), _speed * Time.deltaTime);
            Debug.Log("move Towards");
            if(transform.position.x >= _rightMax) {
                _xPos = _leftMax;
                _yPos = Random.Range(1f, 3.8f);
                StartCoroutine("Shoot");
                yield return new WaitForSeconds(3.0f);
                StopCoroutine("Shoot");
            }
            else if(transform.position.x <= _leftMax) {
                _xPos = _rightMax;
                _yPos = Random.Range(1f, 3.8f);
                StartCoroutine("Shoot");
                yield return new WaitForSeconds(3.0f);
                StopCoroutine("Shoot");

            }
        }
    }

    IEnumerator Shoot() {
        while(true) {
            yield return new WaitForSeconds(0.5f);
            Instantiate(_missile, transform.position, Quaternion.identity);
        }
    }

    public void SetHp(int hp) {
        _hp = hp;
    }

    public void SetSpeed(float speed) {
        _speed = speed;
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }
}
