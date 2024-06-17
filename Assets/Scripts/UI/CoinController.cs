using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;



/// <summary>
/// Ŭ�� �̺�Ʈ�� �޾� ������ ������Ű�� ��Ʈ�ѷ�
/// Auto Clicker�� Clicker�� �����Ͽ� ������ ������Ų��.
/// </summary>
public class CoinController : MonoBehaviour
{
    private BigInteger value = 0;
    private BigInteger perClickValue = 1;
    private BigInteger perClickCost = 1;
    private BigInteger perAutoClickValue = 1;
    private BigInteger perAutoClickCost = 1;

    [SerializeField] private UIScene_ClickHandler clickHandler;

    public BigInteger Value => value;
    public BigInteger PerClickValue => perClickValue;
    public BigInteger PerClickCost => perClickCost;
    public BigInteger PerAutoClickValue => perAutoClickValue;
    public BigInteger PerAutoClickCost => perAutoClickCost;

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

    public void UpgradePerAutoClick()
    {
        if (value >= perAutoClickCost)
        {
            value -= perAutoClickCost;
            perAutoClickValue++;
            perAutoClickCost *= 2;
        }
    }

}
