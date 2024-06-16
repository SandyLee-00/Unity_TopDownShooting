using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinData
{
    public int Value;
    public int PerClickValue;
    public int PerAutoClickValue;
}

/// <summary>
/// Ŭ�� �̺�Ʈ�� �޾� ������ ������Ű�� ��Ʈ�ѷ�
/// Auto Clicker�� Clicker�� �����Ͽ� ������ ������Ų��.
/// </summary>
public class CoinController : MonoBehaviour
{
    private int value = 0;
    private int perClickValue = 1;
    private int perClickCost = 1;
    private int perAutoClickValue = 1;

    [SerializeField] private UIScene_ClickHandler clickHandler;

    public int Value => value;
    public int PerClickValue => perClickValue;
    public int PerClickCost => perClickCost;
    public int PerAutoClickValue => perAutoClickValue;

    public event Action OnCoinChanged;

    private void Start()
    {
        clickHandler = FindObjectOfType<UIScene_ClickHandler>();

        clickHandler.OnClickerClickEvent -= OnClickerClickEvent;
        clickHandler.OnClickerClickEvent += OnClickerClickEvent;

        StartCoroutine(AutoClickCoroutine());
    }

    private void OnClickerClickEvent()
    {
        value += perClickValue;
        OnCoinChanged?.Invoke();
    }

    private IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            value += perAutoClickValue;
            OnCoinChanged?.Invoke();
        }
    }

    public void UpgradePerClick()
    {
        if (value >= perClickCost)
        {
            value -= perClickCost;
            perClickValue++;
            perClickCost *= 2;
        }
    }

}
