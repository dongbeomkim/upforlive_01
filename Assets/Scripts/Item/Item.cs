using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public bool equipment;
    public bool Expendables;
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public float power;
    public float heal;
    public float defense;
}
