using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _velocity;

    public void Init(Vector3 origin, float angle) {
        transform.position = origin;
        _velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void Update() {
        transform.Translate(_velocity * Time.deltaTime);
    }

    // 画面外に移動したら削除する
    // （Unityのエディター上ではシーンビューの画面も影響するので注意）
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}