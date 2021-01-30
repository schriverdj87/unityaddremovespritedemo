using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerDemo : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public Sprite mainSprite;
    private GameObject theGreatBurger;
    //When the counter hits 0 the burger toggles on and off.
    private int counter;
    private int counterToggleAt;
    //Whether or not the great burger is there.
    private bool theGreatBurgerThere;

    void Start()
    {
        
        counterToggleAt = 60;
        counter = counterToggleAt;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (counter == 0)
        {
            toggleBurger();
            counter = counterToggleAt;
        }
        else
        {
            counter--;
        }
    }

    void toggleBurger()
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
}
