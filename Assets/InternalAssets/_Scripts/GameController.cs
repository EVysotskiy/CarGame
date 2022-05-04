using System;
using System.Collections;
using System.Collections.Generic;
using InternalAssets._Scripts.Audio;
using InternalAssets._Scripts.Car;
using InternalAssets._Scripts.Monetization.Ads;
using InternalAssets._Scripts.UI.Settings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public delegate void SoundHandler();
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _recordText;
    [SerializeField] private CarPlayerController _carPlayerController;
    [HideInInspector] private int _record = 0;
    public int record { get {return _record; } }
    public Image _imageDirectTurn;
    public TrafficAIController trafficAIController;
    private TypeAds _typeAds;
    public  event SoundHandler OnClickBottonUI;
    public  event SoundHandler OnPlayerTurn;
    
    public TypeAds TypeAds {get => _typeAds;}
    [SerializeField] private Sprite[] _imageDirectionalTurn = new Sprite[2];
    public delegate void GameHandler();
    public event GameHandler eventStart;
    public event GameHandler eventLose;
    public event GameHandler eventContinue;
    public event GameHandler eventFinish;
    internal DirectionTurn directionTurn;
    private AdsController _adsController;
    private SettingsController _settingsController;
    private Coroutine _coroutineOpenPanelLoseGame;
    private AudioController _audioController;
    private CarController _carController;
    public IContext Context
    {
        get => _context;
    }
    
    private IContext _context;
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (GameController._instance == null)
            {
                GameController._instance = new GameController();
            }

            return _instance;
        }
    }

    
    private void OnDestroy()
    {
        _instance._context.Current.StopAllCoroutines();
    }

    public void SetTypeAds(TypeAds typeAds)
    {
        _typeAds = typeAds;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InitializeContext();
        SetRecordPoint(Record.GetRecord());
        directionTurn = new DirectionTurn(_imageDirectionalTurn);
        InitializeCarController();
        InitializeAdsController();
        InitializeAudio();
        OnPlayerTurn += OnTurnPlayerCar;
        OnClickBottonUI += OnClickButton;
    }

    public void OnClickButton()
    {
        _audioController.OnClick();
    }

    public void OnEndTurn()
    {
        _carPlayerController.OnEndTurn();
    }
    
    private void InitializeCarController()
    {
        _carController = new CarController(_context);
    }
    
    public void OnTurnPlayerCar()
    {
        _audioController.OnTurn();
        _carController.IncrementSpeed();
    }
    public void UpdateRecord(int stepRecord)
    {
        _record += stepRecord;
        _recordText.text = string.Format ("{0}",_record);
    }

    private void InitializeAudio()
    {
        _audioController = new AudioController(_context);
    }
    private void InitializeContext()
    {
        _context = new Context(this);
    }

    public void ShowSettings()
    {
        _settingsController = new SettingsController(_context);
        OnClickButton();
    }
    private void InitializeAdsController()
    {
        _adsController = new AdsController(_context);
    }

    public void OnDestroyPlayerCar(GameObject car)
    {
        if (directionTurn.directionTurn is "Left")
        {
            _carController.AddCarByGameobject(car.AddComponent<OncomingCarView>());
        }
        else
        {
            _carController.AddCarByGameobject(car.AddComponent<PassingCarView>());
        }
        directionTurn.SetTurn(_imageDirectTurn);
    }
    public void OnCrashed()
    {
        _audioController.OnCrashed();
        _carController.StopCars();
        LoseGame();
    }
    public void SetRecordPoint(int value)
    {
        _record = value;
        _recordText.text = string.Format("{0}", _record);
    }

    public void StartGame()
    {
        SetRecordPoint(0);
        _menu.SetActive(false);
        eventStart?.Invoke();
        _carController.StartCars();
        directionTurn.SetTurn(_imageDirectTurn);
        _audioController.OnClick();
        _carController.SetStartSpeed();
    }

    public void LoseGame()
    {
        Handheld.Vibrate();
        _coroutineOpenPanelLoseGame = _context.Current.StartCoroutine(ShowLosePanel());
        eventLose?.Invoke();

    }


    public void OnEditStateSound()
    {
        _audioController.OnEditState();
    }
    private IEnumerator ShowLosePanel()
    {
        while (_losePanel.activeSelf is false)
        {
            yield return new WaitForSeconds(1f);
            _losePanel.SetActive(true);
        }
        yield break;
    }
    public void ContinuePlaying()
    {
        _adsController.ShowAdd();
        _losePanel.SetActive(false);
        _carController.StartCars();
        OnClickButton();
        eventContinue?.Invoke();
    }
    public void FinishGame()
    {
        eventFinish?.Invoke();
        if (Record.GetRecord() < _record)
        {
            Record.SaveRecord(_record);
        }
        SetRecordPoint(Record.GetRecord());
        trafficAIController.ResetSpeed();
        _menu.SetActive(true);
        _losePanel.SetActive(false);
        OnClickButton();
    }
}

class DirectionTurn
{
    public DirectionTurn(Sprite[] imageDirectionalTurn)
    {
        _imageDirectionalTurn.Add("Left", imageDirectionalTurn[0]);
        _imageDirectionalTurn.Add("Right", imageDirectionalTurn[1]);
    }

    private Dictionary<string, Sprite> _imageDirectionalTurn = new Dictionary<string, Sprite>();
    private Image image;
    private string _directionTurn;
    public string directionTurn
    {
        get { return _directionTurn == "Right" || _directionTurn == "Left" ? _directionTurn : null; }

        private set
        {
            if (value == "Right" || value == "Left")
            {
                _directionTurn = value;
            }
        }
    }

    public string GetDirectionTurn() => 
        _directionTurn;

    public void SetTurn(Image image)
    {
        SetDirectionTurn();
        SetImageDirectionTurn(image);
    }

    public void SetDirectionTurn()
    {
        if (Random.Range(-1, 3) > 0)
        {
            directionTurn = "Left";
        }
        else
        {
            directionTurn = "Right";
        }
    }

    private void SetImageDirectionTurn(Image image)
    {
        image.sprite = _imageDirectionalTurn[directionTurn];
    }
}
public static class Record
{

    public static void SaveRecord(int record)
    {
        PlayerPrefsUtils.SetInt("record", record);
    }

    public static int GetRecord()
    {
        return PlayerPrefsUtils.GetInt("record");
    }

}