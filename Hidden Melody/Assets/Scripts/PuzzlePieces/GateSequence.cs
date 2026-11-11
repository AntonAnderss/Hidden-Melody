using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GateSequence : MonoBehaviour
{
    public Rigidbody player;
    public float openHeight = 5f;
    public float speed = 3f;

    private Rigidbody rb;
    private Vector3 openPosition;
    private int nextButton = 1;
    private bool opened;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private int lightsOrder;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        openPosition = rb.position + Vector3.up * openHeight;
    }

    void FixedUpdate()
    {
        if (opened)
        {
            rb.MovePosition(Vector3.MoveTowards(
                rb.position,
                openPosition,
                speed * Time.fixedDeltaTime
            ));
        }
    }

    public void PressButton(int number)
    {
        if (opened)
            return;

        if (number != nextButton)
        {
            nextButton = 1;
            Debug.Log("Wrong order! Start again from button 1.");
            for(int i = 0; i<lights.Count(); i++)
            {
                lights[i].SetActive(false);
            }
            lightsOrder = 0;
            return;
        }

        Debug.Log("Correct button: " + number);
        nextButton++;
        lights[lightsOrder].SetActive(true);
        lightsOrder++;
        
        if (nextButton > 3)
        {
            opened = true;      
        }
    }
}