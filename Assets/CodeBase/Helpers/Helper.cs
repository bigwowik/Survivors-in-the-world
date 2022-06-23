using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CodeBase.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Ienumerator. Start with coorutine.
        /// </summary>
        /// <param name="fadeIn"></param>
        /// <param name="fadeTimeInSec"></param>
        /// <param name="canvasGroup"></param>
        /// <returns></returns>
        public static IEnumerator FadeCanvasGroup(bool fadeIn, float fadeTimeInSec, CanvasGroup canvasGroup)
        {
            //Debug.Log("fadeCanvas group:" + canvasGroup.name);

            if (canvasGroup == null)
                yield break;

            if (fadeIn)
                canvasGroup.gameObject.SetActive(true);

            float start = fadeIn ? 0 : 1;
            float end = fadeIn ? 1 : 0;
            float lerpAmount = 0;


            float timer = Time.time;

            while (lerpAmount < 1.01F)
            {
                float fadeSpeed = Time.deltaTime / fadeTimeInSec;
                float alpha = canvasGroup.alpha;
                alpha = Mathf.Lerp(start, end, lerpAmount);
                canvasGroup.alpha = alpha;
                lerpAmount += fadeSpeed;
                //timer += Time.deltaTime;

                yield return null;
            }

            if (fadeIn)
                canvasGroup.alpha = 1f;


            //Debug.Log("fadeCanvas group :" + (-timer + Time.time).ToString());
            if (!fadeIn)
                canvasGroup.gameObject.SetActive(false);
        }

        /// <summary>
        /// Ienumerator. Start with coorutine.
        /// </summary>
        /// <param name="fadeIn"></param>
        /// <param name="fadeTimeInSec"></param>
        /// <param name="graphic"></param>
        /// <returns></returns>
        public static IEnumerator FadeGraphic(bool fadeIn, float fadeTimeInSec, Graphic graphic)
        {
            float start = fadeIn ? 0 : 1;
            float end = fadeIn ? 1 : 0;
            float lerpAmount = 0;


            float timer = Time.time;

            while (lerpAmount < 1.01F)
            {
                float fadeSpeed = Time.deltaTime / fadeTimeInSec;
                Color c = graphic.color;
                c.a = Mathf.Lerp(start, end, lerpAmount);
                graphic.color = c;
                lerpAmount += fadeSpeed;
                //timer += Time.deltaTime;
                yield return null;
            }


            //Debug.Log("FadeTime : " + (-timer + Time.time).ToString());
            //if (!fadeIn)
            //    img.gameObject.SetActive(false);
        }

        /// <summary>
        /// Ienumerator. Start with coorutine.
        /// </summary>
        /// <param name="fadeIn"></param>
        /// <param name="fadeTimeInSec"></param>
        /// <param name="graphic"></param>
        /// <returns></returns>
        public static IEnumerator FadeSpriteRenderer(bool fadeIn, float fadeTimeInSec, SpriteRenderer graphic)
        {
            float start = fadeIn ? 0 : 1;
            float end = fadeIn ? 1 : 0;
            float lerpAmount = 0;


            float timer = Time.time;

            while (lerpAmount < 1.01F)
            {
                float fadeSpeed = Time.deltaTime / fadeTimeInSec;
                Color c = graphic.color;
                c.a = Mathf.Lerp(start, end, lerpAmount);
                graphic.color = c;
                lerpAmount += fadeSpeed;
                //timer += Time.deltaTime;
                yield return null;
            }

            //Debug.Log("FadeTime : " + (-timer + Time.time).ToString());
            //if (!fadeIn)
            //    img.gameObject.SetActive(false);
        }


        public static IEnumerator SelectContinueButtonLater(GameObject selectObj)
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectObj);
        }

        public static Transform GetNearTransform(Transform centerTransform, float radius, LayerMask layerMask,
            Func<Transform, bool> predicate = null)
        {
            var findedTransforms = Physics2D.OverlapCircleAll(centerTransform.position, radius, layerMask);

            List<Transform> listTransform = new List<Transform>();

            foreach (Collider2D col in findedTransforms)
            {
                if (col.transform != centerTransform && predicate(col.transform))
                {
                    listTransform.Add(col.transform);
                }
            }

            if (listTransform.Count == 0)
                return null;

            if (listTransform.Count == 1)
                return listTransform[0];

            Transform nearestTransform = listTransform[0];

            foreach (Transform element in listTransform)
            {
                if (Vector2.Distance(element.transform.position, centerTransform.position) <
                    Vector2.Distance(nearestTransform.transform.position, centerTransform.position))
                {
                    nearestTransform = element;
                }
            }

            return nearestTransform;
        }

        public static Transform GetNearTransform(Transform centerTransform, Transform[] listOfTransforms,
            float Radius = 99999)
        {
            Transform nearestTransform = null;

            foreach (Transform element in listOfTransforms)
            {
                if (centerTransform == element)
                    continue;

                if (Vector2.Distance(element.transform.position, centerTransform.position) > Radius)
                    continue;


                if (nearestTransform == null)
                    nearestTransform = element;

                if (Vector2.Distance(element.transform.position, centerTransform.position) <
                    Vector2.Distance(nearestTransform.transform.position, centerTransform.position))
                {
                    nearestTransform = element;
                }
            }

            return nearestTransform;
        }
    

        public static bool GetArrayOfTypeByGameObjects<T, TBase>(List<TBase> gameObjectsList, out List<T> componentsList)
            where TBase : MonoBehaviour
        {
            componentsList = new List<T>();
            try
            {
                foreach (var go in gameObjectsList)
                {
                    T component = go.GetComponent<T>();
                    componentsList.Add(component);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RandomPointOnNavmesh(Vector3 startPoint, Vector3 endPoint, float range, out Vector3 result,
            float minRange = 0f)
        {
            var vectorDirectionLineSpawn = endPoint - startPoint;

            for (int i = 0; i < 50; i++)
            {
                Vector3 randomPoint = startPoint +
                                      vectorDirectionLineSpawn.normalized * vectorDirectionLineSpawn.magnitude *
                                      (Random.Range(0, 1f)) + (Vector3) RandomInCircle(minRange, range);
                if (IsPointOnNavMesh(out result, randomPoint)) return true;
            }

            result = Vector3.zero;
            return false;
        }

        public static bool RandomPointOnNavmesh(Vector3 point, float range, out Vector3 result, float minRange = 0f)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector3 randomPoint = point + (Vector3) RandomInCircle(minRange, range);
                if (IsPointOnNavMesh(out result, randomPoint)) return true;
            }

            result = Vector3.zero;
            return false;
        }

        public static bool IsPointOnNavMesh(out Vector3 pointOnNavMesh, Vector3 pointToCheck)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pointToCheck, out hit, 1.0f, NavMesh.AllAreas))
            {
                pointOnNavMesh = hit.position;
                return true;
            }

            pointOnNavMesh = pointToCheck;
            return false;
        }

        public static Vector2 RandomInCircle(float minRange, float maxRange)
        {
            var x = Random.Range(minRange, maxRange);
            var y = Random.Range(minRange, maxRange);

            x = RandomBool(0.5f) ? x * (1) : x * (-1);
            y = RandomBool(0.5f) ? y * (1) : y * (-1);

            return new Vector2(x, y);
        }

        public static void DrawCross(Vector2 position, float duration = 3f)
        {
            var up = position + Vector2.up;
            var down = position - Vector2.up;
            var left = position - Vector2.right;
            var right = position + Vector2.right;

            var color = Color.yellow;

            Debug.DrawLine(up, down, color, duration);
            Debug.DrawLine(left, right, color, duration);
        }


        public static T[] ChoiceRandomElements<T>(T[] elements, int count, int seed = 0)
        {
            T[] rooms = new T[count];
            if (seed != 0)
                UnityEngine.Random.InitState(seed);
            for (int i = 0; i < count; i++)
            {
                int index = UnityEngine.Random.Range(0, elements.Length);
                rooms[i] = elements[index];
            }

            return rooms;
        }

        public static T ChoiceRandomElement<T>(T[] elements, int seed = 0)
        {
            if (seed != 0)
                UnityEngine.Random.InitState(seed);
            int index = UnityEngine.Random.Range(0, elements.Length);
            return elements[index];
        }

        public static bool RandomBool()
        {
            return UnityEngine.Random.Range(0, 1f) <= 0.5f;
        }

        public static bool RandomBool(float chance)
        {
            return UnityEngine.Random.Range(0, 1f) < chance;
        }
    }
}