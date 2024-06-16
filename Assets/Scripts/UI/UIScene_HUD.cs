using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 체력바, 웨이브 표시, 코인 표시를 담당하는 UI
/// </summary>
public class UIScene_HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WaveNumber_Text;
    [SerializeField] private Slider HPBar_Slider;
    [SerializeField] private TextMeshProUGUI CoinNumber_Text;

    private HealthSystem playerHealthSystem;

    private void Start()
    {
        playerHealthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += Refresh;
        playerHealthSystem.OnHeal += Refresh;

        GameManager.Instance.OnWaveChanged += Refresh;
        GameManager.Instance.Coin.OnCoinChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        HPBar_Slider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
        WaveNumber_Text.text = (GameManager.Instance.CurrentWaveIndex + 1).ToString();
        CoinNumber_Text.text = GameManager.Instance.Coin.Value.ToString();
    }
}
