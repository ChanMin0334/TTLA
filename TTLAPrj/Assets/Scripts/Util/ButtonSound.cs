using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ButtonSoundEditor
{
    [MenuItem("Tools/�ϰ� ��ư Ŭ�� ���� �߰�")]
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
        EditorUtility.DisplayDialog("�Ϸ�", $"ButtonClickSound�� {count}�� ��ư�� �߰��Ǿ����ϴ�.", "Ȯ��");
    }
}
