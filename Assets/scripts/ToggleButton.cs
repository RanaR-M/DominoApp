using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    //انشاء الزر من يونتي
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Toggle Button")]
    public static void AddToggleButton()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Toggle Button"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif


    [Header("Sprites")]
    public Sprite sp_ON;
    public Sprite sp_OFF;

    private Image image;

    [Header("Save Key")]
    public string SaveKey;

    public bool IsToggled { get; private set; }

    private void Start()
    {
        image = GetComponent<Image>();

        //في حالو نسيان تحديد اسم
        if (SaveKey == "")
        {
            SaveKey = name;
        }

        //قم بحميل اخر وضع للزر
        IsToggled = PlayerPrefs.GetInt(SaveKey) == 0 ? true : false;


        //قم بتحديث صزرة الزر
        UpdateSprite();

    }

    //تغيير وضع الزر من سكريبت اخر
    public void SetToggleButton(bool _toggle)
    {
        IsToggled = _toggle;
        UpdateSprite();
    }

    //عند الضغط على الزر
    public void Toggle()
    {
        IsToggled = !IsToggled;
        UpdateSprite();
    }


    private void UpdateSprite()
    {
        //قم بتغيير الصورة عند الضغط على الزر
        if (IsToggled)
        {
            image.sprite = sp_ON;
            PlayerPrefs.SetInt(SaveKey, 0);
        }
        else
        {
            image.sprite = sp_OFF;
            PlayerPrefs.SetInt(SaveKey, 1);

        }

    }
}