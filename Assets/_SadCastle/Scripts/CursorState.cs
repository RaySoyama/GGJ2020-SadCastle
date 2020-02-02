using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CursorState : ScriptableObject
{
    public Texture2D cursorTexture;
    public Vector2 cursorHotspot;

    [SerializeField]
    CursorStateMatcher[] matchCriteria;

    public bool MatchTarget(GameObject target)
    {
        foreach(var criterion in matchCriteria)
        {
            if (criterion.Match(target)) { return true; }
        }
        return false;
    }

#if UNITY_EDITOR
    // private void OnValidate()
    // {
    //     foreach (var criterion in matchCriteria)
    //     {
    //         if(criterion.type == CursorStateMatcher.MatcherType.Tag)
    //         {
    //             var definedTags = UnityEditorInternal.InternalEditorUtility.tags;
    //             foreach(var tag in criterion.tags)
    //             {
    //                 Debug.Assert(System.Array.Find(definedTags, t => t == tag) != default(string), "The " + tag + " is not a valid tag.", this);
    //             }
    //         }
    //         else if(criterion.type == CursorStateMatcher.MatcherType.Component)
    //         {
    //             foreach(var componentType in criterion.components)
    //             {
    //                 Debug.Assert(System.Type.GetType(componentType) != null, componentType + " is not a valid type.", this);
    //             }
    //         }
    //     }
    // }
#endif
}

[CreateAssetMenu]
public class CursorStateMatcher : ScriptableObject
{
    public enum MatcherType
    {
        Tag,
        Component,
        Custom,
        Always
    }
    public MatcherType type;

    public string[] tags;
    public string[] components;

    public bool Match(GameObject target)
    {
        switch (type)
        {
            case MatcherType.Always:
                return true;
            case MatcherType.Tag:
                return MatchByTag(target);
            case MatcherType.Component:
                return MatchByComponent(target);
            case MatcherType.Custom:
                return MatchByCustom(target);
            default:
                Debug.AssertFormat(false, "Invalid matcher type!", "");
                return false;
        }
    }

    private bool MatchByTag(GameObject target)
    {
        foreach (var tag in tags)
        {
            if (target.CompareTag(tag)) { return true; }
        }

        return false;
    }

    private bool MatchByComponent(GameObject target)
    {
        foreach (var component in components)
        {
            if (target.GetComponent(component) != null) { return true; }
        }

        return false;
    }

    protected virtual bool MatchByCustom(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
