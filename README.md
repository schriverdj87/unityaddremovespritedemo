This is sort of like the old starfield simulation screensaver, but with cheeseburgers instead.
This project serves to demonstrate some technical basics for game design. In particular, adding and removing GameObjects on the stage. 

===========
Example Highlights
===========

Adding a GameObject to the stage
-----------------------------
GameObject newBurger = new GameObject("smallBurger", typeof(SpriteRenderer));
newBurger.GetComponent<SpriteRenderer>().sprite = mainSprite;


Removing a GameObject from the stage
-----------------------------
Object.Destroy(theGreatBurger);


Reverse for loop (Necessary when you might remove something from the array)
-----------------------------

for (var b = mainList.Count -1; b > -1; b--)
{
    if (GetPointFDistance(mainList[b], theCenter) >= maxDistance)
    {
        mainList.Remove(mainList[b]);
    }
            
}