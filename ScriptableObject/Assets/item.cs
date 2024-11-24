using UnityEngine;

public enum ItemType { Euqipment, Consumable, Other}

//스크립터블 오브젝트 선언 방법
[CreateAssetMenu(fileName = "item", menuName = "Scriptable Objects/item", order = int.MaxValue)]

//상속자는 ScriptableObject
public class item : ScriptableObject
{
    //스크립터블 오브젝트는 유니티에서 제공하는 데이터 컨테이너
    //스크립팅이 가능한 리소스 오브젝트이다.

    public ItemType Type;
    public string itemName;
    public int item_Count;
    public int Price;

}
