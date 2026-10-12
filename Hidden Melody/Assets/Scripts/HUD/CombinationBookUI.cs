using UnityEngine;

public class CombinationBookUI : MonoBehaviour
{
    [SerializeField] private GameObject CombinationBook;

    public void ToggleBook()
    {
        CombinationBook.SetActive(!CombinationBook.activeSelf);
    }
}