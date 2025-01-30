using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.5f;
    [SerializeField]
    public int _damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up *_speed * Time.deltaTime;
        if(transform.position.y >= 6.0f) {
            Destroy(gameObject, 0.0f);
        }
    }
}
