using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarPlayerController : MonoBehaviour,IPointerClickHandler
{
    
    public GameController gameController;
    [SerializeField] private GameObject carPlayer;
    [SerializeField] private GameObject _carPlayerPrefabs;
    private bool _isPlay = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (_isPlay)
        {
            SetSpeedAnimation(carPlayer.GetComponent<Animator>(), gameController.record * 0.001f);
            TurnCarPlayer();
            carPlayer.GetComponent<PlayerCar>().isColision = false;
            _isPlay = false;
        }
    }
    
    public void OnEndTurn()
    {
        CreateNewCarPlayer();
        SetAnimatorStart(true);
        _isPlay = true;
        gameController.UpdateRecord(1);
        gameController.trafficAIController.UpSpeed();
    }
    
    private void TurnCarPlayer()
    {
        carPlayer.GetComponent<PlayerCar>().speedCar = gameController.trafficAIController.speedCarTraffic;
        carPlayer.GetComponent<BoxCollider>().enabled = true;
        if (gameController.directionTurn.GetDirectionTurn() == "Left")
        {
            TurnLeft();
        }
        else
        {
            TurnRight();
        }
    }

    private void CreateNewCarPlayer()
    {
        /*gameController.directionTurn.SetTurn(gameController._imageDirectTurn);*/
        carPlayer = Instantiate(_carPlayerPrefabs);
        
    }

    public void SetAnimatorStart(bool value) =>
        carPlayer.GetComponent<Animator>().SetBool("StartGame", value);

    public void LosePlay()
    {
        _isPlay = false;
    }
    private void StartGameCar()
    {
        _isPlay = true;
        SetAnimatorStart(true);
    }
    private void FinishPlay()
    {
        StopAllCoroutines();
        DestroyCurrentCar(ref carPlayer);
        CreateNewCarPlayer();
    }
    private void ContinuePlay()
    {
        SetFalseTurnAnimator(carPlayer.GetComponent<Animator>());
        _isPlay = true;
    }

    private void SetFalseTurnAnimator(Animator animator)
    {
        SetSpeedAnimation(animator, 1f);
        animator.SetBool("RightTurn", false);
        animator.SetBool("LeftTurn", false);
    }
    
  
    private void SetSpeedAnimation(Animator animator,float speed)
    {
        if (speed < 2)
        {
            animator.SetFloat("SpeedAnimation", 1+speed);
        }
    }
    
    private void DestroyCurrentCar(ref GameObject carPlayer)
    {
        Destroy(carPlayer);
    }

    private void TurnRight()
    {
        carPlayer.GetComponent<Animator>().SetBool("RightTurn", true);
        GameController.Instance.OnTurnPlayerCar();
    }


    private void TurnLeft()
    {
        carPlayer.GetComponent<Animator>().SetBool("LeftTurn", true);
        GameController.Instance.OnTurnPlayerCar();
    }

    private void Start()
    {
        _isPlay = true;
        gameController.eventStart += StartGameCar;
        gameController.eventLose += LosePlay;
        gameController.eventContinue += ContinuePlay;
        gameController.eventFinish += FinishPlay;
    }
    
}


