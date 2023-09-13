using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UITextFontSetter
{
    //��Ʈ ��� ����.
    public const string PATH_FONT_UITEXT_JALNAN = "Assets/TextMesh Pro/Examples & Extras/Resources/Fonts & Materials/Jalnan.ttf";
    public const string PATH_FONT_TEXTMESHPRO_JALNAN = "Assets/TextMesh Pro/Examples & Extras/Resources/Fonts & Materials/Electronic Highway Sign SDF.asset";

    [MenuItem("CustomMenu/ChangeUITextFont(���� Scene �� UIText ��Ʈ�� Jalnan ��Ʈ�� ��ü��)")]
    public static void ChangeFontInUIText()
    {
        GameObject[] rootObj = GetSceneRootObjects();

        for (int i = 0; i < rootObj.Length; i++)
        {
            GameObject gbj = (GameObject)rootObj[i] as GameObject;
            Component[] com = gbj.transform.GetComponentsInChildren(typeof(Text), true);
            foreach (Text txt in com)
            {
                txt.font = AssetDatabase.LoadAssetAtPath<Font>(PATH_FONT_UITEXT_JALNAN);
            }
        }
    }

    [MenuItem("CustomMenu/ChangeTextMeshPro(���� Scene �� TextMeshProUGUI ��Ʈ�� Jalnan ��Ʈ�� ��ü��)")]
    public static void ChangeFontInTextMeshPro()
    {
        GameObject[] rootObj = GetSceneRootObjects();

        for (int i = 0; i < rootObj.Length; i++)
        {
            GameObject gbj = (GameObject)rootObj[i] as GameObject;
            Component[] com = gbj.transform.GetComponentsInChildren(typeof(TextMeshProUGUI), true);
            foreach (TextMeshProUGUI txt in com)
            {
                txt.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(PATH_FONT_TEXTMESHPRO_JALNAN);
            }
        }
    }

    /// <summary>
    /// ��� �ֻ��� Root�� GameObject�� �޾ƿ�.
    /// </summary>
    /// <returns></returns>
    private static GameObject[] GetSceneRootObjects()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        return currentScene.GetRootGameObjects();
    }
}
