using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI _score;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private RectTransform _hp;
    private float _hpUIWidth;

    private int _coin = 0;
    private void Awake() {
        if(instance == null) {
            instance = this;
            _hpUIWidth = _hp.sizeDelta.x;
        }
    }

    public void IncreaseCoin() {
        _coin++;
        _score.SetText(_coin.ToString());
    }

    public void DecreaseHpUI() {
        if(_hpUIWidth <= 0) {
            return;
        }
        _hpUIWidth -= 80f;
        _hp.sizeDelta = new Vector2(_hpUIWidth, _hp.sizeDelta.y);
    }

    public void SetGameOver() {
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        if(enemySpawner != null) {
            enemySpawner.StopSpawn();
        }

        Invoke("ShowGameOverScreen", 1f);
    }

    void ShowGameOverScreen() {
        _gameOverPanel.SetActive(true);
    }
    
    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
