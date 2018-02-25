using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {
    public GameObject popUpLose;
    public GameObject popUpWin;
    public int speedBullet;
    bool endGame;
    public static bool win;
    public GameObject particle;
    public GameObject moveExplosion;
    public GameObject explosionAnimPos;
    public GameObject enemyExplosionPos;
    public Collider2D explosion;
    Vector3 actualPosition;
    Vector3 finishPosition;
    Vector3 pixelMove;
    public int level;
    float counterEnemyExplosion;
    float pixelPerfectX;
    float pixelPerfectY;
    bool BeginMovement;
    bool finishMovement;
    bool moving;
    bool startCounter;
    bool startcounterEnemy;
    float counter;
    public Text scoreText;
    public Text lives;
    public Text finalScore;
    public static bool hitExplosion;
    public static Vector3 enemyExPos;
    public static int counterHit;
    public static int score;
    public static int bulletTimes;
    float counterExplosion;
    private void Start()
    {
        bulletMovement.reversalSpeed = speedBullet;
        win = false;
        endGame = false;
        bulletTimes = 0;
        score = 0;
        counterHit = 0;
        counter = 0;
        startCounter = false;
        endGame = false;
        popUpLose.SetActive(false);
        popUpWin.SetActive(false);
        explosionAnimPos.SetActive(false);
        enemyExplosionPos.SetActive(false);
        lives.text = "Lives: " + (5 - counterHit);
        scoreText.text = "Score: " + 0;
    }
    void Update()
    {
        
        counter = counter + Time.deltaTime;
        lives.text = "Lives: " + (5 - counterHit);
        scoreText.text = "Score: " + score;
        if(startcounterEnemy == true)
        {
            counterEnemyExplosion = counterEnemyExplosion + Time.deltaTime;
            if (counterEnemyExplosion >= 0.5)
            {
                Debug.Log("Ha entrado");
                enemyExplosionPos.SetActive(false);
                counterEnemyExplosion = 0.0f;
                startcounterEnemy = false;
            }
        }
        else
        {
            enemyExplosionPos.transform.position = enemyExPos;
        }
        
        if (endGame == false)
        {
            if(win == true)
            {
                finalScore.text = "You get " + score + " points";
                popUpWin.SetActive(true);
            }
            if (counter >= 5 && bulletTimes < 10)
            {
                bulletMovement.reversalSpeed = speedBullet;
                Instantiate(particle, transform.position, transform.rotation);
                bulletTimes = bulletTimes + 1;
                counter = 0;
            }

            if (startCounter == true)
            {
                counterExplosion = counterExplosion + Time.deltaTime;
                if (counterExplosion >= 1)
                {
                    counterExplosion = 0;
                    explosion.enabled = false;
                    explosionAnimPos.SetActive(false);
                    startCounter = false;
                    moveExplosion.transform.position = new Vector3(7, 6, 0);
                }
            }
        }
        else
        {
            popUpLose.SetActive(true);
        }

        if (BeginMovement == true)
        {
            actualPosition = new Vector3(0, -5, 0);
            pixelMove = new Vector3 (((finishPosition.x-actualPosition.x)/40),((finishPosition.y - actualPosition.y) / 40),0);
            Debug.Log(pixelMove.x);
            Debug.Log(pixelMove.y);
            moving = true;
            BeginMovement = false;
        }

        if(moving == true)
        {
            actualPosition = actualPosition + (pixelMove);
            if(actualPosition.y >= finishPosition.y)
            {
                actualPosition = finishPosition;
                moving = false;
                explosionAnimPos.SetActive(true);
                finishMovement = true;
            }
            moveExplosion.transform.position = actualPosition;
        }
        if(finishMovement == true)
        {
            explosion.enabled = true;
            startCounter = true;
            finishMovement = false;
        }
        if (endGame == false && win == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log(counterHit);
                Vector3 MousePos = Input.mousePosition;
                Debug.Log(MousePos.x);
                Debug.Log(MousePos.x - 546.5f);
                pixelPerfectX = (MousePos.x - 478f) / 51.4f;
                Debug.Log(MousePos.y);
                Debug.Log(MousePos.y - 257f);
                pixelPerfectY = (MousePos.y - 257f) / 51.4f;
                finishPosition = new Vector3((pixelPerfectX), (pixelPerfectY), 0);
                explosionAnimPos.transform.position = finishPosition;
                BeginMovement = true;
            }
        }
        if(hitExplosion == true)
        {
            enemyExplosionPos.SetActive(true);
            startcounterEnemy = true;
            hitExplosion = false;
        }
        if(counterHit == 5)
        {
            endGame = true;
        }
    }
    public void NextLevel(int nextlevel)
    {
        popUpWin.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextlevel);
    }
    

}

