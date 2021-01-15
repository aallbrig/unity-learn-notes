using System;
using UnityEngine.Events;

public class Events
{
    [Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> {}
    [Serializable] public class EventFadeComplete: UnityEvent<bool> { }
}
