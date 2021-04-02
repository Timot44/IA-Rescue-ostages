using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable/Weapons")]
public class Weapons : ScriptableObject
{
    public string name;
    public float fireRate;
    public int currentAmmo;
    public int damage;
    public float range;
    public LayerMask layers;
}
