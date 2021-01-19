using System.Collections.Generic;
using UnityEngine;

public class BattleEventBroker : Singleton<BattleEventBroker>
{
    private List<IBattleMeterTick> _battleMeterTickSubscribers = new List<IBattleMeterTick>();

    #region BattleMeterTick
    public void SubscribeToBattleMeterTick(IBattleMeterTick subscriber) =>
        _battleMeterTickSubscribers.Add(subscriber);
    public void UnsubscribeToBattleMeterTick(IBattleMeterTick subscriber) =>
        _battleMeterTickSubscribers.Remove(subscriber);
    private void OnBattleMeterTick(GameObject battleChar, float battleMeter) =>
        _battleMeterTickSubscribers.ForEach((sub) => sub.NotifyBattleMeterTick(battleChar, battleMeter));
    #endregion
    
    private void Start()
    {
        BattleManager.Instance.OnBattleMeterTickEvent += OnBattleMeterTick;
    }
}
