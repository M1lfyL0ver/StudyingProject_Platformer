using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealPotion : MonoBehaviour
{
    [SerializeField] private int _heal = 10;

    public int Heal { get; private set; }

    private void Awake()
    {
        Heal = _heal;
    }
}