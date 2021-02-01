using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System;

/*
 * This class creates a "starburst" effect of points accelerating away from a point
 * Best of all, it has no dependency on Unity!
 * You'll find some very useful trigonometry functions towards the bottom
 * Made by David Schriver, January 2021
 */
public class Starburst
{
    private List<PointF> mainList;
    private int mainListMaxSize = 100;
    //The point from which all points radiate out of.
    private PointF theCenter;
    //Initial distance from the center.
    private float iDistance = 3f;
    //The maximum distance from the center before the point is removed
    private float maxDistance = 100f;
    //The multiplier by which the particles accelerate
    private float speed = 1.1f;

    //Countdown to the next burst of points
    private int burstCountDown = 0;
    private int burstCountDownMin = 10;
    private int burstCountDownMax = 15;
    private int burstSizeMin = 1;
    private int burstSizeMax = 10;



    private System.Random random;
    

public Starburst()
    {
        theCenter = new PointF(0, 0);
        mainList = new List<PointF>();
        random = new System.Random();
        
    }
    
    /*
     * Returns the mainList after moving each point and adding to it.
     */
    public List<PointF> GetPoints()
    {
        //Move them
        for (var a = 0; a < mainList.Count; a++)
        {
            /* Divides into 8 directions, doesn't look good.
            double angle = CalcAngle(mainList[a], theCenter);
            if (a == 0) { Debug.Log(angle); }
            PointF offset = CalcGo(angle, speed);*/
            PointF newPoint = mainList[a];
            //newPoint.X += offset.X;
            //newPoint.Y += offset.Y;
            newPoint.X *= speed;
            newPoint.Y *= speed;
            mainList[a] = newPoint;
        }

        //Remove any that have gone too far.
        for (var b = mainList.Count -1; b > -1; b--)
        {
            if (GetPointFDistance(mainList[b], theCenter) >= maxDistance)
            {
                mainList.Remove(mainList[b]);
            }
            
        }

        //Add points when needed
        if (burstCountDown == 0)
        {
            int burst = random.Next(burstSizeMin, burstSizeMax);

            while (burst > 0)
            {
                NewPoint();
                burst--;
            }

            burstCountDown = random.Next(burstCountDownMin, burstCountDownMax);
        }
        else
        {
            burstCountDown--;
        }

        return mainList;
    }

    public List<PointF> GetPointsWithoutUpdate()
    {
        return mainList;
    }

    /*
     * Creates a new randomized point
     */
    private void NewPoint()
    {
        if (mainList.Count < mainListMaxSize)
        {

            PointF newPoint = CalcGo(((Math.PI*2)*(random.NextDouble() )), iDistance);
            mainList.Add(newPoint);
        }
    }

    /**
     * Calculates the angle between two points
     **/
    public static double CalcAngle(PointF point1, PointF point2)
    {
        double xDiff = point1.X - point2.X + 0.001;
        double yDiff = point1.Y - point2.Y + 0.001;

        double toSend = Math.Atan(yDiff / xDiff);

        if (toSend < 0)
        {
            toSend += Math.PI;
        }

        if (point1.Y < point2.Y)
        {
            toSend += Math.PI;
        }

        return toSend;
    }


    /**
    *Takes angle and speed and converts it into a relative point.
    **/
    public static PointF CalcGo(double angle, double speed)
    {
        PointF toSend = new PointF();

        toSend.X = Convert.ToInt32(Math.Cos(angle) * speed);
        toSend.Y = Convert.ToInt32(Math.Sin(angle) * speed);

        return toSend;
    }
    /**
    *Returns the difference between two points
    **/
    public static double GetPointFDistance(PointF ptA, PointF ptB)
    {
        return Math.Sqrt(Math.Pow((ptA.X - ptB.X),2) + Math.Pow((ptA.Y - ptB.Y), 2));
    }


}
