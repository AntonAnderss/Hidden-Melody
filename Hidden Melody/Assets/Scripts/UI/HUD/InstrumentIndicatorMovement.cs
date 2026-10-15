using UnityEngine;

public class InstrumentIndicatorMovement : MonoBehaviour
{
    [SerializeField] private float speed = 15f;


    void Update()
    {
        transform.Rotate(0, 0, -speed * Time.deltaTime);
    }
}
