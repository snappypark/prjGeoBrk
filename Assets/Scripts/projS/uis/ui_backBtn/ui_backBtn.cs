using UnityEngine;
using UnityEngine.UI;

using Beebyte.Obfuscator;

public class ui_backBtn : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Text _lbYes;
    [SerializeField] Text _lbNo;

    public void Show()
    {
        _text.text = LangTable.ExitGame();
        _lbYes.text = LangTable.Yes();
        _lbNo.text = LangTable.No();
        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        Time.timeScale = 0;
    }
    
    #region UI ACTION
    [SkipRename]
    public void OnBtn_OK()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    [SkipRename]
    public void OnBtn_NO()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    #endregion
}
