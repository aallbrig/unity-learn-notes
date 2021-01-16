using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IGameStateChange
{
    public delegate void MainMenuFadeComplete(bool isFadeIn);
    public static event MainMenuFadeComplete OnMainMenuFadeStart;
    public static event MainMenuFadeComplete OnMainMenuFadeComplete;

    [SerializeField] private Animation animator;
    [SerializeField] private AnimationClip fadeInAnimation;
    [SerializeField] private AnimationClip fadeOutAnimation;
    
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    [SerializeField] private Button options;
    [SerializeField] private Button credits;

    public void OnFadeInStart()
    {
        OnMainMenuFadeStart?.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete?.Invoke(true);
    }

    public void OnFadeOutStart()
    {
        OnMainMenuFadeStart?.Invoke(false);
    }

    public void OnFadeOutComplete()
    {
        OnMainMenuFadeComplete?.Invoke(false);
    }

    private void PlayClip(AnimationClip clip)
    {
        animator.Stop();
        clip.legacy = true;
        animator.clip = clip;
        animator.Play();
    }

    #region Monobehaviour Functions

    private void Start()
    {
        newGame.onClick.AddListener(GameManager.Instance.StartGame);
        loadGame.onClick.AddListener(GameManager.Instance.StartGame);
        options.onClick.AddListener(GameManager.Instance.StartGame);
        credits.onClick.AddListener(GameManager.Instance.StartGame);
        
        EventsBroker.Instance.SubscribeToGameStateChange(this);
    }

    #endregion

    #region Subscriptions

    public void Notify(GameManager.GameState prevState, GameManager.GameState currentState)
    {
        if (prevState == GameManager.GameState.Pregame && currentState == GameManager.GameState.Running)
        {
            PlayClip(fadeOutAnimation);
        }
        else if (prevState != GameManager.GameState.Pregame && currentState == GameManager.GameState.Pregame)
        {
            PlayClip(fadeInAnimation);
        }
    }

    #endregion
}
