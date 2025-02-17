using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;  

public class Boss : MonoBehaviour
{
    [SerializeField]
    GameObject _coin;
    [SerializeField]
    GameObject _missile;

    [SerializeField]
    private Transform _shootPoint;
    private float _downPoint = 2.0f;

    [SerializeField]
    public int _hp = 2;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private int _rewardCount = 5;


    private float _currentPosition;
    private float _rightMax = 1.44f;
    private float _leftMax = -1.44f;

    [SerializeField]
    private float _shootInterval = 0.5f;
    private float _lastShootTime = 0.0f;

    private bool _shootState;
    private bool _inPositionState;
    private bool _destroyedState;
    private float _xPos, _yPos;

    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _shootState = false;
        _inPositionState = false;
        _destroyedState = false;
        _xPos = -1.44f;
        GameManager.instance.ShowBossHP(_hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(_inPositionState == false) { //not in start position
            GetComponent<BoxCollider2D>().enabled = false;
            transform.position += Vector3.down * _speed * Time.deltaTime;
            if(transform.position.y <= _downPoint) {
                GetComponent<BoxCollider2D>().enabled = true;
                _inPositionState = true;
                _shootState = true;
                _yPos = transform.position.y;
            }
        }
        else if(_destroyedState == true) {
            GetComponent<BoxCollider2D>().enabled = false;
            _shootState = false;
        }
        Shoot();
        StartPattern();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Missile") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            _hp -= weapon._damage;
            GameManager.instance.DecreaseBossHP(weapon._damage);
            if(_hp <= 0) {
                _destroyedState = true;
                for(int i = 0; i < _rewardCount; i++) {
                    Instantiate(_coin, transform.position, Quaternion.identity);
                }
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
        if(_inPositionState == false) {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_xPos, _yPos, transform.position.z), _speed * Time.deltaTime);
        if(transform.position.x >= _rightMax) {
            _xPos = _leftMax;
            _yPos = Random.Range(1f, 3.8f);
        }
        else if(transform.position.x <= _leftMax) {
            _xPos = _rightMax;
            _yPos = Random.Range(1f, 3.8f);
        }
    }

    private void Shoot() {
        if(_shootState == false) {
            return;
        }

        if(Time.time - _lastShootTime > _shootInterval) {
            float shootDir = 150f;
            for(int i = 0; i < 3; i++) {
                GameObject gameObject = Instantiate(_missile, _shootPoint.position, Quaternion.identity);
                BossWeapon bossWeapon = gameObject.GetComponent<BossWeapon>();
                bossWeapon.SetShootDir(shootDir);
                shootDir += 30;
            }
            _lastShootTime = Time.time;
        }
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
        GameManager.instance.HideBossHP();
    }
}
