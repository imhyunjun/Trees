using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Dialogue")]
    public static void CreateDialogueAssetFile()
    {
        Dialogue asset = CustomAssetUtility.CreateAsset<Dialogue>();
        asset.SheetName = "Trees_Dialogue";
        asset.WorksheetName = "Dialogue";
        EditorUtility.SetDirty(asset);        
    }
    
}