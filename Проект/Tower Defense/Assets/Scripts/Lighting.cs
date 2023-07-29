using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Lighting : RayWeapon
    {
        [SerializeField] float _arcLength = 2.0f;
        [SerializeField] float _arcVariation = 2.0f;
        [SerializeField] float _inaccuracy = 1.0f;

        public override void Render(Vector2 turret, Vector2 target)
        {
            Vector2 lastPoint = turret;
            //сделать начало LR в точке начала молнии
            _lr.positionCount = 1;
            _lr.SetPosition(0, lastPoint);

            while (Vector2.Distance(target, lastPoint) > .5)
            {
                //последний отрезок молнии не попал в цель - нужно добавить к молнии ещё один отрезок
                _lr.positionCount++;
                //получить направление на цель от конца последнего отрезка молнии
                Vector2 fwd = target - lastPoint;
                //нормализовать вектор
                fwd.Normalize();
                //молния движется зигзагом
                fwd = Randomize(fwd, _inaccuracy);
                //природа не терпит однообразия
                fwd *= Random.Range(_arcLength * _arcVariation, _arcLength);
                //новый конец молнии = point + distance * direction
                fwd += lastPoint;
                //this tells the line renderer where to draw to
                _lr.SetPosition(_lr.positionCount - 1, fwd);
                //точка, с которой будет продолжаться молния
                lastPoint = fwd;
            }
        }

        private Vector2 Randomize(Vector2 v2, float inaccuracy)
        {
            Vector2 result = v2 + new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * inaccuracy;
            result.Normalize();
            return result;
        }

        public override void Clear()
        {
            if(_lr.positionCount != 0) _lr.positionCount = 0;
        }
    }
}
