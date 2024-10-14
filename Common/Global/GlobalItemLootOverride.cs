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
    public class GlobalItemLootOverride : GlobalItem
    {

        [JITWhenModsEnabled("CalamityMod")]
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            // Checks to see if Calamity is installed before making changes.

            if (!ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
            {
                return;
            }
            // Replace and suppress Gorecodile's Calamity Blood Orbs.
            if (item.type == CalamityMod.Find<ModItem>("Gorecodile").Type)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StandardBlood>(), 1, 5, 15));
                NPCLoader.blockLoot.Add(CalamityMod.Find<ModItem>("BloodOrb").Type);
            }
        }
        
    }
}
