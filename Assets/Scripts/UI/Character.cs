using UnityEngine;

public class Character
{
    private string _name;
    private Sprite _icon;
    private int _id;
    private string _description;

    public string Name 
    { 
        get => _name;
        private set => _name = value;
    }

    public Sprite Icon
    {
        get => _icon;
        private set => _icon = value;
    }

    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    public string Description
    {
        get => _description;
        private set => _description = value;
    }

    public Character(int id, string name, Sprite icon, string description = "")
    {
        _id = id;
        _name = name;
        _icon = icon;
        _description = description;
    }
} 