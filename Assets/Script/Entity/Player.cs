using System.Diagnostics.Tracing;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerVisibleStat stat;

    private PlayerBehaviour playerBehaviour;
    public PlayerBehaviour PlayerBehaviour
    {
        get { return playerBehaviour; }
    }

    private PlayerInventory playerInventory;
    public PlayerInventory Inventory 
    {
        get { return playerInventory; } 
    }

    private PlayerInteraction playerInteraction;
    public PlayerInteraction PlayerInteraction
    {
        get { return playerInteraction; }
    }

    private void Awake()
    {
        Managers.Object.Player = this;
        playerBehaviour = GetComponent<PlayerBehaviour>();
        playerInventory = GetComponent<PlayerInventory>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }
    private void Start()
    {
        stat = new PlayerVisibleStat();
        stat.lvl = 1;
        stat.name = "UnityChan";
        //stat.ATK = playerBehaviour.attack;
        stat.ATK = 999;
        stat.DEF = 999;
    }
}

public class PlayerVisibleStat
{
    public int lvl;
    public string name;
    public int HP;
    public int ATK;
    public int DEF;
}