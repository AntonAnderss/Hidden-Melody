using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldDuration = 2f;
    [SerializeField] private float shieldCooldown = 3f;

    

    private bool shieldActive = false;
    private float nextShieldTime = 0f;
    private Quaternion startShieldRotation;
    private Vector3 startShieldPosition;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        startShieldPosition = shieldObject.transform.localPosition;
        startShieldRotation = shieldObject.transform.localRotation;

        shieldObject.SetActive(false);

    }

    private void Update()
    {
        if(shieldActive)
        {
            UpdateShieldDirection();
        }
    }

    private void UpdateShieldDirection()
    {
        int direction = playerMovement.LastMoveDirection;

        Vector3 position = startShieldPosition;

        if (direction == 1)
        {
            // Right
            position.x = Mathf.Abs(startShieldPosition.x);

            shieldObject.transform.localPosition = position;
            shieldObject.transform.localRotation = startShieldRotation;
        }
        else
        {
            // Left
            position.x = -Mathf.Abs(startShieldPosition.x);

            shieldObject.transform.localPosition = position;

            shieldObject.transform.localRotation =
                startShieldRotation * Quaternion.Euler(0, 180, 0);
        }
    }
    public void ActivateShield()
    {
        if (shieldActive || Time.time < nextShieldTime)
            return;

        nextShieldTime = Time.time + shieldCooldown;
        UpdateShieldDirection();
        StartCoroutine(ShieldActive());
    }

    private IEnumerator ShieldActive()
    {
        shieldActive = true;

        shieldObject.SetActive(true);

        yield return new WaitForSeconds(shieldDuration);

        shieldObject.SetActive(false);

        shieldActive = false;
    }
}
