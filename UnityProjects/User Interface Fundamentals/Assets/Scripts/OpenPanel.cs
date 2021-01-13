using UnityEngine;
using UnityEngine.EventSystems;

public class OpenPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelOpenFocusElement;

    private GameObject _gameObject;

    public void OnPanelOpen()
    {
        panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(panelOpenFocusElement);
    }

    public void OnPanelClose()
    {
        panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_gameObject);
    }

    private void Start()
    {
        _gameObject = gameObject;
    }

    private void Update()
    {
        if (!panel.active) return;

        if (Input.GetKeyDown(KeyCode.Escape)) OnPanelClose();
    }
}
