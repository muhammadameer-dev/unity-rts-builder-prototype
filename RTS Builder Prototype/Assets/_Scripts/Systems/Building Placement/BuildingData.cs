using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("Basic Info")]
    public string buildingName;
    public GameObject buildingPrefab;
    public Vector2Int size = new Vector2Int(1, 1);

    [Header("Resource Costs")]
    [SerializeField] private List<ResourceCost> resourceCosts = new List<ResourceCost>();
    public IReadOnlyList<ResourceCost> ResourceCosts => resourceCosts;
}

[System.Serializable]
public class ResourceCost
{
    public ResourceType resourceType;
    public int amount;
}
