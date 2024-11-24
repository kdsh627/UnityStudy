using UnityEngine;

public class Example : MonoBehaviour
{
    public item[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < items.Length; i++)
        {
            Debug.Log(items[i].itemName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
