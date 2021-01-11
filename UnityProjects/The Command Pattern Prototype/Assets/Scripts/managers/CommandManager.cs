using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public delegate void Playing();
    public static event Playing OnPlaying;
    public delegate void PlayingComplete();
    public static event PlayingComplete OnPlayingComplete;
    public delegate void DoneBehavior();
    public static event DoneBehavior OnDoneBehavior;

    private static CommandManager _instance;

    private static CommandManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Command Manager is null");

            return _instance;
        }
    }
    private static readonly List<ICommand> CommandBuffer = new List<ICommand>();
    private static bool _playing = false;

    public static void Add(ICommand cmd)
    {
        CommandBuffer.Add(cmd);
    }

    public void Play(float secondsDelay)
    {
        // play back all commands
        if (_playing) return;
        OnPlaying?.Invoke();
        // HACK: reset colors by invoking done
        Done();
        StartCoroutine(PlayRoutine(secondsDelay));
    }

    public void Rewind(float secondsDelay)
    {
        // play back in reverse
        if (_playing) return;
        OnPlaying?.Invoke();
        StartCoroutine(RewindRoutine(secondsDelay));
    }

    public void Done()
    {
        // finish changing colors. Reset all cubes
        OnDoneBehavior?.Invoke();
    }

    public void Reset()
    {
        // Clear the command buffer
        CommandBuffer.Clear();
        // HACK: reset colors by invoking done
        Done();
    }

    private IEnumerator PlayRoutine(float delay)
    {
        foreach (var cmd in CommandBuffer)
        {
            cmd.Execute();
            yield return new WaitForSeconds(delay);
        }
        OnPlayingComplete?.Invoke();
    }

    private IEnumerator RewindRoutine(float delay)
    {
        for (var i = CommandBuffer.Count - 1; i >= 0; i--)
        {
            var cmd = CommandBuffer[i];
            cmd.Undo();
            yield return new WaitForSeconds(delay);
        }
        OnPlayingComplete?.Invoke();
    }

    private void Awake()
    {
        _instance = this;

        OnPlaying += () => _playing = true;
        OnPlayingComplete += () => _playing = false;
    }

    private void OnDestroy() => StopAllCoroutines();
}
