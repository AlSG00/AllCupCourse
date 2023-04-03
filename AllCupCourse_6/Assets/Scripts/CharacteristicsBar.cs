using UnityEngine;
using TMPro;

public class CharacteristicsBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthCountText;
    [SerializeField] private TextMeshProUGUI manaCountText;
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private TextMeshProUGUI chestCountText;
    [SerializeField] private Chest chest;

    private int chestOpenCount = 0;

    private void incrementChestCounter() {
        chestOpenCount++;
        chestCountText.text = chestOpenCount.ToString();
    }

    private void OnEnable() {
        chest.onOpen.AddListener(incrementChestCounter);
    }

    private void OnDisable() {
        chest.onOpen.RemoveListener(incrementChestCounter);
    }

    public void UpdateView(Characteristics characteristics) {
        healthCountText.text = characteristics.Health.ToString();
        manaCountText.text = characteristics.Mana.ToString();
        coinCountText.text = characteristics.Coin.ToString();
    }
}
