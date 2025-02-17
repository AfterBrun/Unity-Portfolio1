using UnityEngine;

public class BossHP : MonoBehaviour
{
    private RectTransform _rectTransform;
    private float _currentWidth;
    private float _widthOrigin, _heightOrigin;
    private float _decreaseUnit;
    void Awake()
    {
        _rectTransform = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        _widthOrigin = _rectTransform.sizeDelta.x;
        _heightOrigin = _rectTransform.sizeDelta.y;
        _currentWidth = _widthOrigin;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseBossHP(int damage) {
        _currentWidth -= damage * _decreaseUnit;
        _rectTransform.sizeDelta = new Vector2(_currentWidth, _heightOrigin);
    }

    public void BossHPStart(int bossHP)
    {
        _decreaseUnit = _widthOrigin / bossHP;
        _rectTransform.sizeDelta = new Vector2(_widthOrigin, _heightOrigin);
        _currentWidth = _widthOrigin;
    }
}
