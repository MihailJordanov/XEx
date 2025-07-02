using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string classicLevelName = "Classic";
    public string ExtremeLevelName = "Extreme";
    public string AdultsLevelName = "Adults";
    public string DirtyLevelName = "Dirty";
    public string mainMenuName = "HomeScene";

    [SerializeField]
    private GameObject uiConfirmMessage;
    [SerializeField]
    private GameObject dirtyWarningPanelUI;

    [SerializeField]
    private GameObject attentionUI;
    [SerializeField]
    private GameObject rulesUI;
    [SerializeField]
    private GameObject preparationUI;
    [SerializeField]
    private GameObject WarningUI;

    private static bool isClosed = false;

    private void Start()
    {
        if(!isClosed)
        {
            attentionUI.SetActive(true);
        }
    }

    public void playClassic()
    {
        SceneManager.LoadScene(classicLevelName);
    }

    public void playExtreme()
    {
        SceneManager.LoadScene(ExtremeLevelName);
    }

    public void playAdults()
    {
        SceneManager.LoadScene(AdultsLevelName);
    }

    public void playDirty()
    {
        SceneManager.LoadScene(DirtyLevelName);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }



    public void openflashConfirmMessage()
    {
        uiConfirmMessage.SetActive(true);
    }

    public void closeflashConfirmMessage()
    {
        uiConfirmMessage.SetActive(false);
    }

    public void closeAllWarningPanels()
    {
        attentionUI.SetActive(false);
        rulesUI.SetActive(false);
        preparationUI.SetActive(false);
        WarningUI.SetActive(false);
        isClosed = true;
    }

    public void goOnAttentionPanel()
    {
        closeAllWarningPanels();
        attentionUI.SetActive(true);
    }

    public void goOnRulesPanel()
    {
        closeAllWarningPanels();
        rulesUI.SetActive(true);
    }

    public void goOnPreparationPanel()
    {
        closeAllWarningPanels();
        preparationUI.SetActive(true);
    }

    public void goOnWarningPanel()
    {
        closeAllWarningPanels();
        WarningUI.SetActive(true);
    }

    public void openDirtyWarningPanelUI()
    {
        dirtyWarningPanelUI.SetActive(true);
    }

    public void closeDirtyWarningPanelUI()
    {
        dirtyWarningPanelUI.SetActive(false);
    }
}
