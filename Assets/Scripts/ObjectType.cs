using UnityEngine;

public class ObjectType : MonoBehaviour
{
    [SerializeField] private ESpawnObjectType m_SpawnObjectType;
    public ESpawnObjectType SpawnObjectType => m_SpawnObjectType;
}

