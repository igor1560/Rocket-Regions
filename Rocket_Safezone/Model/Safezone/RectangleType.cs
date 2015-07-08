﻿using System;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;

namespace Safezone.Model
{
    public class RectangleType : SafeZoneType
    {
        public SerializablePosition Position1;
        public SerializablePosition Position2;

        public override SafeZone Create(RocketPlayer player, String name, string[] args)
        {
            if (!SafeZonePlugin.Instance.HasPositionSet(player))
            {
                RocketChat.Say(player.CSteamID, "Please set pos1 (/spos1) and pos2 (/spos2) before using this command", Color.red);
                return null;
            }

            Position1 = SafeZonePlugin.Instance.GetPosition1(player);
            Position2 = SafeZonePlugin.Instance.GetPosition2(player);
            SafeZone zone = new SafeZone
            {
                Name = name,
                Owner = SafeZonePlugin.GetId(player),
                Type = this
            };

            return zone;
        }

        public override bool IsInSafeZone(SerializablePosition p)
        {
            SerializablePosition p1 = Position1;
            SerializablePosition p2 = Position2;

            bool b1 = p.X >= Math.Min(p1.X, p2.X);
            bool b2 = p.X <= Math.Max(p1.X, p2.X);
            bool b3 = p.Y >= Math.Min(p1.Y, p2.Y);
            bool b4 = p.Y <= Math.Max(p1.Y, p2.Y);

            return b1 && b2 && b3 && b4;
        }

        public override bool Redefine(RocketPlayer player, string[] args)
        {
            if (!SafeZonePlugin.Instance.HasPositionSet(player))
            {
                RocketChat.Say(player.CSteamID, "Please set pos1 (/spos1) and pos2 (/spos2) before using this command", Color.red);
                return false;
            }

            Position1 = SafeZonePlugin.Instance.GetPosition1(player);
            Position2 = SafeZonePlugin.Instance.GetPosition2(player);
            return true;
        }
    }
}