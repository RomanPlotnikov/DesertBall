using UnityEngine;

public class CoinPointer : MonoBehaviour
{
    [SerializeField] private CoinContainer _coinContainer;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _heightAbovePlayer;

    private Coin _closestCoin;

    private void Update()
    {
        FollowThePlayer();
        PointClosestCoin();
    }

    private void FollowThePlayer()
    {
        transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _heightAbovePlayer, _playerTransform.position.z);
    }

    private void PointClosestCoin()
    {
        _closestCoin = _coinContainer.GetClosest(transform.position);

        if (_closestCoin == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 directionToCoin = (_closestCoin.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(directionToCoin);
    }
}