using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScene_HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WaveNumber_Text;
    [SerializeField] private Slider HPBar_Slider;
    [SerializeField] private GameObject UIPopup_GameOver;

    private HealthSystem playerHealthSystem;

    private void Awake()
    {
        UIPopup_GameOver.SetActive(false);
    }

    private void Start()
    {
        playerHealthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += Refresh;
        playerHealthSystem.OnHeal += Refresh;
        playerHealthSystem.OnDeath += GameOver;

        GameManager.Instance.OnWaveChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        HPBar_Slider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
        WaveNumber_Text.text = (GameManager.Instance.CurrentWaveIndex + 1).ToString();
    }

    private void GameOver()
    {
        UIPopup_GameOver.SetActive(true);
        StopAllCoroutines();
    }
}
