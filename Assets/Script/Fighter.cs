using System.Threading.Tasks;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject _coin;
    [SerializeField]
    GameObject _missile;

    [SerializeField]
    private Transform _shootPoint;
    private float _downPoint;
    [SerializeField]
    public int _hp = 2;
    [SerializeField]
    private float _speed = 1f;
    private float _currentPosition;
    private float _rightMax = 2.3f;
    private float _leftMax = -2.3f;
    private int _direction = 1;

    [SerializeField]
    private float _shootInterval = 0.5f;
    private float _lastShootTime = 0.0f;


    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _currentPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        StartPattern();
        transform.position = new Vector3(_currentPosition, transform.position.y, transform.position.z);
        Shoot();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Missile") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            _hp -= weapon._damage;
            if(_hp <= 0) {
                Instantiate(_coin, transform.position, Quaternion.identity);
                _animator.SetTrigger("IsDie");
            }
            Destroy(other.gameObject);
        }
    }

    public void SetHp(int hp) {
        _hp = hp;
    }

    public void SetDownPoint(float downPoint) {
        _downPoint = downPoint;
    }

    private void StartPattern() {
        if(transform.position.y != _downPoint) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _downPoint, transform.position.z), _speed * Time.deltaTime);
        }
        else {
            _currentPosition += Time.deltaTime * _direction * _speed;
            if(_currentPosition >= _rightMax) {
                _direction *= -1;
                _currentPosition = _rightMax;
            }
            else if(_currentPosition <= _leftMax) {
                _direction *= -1;
                _currentPosition = _leftMax;
            }
        }
    }

    private void Shoot() {
        if(transform.position.y != _downPoint) {
            return;
        }

        if(Time.time - _lastShootTime > _shootInterval) {
            GameObject gameObject = Instantiate(_missile, _shootPoint.position, Quaternion.identity);
            _lastShootTime = Time.time;
        }
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }
}
