using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.Upgrading
{
    public class Upgrader : MonoBehaviour
    {
        [SerializeField] private Data data;

        [Header("UI Stuff")]
        [SerializeField] private List<TextMeshProUGUI> names;
        [SerializeField] private List<TextMeshProUGUI> descriptions;
        [SerializeField] private List<TextMeshProUGUI> costs;
        [SerializeField] private List<Image> images;
        [SerializeField] private TextMeshProUGUI currentMoney;

        [Header("Garbage Collector Optimization")]
        [SerializeField] private Upgrade currentUpgrade;

        private void Start()
        {
            Initialize();
        }

        public void BuyLevel(int index)
        {
            if (data.CheckLevelPurchasable(index))
            {
                data.BuyLevel(index);
                Initialize();
            }
        }

        private void Initialize()
        {
            for (int i = 0; i < names.Count; i++)
            {
                currentUpgrade = data.GetLevel(i);

                names[i].text = currentUpgrade.name;
                descriptions[i].text = currentUpgrade.description;
                costs[i].text = currentUpgrade.cost[currentUpgrade.Level].ToString();
                images[i].sprite = currentUpgrade.icon;
            }

            currentMoney.text = data.CoinAmount.ToString();
        }
    }
}