using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float _speed = 0.5f;
    [SerializeField]
    public int _damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float _dir = 0f;

    void Start()
    {
        transform.Rotate(new Vector3(0f, 0f, _dir));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up *_speed * Time.deltaTime;
        if(transform.position.y <= -6.0f) {
            Destroy(gameObject, 0.0f);
        }
    }

    public void SetShootDir(float dir) {
        _dir = dir;
    }
}
