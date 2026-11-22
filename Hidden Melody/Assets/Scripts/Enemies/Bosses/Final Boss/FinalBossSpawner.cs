using UnityEngine;

public class FinalBossSpawner : MonoBehaviour
{
    [SerializeField] GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if (!ValidateBoss())
            return;

        boss.GetComponent<FinalBoss>().StartBattle();
        Destroy(gameObject);
    }

    bool ValidateBoss()
    {
        if (boss != null && boss.TryGetComponent<FinalBoss>(out FinalBoss fb))
            return true;

        Debug.LogError("Final boss isn't defined");
        return false;
    }
}
