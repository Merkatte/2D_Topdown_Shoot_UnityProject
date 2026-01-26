using Unity.VisualScripting;
using UnityEngine;

public static class SpawnPointCalculator
{
    public static void CalculateSpawnPoint(Vector2 minPoint, Vector2 maxPoint, Vector2 centerPoint, float radius)
    {
        
    }

    static Vector2 GetRandomSpawnPosition(float xMin, float yMin, float xMax, float yMax, Vector2 playerPos, float radius)
    {
        float maxDistance = GetMaxDistanceFromPlayer(playerPos);

        return Vector2.zero;
    }

    static float GetMaxDistanceFromPlayer(Vector2 playerPos)
    {
        return 0f;
    }
}

