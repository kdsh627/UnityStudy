using UnityEngine;
//빌더 패턴

public interface IBuilder
{
    void Animator_Part(MonsterState state);

    void State_Part(int hp, int atk, MonsterState state);

    GameObject Result();
}

public class Builder : IBuilder
{
    private GameObject monster = new GameObject();

    //빌더패턴이란 객체의 생성 과정을 단계별로 분리하는 것

    //몬스터 애니메이터 파츠
    public void Animator_Part(MonsterState state)
    {
        monster.AddComponent<SpriteRenderer>();
        //애니메이터를 추가하고 애니메이터 컨트롤러를 Resources라는 폴더에서 가져와는 것
        monster.AddComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(state.ToString());

    }

    //몬스터 상태 파츠
    public void State_Part(int hp, int atk, MonsterState state)
    {
        monster.AddComponent<Monster>();
        State m_state = new State();
        m_state.HP = hp;
        m_state.ATK = atk;
        m_state.state = state;

        monster.GetComponent<Monster>().state = m_state;
    }

    public GameObject Result()
    {
        return monster;
    }
}
