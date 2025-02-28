using UnityEngine;
using TMPro;

public class CodePanelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] digits;
    int[] trueCode = { 9, 3, 4, 8, 5 };
    int[] currentCode = { 0, 0, 0, 0, 0 };
    [SerializeField] Animator numCodeScreen;

    [SerializeField] Animator escapePod;
    [SerializeField] GameObject player;
    [SerializeField] Camera ggCamera;

    public void OpenPanel()
    {
        numCodeScreen.Play("Enter");
        player.GetComponent<Player>().isUsingUI = true;
    }

    public void ClosePanel()
    {
        numCodeScreen.Play("Leave");
        Time.timeScale = 1;
        player.GetComponent<Player>().isUsingUI = false;
    }

    // digitIndex is from 1 to 5
    public void OnBtnDigitUp(int digitNo)
    {
        currentCode[digitNo - 1]++;
        if (currentCode[digitNo - 1] == 10)
        {
            currentCode[digitNo - 1] = 0;
        }
        digits[digitNo - 1].text = currentCode[digitNo - 1].ToString();
        CheckCode();
    }

    // digitIndex is from 1 to 5
    public void OnBtnDigitDown(int digitNo)
    {
        currentCode[digitNo - 1]--;
        if (currentCode[digitNo - 1] == -1)
        {
            currentCode[digitNo - 1] = 9;
        }
        digits[digitNo - 1].text = currentCode[digitNo - 1].ToString();
        CheckCode();
    }

    private void CheckCode()
    {
        bool isCorrect = true;
        // If this is the right code, then use the escape pod, and after a few seconds, win the game!
        for (int i = 0; i < trueCode.Length; i++)
        {
            if(currentCode[i] != trueCode[i])
            {
                isCorrect = false;
                break;
            }
        }
        if(isCorrect)
        {
            numCodeScreen.Play("Leave");
            Time.timeScale = 1;
            escapePod.Play("Entry");
            Camera.main.gameObject.SetActive(false);
            player.SetActive(false);
            ggCamera.gameObject.SetActive(true);
        }
    }
}
