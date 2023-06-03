using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    [SerializeField] private ESpawnObjectType m_SpawnObjectType;
    public ESpawnObjectType SpawnObjectType => m_SpawnObjectType;
}

