using System.Threading.Tasks;
using UnityEngine;

public class EliteEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject _coin;

    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    public int _hp = 10;

    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async Task OnTriggerEnter2D(Collider2D other) {
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

    public void SetSpeed(float speed) {
        _speed = speed;
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }
}
