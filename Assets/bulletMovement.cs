using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour {
    public Collider2D myself;
    Vector3 enterPosition;
    Vector3 actualPosition;
    Vector3 finishPosition;
    Vector3 movePosition;
    float timeExplosion;
    int maxPoints;
    int points;
    int decresedPoints;
    bool moving;
    bool hitExplosion;
    public static int reversalSpeed;
    public Collider2D objectCollider;
	// Use this for initialization
	void Start () {
        reversalSpeed = 200;
        maxPoints = 200;
        timeExplosion = 0.0f;
        objectCollider = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>();
        enterPosition = new Vector3(Random.Range(-9.2f, 9.2f), 5, 0);
        actualPosition = enterPosition;
        hitExplosion = false;
        finishPosition = new Vector3(Random.Range(-9.2f, 9.2f), -5, 0);
        MainScript.enemyExPos = finishPosition;
        movePosition = new Vector3(((finishPosition.x - enterPosition.x) / 200), ((finishPosition.y - enterPosition.y) / reversalSpeed), 0);
        moving = true;
        decresedPoints = maxPoints / reversalSpeed;
        points = maxPoints;
	}
	
	// Update is called once per frame
	void Update () {
        points = points - decresedPoints;
        if(hitExplosion == true)
        {
            timeExplosion = timeExplosion + Time.deltaTime;
            if (timeExplosion >= 0.5)
            {
                timeExplosion = 0.0f;
                hitExplosion = false;
            }
        }
		if(moving == true)
        {
            actualPosition = new Vector3((actualPosition.x + movePosition.x), (actualPosition.y + movePosition.y), 0);
            
            if(actualPosition.y <= -5)
            {
                MainScript.counterHit = MainScript.counterHit + 1;
                Debug.Log(MainScript.counterHit);
                hitExplosion = true;
                points = maxPoints;
                MainScript.hitExplosion = true;
                Destroy(gameObject);
            }
            this.gameObject.transform.position = actualPosition;
        }
        if (myself.IsTouching(objectCollider))
        {
            MainScript.score = MainScript.score + points;
            if(MainScript.bulletTimes >= 10)
            {
                MainScript.win = true;
            }
            points = maxPoints;
            Destroy(gameObject);
        }

    }

}
