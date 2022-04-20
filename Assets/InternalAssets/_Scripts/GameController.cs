using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _recordText;
    [HideInInspector] private int _record = 0;

    public int record { get {return _record; } }
    public Image _imageDirectTurn;
    public TrafficAIController trafficAIController;
    [SerializeField] private Sprite[] _imageDirectionalTurn = new Sprite[2];
    public delegate void GameHandler();
    public event GameHandler eventStart;
    public event GameHandler eventLose;
    public event GameHandler eventContinue;
    public event GameHandler eventFinish;
    internal DirectionTurn directionTurn;
    private Ads c_AdsUnity;
    private Ads c_AdsGoogle;
    [HideInInspector] public GameObject coliderCar { get; private set; }
    private void Start()
    {
        SetVsync1();
        //Application.targetFrameRate = -1;
        ResetRecord(Record.GetRecord());
        c_AdsGoogle = new Ads(new GoogleAds());
        c_AdsUnity = new Ads(new UnityAds());
        directionTurn = new DirectionTurn(_imageDirectionalTurn);
        //Application.targetFrameRate = 30;
    }
    public void SetVsync1() { QualitySettings.vSyncCount = 0; Application.targetFrameRate = -1; }
    public void UpdateRecord(int stepRecord)
    {
        _record += stepRecord;
        _recordText.text = string.Format ("{0}",_record);
    }

    public void ResetRecord(int value)
    {
        _record = value;
        _recordText.text = string.Format("{0}", _record);
    }

    public void StartGame()
    {
        ResetRecord(0);
        _menu.SetActive(false);
        eventStart?.Invoke();
        directionTurn.SetTurn(_imageDirectTurn);
    }

    public void LoseGame()
    {
        eventLose?.Invoke();
        _losePanel.SetActive(true);
    }

    public void ContinuePlaying()
    {
        _losePanel.SetActive(false);
        trafficAIController.speedCarTraffic = 10f;
        eventContinue?.Invoke();
    }

    public void SetColiderCarPlayer(ref GameObject carPlayer) => 
        coliderCar = carPlayer;

    public GameObject GetColiderCarPlayer() => 
        coliderCar;
    
    public void FinishGame()
    {
        eventFinish?.Invoke();
        if (Record.GetRecord() < _record)
        {
            Record.SaveRecord(_record);
        }
        ResetRecord(Record.GetRecord());
        trafficAIController.ResetSpeed();
        _menu.SetActive(true);
        _losePanel.SetActive(false);
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
        if (Random.Range(-1, 2) > 0)
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

interface IAds
{
    void MoveAds();
}

class UnityAds: IAds
{
    public void MoveAds()
    {

    }
}

class GoogleAds : IAds
{
    public void MoveAds()
    {

    }
}

class Ads
{
    public IAds iAds { get; set; }

    public Ads(IAds iAds)
    {
        this.iAds = iAds;
    }

    public void MoveAds()
    {
        iAds.MoveAds();
    }
}

public static class Record
{

    public static void SaveRecord(int record)
    {
        SPlayerPrefs.SetInt("record", record);
    }

    public static int GetRecord()
    {
        return SPlayerPrefs.GetInt("record");
    }

}