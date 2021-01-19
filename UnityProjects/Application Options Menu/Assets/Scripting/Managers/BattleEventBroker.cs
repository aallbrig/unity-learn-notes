using System.Collections.Generic;
using UnityEngine;

public class BattleEventBroker : Singleton<BattleEventBroker>
{

    #region BattleMeterTick
    private List<IBattleMeterTick> _battleMeterTickSubscribers = new List<IBattleMeterTick>();
    public void SubscribeToBattleMeterTick(IBattleMeterTick subscriber) =>
        _battleMeterTickSubscribers.Add(subscriber);
    public void UnsubscribeToBattleMeterTick(IBattleMeterTick subscriber) =>
        _battleMeterTickSubscribers.Remove(subscriber);
    private void OnBattleMeterTick(GameObject battleChar, float battleMeter) =>
        _battleMeterTickSubscribers.ForEach((sub) => sub.NotifyBattleMeterTick(battleChar, battleMeter));
    #endregion
    
    #region BattleCharacterReadyToAct
    private List<IBattleCharReadyToAct> _battleCharReadyToActSubscribers = new List<IBattleCharReadyToAct>();
    public void SubscribeToBattleCharReadyToAct(IBattleCharReadyToAct subscriber) =>
        _battleCharReadyToActSubscribers.Add(subscriber);
    public void UnsubscribeToBattleCharReadyToAct(IBattleCharReadyToAct subscriber) =>
        _battleCharReadyToActSubscribers.Remove(subscriber);
    private void OnBattleCharReadyToAct(GameObject battleChar) =>
        _battleCharReadyToActSubscribers.ForEach(sub => sub.NotifyBattleCharReadyToAct(battleChar));
    #endregion

    #region BattleCharacterReadyToAct
    
    #endregion
    
    private void Start()
    {
        BattleManager.Instance.OnBattleMeterTickEvent += OnBattleMeterTick;
        BattleManager.Instance.OnBattleCharacterReadyToActEvent += OnBattleCharReadyToAct;
    }
}
