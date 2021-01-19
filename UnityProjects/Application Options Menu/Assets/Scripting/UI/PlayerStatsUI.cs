using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hp;
    public Slider battleMeter;
    private BattleCharacterStats _battleChar;

    public void SetBattleCharacter(BattleCharacterStats battleChar)
    {
        _battleChar = battleChar;
        UpdateStatsUI();
    }

    public void UpdateBattleMeter(float meterValue)
    {
        battleMeter.value = meterValue;
    }

    private void UpdateStatsUI()
    {
        nameText.text = _battleChar.GetCharacterName();
        hp.text = _battleChar.GetHealth() + " / " + _battleChar.GetMaxHealth();
    }

    private IEnumerator KeepStatsUIUpdated()
    {
        while (true)
        {
            UpdateStatsUI();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void Start()
    {
        StartCoroutine(KeepStatsUIUpdated());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
