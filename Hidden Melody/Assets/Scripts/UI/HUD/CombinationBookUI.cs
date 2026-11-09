using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombinationBookUI : MonoBehaviour
{
    [SerializeField] private GameObject CombinationBook;
    [SerializeField] private InputActionReference bookInput;
    [SerializeField] private InstrumentManager instrumentManager;
    [SerializeField] private Image[] combinationImages;
    [SerializeField] private TMP_Text[] nameText;
    [SerializeField] private TMP_Text[] combinationText;
    [SerializeField] private TMP_Text[] infoText;
    [SerializeField] private GameObject[] abilitySlots;

    private int lastInstrumentIndex = -1;
    private List<Ability> unlockedAbilities = new List<Ability>();

    private int currentPage = 0;
    private void Start()
    {
        CombinationBook.SetActive(false);
    }
    private void OnEnable()
    {
        RefreshBook();
        bookInput.action.Enable();
        bookInput.action.performed += ToggleBookInput;
    }
    private void OnDisable()
    {
        bookInput.action.performed -= ToggleBookInput;
        bookInput.action.Disable();
    }
    private void ToggleBookInput(InputAction.CallbackContext context)
    {
        ToggleBook();
    }

    private void RefreshBook()
    {
        unlockedAbilities.Clear();

        lastInstrumentIndex = instrumentManager.CurrentInstrumentIndex;

        Instrument currentInstrument = instrumentManager.GetCurrentInstrument();

        foreach (Ability ability in currentInstrument.abilities)
        {
            if (ability.Unlocked)
            {
                unlockedAbilities.Add(ability);
            }
        }
        currentPage = 0;
        ShowPage();
    }
    private void Update()
    {
        if (!CombinationBook.activeSelf)
            return;

        int currentInstrumentIndex = instrumentManager.CurrentInstrumentIndex;

        if (currentInstrumentIndex != lastInstrumentIndex)
        {
            lastInstrumentIndex = currentInstrumentIndex;
            RefreshBook();
        }
    }
    private void ShowPage()
    {
        for (int i = 0; i < 3; i++)
        {
            int abilityIndex = currentPage * 3 + i;

            if (abilityIndex >= unlockedAbilities.Count)
            {
                abilitySlots[i].SetActive(false);
                continue;
            }

            abilitySlots[i].SetActive(true);

            Ability ability = unlockedAbilities[abilityIndex];

            nameText[i].text = ability.Name;
            combinationText[i].text = GetCombinationText(ability);
            infoText[i].text = ability.Description;
        }
    }
    private string GetCombinationText(Ability ability)
    {
        string result = "";

        foreach (Note note in ability.Combination)
        {
            if (note == Note.One)
                result += "J";
            else if (note == Note.Two)
                result += "K";
            else if (note == Note.Three)
                result += "L";
        }

        return result;
    }

    public void NextPage()
    {
        if (unlockedAbilities.Count == 0)
            return;

        int pageCount = Mathf.CeilToInt(unlockedAbilities.Count / 3f);

        currentPage++;

        if (currentPage >= pageCount)
        {
            currentPage = 0;
        }

        ShowPage();
    }

    public void PreviousPage()
    {
        if (unlockedAbilities.Count == 0)
            return;

        int pageCount = Mathf.CeilToInt(unlockedAbilities.Count / 3f);

        currentPage--;

        if (currentPage < 0)
        {
            currentPage = pageCount - 1;
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