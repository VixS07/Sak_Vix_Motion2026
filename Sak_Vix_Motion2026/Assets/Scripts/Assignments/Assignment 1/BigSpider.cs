using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigSpider : MonoBehaviour
{

    public GameObject pointPrefab;
    public GameObject spiderPrefab;
    public GameObject eggPrefab;
    public GameObject player;
    GameObject egg;
    GameObject spider;
    Vector2 bottomLeft;
    Vector2 topRight;
    public float distance;
    public List<Transform> pointTransforms;
    public int pointAmount;
    public int spawnInterval;
    bool spawned;
    public float t;
    public float spiderSpeed;
    bool move;
    Vector2 direction;
    Vector2 startPos;
    Vector2 endPos;
    public List<Vector2> linePoints;
    bool added;
    bool count = true;
    float playerAngle;
    float spiderAngle;
    public int rotationSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get screen limits
        bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


        spawnPoints();
        SpiderMovement();
        Web();
        Eggs();
        SquishEgg();
        LookStraight();
    }

    public void spawnPoints()
    {
        //spawning the 5 points once
        if (pointTransforms.Count < pointAmount)
        {
            //top
            for (int i = 0; i < pointAmount / 2; i++)
            {
                //instantiate them
                GameObject point = Instantiate(pointPrefab);
                //store them
                pointTransforms.Add(point.transform);
                //edit their positions to space them out
                Vector2 pointPos = point.transform.position;
                pointPos.y = topRight.y + 2;
                pointPos.x = bottomLeft.x;
                //apply changes
                point.transform.position = pointPos;
            }
            //bottom
            for (int i = pointAmount / 2; i < pointAmount; i++)
            {
                //instantiate them
                GameObject point = Instantiate(pointPrefab);
                //store them
                pointTransforms.Add(point.transform);
                //edit their positions to space them out
                Vector2 pointPos = point.transform.position;
                pointPos.y = bottomLeft.y - 2;
                pointPos.x = bottomLeft.x;
                //apply changes
                point.transform.position = pointPos;
            }
        }

        //live updating the top point positions if the screen width changes
        //top
        for (int i = 0; i < pointTransforms.Count / 2; i++)
        {
            //get increase number
            float halfWidth = topRight.x / 2f;
            //acess the positions through the list
            Vector2 pointPos = pointTransforms[i].transform.position;
            //double checking we're changing the top ones
            if (pointPos.y > topRight.y)
            {
                Vector2 pos = new Vector2(bottomLeft.x, topRight.y + 2);
                //increase x by the half width every time
                distance = bottomLeft.x + halfWidth * i;
                pos.x = distance;
                //apply changes
                pointTransforms[i].transform.position = pos;
            }
        }
        //bottom
        for (int i = pointTransforms.Count / 2; i < pointTransforms.Count; i++)
        {
            //get increase number
            float halfWidth = topRight.x / 2f;
            Vector2 pointPos = pointTransforms[i].transform.position;
            if (pointPos.y < bottomLeft.y)
            {
                Vector2 pos = new Vector2(bottomLeft.x, bottomLeft.y - 2);
                //have to add the subtraction so that the bottom row mirrors the top
                //rather than starting on the x positions after it
                distance = bottomLeft.x + halfWidth * (i - pointTransforms.Count / 2);
                pos.x = distance;
                pointTransforms[i].transform.position = pos;
            }
        }
    }

    public void SpiderMovement()
    {
        Vector2 lookingDirection;

        //Vector mechs
        //make sure it only spawns once
        if (!spawned)
        {
            //make spider
            spider = Instantiate(spiderPrefab);
            //place it off screen towards the bottom
            spider.transform.position = new Vector2(0, bottomLeft.y - 10);
            //make sure we cant spawn another
            spawned = true;
        }

        //variable which tracks whather or not the timer can increase
        if (count)
        {
            //increase timer
            t += Time.deltaTime;
        }

        if (t >= spawnInterval)
        {
            //get a random start and end point
            endPos = pointTransforms[Random.Range(0, 5)].transform.position;
            startPos = pointTransforms[Random.Range(5, 10)].transform.position;

            //place spider at start position
            spider.transform.position = startPos;

            //get the diredction of travel
            direction = (endPos - startPos).normalized;
            //start movement
            move = true;

            t = 0;
        }

        //checking if we can move, and theres no line points already
        if (move && linePoints != null)
        {
            //move in the direction
            //controlled by a variable speed and time
            spider.transform.position += (Vector3)(direction * spiderSpeed * Time.deltaTime);

        }

        //rotation mechs
        //find out where the spider is supposed to be looking
        lookingDirection = endPos - (Vector2)spider.transform.position;
        spiderAngle = (Mathf.Atan2(lookingDirection.y, lookingDirection.x) * Mathf.Rad2Deg);
        //look that way
        spider.transform.eulerAngles = new Vector3(0, 0, spiderAngle - 90f);

    }

    public void Web()
    {
        if (move)
        {
            //draw the web as the spider comes across the screen
            Debug.DrawLine(startPos, spider.transform.position, Color.ghostWhite);
            //are we above screen, and have we added to the list yet
            if (spider.transform.position.y > topRight.y + 5 && !added)
            {
                //store the points of the line
                linePoints.Add(startPos);
                linePoints.Add(endPos);
                //track that the list has been added to 
                added = true;
                //make it so we cant move anymore
                move = false;
            }
        }

        //if we finished moving and we have point sin our list
        if (!move && added)
        {
            //keep the line drawn, stop the counter (prevents spider from re-spawning and changing the line)
            Debug.DrawLine(linePoints[0], linePoints[1], Color.ghostWhite);
            count = false;
        }

    }

    public void Eggs()
    {
        Vector2 lookingDirection;

        //calculate the middle of the path for the spider
        //since it will often be a diagonal we cant just use screen height
        float midOfPath = (startPos.y + endPos.y) / 2;
        //if were at around the middle of the path (it will virtually never be exact), and we dont have an egg
        if (spider.transform.position.y > midOfPath - 1 && spider.transform.position.y < midOfPath + 1 && egg == null)
        {
            //spawn the egg
            egg = Instantiate(eggPrefab);
            //spawn it where the spider is
            egg.transform.position = spider.transform.position;

        }
        //do we have an egg
        if (egg != null)
        {
            //rotation mechs
            //figure out where were supposed to look
            lookingDirection = egg.transform.position - player.transform.position;
            playerAngle = (Mathf.Atan2(lookingDirection.y, lookingDirection.x) * Mathf.Rad2Deg);
            //look towards the egg
            player.transform.eulerAngles = new Vector3(0, 0, playerAngle - 90f);
        }
    }

    public void SquishEgg()
    {
        //do we have an egg
        if (egg != null)
        {
            //is the player close to the egg
            if (Vector3.Distance(egg.transform.position, player.transform.position) < 1)
            {
                //delete the object
                Destroy(egg);
                //clear the list, and therefore delete the web line
                linePoints.Clear();
                //restart the timer
                count = true;
                //state that we have cleared the list
                added = false;

            }
        }
    }

    public void LookStraight()
    {
        //using dot product, check if the player is looking left or right and then slowly rotate it so its looking upwards again
        float currentDirecttion = Vector2.Dot(player.transform.up, Vector2.right);
        //are we looking right
        if (currentDirecttion > 0f)
        {
            //double check we are not upright, and make sure the egg is gone
            if(player.transform.eulerAngles.z != 0 && egg == null)
            {
                //move back
                player.transform.eulerAngles += new Vector3(0,0,1 * rotationSpeed * Time.deltaTime); 
            }
        }
        if (currentDirecttion < 0f)
        {
            //double check we are not upright, and make sure the egg is gone
            if (player.transform.eulerAngles.z != 0 && egg == null)
            {
                //move back
                player.transform.eulerAngles -= new Vector3(0, 0, 1 * rotationSpeed * Time.deltaTime);
            }
        }

    }
}
