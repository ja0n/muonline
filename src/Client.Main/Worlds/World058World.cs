﻿using Client.Main.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Main.Worlds
{
    public class World058World : WalkableWorldControl
    {
        public World058World() : base(worldIndex: 58) // RAKLION
        {

        }

        public override void AfterLoad()
        {
            Walker.Location = new Vector2(222, 210);
            base.AfterLoad();
        }
    }
}
