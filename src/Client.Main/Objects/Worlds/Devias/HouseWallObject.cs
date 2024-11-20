﻿using Client.Data;
using Client.Main.Content;
using Client.Main.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace Client.Main.Objects.Worlds.Devias
{
    public class HouseWallObject : ModelObject
    {
        private float _alpha = 1f;
        private bool _playerInside = false;
        private const float TARGET_ALPHA = 0.25f;
        private const float FADE_SPEED = 0.1f;
        private const float Y_PROXIMITY_THRESHOLD = 100f;

        public override async Task Load()
        {
            BlendState = BlendState.AlphaBlend;
            LightEnabled = true;
            if (Type == 78)
            {
                LightEnabled = true;
                BlendMesh = 3;
                BlendMeshState = BlendState.Additive;
            }
            Model = await BMDLoader.Instance.Prepare($"Object3/Object{Type + 1}.bmd");
            await base.Load();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Type == 75 ||
                Type == 76 ||
                Type == 77 ||
                Type == 78)
            {
                Vector2 playerPosition2D = Vector2.Zero;
                if (World is WalkableWorldControl walkableWorld)
                {
                    playerPosition2D = walkableWorld.Walker.Location;
                }

                bool isBehind = playerPosition2D.X * 100 < Position.X && Math.Abs(playerPosition2D.X * 100 - Position.X) < 300f;

                bool isWithinY = Math.Abs(playerPosition2D.Y * 100 - Position.Y) <= Y_PROXIMITY_THRESHOLD;

                float targetAlpha = (isBehind && isWithinY) ? TARGET_ALPHA : 1f;

                _alpha = MathHelper.Lerp(_alpha, targetAlpha, FADE_SPEED);
                Alpha = _alpha;
            }

            if (Type == (ushort)ModelType.Fence01 || Type == (ushort)ModelType.Fence02 || Type == 98)
            {
                _playerInside = IsPlayerUnderRoof();
                float targetAlpha = _playerInside ? 0f : 1f;
                Alpha = MathHelper.Lerp(Alpha, targetAlpha, FADE_SPEED);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private bool IsPlayerUnderRoof()
        {
            if (World is WalkableWorldControl walkableWorld)
            {
                Vector2 playerPosition2D = walkableWorld.Walker.Location;

                Vector3 playerPosition3D = new Vector3(playerPosition2D.X * 100, playerPosition2D.Y * 100, Position.Z);

                BoundingBox buildingBounds = new BoundingBox(
                    Position - new Vector3(500f, 400f, 1000f),
                    Position + new Vector3(400f, 500f, 1000f)
                );

                return buildingBounds.Contains(playerPosition3D) == ContainmentType.Contains;
            }

            return false;
        }
    }
}