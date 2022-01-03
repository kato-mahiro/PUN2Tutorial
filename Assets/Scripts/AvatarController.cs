using System;
using System.Collections.Concurrent;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float MaxStamina = 1f;
    private float currentStamina = MaxStamina;
    public Slider staminaBar;
    private void Start()
    {
        PhotonNetwork.NickName = "Player";
    }

    private void Update() {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine) {
            var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            if (input.sqrMagnitude > 0f)
            {
                currentStamina = Mathf.Max(0f, currentStamina - Time.deltaTime);
                transform.Translate(6f * Time.deltaTime * input.normalized);
            }
            else
            {
                currentStamina = Mathf.Min(currentStamina + Time.deltaTime * 2, MaxStamina);
            }
        }

        staminaBar.value = currentStamina / MaxStamina;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //自身のアバターのスタミナを送信する
            stream.SendNext(currentStamina);
        }
        else
        {
            currentStamina = (float) stream.ReceiveNext();
        }
    }
}