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
    private TextMeshProUGUI _gameoverText;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private RectTransform _hp;

    [SerializeField]
    private GameObject _bossHP;
    private float _hpUIWidth;

    private int _coin = 0;
    private void Awake() {
        if(instance == null) {
            instance = this;
            _hpUIWidth = _hp.sizeDelta.x;
        }
    }

    private void Update() {
        if(Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
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

    public void SetYouWin() {
        Invoke("ShowYouWin", 1f);
    }

    void ShowGameOverScreen() {
        _gameOverPanel.SetActive(true);
    }
    
    void ShowYouWin() {
        _gameOverPanel.SetActive(true);
        _gameoverText.text = "You Win!";
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowBossHP(int hp) {
        _bossHP.SetActive(true);
        _bossHP.GetComponent<BossHP>().BossHPStart(hp);
        
    }

    public void DecreaseBossHP(int damage) {
        _bossHP.GetComponent<BossHP>().DecreaseBossHP(damage);
    }

    public void HideBossHP() {
        _bossHP.SetActive(false);
    }
}
