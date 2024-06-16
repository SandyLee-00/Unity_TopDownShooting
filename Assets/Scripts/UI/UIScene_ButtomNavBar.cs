using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScene_ButtomNavBar : MonoBehaviour
{
    [SerializeField] private Button Growth_Button;
    [SerializeField] private Button Inventory_Button;
    [SerializeField] private Button Shop_Button;
    [SerializeField] private Button Summon_Button;
    [SerializeField] private Button Pet_Button;
    [SerializeField] private Button Relic_Button;

    [SerializeField] private GameObject openedTab;

    [SerializeField] private GameObject Growth_Tab;
    [SerializeField] private GameObject Inventory_Tab;
    [SerializeField] private GameObject Shop_Tab;
    [SerializeField] private GameObject Summon_Tab;
    [SerializeField] private GameObject Pet_Tab;
    [SerializeField] private GameObject Relic_Tab;

    private void Start()
    {
        Growth_Tab.SetActive(false);
        Growth_Button.onClick.AddListener(() => ShowTab(Growth_Tab));
    }

    private void ShowTab(GameObject tab)
    {
        if(tab == this.openedTab)
        {
            tab.SetActive(false);
            openedTab = null;
            return;
        }

        openedTab = tab;

        Growth_Tab.SetActive(false);
        /*Inventory_Tab.SetActive(false);
        Shop_Tab.SetActive(false);
        Summon_Tab.SetActive(false);
        Pet_Tab.SetActive(false);
        Relic_Tab.SetActive(false);*/

        tab.SetActive(true);
    }
}
