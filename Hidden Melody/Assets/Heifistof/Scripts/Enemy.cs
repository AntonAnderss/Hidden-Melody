using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] protected int hp = 20;
    [SerializeField] protected int damage = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int hurt)
    {
        hp -= hurt;
    }
    
}
