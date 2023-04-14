using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.Scripts 
{
    class ForceObject : CollisionObject
    {
        [SerializeField] private Vector2 force;

        protected override void DoSomething()
        {
            BallBehaviour.Instance.AddForce(force);
        }
    }
}
