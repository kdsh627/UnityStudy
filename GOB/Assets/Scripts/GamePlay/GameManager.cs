using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static GameManager instance;
    [SerializeField] private GameObject skillGroup;
    [SerializeField] private PlayerClass player;
    [SerializeField] private BeHolder boss;
    [SerializeField] private GameUI gameUI;
    private Dictionary<string, Skill> skillDict;

    public Dictionary<string, Skill> SkillDict => skillDict;
    public PlayerClass Player => player;
    public BeHolder Boss => boss;
    public GameUI UI => gameUI;
    public static GameManager Instance => instance;

    void Awake()
    {
        instance = this;

        skillDict = new Dictionary<string, Skill>();

        Skill[] skills = skillGroup.GetComponentsInChildren<Skill>();
        foreach (Skill skill in skills)
        {
            skillDict.Add(skill.name, skill);
        }
    }
}
