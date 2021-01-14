using TMPro;
using UnityEngine;

public class DisplayCharacterInfo : MonoBehaviour
{
    public CharacterSaveData_SO characterStats;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    private void Update()
    {
        healthText.text = "Health: " + characterStats.CurrentHealth;
        levelText.text = "Level: " + characterStats.currentLevel;
        xpText.text = "XP Remaining: " + characterStats.pointsTillNextLevel;
    }
}
