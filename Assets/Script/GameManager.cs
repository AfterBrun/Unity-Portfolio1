using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI _score;

    private int _coin = 0;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void IncreaseCoin() {
        _coin++;
        _score.SetText(_coin.ToString());
    }
}
