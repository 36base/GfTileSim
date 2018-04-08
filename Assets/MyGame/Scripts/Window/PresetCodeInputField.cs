using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 프리셋 복사, 불여넣기 클래스
/// </summary>
public class PresetCodeInputField : UIBackBtnHandle
{
    public InputField inputField;

    bool copyMode;

    public void PasteCode()
    {
        if (isWindow)
            return;
        Open();
        copyMode = false;
        string clipBoardText = UniClipboard.GetText();

        if (clipBoardText.Length != 88)
        {
            inputField.text = null;
            return;
        }

        inputField.text = clipBoardText;
    }

    public void CopyCode()
    {
        if (isWindow)
            return;
        Open();
        copyMode = true;
        string plainText = "";
        for (int i = 0; i < SingleTon.instance.dollSelecter.selects.Length; i++)
        {
            int pos = SingleTon.instance.dollSelecter.selects[i].gridPos;
            int id;
            if (SingleTon.instance.grid.tiles[pos - 1].doll == null)
            {
                id = 0;
            }
            else
            {
                id = SingleTon.instance.grid.tiles[pos - 1].doll.id;
            }

            plainText += "ID" + id.ToString("D5") + "Pos" + pos;
        }

        inputField.text = DataManager.Encrypt(plainText);
    }

    public void SubmitCode()
    {
        if (!isWindow)
            return;
        Close();
        if(copyMode)
        {
            UniClipboard.SetText(inputField.text);
        }

        SingleTon.instance.dollPresetList.presets[0]
            .DataToGrid(DataManager.Decrypt(inputField.text));
    }
}
