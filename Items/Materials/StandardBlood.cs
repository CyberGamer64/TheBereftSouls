//using ExampleMod.Content.Items.Placeable;
//using ExampleMod.Content.Items.Placeable.Furniture;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheBereftSouls.Items.Materials
{
	public class StandardBlood : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
		}

		public override void SetDefaults() {
			Item.width = 20; // The item texture's width
			Item.height = 20; // The item texture's height

			Item.maxStack = Item.CommonMaxStack;
			// Value isn't super important but its the average between the 2 Blood Items
			Item.value = Item.buyPrice(copper: 72);
		}
		// TO DO: Add Recipe if players somehow manage to get Calamity or Thorium Blood.
	}
}
