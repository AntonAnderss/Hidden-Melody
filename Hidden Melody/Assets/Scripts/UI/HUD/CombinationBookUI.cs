using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombinationBookUI : MonoBehaviour
{
    [SerializeField] private GameObject CombinationBook;
    [SerializeField] private List<Ability> abilities;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text combinationText;
    [SerializeField] private TMP_Text infoText;

    private List<Ability> unlockedAbilities = new List<Ability>();

    private int currentPage = 0;

    private void OnEnable()
    {
        RefreshBook();
    }

    private void RefreshBook()
    {
        unlockedAbilities.Clear();

        foreach (Ability ability in abilities)
        {
            if (ability.Unlocked)
            {
                unlockedAbilities.Add(ability);
            }
        }

        if (unlockedAbilities.Count == 0)
        {
            nameText.text = "No abilities unlocked";
            combinationText.text = "";
            infoText.text = "";
            return;
        }

        currentPage = 0;

        ShowPage();
    }

    private void ShowPage()
    {
        Ability ability = unlockedAbilities[currentPage];

        nameText.text = ability.Name;
        infoText.text = ability.Description;

        combinationText.text = "";

        foreach (Note note in ability.Combination)
        {
            if(note.ToString()== "One")
            {
                combinationText.text += "J" + " ";
            }
            if (note.ToString() == "Two")
            {
                combinationText.text += "K" + " ";
            }
            if (note.ToString() == "Three")
            {
                combinationText.text += "L" + " ";
            }
        }
    }

    public void NextPage()
    {
        if (unlockedAbilities.Count == 0)
            return;

        currentPage++;

        if (currentPage >= unlockedAbilities.Count)
        {
            currentPage = 0;
        }

        ShowPage();
    }

    public void PreviousPage()
    {
        if (unlockedAbilities.Count == 0)
            return;

        currentPage--;

        if (currentPage < 0)
        {
            currentPage = unlockedAbilities.Count - 1;
        }

        ShowPage();
    }
    public void ToggleBook()
    {
        bool openingBook = !CombinationBook.activeSelf;

        CombinationBook.SetActive(openingBook);

        if (openingBook)
        {
            RefreshBook();
        }
    }
}