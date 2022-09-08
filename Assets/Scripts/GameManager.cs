using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;
    [SerializeField] Text scoreText;
    [SerializeField] PlayerController playerController;
    public void IncrementScore() 
    {
        score++;
        scoreText.text = "Score: " + score;
        //increase speed
        playerController.speed += playerController.speedIncreasePerPoint;
    }
    void Awake()
    {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
