using UnityEngine;

public enum MonsterState { Defalut, Boss }

[System.Serializable]
public class State
{
    public int HP;
    public int ATK;
    public MonsterState state;
}

public class Monster : MonoBehaviour
{
    public State state;
}
