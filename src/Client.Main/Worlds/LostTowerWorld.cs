using Client.Data.BMD;
using Client.Main.Content;
using Client.Main.Controls;
using Client.Main.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Main.Worlds
{
    public class LostTowerWorld : WalkableWorldControl
    {
        public LostTowerWorld() : base(worldIndex: 5)
        {
       
        }

        public override void AfterLoad()
        {
            base.AfterLoad();
            Walker.Location = new Vector2(208, 81);
        }
    }
}
