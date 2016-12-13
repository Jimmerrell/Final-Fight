using System.Xml;

public class Mercenary_Card : Card 
{
    //This divided by 100 gives a base accuracy
    public int baseAttack = 0;
    //This is the number of health points
    public int healthPoints = 0;
    //Number of attacks before the enemies attack
    public int priorityAttacks = 0;
    //How much the player's movement changes
    public int movement = 0;
    //This is how many turns the mercenary lasts until they disappear
    //(if 0 then they last until they die)
    public int turnsAlive = 0;
}
