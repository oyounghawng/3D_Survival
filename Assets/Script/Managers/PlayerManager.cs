using UnityEngine;

public class PlayerManager
{
    private PlayerBehaviour playerBehaviour;
    public PlayerBehaviour PlayerBehaviour
    {
        get { return playerBehaviour; }
        set { playerBehaviour = value; }
    }

    private PlayerInventory playerInventory;
    public PlayerInventory Inventory 
    {
        get { return playerInventory; } 
        set { playerInventory = value; } 
    }

    private PlayerInteraction playerInteraction;
    public PlayerInteraction PlayerInteraction
    {
        get { return playerInteraction; }
        set { playerInteraction = value; }
    }

    private GameObject player;
    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

}