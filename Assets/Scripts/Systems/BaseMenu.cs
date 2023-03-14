using UnityEngine;

public class BaseMenu : MonoBehaviour
{
    [SerializeField] protected MenuName _name;

    protected bool _state;

    protected AppData data;

    public MenuName Name => _name;
    public bool State => _state;
    public AppData Data => data;


    /// <summary>
    /// The method is called once during initialization of data in InterfaceManager (Awake)
    /// </summary>
    /// <param name="data"></param>
    public virtual void SetData(AppData data)
    {
        this.data = data;
    }

    /// <summary>
    /// The method is called when turning on/off this menu
    /// </summary>
    /// <param name="state"></param>
    public virtual void SetState(bool state)
    {
        _state = state;
    }
}


