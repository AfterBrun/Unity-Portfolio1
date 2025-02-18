using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    [SerializeField]
    float _speed = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
    }
}
