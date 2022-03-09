using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public float speedCar { get; set; }
    private Transform _transform;
    private Vector3 _vector3Forward = new Vector3(0, 0, -1);

    public void AnimationTurnExit()
    {
        if (gameObject.GetComponent<BoxCollider>().enabled)
        {
            _transform = gameObject.transform;
            StartCoroutine(DravingCar());
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
   
    public void CollidedCar()
    {
        GameObject playerCar = gameObject;
        playerCar.GetComponent<BoxCollider>().enabled = false;
        playerCar.GetComponent<Animator>().SetFloat("SpeedAnimation", 0);
    }

    private IEnumerator  DravingCar()
    {
        while (true)
        {
            _transform.Translate(_vector3Forward * Time.deltaTime* speedCar);
            if (_transform.position.x > 120f || _transform.position.x < -30f)
            {
                Destroy(gameObject);
            }
            yield return new WaitForFixedUpdate();
        }
       
    }
}
