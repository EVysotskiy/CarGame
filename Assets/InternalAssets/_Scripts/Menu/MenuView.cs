using UnityEngine;
using UnityEngine.UI;

public delegate void OnStartPlayHandler();
public class MenuView : MonoBehaviour
{
    public event OnStartPlayHandler OnStartPlay;
    [SerializeField] private Text _startPlayText;
    [SerializeField] private Canvas _canvasMenuView;

    public void StartPlay()
    {
        OnStartPlay?.Invoke();
    }

    public void SetStateCanvasMenu(bool enable)
    {
        _canvasMenuView.enabled = enable;
    }
}