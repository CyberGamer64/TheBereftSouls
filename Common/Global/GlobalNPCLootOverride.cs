using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using TheBereftSouls.Common.Utility;
using TheBereftSouls.Items.Materials;
//using Terraria.GameContent;
//using Terraria.GameContent.Bestiary;
//using Terraria.GameContent.UI;
using Terraria.GameContent.ItemDropRules;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace TheBereftSouls.Common.Global
{
    public class GlobalNPCLootOverride : GlobalNPC
    {


        // List of Vanilla NPCs that only have a chance to drop 1 Blood Orb
        private static Dictionary<short, int> vanillaBloodOrbSingleDrop = new Dictionary<short, int>
        {
            {NPCID.MaggotZombie,10},
            {NPCID.TheBride,10},
            {NPCID.TheGroom,10},
            {NPCID.BloodZombie,4},
            {NPCID.Drippler,4}
        };
        // List of Vanilla NPCs that drop a random amount of Blood Orbs.
        private static Dictionary<short, Tuple<int, int, int>> vanillaBloodOrbMultiDrop = new Dictionary<short, Tuple<int, int, int>>
        {
            {NPCID.Clown,Tuple.Create(1,6,12)},
            {NPCID.EyeballFlyingFish,Tuple.Create(1,10,12)},
            {NPCID.ZombieMerman,Tuple.Create(1,10,12)},
            {NPCID.GoblinShark,Tuple.Create(1,40,48)},
            {NPCID.BloodEelHead,Tuple.Create(1,40,48)},
            {NPCID.BloodNautilus,Tuple.Create(1,100,120)}
        };
        // List of Vanilla NPCs that drop Thorium Blood.
        private static Dictionary<short, Tuple<int, int, int>> vanillaThoriumMultiDrop = new Dictionary<short, Tuple<int, int, int>>
        {
            {NPCID.BloodNautilus,Tuple.Create(1,5,5)},
            {NPCID.BloodEelHead, Tuple.Create(1,3,3)},
            {NPCID.GoblinShark,Tuple.Create(1,3,3)},
            {NPCID.BloodSquid,Tuple.Create(1,3,3)}
        };
        private static Dictionary<short, int> vanillaThoriumSingleDrop = new Dictionary<short, int>
        {
            {NPCID.BloodZombie,2},
            {NPCID.Drippler,5}
        };

        // Thorium NPCs that need to be changed
        // Biter
        // EngorgedEye
        // BloodDrop
        private static Dictionary<string, int> thoriumNPC = new Dictionary<string, int>
        {
            {"Biter",1 },
            {"EngorgedEye", 2},
            {"BloodDrop", 1}
        };
        [JITWhenModsEnabled("CalamityMod", "ThoriumMod")]

        // TO DO: Improve readability of code / remove nested if statements. Guard Clauses might help.
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Checks to see if Calamity is installed before making changes.

            if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
            {
                // Sort out chance to drop 1 Blood Orb NPCs
                foreach (KeyValuePair<short, int> currentNPC in vanillaBloodOrbSingleDrop)
                {
                    if (npc.type == currentNPC.Key)
                    {
                        // Add in the Standard Blood.
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StandardBlood>(), currentNPC.Value));
                    }
                }
                // Sort out NPCs that have varying drop amounts
                foreach (KeyValuePair<short,Tuple<int,int,int>> currentNPC in vanillaBloodOrbMultiDrop)
                {
                    if (npc.type == currentNPC.Key)
                    {
                        // Add in the Standard Blood.
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StandardBlood>(), currentNPC.Value.Item1, currentNPC.Value.Item2, currentNPC.Value.Item3));
                    }
                }
            }
            if (ModLoader.TryGetMod("ThoriumMod", out Mod ThoriumMod))
            {
                foreach (KeyValuePair<short,int> currentNPC in vanillaThoriumSingleDrop)
                {
                    if (npc.type == currentNPC.Key)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StandardBlood>(), currentNPC.Value));
                    }
                }
                foreach (KeyValuePair<short,Tuple<int,int,int>> currentNPC in vanillaThoriumMultiDrop)
                {
                    if (npc.type == currentNPC.Key)
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StandardBlood>(), currentNPC.Value.Item1, currentNPC.Value.Item2, currentNPC.Value.Item3));
                }
                foreach (KeyValuePair<string,int> currentNPC in thoriumNPC)
                {
                    if (npc.type == ThoriumMod.Find<ModNPC>(currentNPC.Key).Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StandardBlood>(), currentNPC.Value));
                    }
                }
            }
        }
        // Suppress Redundant drops.
        public override bool PreKill(NPC npc)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
            {
                NPCLoader.blockLoot.Add(CalamityMod.Find<ModItem>("BloodOrb").Type);
            }
            if (ModLoader.TryGetMod("ThoriumMod", out Mod ThoriumMod))
            {
                NPCLoader.blockLoot.Add(ThoriumMod.Find<ModItem>("Blood").Type);
            }
            return true;
        }
    }
}
