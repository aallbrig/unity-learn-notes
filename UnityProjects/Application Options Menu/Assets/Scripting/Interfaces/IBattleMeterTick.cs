using UnityEngine;

public interface IBattleMeterTick
{
    void NotifyBattleMeterTick(GameObject battleChar, float tickValue);
}
