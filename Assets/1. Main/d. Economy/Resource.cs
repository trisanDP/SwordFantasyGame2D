using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resources/Resource")]
public class Resource : ScriptableObject {
    public string resourceName;
    public Sprite icon;
    public int amount;
}
