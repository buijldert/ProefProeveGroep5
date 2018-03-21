using UnityEngine;

[CreateAssetMenu]
public class UITheme : ScriptableObject
{
    [Header("Menu Screens")]
    public Sprite BackdropA;
    public Sprite BackdropB;
    public Sprite BackdropC;
    public Sprite BackdropD;

    [Header("Menu Buttons")]
    public Sprite ButtonPlay;
    public Sprite ButtonTutorial;
    public Sprite ButtonCredits;
    public Sprite ButtonBack;

    [Header("Level Selection")]
    [Tooltip("For demo its only the 'Level 1'-button")]
    public Sprite ButtonLevelOpen;
    public Sprite ButtonLevelLocked;

    [Header("Game Elements")]
    public Sprite GameBackground;
    public Sprite Endpoint;

    [Space(10)]
    public Sprite VictoryBackground;
    public Sprite DefeatBackground;
}