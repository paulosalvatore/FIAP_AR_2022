using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    /*
     * Exibir o TargetMarker no ponto que o jogador está visualizando
     *
     * Identificar que o jogador tocou na tela
     * A partir disso, identificar a posição/rotação nesse ambiente 3D, gerado
     * a partir da câmera do celular
     * Nessa posição/rotação, instanciar a aranha (prefab Spider)
     */

    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private GameObject spiderPrefab;

    private void Update()
    {
        /*
         * Posicionar o targetMarket no centro da visão da câmera
         * Emite um raio a partir do centro da tela
         * O raio estará orientado seguindo a orientação
         * do dispositivo
         * Caso esse raio consiga tocar um plano, exibir o targetMarker
         */

        var x = Screen.width / 2;
        var y = Screen.height / 2;

        var screenCenter = new Vector2(x, y);

        // ARRaycastHit = ponto de interseção do raio com o plano

        var hitResults = new List<ARRaycastHit>();

        var hit = arRaycastManager.Raycast(screenCenter, hitResults, TrackableType.Planes);

        if (hit)
        {
            var hitResult = hitResults[0];

            transform.SetPositionAndRotation(hitResult.pose.position, hitResult.pose.rotation);

            targetMarker.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0)
            && targetMarker.activeSelf)
        {
           Instantiate(spiderPrefab, transform.position, transform.rotation);
        }
    }
}
