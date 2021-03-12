using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable/Weapons")]
public class Weapons : ScriptableObject
{
    public string name;
    public int startAmmo;
    public int fireRate;
    public int damage;
    public MeshRenderer weaponMesh;
    public MeshFilter weaponMeshFilter;
}
