using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Bone
{
    NONE,
    HEAD,
    BODY,
    LOWERBODY
}

public enum Bone_State
{
    NORMAL,
    WEAK
}

public class EnemyBone : MonoBehaviour
{
    public Bone bone;
    public Transform parent;
}
