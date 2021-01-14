using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    //[System.Serializable]
    //public class CharLevelUps
    //{
    //    public int maxHealth;
    //    public int maxMana;
    //    public int maxWealth;
    //    public int baseDamage;
    //    public float baseResistance;
    //    public float maxEncumbrance;
    //}

    public bool setManually = false;
    //public bool saveDataOnClose = false;

    public ItemPickUp Weapon { get; private set; }
    public ItemPickUp HeadArmor { get; private set; }
    public ItemPickUp ChestArmor { get; private set; }
    public ItemPickUp HandArmor { get; private set; }
    public ItemPickUp LegArmor { get; private set; }
    public ItemPickUp FootArmor { get; private set; }
    public ItemPickUp Misc1 { get; private set; }
    public ItemPickUp Misc2 { get; private set; }

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxWealth = 0;
    public int currentWealth = 0;

    public int maxMana = 0;
    public int currentMana = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public float baseResistance = 0;
    public float currentResistance = 0f;

    public float maxEncumbrance = 0f;
    public float currentEncumbrance = 0f;

    public int charExperience = 0;
    //public int charLevel = 0;

    public bool resetLevelOnDeath;

    public CharacterSaveData_SO characterData;

    public int MaxHealth
    {
        get
        {
            if (characterData != null) { return (int)(maxHealth * characterData.LevelMultiplier); }
            else { return maxHealth; }
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData != null) { return characterData.CurrentHealth; }
            else { return currentHealth; }
        }
        set
        {
            if (characterData != null) { characterData.CurrentHealth = value; }
            else { currentHealth = value; }
        }
    }

    public void Init()
    {
        if (CurrentHealth <= 0) { CurrentHealth = MaxHealth; }
    }

    public void AggregateAttackPoints(int points)
    {
        if (characterData != null) { characterData.AggregateAttackPoints(points); }
    }

    public float LevelMultiplier
    {
        get
        {
            if (characterData != null) { return characterData.LevelMultiplier; }
            else { return 1; }
        }
    }


    #region Stat Increasers
    public void ApplyHealth(int healthAmount)
    {
        if ((CurrentHealth + healthAmount) > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += healthAmount;
        }
    }

    public void ApplyMana(int manaAmount)
    {
        if ((currentMana + manaAmount) > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += manaAmount;
        }
    }

    public void GiveWealth(int wealthAmount)
    {
        if ((currentWealth + wealthAmount) > maxWealth)
        {
            currentWealth = maxWealth;
        }
        else
        {
            currentWealth += wealthAmount;
        }
    }

    public void EquipWeapon(ItemPickUp weaponPickUp, CharacterInventory charInventory, GameObject weaponSlot)
    {
        Rigidbody newWeapon;

        Weapon = weaponPickUp;
        charInventory.inventoryDisplaySlots[2].sprite = weaponPickUp.itemDefinition.itemIcon;
        newWeapon = Instantiate(weaponPickUp.itemDefinition.weaponSlotObject.weaponPreb, weaponSlot.transform);
        currentDamage = baseDamage + Weapon.itemDefinition.itemAmount;
    }

    public void EquipWeapon(ItemPickUp weaponPickUp, GameObject weaponSlot)
    {
        Rigidbody newWeapon;

        Weapon = weaponPickUp;
        newWeapon = Instantiate(weaponPickUp.itemDefinition.weaponSlotObject.weaponPreb, weaponSlot.transform);
        currentDamage = baseDamage + Weapon.itemDefinition.itemAmount;
    }

    public void EquipArmor(ItemPickUp armorPickUp, CharacterInventory charInventory)
    {
        switch (armorPickUp.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubType.Head:
                charInventory.inventoryDisplaySlots[3].sprite = armorPickUp.itemDefinition.itemIcon;
                HeadArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Chest:
                charInventory.inventoryDisplaySlots[4].sprite = armorPickUp.itemDefinition.itemIcon;
                ChestArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Hands:
                charInventory.inventoryDisplaySlots[5].sprite = armorPickUp.itemDefinition.itemIcon;
                HandArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Legs:
                charInventory.inventoryDisplaySlots[6].sprite = armorPickUp.itemDefinition.itemIcon;
                LegArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Boots:
                charInventory.inventoryDisplaySlots[7].sprite = armorPickUp.itemDefinition.itemIcon;
                FootArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
        }
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeMana(int amount)
    {
        currentMana -= amount;

        if (currentMana < 0)
        {
            currentMana = 0;
        }
    }

    public bool UnEquipWeapon(ItemPickUp weaponPickUp, CharacterInventory charInventory, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;

        if (Weapon != null)
        {
            if (Weapon == weaponPickUp)
            {
                previousWeaponSame = true;
            }
            charInventory.inventoryDisplaySlots[2].sprite = null;
            Destroy(weaponSlot.transform.GetChild(0).gameObject);
            Weapon = null;
            currentDamage = baseDamage;
        }

        return previousWeaponSame;
    }

    public bool UnEquipArmor(ItemPickUp armorPickUp, CharacterInventory charInventory)
    {
        bool previousArmorSame = false;

        switch (armorPickUp.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubType.Head:
                if (HeadArmor != null)
                {
                    if (HeadArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }
                    charInventory.inventoryDisplaySlots[3].sprite = null;
                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    HeadArmor = null;
                }
                break;
            case ItemArmorSubType.Chest:
                if (ChestArmor != null)
                {
                    if (ChestArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }
                    charInventory.inventoryDisplaySlots[4].sprite = null;
                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    ChestArmor = null;
                }
                break;
            case ItemArmorSubType.Hands:
                if (HandArmor != null)
                {
                    if (HandArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }
                    charInventory.inventoryDisplaySlots[5].sprite = null;
                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    HandArmor = null;
                }
                break;
            case ItemArmorSubType.Legs:
                if (LegArmor != null)
                {
                    if (LegArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }
                    charInventory.inventoryDisplaySlots[6].sprite = null;
                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    LegArmor = null;
                }
                break;
            case ItemArmorSubType.Boots:
                if (FootArmor != null)
                {
                    if (FootArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }
                    charInventory.inventoryDisplaySlots[7].sprite = null;
                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    FootArmor = null;
                }
                break;
        }

        return previousArmorSame;
    }
    #endregion

    #region Character Death and Reset
    private void Death()
    {
        //if (characterData != null) { characterData.Save(); }

        if (resetLevelOnDeath)
        {
            GameManager gm = GameManager.Instance;
            if (gm != null) { gm.StartCoroutine(Reset(gm)); }
        }
    }

    IEnumerator Reset(GameManager gm)
    {
        yield return new WaitForSeconds(3);
        gm.RestartGame();
    }

    //private void LevelUp()
    //{
    //    charLevel += 1;
    //    //Display Level Up Visualization

    //    maxHealth = charLevelUps[charLevel - 1].maxHealth;
    //    maxMana = charLevelUps[charLevel - 1].maxMana;
    //    maxWealth = charLevelUps[charLevel - 1].maxWealth;
    //    baseDamage = charLevelUps[charLevel - 1].baseDamage;
    //    baseResistance = charLevelUps[charLevel - 1].baseResistance;
    //    maxEncumbrance = charLevelUps[charLevel - 1].maxEncumbrance;
    //}
    #endregion

}
