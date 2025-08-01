using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ButtonSoundEditor
{
    [MenuItem("Tools/일괄 버튼 클릭 사운드 추가")]
    public static void AddButtonClickSoundToAllButtons()
    {
        int count = 0;
        foreach (Button button in Object.FindObjectsOfType<Button>(true))
        {
            if (button.GetComponent<ButtonClickSound>() == null)
            {
                Undo.AddComponent<ButtonClickSound>(button.gameObject);
                count++;
            }
        }
        EditorUtility.DisplayDialog("완료", $"ButtonClickSound가 {count}개 버튼에 추가되었습니다.", "확인");
    }
}
