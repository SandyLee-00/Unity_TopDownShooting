using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScene_ClickHandler : MonoBehaviour
{
    [SerializeField] private Button Click_Button;

    public event Action OnClickerClickEvent;

    private void Awake()
    {
        Click_Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        OnClickerClickEvent?.Invoke();
    }
}
