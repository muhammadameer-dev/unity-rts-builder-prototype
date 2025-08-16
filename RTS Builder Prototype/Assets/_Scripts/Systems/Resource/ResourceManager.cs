using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event Action<ResourceType, int> OnResourceChanged; // Pass type + new value
    private readonly Dictionary<ResourceType, int> resources = new();

    [SerializeField] private int startingAmount = 100;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        InitializeResources();
    }

    private void InitializeResources()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = startingAmount;
        }
    }

    public int GetResources(ResourceType type)
    {
        return resources.TryGetValue(type, out int value) ? value : 0;
    }

    public void AddResources(ResourceType type, int amount)
    {
        if (!resources.ContainsKey(type)) resources[type] = 0;
        resources[type] += Math.Max(0, amount);
        OnResourceChanged?.Invoke(type, resources[type]);
    }

    public bool SpendResources(ResourceType type, int amount)
    {
        if (!resources.ContainsKey(type) || resources[type] < amount) return false;

        resources[type] -= amount;
        OnResourceChanged?.Invoke(type, resources[type]);
        return true;
    }

    public bool HasEnoughResources(IReadOnlyList<ResourceCost> costs)
    {
        foreach (var cost in costs)
        {
            if (!resources.ContainsKey(cost.resourceType) || resources[cost.resourceType] < cost.amount)
                return false;
        }
        return true;
    }

    public void SpendResources(IReadOnlyList<ResourceCost> costs)
    {
        foreach (var cost in costs)
        {
            if (!SpendResources(cost.resourceType, cost.amount))
            {
                Debug.LogError($"ResourceManager: Not enough {cost.resourceType} to spend!");
            }
        }
    }

    public void RefundResources(BuildingData buildingData)
    {
        foreach (var refund in buildingData.ResourceCosts)
        {
            int refundAmount = Mathf.RoundToInt(refund.amount * 0.5f);
            AddResources(refund.resourceType, refundAmount);
        }
    }
}

public enum ResourceType
{
    Food,
    Wood,
    Stone,
    Gold
}
