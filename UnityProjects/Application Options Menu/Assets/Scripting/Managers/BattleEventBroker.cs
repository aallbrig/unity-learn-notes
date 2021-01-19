using System.Collections.Generic;
using UnityEngine;

public class BattleEventBroker : Singleton<BattleEventBroker>
{

    #region BattleMeterTick
    private readonly List<IBattleMeterTick> _battleMeterTickSubscribers = new List<IBattleMeterTick>();
    public void SubscribeToBattleMeterTick(IBattleMeterTick subscriber) =>
        _battleMeterTickSubscribers.Add(subscriber);
    public void UnsubscribeToBattleMeterTick(IBattleMeterTick subscriber) =>
        _battleMeterTickSubscribers.Remove(subscriber);
    private void OnBattleMeterTick(GameObject battleChar, float battleMeter) =>
        _battleMeterTickSubscribers.ForEach((sub) => sub.NotifyBattleMeterTick(battleChar, battleMeter));
    #endregion


    #region BattleCommandStart
    private readonly List<IBattleCommandStart> _battleCommandStartSubscribers = new List<IBattleCommandStart>();
    public void SubscribeToBattleCommandStart(IBattleCommandStart subscriber) =>
        _battleCommandStartSubscribers.Add(subscriber);
    public void UnsubscribeToBattleCommandStart(IBattleCommandStart subscriber) =>
        _battleCommandStartSubscribers.Remove(subscriber);
    private void OnBattleCommandStart() =>
        _battleCommandStartSubscribers.ForEach((sub) => sub.NotifyBattleCommandStart());
    #endregion


    #region BattleCommandComplete
    private readonly List<IBattleCommandComplete> _battleCommandCompleteSubscribers = new List<IBattleCommandComplete>();
    public void SubscribeToBattleCommandComplete(IBattleCommandComplete subscriber) =>
        _battleCommandCompleteSubscribers.Add(subscriber);
    public void UnsubscribeToBattleCommandComplete(IBattleCommandComplete subscriber) =>
        _battleCommandCompleteSubscribers.Remove(subscriber);
    private void OnBattleCommandComplete() =>
        _battleCommandCompleteSubscribers.ForEach((sub) => sub.NotifyBattleCommandComplete());
    #endregion


    #region BattleCharacterReadyToAct
    private readonly List<IBattleCharReadyToAct> _battleCharReadyToActSubscribers = new List<IBattleCharReadyToAct>();
    public void SubscribeToBattleCharReadyToAct(IBattleCharReadyToAct subscriber) =>
        _battleCharReadyToActSubscribers.Add(subscriber);
    public void UnsubscribeToBattleCharReadyToAct(IBattleCharReadyToAct subscriber) =>
        _battleCharReadyToActSubscribers.Remove(subscriber);
    private void OnBattleCharReadyToAct(GameObject battleChar) =>
        _battleCharReadyToActSubscribers.ForEach(sub => sub.NotifyBattleCharReadyToAct(battleChar));
    #endregion


    #region BattleCharacterHasActed
    private readonly List<IBattleCharacterHasActed> _battleCharacterHasActedSubscribers = new List<IBattleCharacterHasActed>();
    public void SubscribeToBattleCharacterHasActed(IBattleCharacterHasActed subscriber) =>
        _battleCharacterHasActedSubscribers.Add(subscriber);
    public void UnsubscribeToBattleCharacterHasActed(IBattleCharacterHasActed subscriber) =>
        _battleCharacterHasActedSubscribers.Remove(subscriber);
    private void OnBattleCharacterHasActed(GameObject battleChar) =>
        _battleCharacterHasActedSubscribers.ForEach(sub => sub.NotifyBattleCharacterHasActed(battleChar));
    #endregion


    #region BattleCharacterDeath
    private readonly List<IBattleCharacterDeath> _battleCharacterDeathSubscribers = new List<IBattleCharacterDeath>();
    public void SubscribeToBattleCharacterDeath(IBattleCharacterDeath subscriber) =>
        _battleCharacterDeathSubscribers.Add(subscriber);
    public void UnsubscribeToBattleCharacterDeath(IBattleCharacterDeath subscriber) =>
        _battleCharacterDeathSubscribers.Remove(subscriber);
    private void OnBattleCharacterDeath(GameObject battleChar) =>
        _battleCharacterDeathSubscribers.ForEach(sub => sub.NotifyBattleCharacterDeath(battleChar));
    #endregion


    #region BattleLost
    private readonly List<IBattleLost> _battleLostSubscribers = new List<IBattleLost>();
    public void SubscribeToBattleLost(IBattleLost subscriber) => _battleLostSubscribers.Add(subscriber);
    public void UnsubscribeToBattleLost(IBattleLost subscriber) => _battleLostSubscribers.Remove(subscriber);
    private void OnBattleLost() => _battleLostSubscribers.ForEach(sub => sub.NotifyBattleLost());
    #endregion


    #region BattleVictory
    private readonly List<IBattleVictory> _battleVictorySubscribers = new List<IBattleVictory>();
    public void SubscribeToBattleVictory(IBattleVictory subscriber) => _battleVictorySubscribers.Add(subscriber);
    public void UnsubscribeToBattleVictory(IBattleVictory subscriber) => _battleVictorySubscribers.Remove(subscriber);
    private void OnBattleVictory() => _battleVictorySubscribers.ForEach(sub => sub.NotifyBattleVictory());
    #endregion


    private void Start()
    {
        BattleManager.Instance.OnBattleMeterTickEvent += OnBattleMeterTick;
        BattleManager.Instance.OnBattleCharacterReadyToActEvent += OnBattleCharReadyToAct;
        BattleManager.Instance.OnBattleCharacterHasActedEvent += OnBattleCharacterHasActed;
        BattleManager.OnBattleLostEvent += OnBattleLost;
        BattleManager.OnBattleVictoryEvent += OnBattleVictory;
        BattleCommandManager.OnBattleCommandStart += OnBattleCommandStart;
        BattleCommandManager.OnBattleCommandComplete += OnBattleCommandComplete;
        BattleCharacterStats.OnBattleCharacterDeath += OnBattleCharacterDeath;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        BattleManager.Instance.OnBattleMeterTickEvent -= OnBattleMeterTick;
        BattleManager.Instance.OnBattleCharacterReadyToActEvent -= OnBattleCharReadyToAct;
        BattleManager.Instance.OnBattleCharacterHasActedEvent -= OnBattleCharacterHasActed;
        BattleManager.OnBattleLostEvent -= OnBattleLost;
        BattleManager.OnBattleVictoryEvent -= OnBattleVictory;
        BattleCommandManager.OnBattleCommandStart -= OnBattleCommandStart;
        BattleCommandManager.OnBattleCommandComplete -= OnBattleCommandComplete;
        BattleCharacterStats.OnBattleCharacterDeath -= OnBattleCharacterDeath;
    }
}
