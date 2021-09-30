using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRange 
{
    private float smallestX;
    private float biggestX;
    private float smallestZ;
    private float biggestZ;

    //constructor
    public PlaneRange(float sX, float bX, float sZ, float bZ)
    {
        smallestX = sX;
        biggestX = bX;
        smallestZ = sZ;
        biggestZ = bZ;
}

    public float GetSmallestX()
    {
        return smallestX;
    }

    public float GetBiggestX()
    {
        return biggestX;
    }

    public float GetSmallestZ()
    {
        return smallestZ;
    }

    public float GetBiggestZ()
    {
        return biggestZ;
    }

    public void SetSmallestX(float sX)
    {
        smallestX = sX;
    }

    public void SetBiggestX(float bX)
    {
        biggestX = bX;
    }

    public void SetSmallestZ(float sZ)
    {
        smallestZ = sZ;
    }

    public void SetBiggestZ(float bZ)
    {
        biggestZ = bZ;
    }
}
