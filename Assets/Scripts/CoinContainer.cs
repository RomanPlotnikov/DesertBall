using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinContainer : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [Space(5)]
    [SerializeField] private float _spawnHeight;
    [SerializeField][Min(1)] private int _totalCoinsNumber;
    [Space(5)]
    [SerializeField] private Vector2 _spawnArea;

    private List<Coin> _coins;
    private int _collectedCoinsNumber;

    public event UnityAction CoinCollected;

    public int TotalCoinsNumber => _totalCoinsNumber;
    public int CollectedCoinsNumber => _collectedCoinsNumber;

    private void Start()
    {
        _coins = new List<Coin>(_totalCoinsNumber);

        SpawnCoins(_coins);
    }

    private void SpawnCoins(List<Coin> coins)
    {
        for (int i = 0; i < coins.Capacity; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-_spawnArea.x, _spawnArea.x), 100f, Random.Range(-_spawnArea.y, _spawnArea.y)) / 2f;

            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hitInfo) && IsPlaceFree(hitInfo))
            {
                randomPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + _spawnHeight, hitInfo.point.z);
                _coins.Add(Instantiate(_coinPrefab, randomPosition, Quaternion.identity, transform));
            }
            else
                i--;
        }
    }

    private bool IsPlaceFree(RaycastHit hitInfo)
    {
        return hitInfo.collider.gameObject.TryGetComponent(out TerrainCollider terrainCollider);
    }

    public void Collect(Coin coin)
    {
        _coins.Remove(coin);
        _collectedCoinsNumber++;
        coin.gameObject.SetActive(false);

        CoinCollected?.Invoke();
    }

    public Coin GetClosest(Vector3 position)
    {
        float minDistance = Mathf.Infinity;
        Coin closestCoin = null;

        for (int i = 0; i < _coins.Count; i++)
        {
            float distance = Vector3.Distance(position, _coins[i].transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestCoin = _coins[i];
            }
        }

        return closestCoin;
    }
}