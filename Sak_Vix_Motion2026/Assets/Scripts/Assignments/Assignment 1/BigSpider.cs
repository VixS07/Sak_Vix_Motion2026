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
    bool spawnedEgg;

    

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
    }

    public void spawnPoints()
    {
        //spawning the 5 points once
        if (pointTransforms.Count < pointAmount)
        {
            //top
            for (int i = 0; i < pointAmount / 2; i++)
            {
                GameObject point = Instantiate(pointPrefab);
                pointTransforms.Add(point.transform);
                Vector2 pointPos = point.transform.position;
                pointPos.y = topRight.y + 2;
                pointPos.x = bottomLeft.x;
                point.transform.position = pointPos;
            }
            //bottom
            for (int i = pointAmount / 2; i < pointAmount; i++)
            {
                GameObject point = Instantiate(pointPrefab);
                pointTransforms.Add(point.transform);
                Vector2 pointPos = point.transform.position;
                pointPos.y = bottomLeft.y - 2;
                pointPos.x = bottomLeft.x;
                point.transform.position = pointPos;
            }
        }

        //live updating the top point positions if the screen width changes
        //top
        for (int i = 0; i < pointTransforms.Count / 2; i++)
        {
            float halfWidth = topRight.x / 2f;
            Vector2 pointPos = pointTransforms[i].transform.position;
            if (pointPos.y > topRight.y)
            {
                Vector2 pos = new Vector2(bottomLeft.x, topRight.y + 2);
                distance = bottomLeft.x + halfWidth * i;
                pos.x = distance;
                pointTransforms[i].transform.position = pos;
            }
        }
        //bottom
        for (int i = pointTransforms.Count / 2; i < pointTransforms.Count; i++)
        {
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


        if (!spawned)
        {
            spider = Instantiate(spiderPrefab);
            spider.transform.position = new Vector2(0, bottomLeft.y - 10);
            spawned = true;
        }
        if (count)
        {
            t += Time.deltaTime;
        }

        if(t >= spawnInterval)
        {
            endPos = pointTransforms[Random.Range(0, 5)].transform.position;
            startPos = pointTransforms[Random.Range(5,10)].transform.position;

            spider.transform.position = startPos;

            direction = (endPos - startPos).normalized; 
            move = true;
  
            t = 0;
        }

        if (move && linePoints != null)
        {
            spider.transform.position += (Vector3)(direction * spiderSpeed * Time.deltaTime);

        }
    }

    public void Web()
    {
        if (move)
        {
            Debug.DrawLine(startPos, spider.transform.position, Color.ghostWhite);
            if(spider.transform.position.y > topRight.y + 5 && !added)
            {
                linePoints.Add(startPos);
                linePoints.Add(endPos);
                added = true;
                move = false;
            }
        }
        if(!move && added)
        {
            Debug.DrawLine(linePoints[0], linePoints[1], Color.ghostWhite);
            count = false;
        }

    }

    public void Eggs()
    {
        float midOfPath = (startPos.y + endPos.y) / 2;
        if(spider.transform.position.y > midOfPath - 1 && spider.transform.position.y < midOfPath + 1 && !spawnedEgg)
        {
            egg = Instantiate(eggPrefab);
            egg.transform.position = spider.transform.position;
            spawnedEgg = true;
        }
    }

    public void SquishEgg()
    {
        if (egg != null)
        {
            if (Vector3.Distance(egg.transform.position, player.transform.position) < 1)
            {
                Destroy(egg);
                spawnedEgg = false;
                linePoints.Clear();
                count = true;
                move = false;
                added = false;

            }
        }
    }
}
