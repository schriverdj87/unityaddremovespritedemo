using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

/*
 * This is a simplish demo to demonstrate a few basic things.
 * Made by David Schriver, January 2021
 */
public class PlacerDemo : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public Sprite mainSprite;
    private GameObject theGreatBurger;
    //When the counter hits 0 the burger toggles on and off.
    private int counter = 0;
    private int counterToggleAt = 60;
    //Whether or not the great burger is there.
    private bool theGreatBurgerThere;
    //Dispenses a continuously changing array of points
    private Starburst starburst;
    //Small burgers to be matched with starburst's array of points.
    private List<GameObject> smallBurgers;
    //smallBurgers scale
    private float smallBurgersSize = 0.1f;
    
    //Center of the screen;
    Vector2 center;

    //How long to "wait" before hitting the starburst
    private int burgerStall = 0;
    private int burgerStallMax = 1;
    void Start()
    {

       //Initialize class based variables 
        center = new Vector2(16,9);
        smallBurgers = new List<GameObject>();
        starburst = new Starburst();

        //Prespin starburst
        int prespin = 100;
        while (prespin > 0)
        {
            starburst.GetPoints();
            prespin--;
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        //Exit if they press escape.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (counter == 0)
        {
           //Uncomment this to make a flashing burger
           //toggleBurger();
            counter = counterToggleAt;
        }
        else
        {
            counter--;
        }

        if (burgerStall == 0)
        {
            SynchLists();
            burgerStall = burgerStallMax;
        }
        else
        {
            burgerStall--;
        }
    }

    void ToggleBurger()
    {
        if (theGreatBurgerThere)
        {
            //Removing an object from the stage
            Object.Destroy(theGreatBurger);
            theGreatBurgerThere = false;
        }
        else
        {
            //I learned this from Code Monkey's Worm game
            //Instantiate the game object
            theGreatBurger = new GameObject("Burger", typeof(SpriteRenderer));
            //Set the actual sprite
            theGreatBurger.GetComponent<SpriteRenderer>().sprite = mainSprite;
            //Position it
            theGreatBurger.transform.position = new Vector2(16, 9);
            theGreatBurgerThere = true;
        }
    }

    //Make a small burger
    void MakeBurger()
    {
        //Create a new burger
        GameObject newBurger = new GameObject("smallBurger", typeof(SpriteRenderer));
        newBurger.GetComponent<SpriteRenderer>().sprite = mainSprite;
        newBurger.transform.localScale = new Vector2(smallBurgersSize, smallBurgersSize);
        smallBurgers.Add(newBurger);
    }

    //Remove a burger.
    void EatBurger()
    {
        //Removes the last burger in the list.
        if (smallBurgers.Count > 0)
        {
            int killIndex = smallBurgers.Count - 1;
            Object.Destroy(smallBurgers[killIndex]);
            smallBurgers.RemoveAt(killIndex);
        }
    }

    //
    void SynchLists()
    {
        
        List<PointF> starburstList = starburst.GetPoints();

        //Make the lists match in size
        while (smallBurgers.Count > starburstList.Count)
        {
            EatBurger();
        }

        while (smallBurgers.Count < starburstList.Count)
        {
            MakeBurger();
        }

        //Synchronize to burgers to the points;
        for (int a = 0; a < starburstList.Count; a++ )
        {
            //Position the burger
            Vector2 newCoord = new Vector2();
            newCoord.x = ((starburstList[a].X / 100) * center.x) + center.x;
            newCoord.y = ((starburstList[a].Y / 100) * center.x) + center.y;
            smallBurgers[a].transform.position = newCoord;

            //Make the bugers larger as they get further from the center
            double distance = Starburst.GetPointFDistance(new PointF(((starburstList[a].X / 100) * center.x) + center.x, ((starburstList[a].Y / 100) * center.x) + center.y), new PointF(center.x, center.y));
            float newSize = (float)distance * smallBurgersSize;
            smallBurgers[a].transform.localScale = new Vector2(newSize, newSize);

        }
    }
}
