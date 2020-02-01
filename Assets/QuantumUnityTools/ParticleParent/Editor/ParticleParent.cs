using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParticleParent
{
    [MenuItem("GameObject/Effects/Particle with Parent",false,0)]
    static void InstantiateParticleWithParent()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:Prefab ParticleWithParent")[0]), typeof(GameObject));
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        if (Selection.transforms.Length == 0)
        {
            instance.transform.parent = null;
            instance.transform.SetAsLastSibling();
        }
        else
        {
            instance.transform.parent = Selection.transforms[0];
        }
        PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }

    [MenuItem("GameObject/Effects/Particle Parent",false,0)]
    static void InstantiateParticleParent()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:Prefab ParticleParent")[0]), typeof(GameObject));

        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (Selection.transforms.Length == 0)
        {
            instance.transform.parent = null;
            instance.transform.SetAsLastSibling();
        }
        else
        {
            instance.transform.parent = Selection.transforms[0];
        }

        PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }
}
