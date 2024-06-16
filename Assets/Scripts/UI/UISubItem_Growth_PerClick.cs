using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISubItem_Growth_PerClick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Level_Text;
    [SerializeField] private TextMeshProUGUI Value_Text;
    [SerializeField] private Button Cost_Button;
    [SerializeField] private TextMeshProUGUI Cost_Text;

    private void Awake()
    {
        Cost_Button.onClick.AddListener(OnClickCostButton);
    }

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        Level_Text.text = "Lv. " + GameManager.Instance.Coin.PerClickValue.ToString();
        Value_Text.text = "Value: " + GameManager.Instance.Coin.PerClickValue.ToString();
        Cost_Text.text = GameManager.Instance.Coin.PerClickCost.ToString();
    }

    private void OnClickCostButton()
    {
        GameManager.Instance.Coin.UpgradePerClick();
        Refresh();
    }
}
