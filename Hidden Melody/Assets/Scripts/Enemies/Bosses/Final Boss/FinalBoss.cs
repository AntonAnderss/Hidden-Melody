using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] bool inBattle = false;

    [Header("Wall")]
    [SerializeField] float maxWallY;
    [SerializeField] float minWallY;
    float WallSize {  get { return maxWallY - minWallY; } }
    [SerializeField] float wallX;
    [SerializeField] float holeProcentage; //Between 0.01 - 1.0

    [Header("Debug")]
    [SerializeField] float wallTime = 3.0f;
    Timer timer;

    public void Start()
    {
        timer = new Timer(wallTime);
    }

    // Boss logic
    public void Update()
    {
        if (!timer.TargetTimeReached())
            return;

        // Remove the old wall, before building a new one
        RemoveWall();
        BuildWall();
    }

    public void StartBattle()
    {
        gameObject.SetActive(true);
        inBattle = true;
    }

    /* Wall handling */
    void BuildWall()
    {
        // Finds out where the hole will be place 
        System.Random rnd = new System.Random();
        int holePlacement = rnd.Next(1, 100);
        float holePlacementProcentage = (float)holePlacement / 100.0f;

        // Define hole
        float holeY = Mathf.Lerp(minWallY, maxWallY, holePlacementProcentage);
        float holeSize = WallSize * holeProcentage;

        //Build top wall
        float topWallBottom = holeY + (holeSize / 2.0f);
        float topWallMiddle = (maxWallY + topWallBottom) / 2.0f;
        GameObject topWall = BuildWallPiece(wallX, topWallMiddle, maxWallY, topWallBottom);

        //Build bottom wall
        float bottomWalltop = holeY - (holeSize / 2.0f);
        float bottomWallMiddle = (bottomWalltop + minWallY) / 2.0f;
        GameObject bottomWall = BuildWallPiece(wallX, bottomWallMiddle, bottomWalltop, minWallY);
    }

    void RemoveWall()
    {
        GameObject[] walls =GameObject.FindGameObjectsWithTag("WallPiece");

        for(int i = 0; i  < walls.Length; i++)
            if (walls[i] != null)
                Destroy(walls[i]);
    }

    GameObject BuildWallPiece(float x, float y, float top, float bottom)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = new Vector3(wallX, y, 0);
        wall.transform.rotation = Quaternion.identity;
        wall.transform.localScale = new Vector3(1, top - bottom, 1);
        wall.tag = "WallPiece";
        wall.name = "Wall piece";

        return wall;
    }
}
