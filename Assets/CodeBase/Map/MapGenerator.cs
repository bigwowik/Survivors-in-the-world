using System.Collections.Generic;
using CodeBase.Helpers;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetsManagment;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Map
{
    public class MapGenerator : MonoBehaviour
    {
        public const int TileSize = 16;
        public const int MinDistanceToPlayer = 20;
        public const int MinDistanceToPlayerToSpawnObjects = 15;

        private const int TileAxisHalfCount = 20;
        private const float ChanceToSpawnObject = 0.1f;

        private GameObject[] _mapFloorTiles;
        private GameObject[] _mapObjectsTiles;
        private GameObject _mapEmptyObjectTile;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private Vector2Int _lastHeroPosition;

        //private List<Transform> tiles = new List<Transform>();

        private Dictionary<Vector2Int, Transform> Tiles = new Dictionary<Vector2Int, Transform>();
        private Dictionary<Vector2Int, Transform> ObjectTiles = new Dictionary<Vector2Int, Transform>();


        [Inject]
        private void Construct(HeroMove heroMove, IGameFactory gameFactory)
        {
            _heroTransform = heroMove.transform;
            _gameFactory = gameFactory;
        }

        public void Init()
        {
            _mapFloorTiles = Resources.LoadAll<GameObject>(AssetPath.FloorTiles);
            _mapObjectsTiles = Resources.LoadAll<GameObject>(AssetPath.MapObjectsTiles);
            _mapEmptyObjectTile = Resources.Load<GameObject>(AssetPath.MapEmptyObjectTiles);

            _lastHeroPosition = _heroTransform.position.ToVector2Int();

            StartGeneration();
        }

        private void StartGeneration()
        {
            var pointCenter = _lastHeroPosition;
            for (int x = -TileAxisHalfCount; x < TileAxisHalfCount; x++)
            {
                for (int y = -TileAxisHalfCount; y < TileAxisHalfCount; y++)
                {
                    var positionToSpawn = PositionToSpawn(pointCenter, x, y);

                    SpawnOneTile(positionToSpawn, _mapFloorTiles, Tiles);

                    if (Helper.RandomBool(ChanceToSpawnObject))
                        SpawnOneTile(positionToSpawn, _mapObjectsTiles, ObjectTiles);
                }
            }
        }

        private void SpawnOneTile(Vector2Int positionToSpawn, GameObject[] tilesArray,
            Dictionary<Vector2Int, Transform> tilesDictionary)
        {
            var floorTile = _gameFactory.CreateTile(GetRandomElement(tilesArray),
                positionToSpawn,
                transform);

            tilesDictionary.Add(
                positionToSpawn,
                floorTile.transform);
        }
        
        private void SpawnOneTile(Vector2Int positionToSpawn, GameObject tile,
            Dictionary<Vector2Int, Transform> tilesDictionary)
        {
            var floorTile = _gameFactory.CreateTile(tile,
                positionToSpawn,
                transform);

            tilesDictionary.Add(
                positionToSpawn,
                floorTile.transform);
        }

        private GameObject GetRandomElement(GameObject[] collection)
        {
            return collection[Random.Range(0, collection.Length)];
        }


        private void Update()
        {
            if (_heroTransform.position.ToVector2Int() == _lastHeroPosition)
                return;

            _lastHeroPosition = _heroTransform.position.ToVector2Int();


            UpdateTiles(_lastHeroPosition, Tiles, _mapFloorTiles);
            UpdateTiles(_lastHeroPosition, ObjectTiles, _mapObjectsTiles, ChanceToSpawnObject);
        }

        private void UpdateTiles(Vector2Int pointCenter, Dictionary<Vector2Int, Transform> tiles,
            GameObject[] tilesGameObjects, float randomChance = 1f)
        {
            var deletedTiles = new List<KeyValuePair<Vector2Int, Transform>>();
            foreach (var tile in tiles)
            {
                var tileTransform = tile.Value.transform;
                if ((int) Vector2Int.Distance(pointCenter, tileTransform.position.ToVector2Int()) > MinDistanceToPlayer)
                    deletedTiles.Add(tile);
            }

            foreach (var deletedTile in deletedTiles)
            {
                tiles.Remove(deletedTile.Key);
                Destroy(deletedTile.Value.gameObject);
            }

            SpawnTiles(pointCenter, tiles, tilesGameObjects, randomChance);
        }

        private void SpawnTiles(Vector2Int pointCenter, Dictionary<Vector2Int, Transform> tiles,
            GameObject[] tilesGameObjects, float randomChance)
        {
            for (int x = -TileAxisHalfCount; x < TileAxisHalfCount; x++)
            {
                for (int y = -TileAxisHalfCount; y < TileAxisHalfCount; y++)
                {
                    var positionToSpawn = PositionToSpawn(pointCenter, x, y);
                    if (!tiles.ContainsKey(positionToSpawn))
                    {
                        if (randomChance >= 1f)
                            SpawnOneTile(positionToSpawn, tilesGameObjects, tiles);
                        else if (Vector2Int.Distance(pointCenter, positionToSpawn) >
                                 MinDistanceToPlayerToSpawnObjects && Helper.RandomBool(randomChance))
                            SpawnOneTile(positionToSpawn, tilesGameObjects, tiles);
                        else
                            SpawnOneTile(positionToSpawn, _mapEmptyObjectTile, tiles);
                    }
                }
            }
        }

        private Vector2Int PositionToSpawn(Vector2Int pointCenter, int x, int y)
        {
            return (pointCenter + Vector2Int.right * x + Vector2Int.up * y);
        }
    }
}