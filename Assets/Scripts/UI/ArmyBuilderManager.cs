using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ArmyBuilderManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Transform unitContainer; // Birim kartlarının yerleştirileceği container
    [SerializeField] private Button backButton;
    [SerializeField] private Button saveArmyButton;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text armyPointsText;

    [Header("Unit Selection")]
    [SerializeField] private int maxArmyPoints = 1000;
    [SerializeField] private int startingGold = 5000;

    private int currentGold;
    private int currentArmyPoints;
    private List<UnitData> selectedUnits = new List<UnitData>();

    private void Start()
    {
        InitializeArmyBuilder();
        SetupListeners();
    }

    private void InitializeArmyBuilder()
    {
        currentGold = startingGold;
        currentArmyPoints = 0;
        UpdateUI();
        LoadAvailableUnits();
    }

    private void SetupListeners()
    {
        backButton.onClick.AddListener(OnBackClicked);
        saveArmyButton.onClick.AddListener(OnSaveArmyClicked);
    }

    private void LoadAvailableUnits()
    {
        // TODO: Mevcut birimleri yükle ve UI'da göster
        // Her birim için UnitCard oluştur
    }

    public void OnUnitSelected(UnitData unit)
    {
        if (CanAddUnit(unit))
        {
            selectedUnits.Add(unit);
            currentGold -= unit.cost;
            currentArmyPoints += unit.points;
            UpdateUI();
        }
        else
        {
            ShowWarning("Yetersiz altın veya ordu puanı!");
        }
    }

    public void OnUnitRemoved(UnitData unit)
    {
        if (selectedUnits.Remove(unit))
        {
            currentGold += unit.cost;
            currentArmyPoints -= unit.points;
            UpdateUI();
        }
    }

    private bool CanAddUnit(UnitData unit)
    {
        return currentGold >= unit.cost && 
               (currentArmyPoints + unit.points) <= maxArmyPoints;
    }

    private void UpdateUI()
    {
        goldText.text = $"Altın: {currentGold}";
        armyPointsText.text = $"Ordu Puanı: {currentArmyPoints}/{maxArmyPoints}";
    }

    private void OnBackClicked()
    {
        // Ana menüye dön
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void OnSaveArmyClicked()
    {
        if (selectedUnits.Count > 0)
        {
            SaveArmy();
            ShowMessage("Ordu başarıyla kaydedildi!");
        }
        else
        {
            ShowWarning("Kaydetmek için en az bir birim seçmelisiniz!");
        }
    }

    private void SaveArmy()
    {
        // TODO: Seçilen orduyu kaydet
        // PlayerPrefs veya başka bir kayıt sistemi kullanılabilir
    }

    private void ShowWarning(string message)
    {
        Debug.LogWarning(message);
        // TODO: UI'da uyarı göster
    }

    private void ShowMessage(string message)
    {
        Debug.Log(message);
        // TODO: UI'da mesaj göster
    }
}

[System.Serializable]
public class UnitData
{
    public string unitName;
    public int cost;
    public int points;
    public Sprite icon;
    // Diğer birim özellikleri eklenebilir
} 