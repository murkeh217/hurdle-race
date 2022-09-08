using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null) 
        {
            gameObject.SetActive(false);
            return;
        } 

        //collides with player?
        if (other.gameObject.name != "Player")
        {
            return;
        }

        //add to score
        GameManager.inst.IncrementScore();

        //destroy coin
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
