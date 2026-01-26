using UnityEngine;

namespace Game.Utils
{
    public static class SpawnPointCalculator
    {
        public static Vector2 GetRandomSpawnPosition(Vector2 minPoint, Vector2 maxPoint, Vector2 playerPosition,
            float minDistance)
        {
            float maxDistance = GetMaxDistanceFromPlayer(
                playerPosition,
                minPoint,
                maxPoint
            );


            float randomDistance = Random.Range(minDistance, maxDistance);
            float randomAngle = Random.Range(0f, 360f);


            Vector2 direction = new Vector2(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                Mathf.Sin(randomAngle * Mathf.Deg2Rad)
            );

            Vector2 spawnPos = playerPosition + direction * randomDistance;


            spawnPos.x = Mathf.Clamp(spawnPos.x, minPoint.x, maxPoint.x);
            spawnPos.y = Mathf.Clamp(spawnPos.y, minPoint.y, maxPoint.y);

            return spawnPos;
        }

        private static float GetMaxDistanceFromPlayer(Vector2 playerPos, Vector2 minPoint, Vector2 maxPoint)
        {
            Vector2[] corners = new Vector2[]
            {
                new Vector2(minPoint.x, minPoint.y),
                new Vector2(minPoint.x, maxPoint.y),
                new Vector2(maxPoint.x, minPoint.y),
                new Vector2(maxPoint.x, maxPoint.y)
            };

            float maxDistance = 0f;

            foreach (var corner in corners)
            {
                float distance = Vector2.Distance(playerPos, corner);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            return maxDistance;
        }
    }
}