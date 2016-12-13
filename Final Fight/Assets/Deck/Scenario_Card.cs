using System.Xml;

public class Scenario_Card : Card 
{
    //Number of zombies to spawn
    public int spawnZombies = 0;
    //Skip a player's turn
    public int skipTurn = 0;
    //Extra cards to draw
    public int numDrawCards = 0;
    //1 if can swap places with a player
    public int swapPlaces = 0;
    //If 1, can save a downed player and kill all zombies
    //on that space
    public int savePlayer = 0;
}
