using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _camera;
    [SerializeField] private float speedTranslate;
    protected int _score = 0;
    private AnimationCamera _animationCamera;

    private void Start()
    {
        _gameController.eventStart += StartGame;
        _animationCamera = new AnimationCamera();
    }

    public void StartGame()
    {
        StartCoroutine(_animationCamera.StartGameAnimation(_camera.transform, speedTranslate, this));
        _gameController.eventStart -= StartGame;
        _gameController.eventFinish += FinishGame;
    }

    public void FinishGame()
    {
        StartCoroutine(_animationCamera.StopGameAnimation(_camera.transform, speedTranslate, this));
        _gameController.eventFinish -= FinishGame;
        _gameController.eventStart += StartGame;
    }
}
class AnimationCamera
{
    private Vector3 _vector3Forward = new Vector3(0, 0, 1);
    public IEnumerator StartGameAnimation (Transform transformCamera,float speedTranslate,MonoBehaviour monoBehaviour)
    {
        while (transformCamera.position.z < 0)
        {
            transformCamera.Translate(_vector3Forward * speedTranslate,Space.World);
            yield return new WaitForFixedUpdate();
        }

        yield break;
    }

    public IEnumerator StopGameAnimation(Transform transformCamera, float speedTranslate, MonoBehaviour monoBehaviour)
    {
        while (transformCamera.position.z > -15.9)
        {
            transformCamera.Translate(-1*_vector3Forward * speedTranslate, Space.World);
            yield return new WaitForFixedUpdate();
        }

        yield break;
    }
}