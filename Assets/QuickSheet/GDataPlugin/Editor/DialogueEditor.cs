using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : BaseGoogleEditor<Dialogue>
{	    
    public override bool Load()
    {        
        Dialogue targetData = target as Dialogue;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<DialogueData>(targetData.WorksheetName) ?? db.CreateTable<DialogueData>(targetData.WorksheetName);
        
        List<DialogueData> myDataList = new List<DialogueData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            DialogueData data = new DialogueData();
            
            data = Cloner.DeepCopy<DialogueData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
