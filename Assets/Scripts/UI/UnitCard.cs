using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image unitIcon;
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;

    private UnitData unitData;
    private ArmyBuilderManager armyBuilder;
    private bool isSelected = false;

    public void Initialize(UnitData data, ArmyBuilderManager manager)
    {
        unitData = data;
        armyBuilder = manager;
        
        // UI'ı ayarla
        unitIcon.sprite = data.icon;
        unitNameText.text = data.unitName;
        costText.text = $"Maliyet: {data.cost}";
        pointsText.text = $"Puan: {data.points}";

        // Buton listenerları ekle
        addButton.onClick.AddListener(OnAddClicked);
        removeButton.onClick.AddListener(OnRemoveClicked);

        // Başlangıçta remove butonu gizli
        removeButton.gameObject.SetActive(false);
    }

    private void OnAddClicked()
    {
        if (!isSelected)
        {
            armyBuilder.OnUnitSelected(unitData);
            isSelected = true;
            addButton.gameObject.SetActive(false);
            removeButton.gameObject.SetActive(true);
        }
    }

    private void OnRemoveClicked()
    {
        if (isSelected)
        {
            armyBuilder.OnUnitRemoved(unitData);
            isSelected = false;
            addButton.gameObject.SetActive(true);
            removeButton.gameObject.SetActive(false);
        }
    }

    public void SetInteractable(bool interactable)
    {
        addButton.interactable = interactable;
    }
} 